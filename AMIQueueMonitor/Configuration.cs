using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Drawing;

namespace AMIQueueMonitor
{
    public class Configuration
    {
        private static string _queueConfigKey = "Queues";
        private static char _queueConfigSectionSeparator = ',';
        private static char _queueConfigParamsSeparator = ':';
        private static int _queuesConfigIndex = 0;
        //private static int _queueConfigLocalWeightIndex = 1;
        private static int _queueConfigDescriptionIndex = 1;

        public IList<string> Queues { get; }
        public Dictionary<string, string> QueuesDescriptions { get; }
        public Dictionary<string, int> QueuesLocalWeights { get; }
        public string Server { get; }
        public int ServerPort { get; }
        public string Username { get; }
        public string Password { get; }
        public bool PopupOnNewCall { get; }
        public bool AlwaysOnTop { get; set; }
        public bool HightlightRowIfQueueNotEmpty { get; }
        public bool StartAutoPollOnStartup { get; }
        public int PollInterval { get; }
        public Font ConsoleFont { get; }
        public Font TableHeadersFont { get; }
        public Font TableRowsFont { get; }
        public Font StateIndicatorsFont { get; }
        public int TableHeadersHeight { get; }
        public Point FormOrigin { get; }
        public Size FormSize { get; }
        public float ConsoleSplitterRatio { get; }
        public float QueuesSplitterRatio { get; }
        public Color TableHeadersColor { get; }
        public Color TableGridsColor { get; }
        public Color ConsoleAreaColor { get; }
        public Color QueuesAreaColor { get; }
        public Color TableHeadersTextColor { get; }
        public Color TableRowsTextColor { get; }
        public Color ConsoleInfoTextColor { get; }
        public Color ConsoleErrorTextColor { get; }
        public Color HighlightRowColor { get; }
        public string DateTimeFormat { get; }
        public string WindowTitle { get; }

        public Configuration()
        {
            //Queues settings
            try
            {
                Queues = ConfigurationManager.AppSettings[_queueConfigKey]
                    .Split(_queueConfigSectionSeparator)
                    .Select(p => p.Split(_queueConfigParamsSeparator))
                    .Select(sp => sp[_queuesConfigIndex])
                    .ToList();

                QueuesDescriptions = ConfigurationManager.AppSettings[_queueConfigKey]
                    .Split(_queueConfigSectionSeparator)
                    .Select(p => p.Split(_queueConfigParamsSeparator))
                    .ToDictionary(sp => sp[_queuesConfigIndex], sp => sp[_queueConfigDescriptionIndex]);

                //QueuesLocalWeights = ConfigurationManager.AppSettings[_queueConfigKey]
                //    .Split(_queueConfigSectionSeparator)
                //    .Select(p => p.Split(_queueConfigParamsSeparator))
                //    .ToDictionary(sp => sp[_queuesConfigIndex], sp => int.Parse(sp[_queueConfigLocalWeightIndex]));
            }
            catch (Exception e)
            {
                throw new ConfigurationErrorsException("Coudln't load Queues settings", e);
            }
            if (Queues == null || Queues.Count == 0 || Queues.Contains(string.Empty))
            {
                throw new ConfigurationErrorsException("Queue configuration is invalid");
            }

            // Server settings
            try
            {
                Server = ConfigurationManager.AppSettings["Server"];
                ServerPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);
                Username = ConfigurationManager.AppSettings["Username"];
                Password = ConfigurationManager.AppSettings["Password"];
                

            }
            catch (Exception e)
            {
                throw new ConfigurationErrorsException("Coudln't load Server settings", e);
            }

            // Application settings
            try
            {
                PopupOnNewCall = bool.Parse(ConfigurationManager.AppSettings["PopupOnNewCall"]);
                AlwaysOnTop = bool.Parse(ConfigurationManager.AppSettings["AlwaysOnTop"]);
                PollInterval = int.Parse(ConfigurationManager.AppSettings["PollInterval"]);
                StartAutoPollOnStartup = bool.Parse(ConfigurationManager.AppSettings["StartAutoPollOnStartup"]);
                HightlightRowIfQueueNotEmpty = bool.Parse(ConfigurationManager.AppSettings["HightlightRowIfQueueNotEmpty"]);
                DateTimeFormat = ConfigurationManager.AppSettings["DateTimeFormat"];
            }
            catch (Exception e)
            {
                throw new ConfigurationErrorsException("Coudln't load Application settings", e);
            }

            // Style settings
            try
            {
                string cnfOrigin = ConfigurationManager.AppSettings["FormOrigin"];
                string[] Coords = cnfOrigin.Split(',');
                string cnfSize = ConfigurationManager.AppSettings["FormSize"];
                string[] Size = cnfSize.Split(',');
                WindowTitle = ConfigurationManager.AppSettings["WindowTitle"];
                var fontConverter = new FontConverter();
                ConsoleFont = fontConverter.ConvertFromString(ConfigurationManager.AppSettings["ConsoleFont"]) as Font;
                TableHeadersFont = fontConverter.ConvertFromString(ConfigurationManager.AppSettings["TableHeadersFont"]) as Font;
                TableRowsFont = fontConverter.ConvertFromString(ConfigurationManager.AppSettings["TableRowsFont"]) as Font;
                StateIndicatorsFont = fontConverter.ConvertFromString(ConfigurationManager.AppSettings["StateIndicatorsFont"]) as Font;
                TableHeadersHeight = int.Parse(ConfigurationManager.AppSettings["TableHeadersHeight"]);
                FormOrigin = new Point(int.Parse(Coords[0]), int.Parse(Coords[1]));
                FormSize = new Size(int.Parse(Size[0]), int.Parse(Size[1]));
                ConsoleSplitterRatio = float.Parse(ConfigurationManager.AppSettings["ConsoleSplitterRatio"]);//,splitter in cfg
                QueuesSplitterRatio = float.Parse(ConfigurationManager.AppSettings["QueuesSplitterRatio"]);//,splitter in cfg
                TableHeadersColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["TableHeadersColor"]);
                TableGridsColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["TableGridsColor"]);
                ConsoleAreaColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["ConsoleAreaColor"]);
                QueuesAreaColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["QueuesAreaColor"]);
                TableHeadersTextColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["TableHeadersTextColor"]);
                TableRowsTextColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["TableRowsTextColor"]);
                ConsoleInfoTextColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["ConsoleInfoTextColor"]);
                ConsoleErrorTextColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["ConsoleErrorTextColor"]);
                HighlightRowColor = ColorTranslator.FromHtml(ConfigurationManager.AppSettings["HighlightRowColor"]);
            }
            catch (Exception e)
            {
                throw new ConfigurationErrorsException("Coudln't load Style settings", e);
            }
        }
    }
}
