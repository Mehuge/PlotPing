using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotPingApp.Common
{
    class MinMaxTracker
    {
        private Dictionary<string, MinMax> minmaxes = new Dictionary<string, MinMax>();

        public MinMax Track(Hop hop, int samples)
        {
            if (hop.ipAddress == null) return null;

            MinMax mm;

            // add new entry
            if (!minmaxes.ContainsKey(hop.ipAddress))
            {
                minmaxes[hop.ipAddress] = mm = new MinMax();
                if (hop.rtt >= 0)
                {
                    mm.ave = mm.min = mm.max = hop.rtt;
                    mm.pl = 0;
                    return mm;
                }

                mm.ave = mm.min = mm.max = -1;
                mm.pl = 1;
                return mm;
            }

            // Update existing entry
            mm = minmaxes[hop.ipAddress];
            if (hop.rtt >= 0)
            {
                if (hop.rtt < mm.min || mm.min == -1) mm.min = hop.rtt;
                if (hop.rtt > mm.max || mm.max == -1) mm.max = hop.rtt;
                samples -= mm.pl;
                if (samples > 0)
                {
                    mm.ave = ((mm.ave * (samples - 1)) + hop.rtt) / samples;
                }
                return mm;
            }
            mm.pl++;
            return mm;
        }

        public MinMax Get(string ipAddress)
        {
            if (ipAddress != null && minmaxes.ContainsKey(ipAddress)) return minmaxes[ipAddress];
            return null;
        }

        public void Clear()
        {
            minmaxes.Clear();
        }
    }
}
