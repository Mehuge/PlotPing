using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotPingApp
{
    internal class MRU
    {
        private string mruFilePath;
        private int MAX_MRU_COUNT;
        private List<string> mru = new List<string>();
        private ContextMenuStrip mruMenu = new ContextMenuStrip();

        public MRU(string filePath, int max = 25)
        {
            mruFilePath = filePath;
            MAX_MRU_COUNT = max;
        }

        internal void Load()
        {
            try
            {
                if (File.Exists(mruFilePath))
                {
                    mru = File.ReadAllLines(mruFilePath)
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .Reverse()
                        .Take(MAX_MRU_COUNT)
                        .Reverse() // Correct order for adding new items
                        .ToList();
                }
            }
            catch { /* Handle file read errors if needed */ }
        }
        internal void Save()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(mruFilePath));
                File.WriteAllLines(mruFilePath, mru);
            }
            catch { /* Handle file write errors if needed */ }
        }
        
        internal void Add(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            mru.RemoveAll(s => s.Equals(text, StringComparison.OrdinalIgnoreCase));
            mru.Add(text.Trim());
            while (mru.Count > MAX_MRU_COUNT)
                mru.RemoveAt(0);
        }

        internal void Show(Control anchor, Form app, Action<string> OnSelected)
        {
            Load();
            if (mru.Count == 0) return;
            mruMenu.Items.Clear();
            foreach (var item in mru.AsEnumerable().Reverse())
            {
                var menuItem = new ToolStripMenuItem(item);
                menuItem.Click += (s, e) =>
                {
                    OnSelected(item);
                    Add(item);
                };
                mruMenu.Items.Add(menuItem);
            }
            int title = app.Size.Height - app.ClientRectangle.Height;
            mruMenu.Show(new Point(app.Left + anchor.Left + 8, app.Top + title + anchor.Top + anchor.Height - 8));
        }
    }
}
