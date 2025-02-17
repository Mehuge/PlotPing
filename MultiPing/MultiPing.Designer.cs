namespace PlotPingApp
{
    partial class MultiPing
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
            System.Windows.Forms.ColumnHeader Index;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiPing));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plotPingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiPingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiPingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonMRU = new System.Windows.Forms.Button();
            this.testIPAddress = new System.Windows.Forms.TextBox();
            this.buttonGoNew = new System.Windows.Forms.Button();
            this.listViewPings = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.min = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.max = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ave = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotPingToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(815, 28);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plotPingToolStripMenuItem
            // 
            this.plotPingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.multiPingToolStripMenuItem,
            this.multiPingToolStripMenuItem1,
            this.settingsToolStripMenuItem});
            this.plotPingToolStripMenuItem.Name = "plotPingToolStripMenuItem";
            this.plotPingToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.plotPingToolStripMenuItem.Text = "Multi Ping";
            // 
            // multiPingToolStripMenuItem
            // 
            this.multiPingToolStripMenuItem.Name = "multiPingToolStripMenuItem";
            this.multiPingToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.multiPingToolStripMenuItem.Text = "Multi Ping ...";
            this.multiPingToolStripMenuItem.Click += new System.EventHandler(this.multiPingToolStripMenuItem_Click);
            // 
            // multiPingToolStripMenuItem1
            // 
            this.multiPingToolStripMenuItem1.Name = "multiPingToolStripMenuItem1";
            this.multiPingToolStripMenuItem1.Size = new System.Drawing.Size(172, 26);
            this.multiPingToolStripMenuItem1.Text = "Plot Ping ...";
            this.multiPingToolStripMenuItem1.Click += new System.EventHandler(this.multiPingToolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.settingsToolStripMenuItem.Text = "Settings ...";
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
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.githubToolStripMenuItem.Text = "Github";
            // 
            // buttonMRU
            // 
            this.buttonMRU.BackColor = System.Drawing.Color.Transparent;
            this.buttonMRU.FlatAppearance.BorderSize = 0;
            this.buttonMRU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMRU.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.buttonMRU.Location = new System.Drawing.Point(0, 30);
            this.buttonMRU.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMRU.Name = "buttonMRU";
            this.buttonMRU.Size = new System.Drawing.Size(34, 28);
            this.buttonMRU.TabIndex = 20;
            this.buttonMRU.Text = "▼";
            this.buttonMRU.UseVisualStyleBackColor = false;
            this.buttonMRU.Click += new System.EventHandler(this.buttonMRU_Click);
            // 
            // testIPAddress
            // 
            this.testIPAddress.Location = new System.Drawing.Point(34, 33);
            this.testIPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.testIPAddress.Name = "testIPAddress";
            this.testIPAddress.Size = new System.Drawing.Size(525, 22);
            this.testIPAddress.TabIndex = 21;
            this.testIPAddress.TextChanged += new System.EventHandler(this.testIPAddress_TextChanged);
            // 
            // buttonGoNew
            // 
            this.buttonGoNew.Location = new System.Drawing.Point(560, 31);
            this.buttonGoNew.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGoNew.Name = "buttonGoNew";
            this.buttonGoNew.Size = new System.Drawing.Size(28, 27);
            this.buttonGoNew.TabIndex = 22;
            this.buttonGoNew.Text = "+";
            this.buttonGoNew.UseVisualStyleBackColor = true;
            // 
            // listViewPings
            // 
            this.listViewPings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewPings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            Index,
            this.IPAddress,
            this.RTT,
            this.min,
            this.max,
            this.ave,
            this.PL,
            this.plot});
            this.listViewPings.HideSelection = false;
            this.listViewPings.Location = new System.Drawing.Point(0, 61);
            this.listViewPings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewPings.Name = "listViewPings";
            this.listViewPings.OwnerDraw = true;
            this.listViewPings.Size = new System.Drawing.Size(815, 281);
            this.listViewPings.TabIndex = 23;
            this.listViewPings.UseCompatibleStateImageBehavior = false;
            this.listViewPings.View = System.Windows.Forms.View.List;
            // 
            // Index
            // 
            Index.Text = "Hop";
            Index.Width = 40;
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
            // MultiPing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 683);
            this.Controls.Add(this.listViewPings);
            this.Controls.Add(this.buttonGoNew);
            this.Controls.Add(this.testIPAddress);
            this.Controls.Add(this.buttonMRU);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultiPing";
            this.Text = "MulitiPing";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plotPingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiPingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiPingToolStripMenuItem;
        private System.Windows.Forms.Button buttonMRU;
        private System.Windows.Forms.TextBox testIPAddress;
        private System.Windows.Forms.Button buttonGoNew;
        private System.Windows.Forms.ListView listViewPings;
        private System.Windows.Forms.ColumnHeader IPAddress;
        private System.Windows.Forms.ColumnHeader RTT;
        private System.Windows.Forms.ColumnHeader min;
        private System.Windows.Forms.ColumnHeader max;
        private System.Windows.Forms.ColumnHeader ave;
        private System.Windows.Forms.ColumnHeader PL;
        private System.Windows.Forms.ColumnHeader plot;
    }
}