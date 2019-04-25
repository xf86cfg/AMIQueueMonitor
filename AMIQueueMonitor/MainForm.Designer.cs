namespace AMIQueueMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionStateIndicator = new System.Windows.Forms.ToolStripStatusLabel();
            this.queueStatusRequestStateIndicator = new System.Windows.Forms.ToolStripStatusLabel();
            this.queueParamsView = new System.Windows.Forms.DataGridView();
            this.queuesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.queueEntriesView = new System.Windows.Forms.DataGridView();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.consoleTextBox = new System.Windows.Forms.RichTextBox();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queueParamsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.queuesSplitContainer)).BeginInit();
            this.queuesSplitContainer.Panel1.SuspendLayout();
            this.queuesSplitContainer.Panel2.SuspendLayout();
            this.queuesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queueEntriesView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStateIndicator,
            this.queueStatusRequestStateIndicator});
            this.statusStrip.Location = new System.Drawing.Point(0, 874);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(698, 30);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // connectionStateIndicator
            // 
            this.connectionStateIndicator.DoubleClickEnabled = true;
            this.connectionStateIndicator.Name = "connectionStateIndicator";
            this.connectionStateIndicator.Size = new System.Drawing.Size(127, 25);
            this.connectionStateIndicator.Text = "                       ";
            this.connectionStateIndicator.DoubleClick += new System.EventHandler(this.ConnectionStateIndicatorDoubleClick);
            // 
            // queueStatusRequestStateIndicator
            // 
            this.queueStatusRequestStateIndicator.Name = "queueStatusRequestStateIndicator";
            this.queueStatusRequestStateIndicator.Size = new System.Drawing.Size(127, 25);
            this.queueStatusRequestStateIndicator.Text = "                       ";
            // 
            // queueParamsView
            // 
            this.queueParamsView.AllowUserToAddRows = false;
            this.queueParamsView.AllowUserToDeleteRows = false;
            this.queueParamsView.AllowUserToResizeRows = false;
            this.queueParamsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.queueParamsView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.queueParamsView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.queueParamsView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.queueParamsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.queueParamsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.queueParamsView.DefaultCellStyle = dataGridViewCellStyle2;
            this.queueParamsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queueParamsView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.queueParamsView.Location = new System.Drawing.Point(0, 0);
            this.queueParamsView.MultiSelect = false;
            this.queueParamsView.Name = "queueParamsView";
            this.queueParamsView.ReadOnly = true;
            this.queueParamsView.RowHeadersVisible = false;
            this.queueParamsView.RowTemplate.Height = 28;
            this.queueParamsView.Size = new System.Drawing.Size(698, 188);
            this.queueParamsView.TabIndex = 8;
            this.queueParamsView.TabStop = false;
            // 
            // queuesSplitContainer
            // 
            this.queuesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queuesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.queuesSplitContainer.Name = "queuesSplitContainer";
            this.queuesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // queuesSplitContainer.Panel1
            // 
            this.queuesSplitContainer.Panel1.Controls.Add(this.queueParamsView);
            // 
            // queuesSplitContainer.Panel2
            // 
            this.queuesSplitContainer.Panel2.Controls.Add(this.queueEntriesView);
            this.queuesSplitContainer.Size = new System.Drawing.Size(698, 437);
            this.queuesSplitContainer.SplitterDistance = 188;
            this.queuesSplitContainer.TabIndex = 10;
            // 
            // queueEntriesView
            // 
            this.queueEntriesView.AllowUserToAddRows = false;
            this.queueEntriesView.AllowUserToDeleteRows = false;
            this.queueEntriesView.AllowUserToResizeRows = false;
            this.queueEntriesView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.queueEntriesView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.queueEntriesView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.queueEntriesView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.queueEntriesView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.queueEntriesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.queueEntriesView.DefaultCellStyle = dataGridViewCellStyle4;
            this.queueEntriesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queueEntriesView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.queueEntriesView.Location = new System.Drawing.Point(0, 0);
            this.queueEntriesView.MultiSelect = false;
            this.queueEntriesView.Name = "queueEntriesView";
            this.queueEntriesView.ReadOnly = true;
            this.queueEntriesView.RowHeadersVisible = false;
            this.queueEntriesView.RowTemplate.Height = 28;
            this.queueEntriesView.Size = new System.Drawing.Size(698, 245);
            this.queueEntriesView.TabIndex = 9;
            this.queueEntriesView.TabStop = false;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.queuesSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.consoleTextBox);
            this.mainSplitContainer.Size = new System.Drawing.Size(698, 874);
            this.mainSplitContainer.SplitterDistance = 437;
            this.mainSplitContainer.TabIndex = 11;
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.consoleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleTextBox.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleTextBox.Location = new System.Drawing.Point(0, 0);
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.consoleTextBox.Size = new System.Drawing.Size(698, 433);
            this.consoleTextBox.TabIndex = 0;
            this.consoleTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(698, 904);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Queue Monitor";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queueParamsView)).EndInit();
            this.queuesSplitContainer.Panel1.ResumeLayout(false);
            this.queuesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.queuesSplitContainer)).EndInit();
            this.queuesSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.queueEntriesView)).EndInit();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.DataGridView queueParamsView;
        private System.Windows.Forms.SplitContainer queuesSplitContainer;
        private System.Windows.Forms.DataGridView queueEntriesView;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.RichTextBox consoleTextBox;
        private System.Windows.Forms.ToolStripStatusLabel connectionStateIndicator;
        private System.Windows.Forms.ToolStripStatusLabel queueStatusRequestStateIndicator;
    }
}

