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
        Traceroute traceroute = null;
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
                foreach (Hop[] hops in traceroute.GetTraces())
                {
                    ExportSample(export, sequence++, hops, traceroute);
                }
            }
        }

        public static string[] ExportSample(int sequence, Hop[] hops, Traceroute traceroute)
        {
            List<string> result = new List<string>();
            result.Add(sequence.ToString("D5") + " " + hops[0].timestamp.ToString("yyyy-MM-dd HH:mm:ss zzz"));
            result.Add("  HOP RTT MIN MAX AVE PL% IP");
            foreach (var hop in hops)
            {
                MinMax minmax = traceroute.GetMinMax(hop.ipAddress);
                result.Add(String.Format(
                    "  {0} {2} {3} {4} {5} {6} {1}",
                        hop.hop.ToString("D3"),
                        hop.ipAddress ?? "Request Timed Out",
                        hop.rtt < 0           ? " * " : hop.rtt.ToString("D3"),
                        hop.ipAddress == null ? " * " : minmax.min.ToString("D3"),
                        hop.ipAddress == null ? " * " : minmax.max.ToString("D3"),
                        hop.ipAddress == null ? " * " : ((int)(minmax.ave)).ToString("D3"),
                        hop.ipAddress == null ? "100" : minmax.pl.ToString("D3")
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
