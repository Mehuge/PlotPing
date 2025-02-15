using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotPingApp
{
    public partial class MultiPing : Form
    {
        public MultiPing()
        {
            InitializeComponent();
        }

        private void multiPingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.RunWithArgs("--start PlotPing");
        }

        private void multiPingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.RunWithArgs("--start MultiPing");
        }
    }
}
