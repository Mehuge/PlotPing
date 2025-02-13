using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using System.Diagnostics;
using ListView = System.Windows.Forms.ListView;
using System.IO;
using System.Linq;

namespace PlotPingApp
{
    public partial class PlotPing : Form
    {
        private MRU mru;
        private Traceroute traceroute = null;
        private int PING_FREQUENCY = 5000;
        private int windowSize = 900 / 5;
        private int offset = 0;

        public PlotPing()
        {
            InitializeComponent();
            formsPlot1.Margin = new Padding(0);
            formsPlot1.Plot.XAxis.Color(Color.Gray);
            formsPlot1.Plot.XAxis.Layout(0, 0, 40);
            formsPlot1.Plot.YAxis.Color(Color.Gray);
            formsPlot1.Plot.YAxis.Layout(0, 0, 40);
            formsPlot1.Plot.Title(null, false, Color.Gray, 10);
            formsPlot1.Invalidate();

            // Load MRU
            mru = new MRU(Path.Combine(Path.GetDirectoryName(Application.UserAppDataPath), "mru.txt"), 25);
            mru.Load();

            window.SelectedIndex = 4;
        }

        private void buttonGoNew_Click(object sender, EventArgs e)
        {
            bool start = buttonGoNew.Text == "Start";
            StopTrace();
            if (start) StartTrace();
        }

        public void Start(string ipAddress)
        {
            StopTrace();
            testIPAddress.Text = ipAddress.Trim();
            StartTrace();
        }

        private void StartTrace()
        {
            string ipAddress = this.testIPAddress.Text.Trim();
            if (traceroute == null)
            {
                traceroute = new Traceroute(ipAddress);
            }
            else if (traceroute.GetHostAddress() != ipAddress)
            {
                traceroute.Clear();
                traceroute.SetHostAddress(ipAddress);
                selectedHops.Clear();
                while (plotters.Count > 0) RemoveLatencyPlotter(0);
            }

            mru.Add(ipAddress);
            buttonGoNew.Text = "Stop";

            traceroute.OnTrace += Traceroute_OnTrace;

            Task.Run(() => { traceroute.StartBackground(PING_FREQUENCY); });
        }

        private void StopTrace()
        {
            traceroute?.StopBackground();
            buttonGoNew.Text = "Start";
            offsetBar.Visible = false;
        }

        private void Traceroute_OnTrace(object sender, Hop[] hops)
        {
            this.BeginInvoke(new Action(() =>
            {
                updateStatus();
                RenderTrace(formsPlot1, listViewTrace, hops, traceroute);
            }));
        }

        private static List<int> selectedHops = new List<int>();
        private static List<FormsPlot> plotters = new List<FormsPlot>();

        private FormsPlot GetLatencyPlotter(int i)
        {
            var plotter = new FormsPlot();
            int h = this.graphs.Size.Height / selectedHops.Count;
            if (h < 90) h = 90;
            int y = (i * h);
            int w = this.graphs.Width;
            int margin = selectedHops.Count * h > this.graphs.Height ? 20 : 0;
            plotter.Location = new Point(0, y);
            plotter.Margin = new Padding(0, 0, 0, 0);
            plotter.Name = "latencyPlot" + i;
            plotter.Size = new Size(w - margin, h);
            plotter.TabIndex = 6 + i;
            plotter.Plot.Margins(0, 0);
            plotter.Plot.XAxis.Color(Color.Gray);
            plotter.Plot.XAxis.Layout(0, 0, 20);
            plotter.Plot.YAxis.Color(Color.Gray);
            plotter.Plot.YAxis.Layout(0, 0, 40);
            plotter.BackColor = Color.WhiteSmoke;
            plotter.Plot.Title("", false, Color.Gray, 10);
            plotters.Add(plotter);
            this.graphs.Controls.Add(plotter);
            return plotter;
        }

        private void RemoveLatencyPlotter(int i)
        {
            this.graphs.Controls.RemoveAt(i);
            plotters.RemoveAt(i);
        }

        private void ResizePlotters()
        {
            this.graphs.AutoScrollPosition = new Point(0, 0);
            if (selectedHops.Count == 0) return;
            int h = this.graphs.Size.Height / selectedHops.Count;
            int w = this.graphs.Width;
            if (h < 90) h = 90;
            int margin = h * selectedHops.Count > this.graphs.Height ? 20 : 0;
            for (int i = 0; i < plotters.Count; i++)
            {
                plotters[i].Location = new Point(0, i * h);
                plotters[i].Size = new Size(w - margin, h);
                plotters[i].Invalidate();
            }
        }

        private void UpdatePlotters()
        {
            bool updated = false;
            while (selectedHops.Count > plotters.Count)
            {
                GetLatencyPlotter(plotters.Count);
                updated = true;
            }

            while (selectedHops.Count < plotters.Count)
            {
                RemoveLatencyPlotter(plotters.Count - 1);
                updated = true;
            }

            if (updated)
            {
                ResizePlotters();
                RenderHopTraces();
            }
        }

        private void RenderTrace(FormsPlot formsPlot1, ListView listViewTrace, Hop[] hops, Traceroute traceroute)
        {
            Hop[][] traces = traceroute.GetTraces();

            offsetBar.Visible = traces.Length > windowSize;
            offsetBar.Maximum = 0;
            offsetBar.Minimum = -(traces.Length - windowSize);

            // TODO: This needs to be filtered by window size
            Plotter.RenderTrace(formsPlot1, hops, traceroute, offset, windowSize);

            RenderHopTraces();

            listViewTrace.Items.Clear();
            listViewTrace.View = View.Details;

            foreach (var hop in hops)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = hop;
                item.Text = $"{hop.hop}";
                item.SubItems.Add(hop.ipAddress);
                item.SubItems.Add(hop.rtt < 0 ? "*" : $"{hop.rtt} ms");
                if (hop.ipAddress != null)
                {
                    MinMax minmax = traceroute.GetMinMax(hop.ipAddress);
                    if (minmax != null && minmax.min != -1)
                    {
                        item.SubItems.Add(minmax.min.ToString() + " ms");
                        item.SubItems.Add(minmax.max.ToString() + " ms");
                        item.SubItems.Add(((int)minmax.ave).ToString() + " ms");
                        item.SubItems.Add(((double)minmax.pl / traces.Length * 100).ToString("n0"));
                        var plot = item.SubItems.Add(selectedHops.Contains(hop.hop) ? "📊" : "");
                        plot.Tag = "graph";
                    }
                }
                listViewTrace.Items.Add(item);
            }
        }

        private void RenderHopTraces()
        {
            if (traceroute == null) return;

            Hop[][] traces = traceroute.GetTraces();
            Hop[] hops = traces.Last();

            if (traces.Length > windowSize)
            {
                int start = traces.Length - windowSize + offset;
                traces = traces.Skip(start).Take(windowSize).ToArray();
            }

            if (selectedHops.Count == 0)
            {
                // auto select the last responding hop
                int showHop = hops.Length;
                if (hops[showHop - 1].rtt == -1) showHop--;
                selectedHops.Add(showHop);
                GetLatencyPlotter(0);           // add single graph
            }

            UpdatePlotters();

            int i = 0;
            selectedHops.ForEach(x =>
            {
                string title = x <= hops.Length ? hops[x - 1].ipAddress : "";      // hop unreachable this trace
                FormsPlot plotter = (FormsPlot)this.graphs.Controls[i++];
                Plotter.RenderTraceOverTime(plotter, traces, x, $"Hop {x} {title} Latency (ms)", PING_FREQUENCY);
            });
        }

        private void testIPAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                mru.Add(testIPAddress.Text.Trim());
                StopTrace();
                StartTrace();
                e.Handled = true; // Prevent system beep
            }
        }

        private void listViewTrace_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                Hop hop = (Hop) e.Item.Tag;
                if (hop.rtt >= 0)
                {
                    if (hop.rtt < 50)
                    {
                        e.Graphics.FillRectangle(Brushes.LightGreen, e.Bounds);
                    }
                    else if (hop.rtt < 100)
                    {
                        e.Graphics.FillRectangle(Brushes.Orange, e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Pink, e.Bounds);
                    }
                }
            }
            TextFormatFlags align = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            ColumnHeader header = ((ListView)sender).Columns[e.ColumnIndex];
            if (header.TextAlign == System.Windows.Forms.HorizontalAlignment.Right) align = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, ((ListView) sender).Font, e.Bounds, Color.Black, align);
        }

        private void listViewTrace_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (Pen borderPen = new Pen(Color.FromArgb(100, Color.Silver), (float)0.5))
            {
                e.Graphics.DrawLine(borderPen, e.Bounds.Right-1, e.Bounds.Top, e.Bounds.Right-1, e.Bounds.Bottom);
            }

            ColumnHeader header = ((ListView)sender).Columns[e.ColumnIndex];
            Rectangle bounds = e.Bounds;
            TextFormatFlags align = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            if (header.TextAlign == System.Windows.Forms.HorizontalAlignment.Right)
            {
                align = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
            }
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, bounds, Color.Black, align);
        }

        private void buttonMRU_Click(object sender, EventArgs e)
        {
            mru.Show(buttonMRU, this);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mru.Save();
        }

        private void window_SelectedIndexChanged(object sender, EventArgs e)
        {
            int seconds = 900;
            switch (window.SelectedIndex)
            {
                case 0: seconds = 60; break;            // 60 Seconds
                case 1: seconds = 300; break;           // 5 Minutes
                case 2: seconds = 600; break;           // 10 Minutes
                case 3: seconds = 900; break;           // 15 Minutes
                case 4: seconds = 1800; break;          // 30 Minutes
                case 5: seconds = 3600; break;          // 1 hour
                case 6: seconds = 4 * 3600; break;        // 4 hour
                case 7: seconds = 8 * 3600; break;        // 8 hour
                case 8: seconds = 12 * 3600; break;        // 12 hour
                case 9: seconds = 24 * 3600; break;        // 24 hour
                case 10: seconds = 48 * 3600; break;       // 48 hour
            }
            windowSize = seconds / (PING_FREQUENCY / 1000);
            updateStatus();
            RenderHopTraces();
        }

        private void updateStatus()
        {
            status.Text = traceroute == null ? "" : String.Format("{0}/{1} of {2}", offset, windowSize, traceroute.GetTraces().Length);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            offset = offsetBar.Value;
            updateStatus();
            RenderHopTraces();
        }

        private void listViewTrace_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTest = listViewTrace.HitTest(e.Location);
            if (hitTest.SubItem != null && hitTest.SubItem.Tag != null && hitTest.SubItem.Tag.ToString() == "graph")
            {
                int hop = int.Parse(hitTest.Item.Text);

                if (hitTest.SubItem.Text == "📊")
                {
                    hitTest.SubItem.Text = "";
                    selectedHops.Remove(hop);
                    UpdatePlotters();
                    return;
                } 

                hitTest.SubItem.Text = "📊";
                selectedHops.Add(hop);
                selectedHops.Sort();
                UpdatePlotters();
            }
        }

        private void PlotPing_Resize(object sender, EventArgs e)
        {
            ResizePlotters();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IPAddress.Text.Length == 0) return;
            StartTrace();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopTrace();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLink.Help();

        }

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLink.Github();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Plot Ping Trace files (*.pp)|*.pp";
            openFileDialog.Title = "Select where to export";
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = false;

            DialogResult result = openFileDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Export export = new Export(traceroute);
                export.ExportTo(openFileDialog.FileName);
            }
        }
    }
}
