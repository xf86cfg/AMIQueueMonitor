using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AsterNET.Manager.Event;
using AsterNET.Manager.Action;
using AsterNET.Manager;
using AMIQueueMonitor.Helpers;
using AMIQueueMonitor.Dto;
using AutoMapper;
using System.Drawing;

namespace AMIQueueMonitor
{ 
    //=> To do: connection states refactoring
    public partial class MainForm : Form
    {
        private struct ConsoleTextColor
        {
            public static Color Error;
            public static Color Info;
        }

        private System.Timers.Timer autoPollTimer;
        private ManagerConnection managerConnection;
        private Configuration configuration;
        private SyncBindingList<QueueParamsDto> queueParamsData;
        private SyncBindingList<QueueEntryDto> queueEntriesData;
        private List<QueueEntryDto> _queueEntriesDataCache = new List<QueueEntryDto>();
        private List<QueueParamsDto> _queueParamsDataCache = new List<QueueParamsDto>();
        private bool _autoPollPaused = true;

        #region Form lifecyle

        public MainForm()
        {
            try
            {
                InitializeComponent();
                InitializeConfiguration();
                InitializeManagerConnection();
                InitializeAutoPollTimer();
                InitializeAutoMapper();
            }
            catch (Exception einit)
            {
                MessageBox.Show(einit.Message, "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                ConfigureQueueParamsView();
                ConfigureQueueEntriesView();
                ConfigureUI();
            }
            catch (Exception eload)
            {
                MessageBox.Show(eload.Message, "OnLoad Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            UpdateConnectionStateIndicator();
            LogToConsole("Appication started.", ConsoleTextColor.Info);

            if (configuration.StartAutoPollOnStartup)
            {
                autoPollTimer.Start();
                _autoPollPaused = false;
                LogToConsole($"Automatic polling started. Poll interval {autoPollTimer.Interval} msec.", ConsoleTextColor.Info);
            }
            else
            {
                LogToConsole("Automatic polling paused. Please double click connection state indicator to unpause.", ConsoleTextColor.Info);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var answer = MessageBox.Show(
                $"Are you sure that you would like to close {configuration.WindowTitle} v{Application.ProductVersion}?",
                "Quit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            if (answer == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region AsternNet Communications

        private void TryRequestQueuesStatus()
        {
            try
            {
                if (managerConnection.IsConnected())
                {
                    ClearCacheIfNeeded();
                    RequestQueuesStatus();
                }
                else
                {
                    LogToConsole($"Not connected.", ConsoleTextColor.Info);
                    managerConnection.Login();
                }
            }
            catch (Exception e)
            {
                UpdateQueueStatusRequestStateIndicator("TX/RX Error");
                LogToConsole($"Communication error {e.Message}", ConsoleTextColor.Error);
            }
        }

        private void RequestQueuesStatus()
        {
            try
            {
                var response = managerConnection.SendAction(new QueueStatusAction());
                if (response.IsSuccess())
                {
                    UpdateQueueStatusRequestStateIndicator("TX OK");
                }
                else
                {
                    UpdateQueueStatusRequestStateIndicator("RX ER");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Helpers

        private void ClearCacheIfNeeded()
        {
            lock (_queueParamsDataCache)
            {
                if (_queueParamsDataCache.Count > 0) _queueParamsDataCache.Clear();
            }
            lock (_queueEntriesDataCache)
            {
                if (_queueEntriesDataCache.Count > 0) _queueEntriesDataCache.Clear();
            }
        }

        private void SyncDataSourceWithCache<IEquitable>(SyncBindingList<IEquitable> dataSource, List<IEquitable> cache)
        {
            lock (cache)
            {
                List<IEquitable> list = dataSource.ToList();
                foreach (var dto in cache)
                {
                    int index = list.IndexOf(dto);
                    if (index >= 0)
                    {
                        dataSource[index] = dto;
                    }
                    else
                    {
                        dataSource.Add(dto);
                    }
                }
                for (int i = dataSource.Count - 1; i >= 0; i--)
                {
                    var entry = dataSource[i];
                    var cacheContainsElement = cache.Contains(entry);
                    if (!cacheContainsElement)
                    {
                        dataSource.RemoveAt(i);
                    }
                }
            }
        }

        private void LogToConsole(string text, Color textColor)
        {
            string newLine = $"{DateTime.Now.ToString(configuration.DateTimeFormat)} > {text}";
            UIThreadHelper.AppendTextBoxText(this, consoleTextBox, newLine, textColor);
        }

        #endregion

        #region UI Helpers

        private void UpdateConnectionStateIndicator()
        {
            UIThreadHelper.SetToolStripBoxLabelText(this, connectionStateIndicator, managerConnection.IsConnected() ? "Connected" : "Disconnected", null);
        }

        private void UpdateQueueStatusRequestStateIndicator(string text)
        {
            UIThreadHelper.SetToolStripBoxLabelText(this, queueStatusRequestStateIndicator, text, null);
        }

        #endregion

        #region Event Handlers

        private void QueueParametersEventHandler(object sender, QueueParamsEvent e)
        {
            if (!configuration.Queues.Contains(e.Queue)) return;
            try
            {
                var qp = Mapper.Map<QueueParamsEvent, QueueParamsDto>(e);
                lock (_queueParamsDataCache)
                {
                    _queueParamsDataCache.Add(qp);
                }
            }
            catch (Exception ep)
            {
                LogToConsole($"Queue parameters processing error occured {ep.Message}", ConsoleTextColor.Error);
            }
        }

        private void QueueEntryEventHandler(object sender, QueueEntryEvent e)
        {
            if (!configuration.Queues.Contains(e.Queue)) return;
            try
            {
                var qp = Mapper.Map<QueueEntryEvent, QueueEntryDto>(e);
                lock (_queueEntriesDataCache)
                {
                    _queueEntriesDataCache.Add(qp);
                }
            }
            catch (Exception ee)
            {
                LogToConsole($"Queue entries processing error occured {ee.Message}", ConsoleTextColor.Error);
            }
        }

        private void QueueStatusCompleteEventHandler(object sender, QueueStatusCompleteEvent e)
        {
            UpdateQueueStatusRequestStateIndicator("RX OK");
            SyncDataSourceWithCache(queueParamsData, _queueParamsDataCache);
            SyncDataSourceWithCache(queueEntriesData, _queueEntriesDataCache);
        }

        private void QueueCallerJoinEventHandler(object sender, QueueCallerJoinEvent e)
        {
            if (configuration.PopupOnNewCall)
            {
                UIThreadHelper.ShowForm(this, this);
            }
            LogToConsole($"Caller {e.Attributes.GetValueOrDefault("calleridnum")} joined queue {e.Queue} on position {e.Position}.", ConsoleTextColor.Info);
        }

        private void QueueCallerAbandonEventHandler(object sender, QueueCallerAbandonEvent e)
        {
            LogToConsole($"Caller {e.Attributes.GetValueOrDefault("calleridnum")} abandoned queue {e.Queue} after {e.HoldTime} sec.", ConsoleTextColor.Info);
        }

        private void QueueMemberAddedEventHandler(object sender, QueueMemberAddedEvent e)
        {
            LogToConsole($"Agent {e.MemberName} logged into queue {e.Queue}.", ConsoleTextColor.Info);
        }

        private void QueueMemberRemovedEventHandler(object sender, QueueMemberRemovedEvent e)
        {
            LogToConsole($"Agent {e.MemberName} logged out of queue {e.Queue}.", ConsoleTextColor.Info);
        }

        private void AgentConnectEventHandler(object sender, AgentConnectEvent e)
        {
            LogToConsole($"Agent {e.MemberName} connected to caller {e.Attributes.GetValueOrDefault("calleridnum")} from queue {e.Queue}.", ConsoleTextColor.Info);
        }

        private void AgentCompleteEventHandler(object sender, AgentCompleteEvent e)
        {
            LogToConsole($"Call completed between {e.Attributes.GetValueOrDefault("calleridnum")} from queue {e.Queue} and agent {e.MemberName}.", ConsoleTextColor.Info);
        }

        private void ConnectionStateEventHandler(object sender, ConnectionStateEvent e)
        {
            LogToConsole(managerConnection.IsConnected() ? "Connected." : "Disconnected.", ConsoleTextColor.Info);
        }

        private void DataGridViewDataErrorEventHandler(object sender, DataGridViewDataErrorEventArgs args)
        {
            LogToConsole($"DataGridView error occured. {args.ToString()}", ConsoleTextColor.Error);
        }

        private void HighlightRowsIfNeeded(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView == null) return;
            var row = dataGridView.Rows[e.RowIndex];
            foreach (DataGridViewTextBoxCell cell in row.Cells)
            {
                if (cell.OwningColumn.Name == "Calls")
                {
                    try
                    {
                        var calls = (int)cell.Value;
                        row.DefaultCellStyle.BackColor = calls > 0 ? configuration.HighlightRowColor : configuration.QueuesAreaColor;
                    }
                    catch (Exception ec)
                    {
                        LogToConsole($"Highlight prepaint error occured {ec.Message}", ConsoleTextColor.Error);
                    }
                }
            }
        }

        private void DataGridViewClearSelection(object sender, EventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView != null) dataGridView.ClearSelection();
        }

        private void ConnectionStateIndicatorDoubleClick(object sender, EventArgs e)
        {
            if (_autoPollPaused)
            {
                autoPollTimer.Start();
                _autoPollPaused = false;
                LogToConsole("Automatic polling started.", ConsoleTextColor.Info);
            }
            else
            {
                autoPollTimer.Stop();
                _autoPollPaused = true;
                LogToConsole("Automatic polling paused. Please double click connection state indicator to unpause.", ConsoleTextColor.Info);
                if (managerConnection.IsConnected())
                {
                    managerConnection.Logoff();
                    LogToConsole("Disconnected.", ConsoleTextColor.Info);
                }
                UpdateConnectionStateIndicator();
            }
        }

        #endregion

        #region Initializers

        private void InitializeConfiguration()
        {
            configuration = new Configuration();
            ConsoleTextColor.Info = configuration.ConsoleInfoTextColor;
            ConsoleTextColor.Error = configuration.ConsoleErrorTextColor;
        }

        private void InitializeManagerConnection()
        {
            managerConnection = new ManagerConnection(
                configuration.Server, configuration.ServerPort, configuration.Username, configuration.Password);
            managerConnection.QueueParams += QueueParametersEventHandler;
            managerConnection.ConnectionState += ConnectionStateEventHandler;
            managerConnection.QueueEntry += QueueEntryEventHandler;
            managerConnection.QueueStatusComplete += QueueStatusCompleteEventHandler;
            managerConnection.QueueCallerJoin += QueueCallerJoinEventHandler;
            managerConnection.QueueCallerAbandon += QueueCallerAbandonEventHandler;
            managerConnection.QueueMemberAdded += QueueMemberAddedEventHandler;
            managerConnection.QueueMemberRemoved += QueueMemberRemovedEventHandler;
            managerConnection.AgentConnect += AgentConnectEventHandler;
            managerConnection.AgentComplete += AgentCompleteEventHandler;
        }

        private void InitializeAutoPollTimer()
        {
            autoPollTimer = new System.Timers.Timer(configuration.PollInterval);
            autoPollTimer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                UpdateConnectionStateIndicator();
                TryRequestQueuesStatus();
            };
        }

        private void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<QueueParamsEvent, QueueParamsDto>()
                    //.ForMember(d => d.QueueLocalWeight, e => e.MapFrom(ev => configuration.QueuesLocalWeights[ev.Queue]))
                    .ForMember(d => d.QueueDescription, e => e.MapFrom(ev => configuration.QueuesDescriptions[ev.Queue]));
                cfg.CreateMap<QueueEntryEvent, QueueEntryDto>()
                    //.ForMember(d => d.QueueLocalWeight, e => e.MapFrom(ev => configuration.QueuesLocalWeights[ev.Queue]))
                    .ForMember(d => d.QueueDescription, e => e.MapFrom(ev => configuration.QueuesDescriptions[ev.Queue]))
                    .ForMember(d => d.CallerId, e => e.MapFrom(ev => ev.Attributes["calleridnum"]))
                    .ForMember(d => d.CallerIdName, e => e.MapFrom(ev => (ev.CallerIdName == ev.Attributes["calleridnum"] ? "" : ev.CallerIdName)));
            });
            Mapper.Configuration.AssertConfigurationIsValid();
        }

        #endregion

        #region Configurators

        private void ConfigureQueueParamsView()
        {
            queueParamsData = new SyncBindingList<QueueParamsDto>(this);
            queueParamsView.DataSource = new BindingSource(queueParamsData, null);
            queueParamsView.DataError += DataGridViewDataErrorEventHandler;
            queueParamsView.Columns["DateReceived"].DefaultCellStyle.Format = configuration.DateTimeFormat;
            queueParamsView.Columns["Queue"].FillWeight = 90;
            queueParamsView.Columns["QueueDescription"].FillWeight = 100;
            queueParamsView.Columns["Calls"].FillWeight = 50;
            queueParamsView.Columns["Abandoned"].FillWeight = 50;
            queueParamsView.Columns["Completed"].FillWeight = 60;
            queueParamsView.Columns["DateReceived"].FillWeight = 80;
            queueParamsView.Columns["QueueDescription"].HeaderText = "Description";
            queueParamsView.Columns["Abandoned"].HeaderText = "Aband";
            queueParamsView.Columns["DateReceived"].HeaderText = "Update";
            //queueParamsView.Columns["QueueLocalWeight"].Visible = false;

            if (configuration.HightlightRowIfQueueNotEmpty)
            {
                queueParamsView.RowPrePaint += HighlightRowsIfNeeded;
            }
            queueParamsView.SelectionChanged += DataGridViewClearSelection;
        }

        private void ConfigureQueueEntriesView()
        {
            queueEntriesData = new SyncBindingList<QueueEntryDto>(this);
            queueEntriesView.DataError += DataGridViewDataErrorEventHandler;
            queueEntriesView.DataSource = new BindingSource(queueEntriesData, null);
            queueEntriesView.Columns["CallerId"].FillWeight = 100;
            queueEntriesView.Columns["CallerIdName"].FillWeight = 120;
            queueEntriesView.Columns["Queue"].FillWeight = 90;
            queueEntriesView.Columns["QueueDescription"].FillWeight = 100;
            queueEntriesView.Columns["Position"].FillWeight = 50;
            queueEntriesView.Columns["Wait"].FillWeight = 50;
            queueEntriesView.Columns["CallerId"].HeaderText = "Number";
            queueEntriesView.Columns["CallerIdName"].HeaderText = "Name";
            queueEntriesView.Columns["QueueDescription"].HeaderText = "Description";
            queueEntriesView.Columns["Position"].HeaderText = "Pos";
            //queueEntriesView.Columns["QueueLocalWeight"].Visible = false;
            queueEntriesView.SelectionChanged += DataGridViewClearSelection;
        }

        private void ConfigureUI()
        {
            // UI Paramters
            queueParamsView.ColumnHeadersHeight = configuration.TableHeadersHeight;
            queueEntriesView.ColumnHeadersHeight = configuration.TableHeadersHeight;
            Location = configuration.FormOrigin;
            Size = configuration.FormSize;
            TopMost = configuration.AlwaysOnTop;
            Text = configuration.WindowTitle;

            // Fonts
            consoleTextBox.Font = configuration.ConsoleFont;
            queueParamsView.ColumnHeadersDefaultCellStyle.Font = configuration.TableHeadersFont;
            queueEntriesView.ColumnHeadersDefaultCellStyle.Font = configuration.TableHeadersFont;
            queueParamsView.DefaultCellStyle.Font = configuration.TableRowsFont;
            queueEntriesView.DefaultCellStyle.Font = configuration.TableRowsFont;
            connectionStateIndicator.Font = configuration.StateIndicatorsFont;
            queueStatusRequestStateIndicator.Font = configuration.StateIndicatorsFont;

            // Splitters
            mainSplitContainer.Panel1Collapsed = false;
            mainSplitContainer.Panel2Collapsed = false;
            mainSplitContainer.SplitterDistance = (int)(mainSplitContainer.ClientSize.Width * configuration.ConsoleSplitterRatio);
            queuesSplitContainer.Panel1Collapsed = false;
            queuesSplitContainer.Panel2Collapsed = false;
            queuesSplitContainer.SplitterDistance = (int)(queuesSplitContainer.ClientSize.Width * configuration.QueuesSplitterRatio);

            // Colors and borders
            Color TableHeadersColor = configuration.TableHeadersColor;
            Color TableGridsColor = configuration.TableGridsColor;
            Color ConsoleAreaColor = configuration.ConsoleAreaColor;
            Color QueuesAreaColor = configuration.QueuesAreaColor;

            mainSplitContainer.BackColor = ConsoleAreaColor;
            queuesSplitContainer.BackColor = QueuesAreaColor;
            queueParamsView.BackgroundColor = QueuesAreaColor;
            queueEntriesView.BackgroundColor = QueuesAreaColor;
            queueParamsView.DefaultCellStyle.BackColor = QueuesAreaColor;
            queueEntriesView.DefaultCellStyle.BackColor = QueuesAreaColor;
            consoleTextBox.BackColor = ConsoleAreaColor;

            queueParamsView.EnableHeadersVisualStyles = false;
            queueEntriesView.EnableHeadersVisualStyles = false;

            queueParamsView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            queueEntriesView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            queueParamsView.GridColor = TableGridsColor;
            queueEntriesView.GridColor = TableGridsColor;
            queueParamsView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            queueParamsView.ColumnHeadersDefaultCellStyle.BackColor = TableHeadersColor;
            queueEntriesView.ColumnHeadersDefaultCellStyle.BackColor = TableHeadersColor;

            queueParamsView.ColumnHeadersDefaultCellStyle.ForeColor = configuration.TableHeadersTextColor;
            queueEntriesView.ColumnHeadersDefaultCellStyle.ForeColor = configuration.TableHeadersTextColor;

            queueParamsView.DefaultCellStyle.ForeColor = configuration.TableRowsTextColor;
            queueEntriesView.DefaultCellStyle.ForeColor = configuration.TableRowsTextColor;
        }

        #endregion

    }
}
