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

        public LogWriter(string logFile)
        {
            this.logFile = logFile;
        }

        private void Open()
        {
            if (stream == null)
            {
                stream = new StreamWriter(logFile, append);
            }
        }

        internal void WriteSample(int sequence, Hop[] hops, Traceroute traceroute)
        {
            Open();
            Export.ExportSample(stream, sequence, hops, traceroute);
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
