using System;
using System.Windows.Forms;

namespace MiNa.InventorAddInDebugger.UI
{
    public partial class AddInLoaderConfigCtrl : UserControl
    {
        private AddInLoaderConfig _config;
        private ReferencesLoader _referencesLoader;

        public AddInLoaderConfigCtrl()
        {
            InitializeComponent();

            _referencesLoader = new ReferencesLoader();

            //Disable NET 8 button on 4.8 Runtime
            btnBrowseNet80.Enabled = Environment.Version.Major >= 8;
        }

        public AddInLoaderConfig Config
        {
            get => _config;
            set
            {
                _config = value;
                ConfigToControls();
            }
        }

        public void Save() => ControlsToConfig();

        private void btnBrowseNet48_Click(object sender, EventArgs e)
        {
            AddInInfoLoader addInInfoLoader = new AddInInfoLoader("AddInInfo48.exe");

            LoadAddInInfo(addInInfoLoader);
        }

        private void btnBrowseNet80_Click(object sender, EventArgs e)
        {
            AddInInfoLoader addInInfoLoader = new AddInInfoLoader("AddInInfo80.exe");

            LoadAddInInfo(addInInfoLoader);
        }

        private void ConfigToControls()
        {
            if (Config == null)
                return;

            tbAssemblyFile.Text = _config.AddInAssemblyFile;
            tbAddinClientId.Text = _config.AddInClientId;
            tbFullName.Text = _config.AddInFullName;
            cbLoadOnStart.Checked = _config.LoadOnStart;
        }

        private void ControlsToConfig()
        {
            _config.AddInAssemblyFile = tbAssemblyFile.Text.Trim(' ', '"');
            _config.AddInClientId = tbAddinClientId.Text.Trim(" {}".ToCharArray());
            _config.AddInFullName = tbFullName.Text;
            _config.LoadOnStart = cbLoadOnStart.Checked;
        }

        private void LoadAddInInfo(AddInInfoLoader addInInfoLoader)
        {
            var fd = new OpenFileDialog();
            fd.Filter = "DLL Files|*.dll|All Files|*.*";
            fd.Title = "Select add-in build file";


            var dialogResult = fd.ShowDialog(this);

            if (dialogResult != DialogResult.OK) return;

            var addInsFromAssembly = addInInfoLoader.GetAddInsFromAssembly(fd.FileName);
            switch (addInsFromAssembly.Count)
            {
                case 0:
                    MessageBox.Show("There is no add-ins in this assembly");
                    break;
                case 1:
                    tbAssemblyFile.Text = fd.FileName;
                    tbAddinClientId.Text = addInsFromAssembly[0].ClientId;
                    tbFullName.Text = addInsFromAssembly[0].FullName;
                    break;
                default:
                    var selectAddInDlg = new SelectAddInDlg() { AddIns = addInsFromAssembly };
                    var showDialog = selectAddInDlg.ShowDialog(this);
                    if (showDialog == DialogResult.OK)
                    {
                        tbAssemblyFile.Text = fd.FileName;
                        tbAddinClientId.Text = selectAddInDlg.SelectedAddInInfo.ClientId;
                        tbFullName.Text = selectAddInDlg.SelectedAddInInfo.FullName;
                    }
                    else
                    {
                        tbAssemblyFile.Text = fd.FileName;
                        tbAddinClientId.Text = addInsFromAssembly[0].ClientId;
                        tbFullName.Text = addInsFromAssembly[0].FullName;
                    }

                    break;
            }
        }
    }
}