using System;
using System.Windows.Forms;
using MiNa.InventorAddInDebugger.Properties;

namespace MiNa.InventorAddInDebugger.UI
{
    public partial class SettingsDlg : Form
    {
        private AddInLoaderConfig _config = new AddInLoaderConfig();
        private AddInLoaderConfigManager loader = new AddInLoaderConfigManager();
        //private MruAssemblies _mruFiles;

        public SettingsDlg()
        {
            InitializeComponent();
            loader.Load(_config);
            addInLoaderConfigCtrl1.Config = _config;

            // Set default ExitCode
            Environment.ExitCode = 1;

            this.Icon = Resources.SettingsCmd_Light;
        }

        //public MruAssemblies MruFiles
        //{
        //    get => _mruFiles;
        //    set
        //    {
        //        _mruFiles = value;
        //        addInLoaderConfigCtrl1.MruFiles = value;
        //    }
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Environment.Exit(1);
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            addInLoaderConfigCtrl1.Save();
            loader.Save(_config);
            //Environment.Exit(0);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}