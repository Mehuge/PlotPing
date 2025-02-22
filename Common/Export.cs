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
        private Traceroute traceroute = null;
        private static MinMaxTracker minmax = null;

        public Export(Traceroute traceroute)
        {
            this.traceroute = traceroute;
        }
        public void ExportTo(string exportTo)
        {
            using (StreamWriter export = new StreamWriter(File.Open(exportTo, FileMode.Create)))
            {
                export.WriteLine($"TRACE {traceroute.GetHostAddress()}");
                int sequence = 0;
                StartExport();
                foreach (Hop[] hops in traceroute.GetTraces())
                {
                    ExportSample(export, sequence++, hops, traceroute);
                }
            }
        }

        public static void StartExport()
        {
            minmax = new MinMaxTracker();
        }

        public static string[] ExportSample(int sequence, Hop[] hops, Traceroute traceroute)
        {
            List<string> result = new List<string>();
            result.Add(sequence.ToString("D5") + " " + hops[0].timestamp.ToString("yyyy-MM-dd HH:mm:ss zzz"));
            result.Add("  HOP RTT MIN MAX AVE PL% IP");
            foreach (var hop in hops)
            {
                MinMax mm = minmax.Track(hop, sequence);
                result.Add(String.Format(
                    "  {0} {2} {3} {4} {5} {6} {1}",
                        hop.hop.ToString("D3"),
                        hop.ipAddress ?? "Request Timed Out",
                        hop.rtt < 0 ? " * " : hop.rtt.ToString("D3"),
                        mm == null ? " * " : mm.min.ToString("D3"),
                        mm == null ? " * " : mm.max.ToString("D3"),
                        mm == null ? " * " : ((int)(mm.ave)).ToString("D3"),
                        mm == null ? " * " : mm.pl.ToString("D3")
                ));
            }
            return result.ToArray();
        }

        public static void ExportSample(StreamWriter export, int sequence, Hop[] hops, Traceroute traceroute)
        {
            foreach (var item in ExportSample(sequence, hops, traceroute))
            {
                export.WriteLine(item);
            }
        }
    }
}
