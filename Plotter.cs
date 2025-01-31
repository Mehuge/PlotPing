using ScottPlot.Ticks;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace PlotPingApp
{
    internal class Plotter
    {
        private static Color errorBarColor = Color.FromArgb(50, Color.Red);
        static private void LatencyBar(FormsPlot plot, int i, Hop hop, int latencyMax)
        {
            if (hop.rtt < 0)
            {
                var bar = plot.Plot.AddBar(++i, latencyMax, 0, errorBarColor, 0.5);
                bar.BorderLineWidth = 0;
            }
            else
            {
                // plot.Plot.AddBar(++i, hop.rtt, 0, Color.Blue, 0.5);
                plot.Plot.AddPoint(++i, hop.rtt, Color.Blue, (float)10, MarkerShape.eks);
            }
        }

        static internal void RenderTrace(FormsPlot plot, Hop[] hops, Traceroute traceroute)
        {
            plot.Plot.Clear();

            // Convert timestamps to seconds from start
            double[] _hops = hops.Select(t => (double)t.hop).ToArray();
            double[] _latencies = hops.Select(t => (double)t.rtt).ToArray();
            Tick[] ticks = hops.Select(t => new Tick(t.hop, t.hop.ToString(), true, false)).ToArray();


            double last = 0;
            double[] lower = hops.Select(t => {
                MinMax mm = traceroute.GetMinMax(t.ipAddress);
                if (mm != null) {
                    last = mm.min;
                    return (double) mm.min;
                }
                return last;
            }).ToArray();

            double[] upper = hops.Select(t => {
                MinMax mm = traceroute.GetMinMax(t.ipAddress);
                if (mm != null) {
                    last = mm.max;
                    return (double) mm.max;
                }
                return last;
            }).ToArray();

            // TODO exclude missing hops from average line (means having sparse hops/average arrays)
            double[] average = hops.Select(t => {
                MinMax mm = traceroute.GetMinMax(t.ipAddress);
                if (mm != null) {
                    last = mm.ave;
                    return mm.ave;
                }
                return last;
            }).ToArray();

            long maxLatency = upper.Select(t => (long) t).Max();
            int latencyMax = (((int)(maxLatency / 30)) + 1) * 30;

            int tickInterval = (latencyMax / 5);
            Tick[] latencyAxis = Enumerable.Range(1, latencyMax).Select(t => new Tick(t, t.ToString(), t % tickInterval == 0, false)).ToArray();

            int i = 0;
            double[] xs = new double[hops.Length];
            foreach (var hop in hops)
            {
                xs[i] = i + 1;
                LatencyBar(plot, i++, hop, latencyMax);
            }

            // plot.Plot.AddScatter(_hops, _latencies, label: "Latency", markerSize: (float)0.1);
            plot.Plot.AddScatter(_hops, average, markerSize: (float)8, color: Color.OrangeRed, markerShape: MarkerShape.openCircle);
            plot.Plot.XLabel("Hop");
            plot.Plot.XAxis.SetTicks(ticks); plot.Plot.YLabel("Latency (ms)");
            plot.Plot.YAxis.SetTicks(latencyAxis);
            plot.Plot.YAxis.AxisTicks.MinorTickVisible = false;
            Debug.Print($"X Axis {1} to {hops.Length} Y Axis {0} to {latencyMax}");
            plot.Plot.SetAxisLimits(0.8, hops.Length > 2 ? hops.Length + 0.2: 2, 0, latencyMax);
            plot.Plot.Title("Traceroute Results");
            plot.Plot.Legend();


            /// plot min max latency as shaded region
            var poly = plot.Plot.AddFill(xs, lower, upper);
            poly.FillColor = Color.FromArgb(50, Color.Green);
            plot.Refresh();
        }
        static int LIMIT = 60*15;
        static internal void RenderTraceOverTime(FormsPlot plot, Hop[][] hops, int hop, string title, int freq)
        {
            Hop[][] filtered = hops.Length > LIMIT ? hops.Skip(hops.Length - LIMIT).ToArray() : hops;

            int i = hop - 1;
            double[] hopLatencies = filtered.Select(x => (double) (i >= x.Length ? -1 : x[i].rtt)).ToArray();
            double[] timeValues = filtered.Select(x => i >= x.Length ? x[x.Length-1].timestamp.ToOADate() : x[i].timestamp.ToOADate()).ToArray();

            long maxLatency = (long) hopLatencies.Max();
            int latencyMax = (((int)(maxLatency / 30)) + 1) * 30;

            double[] dropouts = filtered.Select(x => i >= x.Length || x[i].rtt < 0 ? (double) latencyMax : 0).ToArray();

            plot.Plot.Clear();
            plot.Plot.YAxis.SetInnerBoundary(0, latencyMax);
            plot.Plot.YAxis.SetBoundary(0, latencyMax);
            plot.Plot.XAxis.DateTimeFormat(true);
            plot.Plot.XAxis.TickLabelFormat(x => DateTime.FromOADate(x).ToString("HH:mm:ss"));
            plot.Plot.AddScatter(timeValues, hopLatencies, markerSize: 2);

            var bar = plot.Plot.AddBar(dropouts, timeValues);
            bar.BarWidth = (1.0 / 86400) * (freq / 1000);
            bar.FillColor = Color.Red;
            bar.BorderLineWidth = 0;

            plot.Plot.Legend();
            plot.Plot.Title(title);
            plot.Refresh();

        }

    }
}
