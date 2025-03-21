using ScottPlot.Ticks;
using ScottPlot;
using System;
using System.Drawing;
using System.Linq;
using PlotPingApp.Common;

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

        static internal void RenderTrace(FormsPlot plot, Hop[] hops, Traceroute traceroute, bool isCurrent, int windowSize, MinMaxTracker minmaxes)
        {
            plot.Plot.Clear();

            // Convert timestamps to seconds from start
            double[] _hops = hops.Select(t => (double)t.hop).ToArray();
            double[] _latencies = hops.Select(t => (double)t.rtt).ToArray();
            Tick[] ticks = hops.Select(t => new Tick(t.hop, t.hop.ToString(), true, false)).ToArray();

            double last = 0;
            MinMax [] minmax = hops.Select(t => minmaxes.Get(t.ipAddress)).ToArray();

            double[] lower = minmax.Select(x => x != null ? x.min : last).ToArray();
            double[] upper = minmax.Select(x => x != null ? x.max : last).ToArray();
            double[] average = minmax.Select(x => x != null ? x.ave : last).ToArray();

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
            plot.Plot.XLabel("Hop (timeout " + traceroute.Timeout + "ms)");
            plot.Plot.XAxis.SetTicks(ticks);
            plot.Plot.YLabel("Latency (ms)");
            plot.Plot.YAxis.SetTicks(latencyAxis);
            plot.Plot.YAxis.AxisTicks.MinorTickVisible = false;
            plot.Plot.SetAxisLimits(0.8, hops.Length + 0.2, 0, latencyMax);
            plot.Plot.Title(isCurrent ? "Current Traceroute" : "Traceroute @ " + hops[0].timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            plot.Plot.Legend();


            /// plot min max latency as shaded region
            var poly = plot.Plot.AddFill(xs, lower, upper);
            poly.FillColor = Color.FromArgb(50, Color.Green);
            plot.Refresh();
        }
        static internal void RenderTraceOverTime(FormsPlot plot, Hop[][] hops, int hop, string title, int freq)
        {
            int i = hop - 1;    // i is the hop being graphed

            // Select all non-dropout hops, their latencies and time values
            Hop[][] noDropouts = hops.Where(x => i < x.Length && x[i].rtt >= 0).ToArray();
            double[] hopLatencies = noDropouts.Select(x => (double)x[i].rtt).ToArray();
            double[] timeValues = noDropouts.Select(x => x[i].timestamp.ToOADate()).ToArray();

            double latencyMax = hopLatencies.Length > 0 ? ((int)((hopLatencies.Max() / 30) + 1)) * 30 : 30;

            // select all dropout hops, latencyMax and time values
            Hop[][] dropouts = hops.Where(x => i < x.Length && x[i].rtt == -1).ToArray();
            double[] dropoutBars = dropouts.Select(x => latencyMax).ToArray();
            double[] dropoutTimes = dropouts.Select(x => x[i].timestamp.ToOADate()).ToArray();

            plot.Plot.Clear();
            plot.Plot.YAxis.SetInnerBoundary(0, latencyMax);
            plot.Plot.YAxis.SetBoundary(0, latencyMax);
            plot.Plot.XAxis.DateTimeFormat(true);
            plot.Plot.XAxis.TickDensity(1);
            plot.Plot.XAxis.TickMarkColor(Color.DarkRed);
            plot.Plot.XAxis.Ticks(true, true, true);
            plot.Plot.XAxis.TickLabelFormat(x => DateTime.FromOADate(x).ToString("HH:mm:ss"));

            if (hopLatencies.Length > 0)
            {
                plot.Plot.AddScatter(timeValues, hopLatencies, markerSize: 2);
            }

            if (dropoutBars.Length > 0)
            {
                var bar = plot.Plot.AddBar(dropoutBars, dropoutTimes);
                bar.BarWidth = (1.0 / 86400) * (freq / 1000);
                bar.FillColor = Color.Red;
                bar.BorderLineWidth = 0;
            }

            plot.Plot.Legend();
            plot.Plot.Title(title);
            plot.Refresh();

        }

    }
}
