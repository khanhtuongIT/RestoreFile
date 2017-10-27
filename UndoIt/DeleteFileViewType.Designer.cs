namespace RecoveryThisFile
{
    partial class DeleteFileViewType
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteFileViewType));
            this.fileView = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRecovery = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bRestoreFiles = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
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
            this.fileView.Location = new System.Drawing.Point(0, 44);
            this.fileView.Name = "fileView";
            this.fileView.Size = new System.Drawing.Size(817, 443);
            this.fileView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.fileView.TabIndex = 12;
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
            this.colRecovery.Text = "Recovery Chances";
            this.colRecovery.Width = 160;
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
            this.bRestoreFiles.Location = new System.Drawing.Point(617, 506);
            this.bRestoreFiles.Name = "bRestoreFiles";
            this.bRestoreFiles.Size = new System.Drawing.Size(138, 34);
            this.bRestoreFiles.TabIndex = 12;
            this.bRestoreFiles.Text = "Restore Files...";
            this.bRestoreFiles.UseVisualStyleBackColor = false;
            this.bRestoreFiles.Visible = false;
            this.bRestoreFiles.Click += new System.EventHandler(this.bRestoreFiles_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.BackColor = System.Drawing.Color.White;
            this.progressBar.Location = new System.Drawing.Point(0, 507);
            this.progressBar.Maximum = 10000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(670, 34);
            this.progressBar.TabIndex = 13;
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 200;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "word");
            this.imageList1.Images.SetKeyName(1, "excel");
            this.imageList1.Images.SetKeyName(2, "pp");
            this.imageList1.Images.SetKeyName(3, "pdf");
            // 
            // DeleteFileViewType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.bRestoreFiles);
            this.Controls.Add(this.fileView);
            this.Controls.Add(this.progressBar);
            this.Name = "DeleteFileViewType";
            this.Size = new System.Drawing.Size(942, 544);
            this.Leave += new System.EventHandler(this.DeleteFileViewType_Leave);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.Button bRestoreFiles;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colModified;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colRecovery;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.ListView fileView;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.ColumnHeader colName;
    }
}
