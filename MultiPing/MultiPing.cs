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
            Settings.RestoreFormLocation(this);
        }

        private void multiPingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Action.StartPlotPing();
        }

        private void multiPingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action.StartMultiPing();
        }
    }
}
