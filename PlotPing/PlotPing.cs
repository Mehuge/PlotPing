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
using PlotPingApp.Common;

namespace PlotPingApp
{
    public partial class PlotPing : Form
    {
        private MRU mru;
        private TraceEngine traceroute = null;
        private LogWriter logWriter = null;
        private int PING_FREQUENCY = 5000;
        private int windowSize = 900 / 5;
        private int offset = 0;
        private int selectedSample = -1;          // -1 means track last trace

        public PlotPing()
        {
            InitializeComponent();
            liveView.Margin = new Padding(0);
            liveView.Plot.XAxis.Color(Color.Gray);
            liveView.Plot.XAxis.Layout(0, 0, 40);
            liveView.Plot.YAxis.Color(Color.Gray);
            liveView.Plot.YAxis.Layout(0, 0, 40);
            liveView.Plot.Title(null, false, Color.Gray, 10);
            liveView.Invalidate();

            Settings.LoadSettings();
            Settings.RestoreFormLocation(this);

            // multiPingToolStripMenuItem1.Visible = Debugger.IsAttached;

            // Load MRU
            mru = new MRU(Path.Combine(Path.GetDirectoryName(Application.UserAppDataPath), "mru.txt"), 25);

            window.SelectedIndex = 4;
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            bool start = buttonStartStop.Text == "Start";
            StopTrace();
            if (start) StartTrace();
        }

        public void Start(string ipAddress)
        {
            testIPAddress.Text = ipAddress.Trim();
            StartTrace();
        }

        private void StartTrace()
        {
            StopTrace();                // stop currently running trace

            testIPAddress.BackColor = SystemColors.Window;
            string ipAddress = this.testIPAddress.Text.Trim();
            if (ipAddress.Length == 0) return;

            if (traceroute?.GetHostAddress() == ipAddress)
            {
                // Resume tracing
                buttonStartStop.Text = "Stop";
                traceroute.Start();
                return;
            }

            try
            {
                if (traceroute != null)
                {
                    // change of IP
                    traceroute.Clear();
                    traceroute.SetHostAddress(ipAddress);
                    selectedHops.Clear();
                    listViewTrace.Items.Clear();
                    liveView.Plot.Clear();
                    while (plotters.Count > 0) RemoveLatencyPlotter(0);
                }
                else
                {
                    // Complete new trace
                    traceroute = new TraceEngine(ipAddress);
                    traceroute.SetTimeout(Settings.ICMPTimeout);
                    traceroute.OnProbe += Traceroute_OnProbe;
                    traceroute.OnTrace += Traceroute_OnTrace;
                }
            }
            catch (ArgumentException)
            {
                testIPAddress.BackColor = Color.Red;
                return;
            }

            // starting new trace (either changed IP or new IP)
            buttonStartStop.Text = "Stop";
            mru.Add(ipAddress);
            mru.Save();

            // if logging is required, start logging
            if (logTrace.Checked)
            {
                StartLogging(traceroute.GetHostAddress());
            }

            // New IP, send probe first
            traceroute.SendProbe();
            traceStatus.Text = "Probing...";
            traceStatus.Visible = true;
        }

        private void Traceroute_OnProbe(object sender, Hop[] hops)
        {
            if (hops == null)
            {
                traceStatus.Text = "No route to host";
                testIPAddress.BackColor = Color.Red;
                return;
            }
            StartTracing();
        }

        private void StartTracing()
        {
            traceStatus.Text = "Pinging...";
            traceStatus.Visible = true;
            traceroute.Start();
        }

        private void StartLogging(string host)
        {
            if (logWriter != null) logWriter.Close();

            string logDir = Settings.LogOutputFolder;
            if (logDir == "") return;           // no log dir, do nothing

            string logFile = $"{logDir}\\{host}.pp";
            logWriter = new LogWriter(logFile, traceroute.GetMinMaxTracker());
        }

        private void StopTrace()
        {
            traceroute?.Stop();
            buttonStartStop.Text = "Start";
            offsetBar.Visible = false;
        }

        private void Traceroute_OnTrace(object sender, Hop[] hops)
        {
            this.BeginInvoke(new System.Action(() =>
            {
                traceStatus.Visible = false;
                UpdateStatus();
                RenderTrace();
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
                RenderHopTraces(GetTracesForWindow(offset));
            }
        }

        private Hop[][] GetTracesForWindow(int offset)
        {
            if (traceroute == null) return null;

            Hop[][] traces = traceroute.GetTraces();
            if (traces.Length > windowSize)
            {
                int start = traces.Length - windowSize + offset - 1;
                traces = traces.Skip(start).Take(windowSize).ToArray();
            }
            return traces;
        }

        private MinMaxTracker GetMinMaxes(Hop[][] traces)
        {
            MinMaxTracker minmaxes = new MinMaxTracker();
            for (int i = 0; i < traces.Length; i++)
            {
                foreach (Hop hop in traces[i])
                {
                    minmaxes.Track(hop, i);
                }
            }
            return minmaxes;
        }

        private void ShowHideWindowSlider()
        {
            if (traceroute == null)
            {
                offsetBar.Visible = false;
                return;
            }
            int traceCount = traceroute.GetTraces().Length;
            offsetBar.Visible = traceCount > windowSize;
            offsetBar.Maximum = 0;
            offsetBar.Minimum = -(traceCount - windowSize);
        }

        private void RenderTrace()
        {
            ShowHideWindowSlider();

            if (traceroute == null) return;

            Hop[][] all = traceroute.GetTraces();
            Hop[] lastSample = all.Last();

            // Sample window is defined by the current window offset
            int sampleOffset = Settings.ActiveTracksWindow ? offset : 0;
            if (selectedSample > -1)
            {
                // but if a sample has been selected, the sample window should end at this sample
                sampleOffset = -(all.Length - selectedSample - 2);
            }

            // Active trace always uses 0 offset window for minmax calc unless ActiveTracksWindow option is set
            Hop[][] traces = GetTracesForWindow(sampleOffset);
            MinMaxTracker minmaxes = GetMinMaxes(traces);

            // Show the current sample if activeSample is -1, othewise show the active sample
            Hop[] showSample = selectedSample == -1 ? lastSample : all[selectedSample];

            // If logging, write this hop to the log. The log uses the trace route tracked min/max info
            // which covers all samples. It always writes the last sample available.
            if (logWriter != null) logWriter.WriteSample(all.Length, lastSample, traceroute);

            // Render the current traceroute results
            Plotter.RenderTrace(liveView, showSample, traceroute, showSample == lastSample, windowSize, minmaxes);

            // Render the hop graph(s)
            RenderHopTraces(traces);

            // Show the current trace in list view
            ShowTraceroute(showSample, traces.Length, minmaxes);
        }

        private void ShowTraceroute(Hop[] hops, int samples, MinMaxTracker minmaxes)
        {
            // Show current traceroute in list view
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
                    MinMax minmax = minmaxes.Get(hop.ipAddress);
                    if (minmax != null && minmax.min != -1)
                    {
                        item.SubItems.Add(minmax.min.ToString() + " ms");
                        item.SubItems.Add(minmax.max.ToString() + " ms");
                        item.SubItems.Add(((int)minmax.ave).ToString() + " ms");
                        item.SubItems.Add(((double)minmax.pl / samples * 100).ToString("n0"));
                        var plot = item.SubItems.Add(selectedHops.Contains(hop.hop) ? "📊" : "");
                        plot.Tag = "graph";
                    }
                }
                listViewTrace.Items.Add(item);
            }
        }

        private void RenderHopTraces(Hop[][] traces)
        {
            if (traceroute == null) return;

            Hop[] hops = traces.Last();

            if (selectedHops.Count == 0 && hops.Length > 0)
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
            mru.Show(buttonMRU, this, (string ipAddress) => { this.Start(ipAddress); });
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
            Redraw();
        }

        private void UpdateStatus()
        {
            status.Text = "";
            if (traceroute == null) return;
            status.Text = String.Format("{0}/{1} of {2}", offset, windowSize, traceroute.GetTraces().Length)
                            + (selectedSample > -1 ? String.Format(" show {0}", selectedSample) : "");
        }

        private void Redraw()
        {
            UpdateStatus();
            if (Settings.ActiveTracksWindow)
            {
                RenderTrace();
                return;
            }
            // active trace is not tracking the selected window, only the hop graphs are
            // so only render those when changing window.
            RenderHopTraces(GetTracesForWindow(offset));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            offset = offsetBar.Value;
            UpdateStatus();
            selectedSample = offset == 0 ? -1 : (traceroute.GetTraces().Length - 1) + offset;
            Redraw();
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
            Utils.ShowHelp();

        }

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.OpenGitHub();
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.ShowSettings(settingsClosed);
        }

        private void settingsClosed(object sender, FormClosedEventArgs e)
        {
            if (traceroute != null) traceroute.SetTimeout(Settings.ICMPTimeout);
        }

        private void logTrace_CheckedChanged(object sender, EventArgs e)
        {
            if (logTrace.Checked)
            {
                if (logWriter == null) StartLogging(traceroute.GetHostAddress());
                return;
            }

            if (logWriter != null)
            {
                logWriter.Close();
                logWriter = null;
            }
        }

        private void multiPingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utils.StartMultiPing();
        }

        private void plotPingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utils.StartPlotPing();
        }
    }
}
