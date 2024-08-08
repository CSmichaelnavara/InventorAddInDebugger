﻿namespace MiNa.InventorAddInDebugger.UI
{
    partial class AddInLoaderConfigCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLoadFromFile = new System.Windows.Forms.Label();
            this.tbAddinClientId = new System.Windows.Forms.TextBox();
            this.lblAssemblyFile = new System.Windows.Forms.Label();
            this.lblAddinClientId = new System.Windows.Forms.Label();
            this.tbAssemblyFile = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.tbFullName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBrowseNet48 = new System.Windows.Forms.Button();
            this.btnBrowseNet80 = new System.Windows.Forms.Button();
            this.lblLoadOnStart = new System.Windows.Forms.Label();
            this.cbLoadOnStart = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblLoadFromFile, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbAddinClientId, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAssemblyFile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAddinClientId, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbAssemblyFile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblFullName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbFullName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblLoadOnStart, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbLoadOnStart, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(463, 195);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblLoadFromFile
            // 
            this.lblLoadFromFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLoadFromFile.AutoSize = true;
            this.lblLoadFromFile.Location = new System.Drawing.Point(3, 98);
            this.lblLoadFromFile.Name = "lblLoadFromFile";
            this.lblLoadFromFile.Size = new System.Drawing.Size(73, 13);
            this.lblLoadFromFile.TabIndex = 8;
            this.lblLoadFromFile.Text = "Browse AddIn";
            // 
            // tbAddinClientId
            // 
            this.tbAddinClientId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddinClientId.Location = new System.Drawing.Point(103, 33);
            this.tbAddinClientId.Name = "tbAddinClientId";
            this.tbAddinClientId.Size = new System.Drawing.Size(357, 20);
            this.tbAddinClientId.TabIndex = 3;
            // 
            // lblAssemblyFile
            // 
            this.lblAssemblyFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAssemblyFile.AutoSize = true;
            this.lblAssemblyFile.Location = new System.Drawing.Point(3, 8);
            this.lblAssemblyFile.Name = "lblAssemblyFile";
            this.lblAssemblyFile.Size = new System.Drawing.Size(70, 13);
            this.lblAssemblyFile.TabIndex = 0;
            this.lblAssemblyFile.Text = "Assembly File";
            // 
            // lblAddinClientId
            // 
            this.lblAddinClientId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAddinClientId.AutoSize = true;
            this.lblAddinClientId.Location = new System.Drawing.Point(3, 37);
            this.lblAddinClientId.Name = "lblAddinClientId";
            this.lblAddinClientId.Size = new System.Drawing.Size(72, 13);
            this.lblAddinClientId.TabIndex = 1;
            this.lblAddinClientId.Text = "Addin ClientId";
            // 
            // tbAssemblyFile
            // 
            this.tbAssemblyFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAssemblyFile.Location = new System.Drawing.Point(103, 4);
            this.tbAssemblyFile.Name = "tbAssemblyFile";
            this.tbAssemblyFile.Size = new System.Drawing.Size(357, 20);
            this.tbAssemblyFile.TabIndex = 2;
            // 
            // lblFullName
            // 
            this.lblFullName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(3, 66);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(78, 13);
            this.lblFullName.TabIndex = 4;
            this.lblFullName.Text = "Type FullName";
            // 
            // tbFullName
            // 
            this.tbFullName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFullName.Enabled = false;
            this.tbFullName.Location = new System.Drawing.Point(103, 62);
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.Size = new System.Drawing.Size(357, 20);
            this.tbFullName.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnBrowseNet48, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnBrowseNet80, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(103, 90);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(162, 29);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // btnBrowseNet48
            // 
            this.btnBrowseNet48.Location = new System.Drawing.Point(3, 3);
            this.btnBrowseNet48.Name = "btnBrowseNet48";
            this.btnBrowseNet48.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNet48.TabIndex = 6;
            this.btnBrowseNet48.Text = "NET 4.8";
            this.btnBrowseNet48.UseVisualStyleBackColor = true;
            this.btnBrowseNet48.Click += new System.EventHandler(this.btnBrowseNet48_Click);
            // 
            // btnBrowseNet80
            // 
            this.btnBrowseNet80.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseNet80.Location = new System.Drawing.Point(84, 3);
            this.btnBrowseNet80.Name = "btnBrowseNet80";
            this.btnBrowseNet80.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNet80.TabIndex = 3;
            this.btnBrowseNet80.Text = "NET 8";
            this.btnBrowseNet80.UseVisualStyleBackColor = true;
            this.btnBrowseNet80.Click += new System.EventHandler(this.btnBrowseNet80_Click);
            // 
            // lblLoadOnStart
            // 
            this.lblLoadOnStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLoadOnStart.AutoSize = true;
            this.lblLoadOnStart.Location = new System.Drawing.Point(3, 130);
            this.lblLoadOnStart.Name = "lblLoadOnStart";
            this.lblLoadOnStart.Size = new System.Drawing.Size(69, 13);
            this.lblLoadOnStart.TabIndex = 9;
            this.lblLoadOnStart.Text = "Load on start";
            // 
            // cbLoadOnStart
            // 
            this.cbLoadOnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLoadOnStart.Location = new System.Drawing.Point(103, 125);
            this.cbLoadOnStart.Name = "cbLoadOnStart";
            this.cbLoadOnStart.Size = new System.Drawing.Size(357, 23);
            this.cbLoadOnStart.TabIndex = 10;
            this.cbLoadOnStart.UseVisualStyleBackColor = true;
            // 
            // AddInLoaderConfigCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AddInLoaderConfigCtrl";
            this.Size = new System.Drawing.Size(463, 195);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbAddinClientId;
        private System.Windows.Forms.Label lblAssemblyFile;
        private System.Windows.Forms.Label lblAddinClientId;
        private System.Windows.Forms.TextBox tbAssemblyFile;
        private System.Windows.Forms.Button btnBrowseNet80;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox tbFullName;
        private System.Windows.Forms.Button btnBrowseNet48;
        private System.Windows.Forms.Label lblLoadFromFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblLoadOnStart;
        private System.Windows.Forms.CheckBox cbLoadOnStart;
    }
}
