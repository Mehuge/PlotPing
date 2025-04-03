using PlotPingApp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotPingApp
{
    internal class Export
    {
        private TraceEngine traceroute = null;
        private MinMaxTracker minmax = null;
        private bool track = false;

        public Export(TraceEngine traceroute, MinMaxTracker minmax = null)
        {
            this.traceroute = traceroute;
            this.minmax = minmax == null ? new MinMaxTracker() : minmax;
            this.track = minmax == null;            // if started a new tracker, we need to track the samples, else just reference provided minmax tracker
        }
        public void ExportTo(string exportTo)
        {
            using (StreamWriter export = new StreamWriter(File.Open(exportTo, FileMode.Create)))
            {
                export.WriteLine($"TRACE {traceroute.GetHostAddress()}");
                int sequence = 0;
                foreach (Hop[] hops in traceroute.GetTraces())
                {
                    if (hops.Length > 0) ExportSample(export, sequence++, hops, traceroute);
                }
            }
        }

        public string[] ExportSample(int sequence, Hop[] hops, TraceEngine traceroute)
        {
            List<string> result = new List<string>();
            result.Add(sequence.ToString("D5") + " " + hops[0].timestamp.ToString("yyyy-MM-dd HH:mm:ss zzz"));
            result.Add("  HOP RTT MIN MAX AVE PL% IP");
            foreach (var hop in hops)
            {
                MinMax mm = track ? minmax.Track(hop, sequence) : minmax.Get(hop.ipAddress);
                result.Add(String.Format(
                    "  {0} {2} {3} {4} {5} {6} {1}",
                        hop.hop.ToString("D3"),
                        hop.ipAddress ?? "Request Timed Out",
                        hop.rtt < 0 ? " * " : hop.rtt.ToString("D3"),
                        mm == null ? " * " : mm.min.ToString("D3"),
                        mm == null ? " * " : mm.max.ToString("D3"),
                        mm == null ? " * " : ((int)(mm.ave)).ToString("D3"),
                        mm == null ? " * " : (mm.pl * 100 / (sequence + 1)).ToString("D3")
                ));
            }
            return result.ToArray();
        }

        public void ExportSample(StreamWriter export, int sequence, Hop[] hops, TraceEngine traceroute)
        {
            foreach (var item in ExportSample(sequence, hops, traceroute))
            {
                export.WriteLine(item);
            }
        }
    }
}
