﻿namespace PlotPingApp
{
    partial class PlotPing
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
            System.Windows.Forms.ColumnHeader HOP;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlotPing));
            this.testIPAddress = new System.Windows.Forms.TextBox();
            this.liveView = new ScottPlot.FormsPlot();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.listViewTrace = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.min = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.max = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ave = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.graphs = new System.Windows.Forms.Panel();
            this.buttonMRU = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.window = new System.Windows.Forms.ComboBox();
            this.offsetBar = new System.Windows.Forms.TrackBar();
            this.status = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plotPingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotPingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.multiPingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logTrace = new System.Windows.Forms.CheckBox();
            this.traceStatus = new System.Windows.Forms.Label();
            HOP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.offsetBar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HOP
            // 
            HOP.Text = "Hop";
            HOP.Width = 40;
            // 
            // testIPAddress
            // 
            this.testIPAddress.Location = new System.Drawing.Point(40, 36);
            this.testIPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.testIPAddress.Name = "testIPAddress";
            this.testIPAddress.Size = new System.Drawing.Size(525, 22);
            this.testIPAddress.TabIndex = 0;
            this.testIPAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.testIPAddress_KeyPress);
            // 
            // liveView
            // 
            this.liveView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.liveView.BackColor = System.Drawing.SystemColors.Window;
            this.liveView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.liveView.Location = new System.Drawing.Point(670, 66);
            this.liveView.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.liveView.Name = "liveView";
            this.liveView.Size = new System.Drawing.Size(682, 468);
            this.liveView.TabIndex = 3;
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(573, 31);
            this.buttonStartStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(98, 32);
            this.buttonStartStop.TabIndex = 5;
            this.buttonStartStop.Text = "Start";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // listViewTrace
            // 
            this.listViewTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewTrace.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            HOP,
            this.IPAddress,
            this.RTT,
            this.min,
            this.max,
            this.ave,
            this.PL,
            this.plot});
            this.listViewTrace.HideSelection = false;
            this.listViewTrace.Location = new System.Drawing.Point(0, 66);
            this.listViewTrace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewTrace.Name = "listViewTrace";
            this.listViewTrace.OwnerDraw = true;
            this.listViewTrace.Size = new System.Drawing.Size(671, 468);
            this.listViewTrace.TabIndex = 6;
            this.listViewTrace.UseCompatibleStateImageBehavior = false;
            this.listViewTrace.View = System.Windows.Forms.View.List;
            this.listViewTrace.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listViewTrace_DrawColumnHeader);
            this.listViewTrace.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listViewTrace_DrawSubItem);
            this.listViewTrace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewTrace_MouseDown);
            // 
            // IPAddress
            // 
            this.IPAddress.Text = "IP Address";
            this.IPAddress.Width = 120;
            // 
            // RTT
            // 
            this.RTT.Text = "RTT";
            this.RTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // min
            // 
            this.min.Text = "Min";
            this.min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // max
            // 
            this.max.Text = "Max";
            this.max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ave
            // 
            this.ave.Text = "Ave";
            this.ave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PL
            // 
            this.PL.Text = "PL%";
            this.PL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PL.Width = 30;
            // 
            // plot
            // 
            this.plot.Text = "Plot";
            this.plot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.plot.Width = 30;
            // 
            // graphs
            // 
            this.graphs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphs.AutoScroll = true;
            this.graphs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.graphs.Location = new System.Drawing.Point(0, 564);
            this.graphs.Margin = new System.Windows.Forms.Padding(0);
            this.graphs.Name = "graphs";
            this.graphs.Size = new System.Drawing.Size(1352, 357);
            this.graphs.TabIndex = 7;
            // 
            // buttonMRU
            // 
            this.buttonMRU.BackColor = System.Drawing.Color.Transparent;
            this.buttonMRU.FlatAppearance.BorderSize = 0;
            this.buttonMRU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMRU.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.buttonMRU.Location = new System.Drawing.Point(4, 35);
            this.buttonMRU.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMRU.Name = "buttonMRU";
            this.buttonMRU.Size = new System.Drawing.Size(34, 25);
            this.buttonMRU.TabIndex = 8;
            this.buttonMRU.Text = "▼";
            this.buttonMRU.UseVisualStyleBackColor = false;
            this.buttonMRU.Click += new System.EventHandler(this.buttonMRU_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1117, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sample Window";
            // 
            // window
            // 
            this.window.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.window.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.window.FormattingEnabled = true;
            this.window.Items.AddRange(new object[] {
            "60 Seconds",
            "5 Minutes",
            "10 Minutes",
            "15 Minutes",
            "30 Minutes",
            "60 Minutes",
            "4 Hours",
            "8 Hours",
            "12 Hours",
            "24 Hours",
            "48 Hours"});
            this.window.Location = new System.Drawing.Point(1228, 35);
            this.window.Name = "window";
            this.window.Size = new System.Drawing.Size(121, 24);
            this.window.TabIndex = 12;
            this.window.SelectedIndexChanged += new System.EventHandler(this.window_SelectedIndexChanged);
            // 
            // offsetBar
            // 
            this.offsetBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.offsetBar.Location = new System.Drawing.Point(0, 536);
            this.offsetBar.Name = "offsetBar";
            this.offsetBar.Size = new System.Drawing.Size(1352, 56);
            this.offsetBar.TabIndex = 16;
            this.offsetBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.offsetBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(932, 38);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 16);
            this.status.TabIndex = 17;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotPingToolStripMenuItem,
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1352, 28);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plotPingToolStripMenuItem
            // 
            this.plotPingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotPingToolStripMenuItem1,
            this.multiPingToolStripMenuItem1,
            this.settingsToolStripMenuItem});
            this.plotPingToolStripMenuItem.Name = "plotPingToolStripMenuItem";
            this.plotPingToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.plotPingToolStripMenuItem.Text = "Plot Ping";
            // 
            // plotPingToolStripMenuItem1
            // 
            this.plotPingToolStripMenuItem1.Name = "plotPingToolStripMenuItem1";
            this.plotPingToolStripMenuItem1.Size = new System.Drawing.Size(261, 26);
            this.plotPingToolStripMenuItem1.Text = "New Plot Ping Window...";
            this.plotPingToolStripMenuItem1.Click += new System.EventHandler(this.plotPingToolStripMenuItem1_Click);
            // 
            // multiPingToolStripMenuItem1
            // 
            this.multiPingToolStripMenuItem1.Name = "multiPingToolStripMenuItem1";
            this.multiPingToolStripMenuItem1.Size = new System.Drawing.Size(261, 26);
            this.multiPingToolStripMenuItem1.Text = "New Multi Ping Window...";
            this.multiPingToolStripMenuItem1.Visible = false;
            this.multiPingToolStripMenuItem1.Click += new System.EventHandler(this.multiPingToolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.settingsToolStripMenuItem.Text = "Settings ...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.fileToolStripMenuItem.Text = "Trace";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.exportToolStripMenuItem.Text = "Export ...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.helpToolStripMenuItem,
            this.githubToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(136, 26);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.githubToolStripMenuItem.Text = "Github";
            this.githubToolStripMenuItem.Click += new System.EventHandler(this.githubToolStripMenuItem_Click);
            // 
            // logTrace
            // 
            this.logTrace.AutoSize = true;
            this.logTrace.Location = new System.Drawing.Point(678, 39);
            this.logTrace.Name = "logTrace";
            this.logTrace.Size = new System.Drawing.Size(91, 20);
            this.logTrace.TabIndex = 19;
            this.logTrace.Text = "Log Trace";
            this.logTrace.UseVisualStyleBackColor = true;
            this.logTrace.CheckedChanged += new System.EventHandler(this.logTrace_CheckedChanged);
            // 
            // traceStatus
            // 
            this.traceStatus.BackColor = System.Drawing.Color.White;
            this.traceStatus.Location = new System.Drawing.Point(172, 260);
            this.traceStatus.Name = "traceStatus";
            this.traceStatus.Size = new System.Drawing.Size(379, 23);
            this.traceStatus.TabIndex = 20;
            this.traceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.traceStatus.Visible = false;
            // 
            // PlotPing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1352, 923);
            this.Controls.Add(this.traceStatus);
            this.Controls.Add(this.logTrace);
            this.Controls.Add(this.status);
            this.Controls.Add(this.window);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonMRU);
            this.Controls.Add(this.graphs);
            this.Controls.Add(this.listViewTrace);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.testIPAddress);
            this.Controls.Add(this.liveView);
            this.Controls.Add(this.offsetBar);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PlotPing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Plot Ping";
            this.Resize += new System.EventHandler(this.PlotPing_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.offsetBar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox testIPAddress;
        private ScottPlot.FormsPlot liveView;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.ListView listViewTrace;
        private System.Windows.Forms.ColumnHeader IPAddress;
        private System.Windows.Forms.ColumnHeader RTT;
        private System.Windows.Forms.ColumnHeader min;
        private System.Windows.Forms.ColumnHeader max;
        private System.Windows.Forms.ColumnHeader ave;
        private System.Windows.Forms.Panel graphs;
        private System.Windows.Forms.Button buttonMRU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox window;
        private System.Windows.Forms.TrackBar offsetBar;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.ColumnHeader PL;
        private System.Windows.Forms.ColumnHeader plot;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;
        private System.Windows.Forms.CheckBox logTrace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem plotPingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiPingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plotPingToolStripMenuItem1;
        private System.Windows.Forms.Label traceStatus;
    }
}

