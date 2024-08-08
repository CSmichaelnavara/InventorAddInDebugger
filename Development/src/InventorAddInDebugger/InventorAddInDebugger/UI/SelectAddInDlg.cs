using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MiNa.InventorAddInDebugger.Properties;

namespace MiNa.InventorAddInDebugger.UI
{
    internal partial class SelectAddInDlg : Form
    {
        private List<AddInInfo> _addIns;

        public SelectAddInDlg()
        {
            InitializeComponent();
            listBox1.DisplayMember = nameof(AddInInfo.FullName);
            Icon = Resources.SettingsCmd_Light;
        }

        public List<AddInInfo> AddIns
        {
            get => _addIns;
            set
            {
                _addIns = value;
                listBox1.Items.Clear();
                if (value != null && value.Count > 0)
                    listBox1.Items.AddRange(value.ToArray());
            }
        }

        public AddInInfo SelectedAddInInfo
        {
            get => listBox1.SelectedItem as AddInInfo;
            set => listBox1.SelectedItem = value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            btnOK.PerformClick();
        }
    }
}