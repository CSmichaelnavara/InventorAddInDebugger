using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MiNa.InventorAddInDebugger.Properties;

namespace MiNa.InventorAddInDebugger
{
    class AddInInfoLoader
    {
        string _addInInfoExe;

        public AddInInfoLoader(string addInInfoExe)
        {
            _addInInfoExe = addInInfoExe;
        }

        public List<AddInInfo> GetAddInsFromAssembly(string lastBuild)
        {
            var addIns = new List<AddInInfo>();
            var sb = new List<string>();

            string settingsExe = GetSettingsExe();
            var startInfo = new ProcessStartInfo(settingsExe)
            {
                Arguments = lastBuild,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(settingsExe)
            };

            var proc = Process.Start(startInfo);
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                sb.Add(line);
            }


            if (proc.ExitCode == 0)
            {
                foreach (string line in sb)
                {
                    string[] strings = line.Split(';');
                    addIns.Add(new AddInInfo(strings[1], strings[0]));
                }
            }
            else
            {
                MessageBox.Show($"{Resources.Failed}\r\n{string.Join("\r\n", sb)}", Resources.AddIn_DisplayName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return addIns;
        }

        private string GetSettingsExe()
        {
            return Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                _addInInfoExe);
        }
    }
}