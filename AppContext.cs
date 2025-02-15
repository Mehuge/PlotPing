using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace PlotPingApp
{
    public class AppContext : ApplicationContext
    {
        public AppContext()
        {
            ShowForm(GetStartForm(Environment.GetCommandLineArgs()));
        }

        private Form GetStartForm(string [] args)
        {
            Form startForm = null;
            int i = 1;
            while (args.Length > i)
            {
                string opt = args[i++];
                if (opt == "--start" && args.Length > i)
                {
                    string form = args[i++];
                    switch (form)
                    {
                        case "MultiPing":
                            startForm = new MultiPing();
                            break;
                    }
                    continue;
                }

                i++;
            }
            if (startForm == null) return new PlotPing();
            return startForm;
        }

        public void ShowForm(Form form)
        {
            form.FormClosed += FormClosedHandler;
            form.Show();
            if (MainForm == null) MainForm = form;
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            // Closing main form should not exit the app
            if (Application.OpenForms.Count < 1)
            {
                base.OnMainFormClosed(sender, e);
            }
        }

        private void FormClosedHandler(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count < 1)
            {
                ExitThread();
                return;
            }

            if (sender == MainForm)
            {
                MainForm = Application.OpenForms[0];
            }
        }
    }
}