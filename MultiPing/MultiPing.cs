using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotPingApp
{
    public partial class MultiPing : Form
    {
        private MRU mru;
        private List<string> hosts = new List<string>();

        public MultiPing()
        {
            InitializeComponent();
            Settings.LoadSettings();
            Settings.RestoreFormLocation(this);
            // Load MRU
            mru = new MRU(Path.Combine(Path.GetDirectoryName(Application.UserAppDataPath), "mru.txt"), 25);
        }

        private void multiPingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utils.StartPlotPing();
        }

        private void multiPingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.StartMultiPing();
        }

        private void buttonMRU_Click(object sender, EventArgs e)
        {
            mru.Show(buttonMRU, this, (string ipAddress) => { this.Start(ipAddress); });
        }

        private void Start(string ipAddress)
        {
            hosts.Add(ipAddress);
            mru.Add(ipAddress);
            mru.Save();
            StartPinging();
        }

        private void StartPinging()
        {
            // Need to think about how this will work. Thinking a dictionary of hosts, keyed by address
            // and the value is an array of Hops (or Pings)... Probably rename Hop, as Ping and make traceroute
            // hops, arrays of Pings, so that the graphs work from Pings not Hops.
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mru.Save();
            Settings.SaveSettings();
        }

        private void testIPAddress_TextChanged(object sender, EventArgs e)
        {
            this.Start(testIPAddress.Text);
        }
    }
}
