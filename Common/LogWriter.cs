using PlotPingApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotPingApp
{
    internal class LogWriter
    {
        private string logFile;
        private bool append = true;
        public StreamWriter stream;
        private MinMaxTracker minmax;
        private Export exporter;

        public LogWriter(string logFile, MinMaxTracker minmax)
        {
            this.logFile = logFile;
            this.minmax = minmax;
        }

        private void Open(TraceEngine traceroute)
        {
            if (stream == null)
            {
                stream = new StreamWriter(logFile, append);
                exporter = new Export(traceroute, minmax);
            }
        }

        internal void WriteSample(int sequence, Hop[] hops, TraceEngine traceroute)
        {
            Open(traceroute);
            exporter.ExportSample(stream, sequence, hops, traceroute);
            stream.Flush();
        }

        internal void Close()
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
        }

        internal void Dispose(bool disposing)
        {
            Close();
        }
    }
}
