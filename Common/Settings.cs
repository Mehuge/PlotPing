using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ScottPlot;

namespace PlotPingApp
{
    public partial class Settings : Form
    {
        // Application Data Folder
        private static string appData = Path.GetDirectoryName(Application.UserAppDataPath);

        // App Settings, and their defaults.
        private static Dictionary<string, string> settings = new Dictionary<string, string>()
        {
            // Default settings
            ["LogOutputFolder"] = GetDefaultLogOutputFolder()
        };

        internal static string Get(string key)
        {
            string value;
            return settings.TryGetValue(key, out value) ? value : null;
        }

        internal static void Set(string key, string value)
        {
            settings[key] = value;
        }

        internal static string LogOutputFolder { get { return Get("LogOutputFolder"); } set { Set("LogOutputFolder", value); } }

        internal static void LoadSettings()
        {
            try
            {
                string json = File.ReadAllText(Path.Combine(appData, "settings.json"));
                settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch
            {
            }
        }

        internal static void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(Path.Combine(appData, "settings.json"), json);
        }

        internal Settings()
        {
            InitializeComponent();
            LoadSettings();
            logOutputFolder.Text = LogOutputFolder;
            if (logOutputFolder.Text == "")
            {
                LogOutputFolder = logOutputFolder.Text = GetDefaultLogOutputFolder();
            }
        }

        private static string GetDefaultLogOutputFolder() {
            return appData;
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LogOutputFolder = logOutputFolder.Text = dialog.SelectedPath;
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        internal static void RestoreFormLocation(Form form)
        {
            string bounds = Get(form.Name + ".Bounds");
            if (bounds != null)
            {
                Rectangle rect = JsonConvert.DeserializeObject<Rectangle>(bounds);
                form.Location = rect.Location;
                form.Size = rect.Size;
            }
        }

        internal static void SaveFormLocation(Form form)
        {
            LoadSettings();
            Set(form.Name + ".Bounds", JsonConvert.SerializeObject(form.Bounds));
            SaveSettings();
        }
    }
}
