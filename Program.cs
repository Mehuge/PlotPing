using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotPingApp
{
    internal static class Program
    {
        public static AppContext context = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(context = new AppContext());
        }

        public static void RunWithArgs(string args)
        {
            string exe = Environment.GetCommandLineArgs()[0];
            Process.Start(exe, args);
        }
    }
}
