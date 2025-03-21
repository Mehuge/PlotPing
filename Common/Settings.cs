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
            ["LogOutputFolder"] = GetDefaultLogOutputFolder(),
            ["ActiveTracksWindow"] = "true",
            ["ICMPTimeout"] = "3000",
        };

        internal static string Get(string key)
        {
            string value;
            if (settings.TryGetValue(key, out value)) return value;
            return null;
        }

        internal static void Set(string key, string value)
        {
            settings[key] = value;
        }

        internal static string LogOutputFolder { get { return Get("LogOutputFolder"); } set { Set("LogOutputFolder", value); } }
        internal static bool ActiveTracksWindow { 
            get { return Get("ActiveTracksWindow") == "true"; } 
            set { Set("ActiveTracksWindow", value ? "true" : "false"); }
        }
        internal static int ICMPTimeout
        {
            get { return int.Parse(Get("ICMPTimeout")); }
            set { Set("ICMPTimeout", value.ToString()); }
        }

        internal static void LoadSettings()
        {
            try
            {
                string json = File.ReadAllText(Path.Combine(appData, "settings.json"));
                Dictionary<string, string> loadedSettings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                foreach (var setting in loadedSettings)
                {
                    Set(setting.Key, setting.Value);
                }
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
            activeTracksWindowCheckBox.Checked = ActiveTracksWindow;
            timeout.Text = ICMPTimeout.ToString();
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

        private void activeTracksWindow_CheckedChanged(object sender, EventArgs e)
        {
            ActiveTracksWindow = activeTracksWindowCheckBox.Checked;
        }

        private void timeout_TextChanged(object sender, EventArgs e)
        {
            int timeoutValue;
            if (int.TryParse(timeout.Text, out timeoutValue))
            {
                ICMPTimeout = timeoutValue;
                return;
            }
            timeout.Text = "4000";
        }
    }
}
