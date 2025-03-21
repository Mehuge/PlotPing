namespace PlotPingApp
{
    partial class Settings
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
            this.logOutputFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.activeTracksWindowCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timeout = new System.Windows.Forms.TextBox();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // logOutputFolder
            // 
            this.logOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logOutputFolder.Location = new System.Drawing.Point(150, 14);
            this.logOutputFolder.Name = "logOutputFolder";
            this.logOutputFolder.ReadOnly = true;
            this.logOutputFolder.Size = new System.Drawing.Size(412, 22);
            this.logOutputFolder.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Log Output Folder";
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectFolderButton.Location = new System.Drawing.Point(568, 12);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(32, 27);
            this.selectFolderButton.TabIndex = 2;
            this.selectFolderButton.Text = "...";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.selectFolderButton_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(525, 211);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 32);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.activeTracksWindowCheckBox);
            this.settingsPanel.Location = new System.Drawing.Point(15, 90);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(585, 115);
            this.settingsPanel.TabIndex = 4;
            // 
            // activeTracksWindowCheckBox
            // 
            this.activeTracksWindowCheckBox.AutoSize = true;
            this.activeTracksWindowCheckBox.Location = new System.Drawing.Point(15, 13);
            this.activeTracksWindowCheckBox.Name = "activeTracksWindowCheckBox";
            this.activeTracksWindowCheckBox.Size = new System.Drawing.Size(199, 20);
            this.activeTracksWindowCheckBox.TabIndex = 4;
            this.activeTracksWindowCheckBox.Text = "Active Tracks Window Offset";
            this.activeTracksWindowCheckBox.UseVisualStyleBackColor = true;
            this.activeTracksWindowCheckBox.CheckedChanged += new System.EventHandler(this.activeTracksWindow_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Timeout (ms)";
            // 
            // timeout
            // 
            this.timeout.Location = new System.Drawing.Point(150, 43);
            this.timeout.Name = "timeout";
            this.timeout.Size = new System.Drawing.Size(100, 22);
            this.timeout.TabIndex = 7;
            this.timeout.Text = "4000";
            this.timeout.TextChanged += new System.EventHandler(this.timeout_TextChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 255);
            this.Controls.Add(this.timeout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.selectFolderButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logOutputFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plot Ping Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logOutputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.CheckBox activeTracksWindowCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timeout;
    }
}