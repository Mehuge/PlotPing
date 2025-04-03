using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using PlotPingApp.Common;

namespace PlotPingApp
{
    internal class Hop
    {
        internal int hop;                       // Hop (base 1)
        internal DateTime timestamp;            // Timestamp
        internal long rtt;                      // Round Trip Time (ms)
        internal string ipAddress;              // IP address responding to this hop
        internal int timeout;                   // Timeout used for this hop
    }

    public class MinMax
    {
        internal long min;
        internal long max;
        internal double ave;
        internal int pl;
    }

    internal class Trace
    {
        internal string ipAddress;
        internal int sequence;
        internal List<Hop> hops;
    }

    internal class AsyncPing
    {
        internal int traceId;
        internal Stopwatch sw;
        internal Hop hop;
        internal Action<AsyncPing> callback;
    }

    class TraceEngine
    {
        // Need a list of completed traces
        private byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        private int timeout = 3000;
        private int maxTTL = 30;
        private string hostAddress;
        private string ipAddress;
        private int PING_FREQUENCY = 5000;
        private System.Threading.Timer backgroundTimer;
        private int activeTraceId = 0;

        public delegate void TraceEventHandler(object sender, Hop[] hops);
        public event TraceEventHandler OnTrace;
        public event TraceEventHandler OnProbe;

        // This is a list of completed traces. A trace is an array of hops.
        // only complete traces are added to this list
        private List<Hop[]> traces = new List<Hop[]>();
        private List<Trace> backlog = new List<Trace>();
        private MinMaxTracker minmax = new MinMaxTracker();

        internal int Timeout { get { return timeout; } }

        public TraceEngine(string hostOrIp)
        {
            SetHostAddress(hostOrIp);
        }

        internal void SetHostAddress(string hostOrIp)
        {
            this.hostAddress = hostOrIp;
            this.ipAddress = ResolveToIPAddress(hostOrIp);
            Debug.Print(hostOrIp + " is " + this.ipAddress);
        }

        internal string ResolveToIPAddress(string hostOrIp)
        {
            IPAddress parsedIP;
            if (IPAddress.TryParse(hostOrIp, out parsedIP)) return parsedIP.ToString();
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(hostOrIp);
                if (hostEntry.AddressList.Length > 0)
                {
                    var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                    if (ipv4Address != null)
                    {
                        return ipv4Address.ToString();
                    }
                }
                throw new ArgumentException("Could not resolve host to IP Address");
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid host name or IP address");
            }
        }
        public void Clear()
        {
            Stop();
            traces.Clear();
            backlog.Clear();
            minmax.Clear();
            maxTTL = 30;
            ++activeTraceId;
        }

        internal void Start()
        {
            Task.Run(() => { StartBackground(PING_FREQUENCY); });
        }

        internal void Stop()
        {
            StopBackground();
        }

        private void StartBackground(int interval = 5000)
        {
            if (backgroundTimer != null) StopBackground();
            ++activeTraceId;
            backgroundTimer = new System.Threading.Timer(
                callback: QueueTrace,
                state: null,
                dueTime: 0,
                period: interval
            );
        }

        private void QueueTrace(object state = null)
        {
            QueueTrace(false, TraceComplete);
        }

        private void StopBackground()
        {
            if (backgroundTimer == null) return;
            backgroundTimer.Dispose();
            backgroundTimer = null;
        }

        internal void SendProbe()
        {
            ++activeTraceId;
            QueueTrace(true, ProbeComplete);
        }

        private void QueueTrace(bool isProbe, Action<Trace> traceComplete)
        {
            // This is called every PING_FREQUENCY to start a trace to the
            // destination IP.
            // We should run each hop in a separate thread, up to the current
            // TTL or until we see the destination IP.
            Trace trace = new Trace();
            trace.ipAddress = this.ipAddress;
            trace.sequence = traces.Count;
            if (!isProbe)
            {
                Hop[] empty = new Hop[] { };
                traces.Add(empty);
            }
            trace.hops = new List<Hop>(maxTTL);
            DateTime timestamp = DateTime.Now;
            int queued = 0;
            for (int i = 1; i <= maxTTL; ++i)
            {
                Hop hop = new Hop();
                hop.hop = i;
                hop.timestamp = timestamp;
                hop.timeout = timeout;
                trace.hops.Add(hop);
                // start a thread to ping this hop
                queued++;
                PingAsync(trace, hop, (AsyncPing ping) =>
                {
                    queued--;
                    if (queued == 0)
                    {
                        traceComplete(trace);
                    }
                });
            }

            // trace is now running
        }

        private void PingAsync(Trace trace, Hop hop, Action<AsyncPing> complete)
        {
            AsyncPing ping = new AsyncPing();
            ping.traceId = activeTraceId;
            ping.sw = new Stopwatch();
            ping.hop = hop;
            ping.callback = complete;
            Ping pingSender = new Ping();
            pingSender.PingCompleted += PingSender_PingCompleted;
            ping.sw.Start();
            pingSender.SendAsync(trace.ipAddress, hop.timeout, buffer, new PingOptions() { DontFragment = true, Ttl = hop.hop }, ping);
        }

        private void PingSender_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            AsyncPing ping = (AsyncPing)e.UserState;
            ping.sw.Stop();

            if (e.Cancelled)
            {
                Debug.Print("CANCELLED");
                return;
            }

            if (ping.traceId != activeTraceId)
            {
               return;                    // If a new trace was started while a trace was running, ignore old trace responses
            }

            if (e.Error != null)
            {
                Debug.Print(e.Error.Message);
            }

            PingReply reply = e.Reply;

            switch (reply.Status)
            {
                case IPStatus.Success:
                    ping.hop.rtt = reply.RoundtripTime;
                    ping.hop.ipAddress = reply.Address?.ToString();
                    break;
                case IPStatus.TtlExpired:
                    ping.hop.rtt = ping.sw.ElapsedMilliseconds;
                    ping.hop.ipAddress = reply.Address?.ToString();
                    break;
                case IPStatus.TimedOut:
                    ping.hop.rtt = -1;
                    ping.hop.ipAddress = null;
                    break;
                default:
                    ping.hop.rtt = -2;
                    ping.hop.ipAddress = null;
                    break;
            }

            ping.callback(ping);
        }

        private void TraceComplete(Trace trace)
        {
            int count = traces.Count;

            Debug.Print("TRACE " + trace.sequence + " " + trace.ipAddress + " MAXTTL " + maxTTL + " TRACE ID " + activeTraceId);
            int index = trace.hops.FindIndex(hop => hop.ipAddress == trace.ipAddress);
            maxTTL = index < 0 ? maxTTL + 1 : index + 1;
            Hop[] hops = trace.hops.Take(maxTTL).ToArray();
            foreach (Hop hop in hops)
            {
                MinMax mm = minmax.Track(hop, count);
                Debug.Print("  HOP {0} IP {1} TTL {2} RTT {3}ms MIN {4} MAX {5} AVE {6} PL {7}",
                    hop.hop,
                    hop.ipAddress ?? "Request Timed Out",
                    hop.hop,
                    hop.rtt < 0 ? "*" : hop.rtt.ToString("D"),
                    mm == null ? "*" : mm.min.ToString(),
                    mm == null ? "*" : mm.max.ToString(),
                    mm == null ? "*" : ((int)(mm.ave)).ToString(),
                    mm == null ? "*" : mm.pl.ToString()
                );
            }

            traces[trace.sequence] = hops;
            OnTrace?.Invoke(this, hops);
        }

        private void ProbeComplete(Trace trace)
        {
            Debug.Print("PROBE " + trace.ipAddress + " MAXTTL " + maxTTL);
            int index = trace.hops.FindIndex(hop => hop.ipAddress == trace.ipAddress);
            if (index >= 0)
            {
                Hop[] hops = trace.hops.Take(index + 1).ToArray();
                foreach (Hop hop in hops)
                {
                    Debug.Print("  HOP {0} IP {1} TTL {2} RTT {3}ms",
                        hop.hop,
                        hop.ipAddress ?? "Request Timed Out",
                        hop.hop,
                        hop.rtt < 0 ? "*" : hop.rtt.ToString("D")
                    );
                }
                maxTTL = index + 1;
                OnProbe?.Invoke(this, hops);
            }
        }

        internal void SetTimeout(int ICMPTimeout)
        {
            this.timeout = ICMPTimeout;
        }

        internal string GetHostAddress()
        {
            return hostAddress;
        }
        public MinMax GetMinMax(string ip)
        {
            return minmax.Get(ip);
        }

        public MinMaxTracker GetMinMaxTracker()
        {
            return minmax;
        }
        public Hop[][] GetTraces()
        {
            return traces.ToArray();
        }
    }
}
