namespace RecoveryThisFile
{
    partial class FrmFileName
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFileName));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbfilename = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.bRestoreFiles = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.fileView = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRecovery = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Tag = "";
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "HDD1");
            this.imageList1.Images.SetKeyName(1, "folder");
            this.imageList1.Images.SetKeyName(2, "HDD");
            this.imageList1.Images.SetKeyName(3, "folder1");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbfilename);
            this.panel1.Location = new System.Drawing.Point(0, 570);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(876, 31);
            this.panel1.TabIndex = 1;
            // 
            // lbfilename
            // 
            this.lbfilename.AutoSize = true;
            this.lbfilename.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbfilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbfilename.ForeColor = System.Drawing.Color.White;
            this.lbfilename.Location = new System.Drawing.Point(10, 6);
            this.lbfilename.Name = "lbfilename";
            this.lbfilename.Size = new System.Drawing.Size(0, 16);
            this.lbfilename.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bRestoreFiles);
            this.panel2.Controls.Add(this.btnScan);
            this.panel2.Controls.Add(this.fileView);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(879, 564);
            this.panel2.TabIndex = 2;
            // 
            // bRestoreFiles
            // 
            this.bRestoreFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRestoreFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.bRestoreFiles.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bRestoreFiles.BackgroundImage")));
            this.bRestoreFiles.Enabled = false;
            this.bRestoreFiles.FlatAppearance.BorderSize = 0;
            this.bRestoreFiles.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bRestoreFiles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bRestoreFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRestoreFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRestoreFiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.bRestoreFiles.Location = new System.Drawing.Point(542, 527);
            this.bRestoreFiles.Name = "bRestoreFiles";
            this.bRestoreFiles.Size = new System.Drawing.Size(141, 34);
            this.bRestoreFiles.TabIndex = 26;
            this.bRestoreFiles.Text = "Restore Files...";
            this.bRestoreFiles.UseVisualStyleBackColor = false;
            this.bRestoreFiles.Visible = false;
            this.bRestoreFiles.Click += new System.EventHandler(this.bRestoreFiles_Click);
            // 
            // btnScan
            // 
            this.btnScan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnScan.BackgroundImage")));
            this.btnScan.FlatAppearance.BorderSize = 0;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnScan.Location = new System.Drawing.Point(689, 527);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(141, 34);
            this.btnScan.TabIndex = 25;
            this.btnScan.Tag = "scan";
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // fileView
            // 
            this.fileView.CheckBoxes = true;
            this.fileView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colSize,
            this.colModified,
            this.colPath,
            this.colRecovery});
            this.fileView.Cursor = System.Windows.Forms.Cursors.Default;
            this.fileView.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileView.FullRowSelect = true;
            this.fileView.Location = new System.Drawing.Point(2, 52);
            this.fileView.Name = "fileView";
            this.fileView.Size = new System.Drawing.Size(817, 443);
            this.fileView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.fileView.TabIndex = 24;
            this.fileView.UseCompatibleStateImageBehavior = false;
            this.fileView.View = System.Windows.Forms.View.Details;
            this.fileView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.fileView_ColumnClick);
            this.fileView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.fileView_ItemCheck);
            this.fileView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.fileView_ItemSelectionChanged);
            this.fileView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fileView_MouseClick);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 193;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 80;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 80;
            // 
            // colModified
            // 
            this.colModified.Text = "Last Modified";
            this.colModified.Width = 150;
            // 
            // colPath
            // 
            this.colPath.Text = "Path";
            this.colPath.Width = 150;
            // 
            // colRecovery
            // 
            this.colRecovery.Text = "Recovery Chances ";
            this.colRecovery.Width = 160;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.White;
            this.progressBar1.Location = new System.Drawing.Point(3, 527);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(670, 34);
            this.progressBar1.TabIndex = 23;
            this.progressBar1.Visible = false;
            // 
            // FrmFileName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmFileName";
            this.Size = new System.Drawing.Size(879, 604);
            this.Load += new System.EventHandler(this.FrmFileName_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbfilename;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ListView fileView;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colModified;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colRecovery;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Button btnScan;
        public System.Windows.Forms.Button bRestoreFiles;
    }
}