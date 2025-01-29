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
            this.graphs = new System.Windows.Forms.Panel();
            this.picker = new System.Windows.Forms.Button();
            HOP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.testIPAddress.Size = new System.Drawing.Size(1211, 22);
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
            this.buttonGoNew.Location = new System.Drawing.Point(1236, 14);
            this.buttonGoNew.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGoNew.Name = "buttonGoNew";
            this.buttonGoNew.Size = new System.Drawing.Size(100, 24);
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
            this.ave});
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
            // graphs
            // 
            this.graphs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphs.Location = new System.Drawing.Point(17, 542);
            this.graphs.Name = "graphs";
            this.graphs.Size = new System.Drawing.Size(1323, 369);
            this.graphs.TabIndex = 7;
            // 
            // picker
            // 
            this.picker.BackColor = System.Drawing.Color.Transparent;
            this.picker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.picker.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.picker.Location = new System.Drawing.Point(1192, 14);
            this.picker.Margin = new System.Windows.Forms.Padding(2);
            this.picker.Name = "picker";
            this.picker.Size = new System.Drawing.Size(38, 24);
            this.picker.TabIndex = 8;
            this.picker.Text = "▼";
            this.picker.UseVisualStyleBackColor = false;
            // 
            // PlotPing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 923);
            this.Controls.Add(this.picker);
            this.Controls.Add(this.graphs);
            this.Controls.Add(this.listViewTrace);
            this.Controls.Add(this.buttonGoNew);
            this.Controls.Add(this.testIPAddress);
            this.Controls.Add(this.formsPlot1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PlotPing";
            this.Text = "Plot Ping";
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
        private System.Windows.Forms.Button picker;
    }
}

