using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiNa.InventorAddInDebugger.UI
{
    public partial class SelectDllDlg : Form
    {
        private string[] foundDlls;

        public SelectDllDlg()
        {
            InitializeComponent();
        }

        public string[] FoundDlls
        {
            get => foundDlls;
            set
            {
                foundDlls = value;
                PopulateListView();
            }
        }

        public string RootDir { get; set; }

        public string SelectedDll =>
            listView1.SelectedItems.Count > 0 ? listView1.SelectedItems[0].SubItems[1].Text : null;

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

        private void PopulateListView()
        {
            listView1.Items.Clear();
            if (FoundDlls.Length == 0)
                return;

            foreach (string foundDll in FoundDlls)
            {
                string relativePath = Path.GetDirectoryName(foundDll).Replace(RootDir, "");
                listView1.Items.Add(new ListViewItem(new[]
                {
                    //relativePath,
                    relativePath,
                    foundDll
                }));
            }

            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Items[0].Selected = true;
        }
    }
}