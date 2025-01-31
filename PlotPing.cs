using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using System.Diagnostics;
using ListView = System.Windows.Forms.ListView;

namespace PlotPingApp
{
    public partial class PlotPing : Form
    {
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
        }

        private Traceroute traceroute = null;
        private int PING_FREQUENCY = 5000;

        private void buttonGoNew_Click(object sender, EventArgs e)
        {
            ToggleTrace();
        }

        private void ToggleTrace() {
            string ipAddress = this.testIPAddress.Text.Trim();
            if (buttonGoNew.Text == "Start")
            {
                StartTrace(ipAddress);
                buttonGoNew.Text = "Stop";
                return;
            }

            StopTrace();
            buttonGoNew.Text = "Start";
        }

        private void StartTrace(string ipAddress)
        {
            if (traceroute == null)
            {
                traceroute = new Traceroute(ipAddress);
            }
            else if (traceroute.GetIPAddress() != ipAddress)
            {
                traceroute.Clear();
                traceroute.SetIPAddress(ipAddress);
            }
            
            traceroute.OnTrace += Traceroute_OnTrace;

            // traceroute.Start();
            Task.Run(() => { traceroute.StartBackground(PING_FREQUENCY); });
        }

        private void StopTrace()
        {
            //traceroute?.Stop();
            traceroute?.StopBackground();
            selectedHops.Clear();
        }

        private void Traceroute_OnTrace(object sender, Hop[] hops)
        {
            this.BeginInvoke(new Action(() =>
            {
                RenderTrace(formsPlot1, listViewTrace, hops, traceroute);
            }));
        }

        private static List<int> selectedHops = new List<int>();
        private static List<FormsPlot> plotters = new List<FormsPlot>();

        private FormsPlot GetLatencyPlotter(int i)
        {
            var plotter = new FormsPlot();
            int h = this.graphs.Size.Height / selectedHops.Count;
            int y = (i * h);
            plotter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plotter.Location = new Point(0, y);
            plotter.Margin = new Padding(0, 0, 0, 0);
            plotter.Name = "latencyPlot" + i;
            plotter.Size = new Size(this.graphs.Size.Width, h);
            plotter.TabIndex = 6 + i;
            plotter.Plot.Margins(0, 0);
            plotter.Plot.XAxis.Color(Color.Gray);
            plotter.Plot.XAxis.Layout(0, 0, 20);
            plotter.Plot.YAxis.Color(Color.Gray);
            plotter.Plot.YAxis.Layout(0, 0, 40);
            plotter.BackColor = Color.FromArgb(10, Color.Black);
            plotter.Plot.Title("", false, Color.Gray, 10);
            plotters.Add(plotter);
            this.graphs.Controls.Add(plotter);
            return plotter;
        }

        private void RenderTrace(FormsPlot formsPlot1, ListView listViewTrace, Hop[] hops, Traceroute traceroute)
        {
            Plotter.RenderTrace(formsPlot1, hops, traceroute);
            Hop[][] traces = traceroute.GetTraces();

            if (selectedHops.Count == 0)
            {
                // auto select the last responding hop
                int showHop = hops.Length;
                if (hops[showHop - 1].rtt == -1) showHop--;
                selectedHops.Add(showHop-1);
                selectedHops.Add(showHop);
                GetLatencyPlotter(0);           // add single graph
                GetLatencyPlotter(1);           // add single graph
            }

            int i = 0;
            selectedHops.ForEach(x =>
            {
                string title = x <= hops.Length ? hops[x-1].ipAddress : "";      // hop unreachable this trace
                FormsPlot plotter = (FormsPlot)this.graphs.Controls[i++];
                Plotter.RenderTraceOverTime(plotter, traces, x, title + " Latency (ms)", PING_FREQUENCY);
            });

            listViewTrace.Items.Clear();
            listViewTrace.View = View.Details;

            if (hops.Length > 0) Debug.Print("TRACE STARTED " + hops[0].timestamp.ToString("HH:mm:ss"));

            foreach (var hop in hops)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = hop;
                item.Text = $"{hop.hop}";
                item.SubItems.Add(hop.ipAddress);
                if (hop.rtt >= 0) item.SubItems.Add($"{hop.rtt} ms");
                if (hop.ipAddress != null)
                {
                    MinMax minmax = traceroute.GetMinMax(hop.ipAddress);
                    if (minmax != null)
                    {
                        item.SubItems.Add(minmax.min.ToString() + " ms");
                        item.SubItems.Add(minmax.max.ToString() + " ms");
                        item.SubItems.Add(((int)minmax.ave).ToString() + " ms");
                    }
                }
                listViewTrace.Items.Add(item);
            }
        }

        private void testIPAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return) ToggleTrace();
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
            /*
            if (e.ColumnIndex == 0)
            {
                // Color the first column header
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }
            else if (e.ColumnIndex > 0)
            {
                e.Graphics.FillRectangle(Brushes.AntiqueWhite, e.Bounds);
            }
            */

            using (Pen borderPen = new Pen(Color.FromArgb(100, Color.Silver), (float)0.5))
            {
                e.Graphics.DrawLine(borderPen, e.Bounds.Right-1, e.Bounds.Top, e.Bounds.Right-1, e.Bounds.Bottom);
            }

            ColumnHeader header = ((ListView)sender).Columns[e.ColumnIndex];
            Rectangle bounds = e.Bounds;
            TextFormatFlags align = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            if (header.TextAlign == System.Windows.Forms.HorizontalAlignment.Right)
            {
                // int textWidth = TextRenderer.MeasureText(e.Header.Text, e.Font).Width;
                // int x = e.Bounds.Right - textWidth;
                // bounds = new Rectangle(x, bounds.Top, textWidth, bounds.Height);
                align = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
            }
            if (header.TextAlign == System.Windows.Forms.HorizontalAlignment.Right)
            {
                // int x = e.Bounds.Left;
                // bounds = new Rectangle(x, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
            }
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, bounds, Color.Black, align);
        }

        private void mruTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
