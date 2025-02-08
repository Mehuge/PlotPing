namespace PlotPingApp
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.product = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.version = new System.Windows.Forms.Label();
            this.donate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // product
            // 
            this.product.AutoSize = true;
            this.product.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.product.Location = new System.Drawing.Point(340, 39);
            this.product.Name = "product";
            this.product.Size = new System.Drawing.Size(208, 54);
            this.product.TabIndex = 3;
            this.product.Text = "Plot Ping";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(59, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 261);
            this.panel1.TabIndex = 2;
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.Location = new System.Drawing.Point(343, 107);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(54, 32);
            this.version.TabIndex = 4;
            this.version.Text = "1.0";
            // 
            // donate
            // 
            this.donate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.donate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("donate.BackgroundImage")));
            this.donate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.donate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.donate.FlatAppearance.BorderSize = 0;
            this.donate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.donate.Location = new System.Drawing.Point(120, 350);
            this.donate.Name = "donate";
            this.donate.Size = new System.Drawing.Size(566, 56);
            this.donate.TabIndex = 5;
            this.donate.UseVisualStyleBackColor = false;
            this.donate.Click += new System.EventHandler(this.donate_Click);
            this.donate.MouseEnter += new System.EventHandler(this.donate_MouseEnter);
            this.donate.MouseLeave += new System.EventHandler(this.donate_MouseLeave);
            this.donate.MouseHover += new System.EventHandler(this.donate_MouseHover);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(349, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 139);
            this.label1.TabIndex = 6;
            this.label1.Text = "Plot Ping was created in my spare time. If you find it useful and would like to m" +
    "ake a small donation as a way of thanks, then please use the PayPal donation but" +
    "ton below.";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.donate);
            this.Controls.Add(this.version);
            this.Controls.Add(this.product);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PlotPing by Mehuge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label product;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Button donate;
        private System.Windows.Forms.Label label1;
    }
}