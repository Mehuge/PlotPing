using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotPingApp
{
    public partial class About : Form
    {
        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private static string donationLink = "https://www.paypal.com/donate/?business=GU7TQQQ3FABSL&no_recurring=0&item_name=Every+coffee+%E2%98%95+you+send+fuels+the+next+version.%0AThank+you+for+being+part+of+our+journey%21&currency_code=GBP";

        public About()
        {

            InitializeComponent();
            version.Text = $"Version {GetVersion()}";
        }

        private void donate_Click(object sender, EventArgs e)
        {
            // Open URL in the default browser
            Process.Start(new ProcessStartInfo(donationLink) { UseShellExecute = true });
        }


    }
}
