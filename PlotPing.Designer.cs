namespace PlotPingApp
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
            this.testIPAddress = new System.Windows.Forms.TextBox();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.buttonGoNew = new System.Windows.Forms.Button();
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
            HOP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.offsetBar)).BeginInit();
            this.SuspendLayout();
            // 
            // HOP
            // 
            HOP.Text = "Hop";
            HOP.Width = 40;
            // 
            // testIPAddress
            // 
            this.testIPAddress.Location = new System.Drawing.Point(17, 14);
            this.testIPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.testIPAddress.Name = "testIPAddress";
            this.testIPAddress.Size = new System.Drawing.Size(671, 22);
            this.testIPAddress.TabIndex = 0;
            this.testIPAddress.Text = "94.250.204.136";
            this.testIPAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.testIPAddress_KeyPress);
            // 
            // formsPlot1
            // 
            this.formsPlot1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlot1.BackColor = System.Drawing.SystemColors.Info;
            this.formsPlot1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.formsPlot1.Location = new System.Drawing.Point(697, 59);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(639, 475);
            this.formsPlot1.TabIndex = 3;
            // 
            // buttonGoNew
            // 
            this.buttonGoNew.Location = new System.Drawing.Point(1235, 8);
            this.buttonGoNew.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGoNew.Name = "buttonGoNew";
            this.buttonGoNew.Size = new System.Drawing.Size(100, 37);
            this.buttonGoNew.TabIndex = 5;
            this.buttonGoNew.Text = "Start";
            this.buttonGoNew.UseVisualStyleBackColor = true;
            this.buttonGoNew.Click += new System.EventHandler(this.buttonGoNew_Click);
            // 
            // listViewTrace
            // 
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
            this.listViewTrace.Location = new System.Drawing.Point(17, 59);
            this.listViewTrace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewTrace.Name = "listViewTrace";
            this.listViewTrace.OwnerDraw = true;
            this.listViewTrace.Size = new System.Drawing.Size(671, 475);
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
            this.graphs.Location = new System.Drawing.Point(17, 564);
            this.graphs.Name = "graphs";
            this.graphs.Size = new System.Drawing.Size(1319, 347);
            this.graphs.TabIndex = 7;
            // 
            // buttonMRU
            // 
            this.buttonMRU.BackColor = System.Drawing.Color.Transparent;
            this.buttonMRU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMRU.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.buttonMRU.Location = new System.Drawing.Point(694, 14);
            this.buttonMRU.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMRU.Name = "buttonMRU";
            this.buttonMRU.Size = new System.Drawing.Size(32, 24);
            this.buttonMRU.TabIndex = 8;
            this.buttonMRU.Text = "▼";
            this.buttonMRU.UseVisualStyleBackColor = false;
            this.buttonMRU.Click += new System.EventHandler(this.buttonMRU_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(957, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sample Window (mins)";
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
            this.window.Location = new System.Drawing.Point(1107, 14);
            this.window.Name = "window";
            this.window.Size = new System.Drawing.Size(121, 24);
            this.window.TabIndex = 12;
            this.window.SelectedIndexChanged += new System.EventHandler(this.window_SelectedIndexChanged);
            // 
            // offsetBar
            // 
            this.offsetBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetBar.Location = new System.Drawing.Point(17, 539);
            this.offsetBar.Name = "offsetBar";
            this.offsetBar.Size = new System.Drawing.Size(1318, 56);
            this.offsetBar.TabIndex = 16;
            this.offsetBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.offsetBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(743, 19);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 16);
            this.status.TabIndex = 17;
            // 
            // PlotPing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 923);
            this.Controls.Add(this.status);
            this.Controls.Add(this.window);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonMRU);
            this.Controls.Add(this.graphs);
            this.Controls.Add(this.listViewTrace);
            this.Controls.Add(this.buttonGoNew);
            this.Controls.Add(this.testIPAddress);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.offsetBar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PlotPing";
            this.Text = "Plot Ping";
            this.Resize += new System.EventHandler(this.PlotPing_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.offsetBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox testIPAddress;
        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.Button buttonGoNew;
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
    }
}

