using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace PlotPingApp
{
    internal class Hop
    {
        internal int hop;                       // Hop (base 1)
        internal DateTime timestamp;            // Timestamp
        internal long rtt;                      // Round Trip Time (ms)
        internal string ipAddress;              // IP address responding to this hop
    }

    public class MinMax
    {
        internal long min;
        internal long max;
        internal double ave;
        internal long pl;
    }

    internal class Traceroute
    {
        private Ping pingSender = new Ping();
        private PingOptions options = new PingOptions() { DontFragment = true };
        private byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        private int timeout = 120;
        private int maxTTL = 30;
        private string hostAddress;
        private string ipAddress;

        // private Timer timer;
        private System.Threading.Timer backgroundTimer;

        private List<Hop[]> traces = new List<Hop[]>();
        private Dictionary<string, MinMax> minmax = new Dictionary<string, MinMax>();
        private bool running;

        public delegate void TraceEventHandler(object sender, Hop[] hops);
        public event TraceEventHandler OnTrace;


        public MinMax GetMinMax(string ip)
        {
            if (ip != null && minmax.ContainsKey(ip)) return minmax[ip];
            return null;
        }

        public Hop[][] GetTraces()
        {
            return traces.ToArray();
        }

        public Hop[] GetLastTrace()
        {
            return traces.Last();
        }

        public string ResolveToIPAddress(string hostOrIp)
        {
            IPAddress parsedIP;
            if (IPAddress.TryParse(hostOrIp, out parsedIP)) return parsedIP.ToString();
            try {
                IPHostEntry hostEntry = Dns.GetHostEntry(hostOrIp);
                if (hostEntry.AddressList.Length > 0) {
                    var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                    if (ipv4Address != null) {
                        return ipv4Address.ToString();
                    }
                }
                throw new ArgumentException("Could not resolve host to IP Address");
            }
            catch (Exception) {
                throw new ArgumentException("Invalid host name or IP address");
            }
        }

        public Traceroute(string hostOrIp) {

            SetHostAddress(hostOrIp);
        }

        public void StartBackground(int interval = 5000)
        {
            if (backgroundTimer != null) StopBackground();

            backgroundTimer = new System.Threading.Timer(
                callback: (state) => {
                    // If last trace is still running, skip this trace. 
                    //
                    // if don't do this get an exception from a system DLL,
                    // perhaps because this is being run on a separate thread
                    // and we end up trying to run two pings at the same time?
                    //
                    // When we were not threading, if one trace took longer than
                    // a second, the trace was delayed anyway.
                    //
                    // This does mean the data doesn't include a trace for every
                    // second, not sure if that will cause problems for the
                    // graphing.
                    //
                    if (this.running) return;               
                    this.running = true;
                    Hop[] trace = Run();
                    traces.Add(trace);
                    this.running = false;
                    OnTrace?.Invoke(this, trace);
                },
                state: null,
                dueTime: 0,
                period: interval
            );
        }

        public void StopBackground()
        {
            if (backgroundTimer == null) return;
            backgroundTimer.Dispose();
            backgroundTimer = null;
        }

        public void Clear()
        {
            // Stop();
            StopBackground();
            traces.Clear();
            minmax.Clear();
            maxTTL = 30;
        }

        private IPAddress GetIP(string ipAddress)
        {
            byte[] addr = new byte[4];
            string[] parts = ipAddress.Split('.');
            for (int i = 0; i < 4; i++)
                addr[i] = byte.Parse(parts[i]);
            return new IPAddress(addr);
        }

        private void CalcMinMax(Hop hop, long samples)
        {
            if (hop.ipAddress == null)
            {
                return;
            }
            
            MinMax mm;

            // add new entry
            if (!minmax.ContainsKey(hop.ipAddress))
            {
                minmax[hop.ipAddress] = mm = new MinMax();
                if (hop.rtt >= 0)
                {
                    mm.ave = mm.min = mm.max = hop.rtt;
                    mm.pl = 0;
                    return;
                }

                mm.ave = mm.min = mm.max = -1;
                mm.pl = 1;
                return;
            }

            // Update existing entry
            mm = minmax[hop.ipAddress];
            if (hop.rtt >= 0)
            {
                if (hop.rtt < mm.min || mm.min == -1) mm.min = hop.rtt;
                if (hop.rtt > mm.max || mm.max == -1) mm.max = hop.rtt;
                samples -= mm.pl;
                if (samples > 0)
                {
                    mm.ave = ((mm.ave * (samples - 1)) + hop.rtt) / samples;
                }
                return;
            }
            mm.pl++;
        }

        private Hop[] Run() {

            List<Hop> hopList = new List<Hop>();
            string lastIp = null;
            int lastSuccess = 0;
            DateTime now = DateTime.Now;

            Debug.Print("TRACE IP {0}", this.ipAddress + " AT " + now.ToString("HH:mm:ss") + " SEQ " + traces.Count);

            for (int hop = 1; hop <= maxTTL; hop++)
            {
                this.options.Ttl = hop;
                Stopwatch sw = new Stopwatch();
                Hop hopData = new Hop();
                hopData.hop = hop;
                hopData.timestamp = DateTime.Now;
                PingReply reply;

                sw.Start();
                try
                {
                    reply = pingSender.Send(ipAddress, timeout, buffer, options);
                    sw.Stop();

                    if (reply.Status == IPStatus.Success && reply.Address != null && reply.Address.ToString() == lastIp)
                    {
                        // reached as far as we can trace
                        break;
                    }

                    hopData.rtt = 
                          reply.Status == IPStatus.Success ? reply.RoundtripTime 
                        : reply.Status == IPStatus.TtlExpired ? sw.ElapsedMilliseconds
                        : -1;
                    hopData.ipAddress = reply.Address?.ToString() ?? (traces.Count > 0 ? traces.Last()[hop - 1].ipAddress : null);
                    lastIp = reply.Address?.ToString();
                    hopList.Add(hopData);
                }
                catch(Exception e)
                {
                    sw.Stop();
                    hopData.rtt = -2;
                    hopData.ipAddress = null;
                    hopList.Add(hopData);
                    Debug.Print("EXCEPTION " + e.Message);
                    break;
                }

                CalcMinMax(hopData, traces.Count + 1);

                Debug.Print("  HOP {0} IP {1} TTL {2} RTT {3}ms MIN {4} MAX {5} AVE {6} PL {7}",
                    hop,
                    hopData.ipAddress ?? "Request Timed Out",
                    this.options.Ttl,
                    hopData.rtt < 0 ? "*" : hopData.rtt.ToString("D"),
                    hopData.ipAddress == null ? "" : minmax[hopData.ipAddress].min.ToString(),
                    hopData.ipAddress == null ? "" : minmax[hopData.ipAddress].max.ToString(),
                    hopData.ipAddress == null ? "" : ((int)(minmax[hopData.ipAddress].ave)).ToString(),
                    hopData.ipAddress == null ? "" : minmax[hopData.ipAddress].pl.ToString()
                );

                if (hopData.rtt >= 0) lastSuccess = hop;

                if (hopData.ipAddress == this.ipAddress)
                    break;          // Finished tracing
            }

            if (lastSuccess < hopList.Count - 1)
            {
                // hopList.RemoveRange(lastSuccess + 1, hopList.Count - lastSuccess - 1);
                // maxTTL = lastSuccess + 1;
            }
            else if (lastSuccess == hopList.Count && maxTTL < 30)
            {
                maxTTL = lastSuccess + 1;
            }

            return hopList.ToArray();
        }

        internal bool IsRunning()
        {
            // return timer != null;
            return backgroundTimer != null;
        }

        internal void SetHostAddress(string hostOrIp)
        {
            this.hostAddress = hostOrIp;
            this.ipAddress = ResolveToIPAddress(hostOrIp);
            Debug.Print(hostOrIp + " is " + this.ipAddress);
        }

        public string GetHostAddress() {
            return hostAddress;
        }

        public string GetIPAddress()
        {
            return ipAddress;
        }
    }
}
