﻿using System.Diagnostics;
using System.Windows.Forms;

namespace PlotPingApp
{
    internal class Utils
    {
        private static string donationLink = "https://www.paypal.com/donate/?business=GU7TQQQ3FABSL&no_recurring=0&item_name=Every+coffee+%E2%98%95+you+send+fuels+the+next+version.%0AThank+you+for+being+part+of+our+journey%21&currency_code=GBP";

        public static void Donation()
        {
            Process.Start(new ProcessStartInfo(donationLink) { UseShellExecute = true });
        }

        public static void OpenGitHub()
        {
            Process.Start(new ProcessStartInfo("https://github.com/mehuge/PlotPing.git") { UseShellExecute = true });
        }

        public static void ShowHelp()
        {
            Process.Start(new ProcessStartInfo("https://github.com/mehuge/PlotPing.git") { UseShellExecute = true });
        }

        public static void StartPlotPing()
        {
            Program.RunWithArgs("--start PlotPing");
        }

        public static void StartMultiPing()
        {
            Program.RunWithArgs("--start MultiPing");
        }

        public static void ShowSettings(FormClosedEventHandler settingsClosed)
        {
            Settings settings = new Settings();
            settings.FormClosed += settingsClosed;
            settings.ShowDialog();
        }
    }
}
