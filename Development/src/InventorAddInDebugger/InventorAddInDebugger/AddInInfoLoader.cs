using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MiNa.InventorAddInDebugger.Properties;
using MiNa.InventorAddInDebugger.UI;

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
                Arguments = $"\"{lastBuild.Trim("\" ".ToCharArray())}\"",
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


            int procExitCode = proc.ExitCode;
            if (procExitCode == 0)
            {
                foreach (string line in sb)
                {
                    string[] strings = line.Split(';');
                    addIns.Add(new AddInInfo(strings[1], strings[0]));
                }
            }
            else
            {
                MessageBox.Show($"{Resources.Failed} (Exit code: {procExitCode})\r\n{string.Join("\r\n", sb)}", Resources.AddIn_DisplayName,
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

    class AddInFileInfoLoader
    {
        public AddInFileInfoLoader()
        {

        }

        /// <summary>
        /// Gets the information about addin from .addin file
        /// </summary>
        /// <param name="addinFile">Full path to the .addin file</param>
        /// <returns></returns>
        public AddInInfo AddInInfo(string addinFile)
        {
            var addinXml = new XmlDocument();
            addinXml.Load(addinFile);
            if (addinXml.DocumentElement == null)
                return null;

            string clientId = addinXml.DocumentElement["ClientId"]?.InnerText; ;
            string assembly = addinXml.DocumentElement["Assembly"]?.InnerText;
            string fullName = GetFullName(addinFile, assembly);
            var addInInfo = new AddInInfo(fullName, clientId);
            return addInInfo;
        }

        private string GetFullName(string addinFile, string assembly)
        {
            //Contains full file name
            if (Path.IsPathRooted(assembly)) return assembly;

            string addinFileDirectory = Path.GetDirectoryName(addinFile);
            string expectedFullName = Path.Combine(addinFileDirectory, assembly);
            if (File.Exists(expectedFullName))
                return expectedFullName;

            string expectedRootDir = Path.GetDirectoryName(expectedFullName);
            string dllFileName = Path.GetFileName(expectedFullName);
            string[] foundDlls = Directory.EnumerateFiles(expectedRootDir, dllFileName, SearchOption.AllDirectories).ToArray();
            switch (foundDlls.Length)
            {
                case 0:
                    return null;
                case 1:
                    return foundDlls[0];
                default:
                    return SelectDll(expectedRootDir, foundDlls);
            }

        }

        private string SelectDll(string rootDir, string[] foundDlls)
        {
            var selectDllDlg = new SelectDllDlg() { RootDir = rootDir, FoundDlls = foundDlls };
            var dialogResult = selectDllDlg.ShowDialog();
            if (dialogResult == DialogResult.OK)
                return selectDllDlg.SelectedDll;
            return null;
        }
    }
}