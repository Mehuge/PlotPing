using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotPingApp
{
    public partial class Donate : Form
    {

        public Donate()
        {
            InitializeComponent();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            OpenLink.Donation();
            this.Close();
        }
    }
}
