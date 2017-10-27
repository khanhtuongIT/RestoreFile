namespace RecoveryThisFile
{
    partial class DeletedAll_File
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeletedAll_File));
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.fileView = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRecovery = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.bRestoreFiles = new System.Windows.Forms.Button();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.cbShowUnknownFiles = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // tbFilter
            // 
            this.tbFilter.BackColor = System.Drawing.Color.White;
            this.tbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFilter.Location = new System.Drawing.Point(1, 8);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(718, 22);
            this.tbFilter.TabIndex = 8;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            this.tbFilter.Enter += new System.EventHandler(this.tbFilter_Enter);
            this.tbFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFilter_KeyDown);
            this.tbFilter.Leave += new System.EventHandler(this.tbFilter_Leave);
            // 
            // fileView
            // 
            this.fileView.BackColor = System.Drawing.SystemColors.Window;
            this.fileView.CheckBoxes = true;
            this.fileView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colSize,
            this.colModified,
            this.colPath,
            this.colRecovery});
            this.fileView.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileView.FullRowSelect = true;
            this.fileView.Location = new System.Drawing.Point(0, 51);
            this.fileView.Name = "fileView";
            this.fileView.Size = new System.Drawing.Size(817, 433);
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
            this.colRecovery.Text = "Chance of Recovery";
            this.colRecovery.Width = 160;
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 200;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
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
            this.bRestoreFiles.Location = new System.Drawing.Point(527, 509);
            this.bRestoreFiles.Name = "bRestoreFiles";
            this.bRestoreFiles.Size = new System.Drawing.Size(141, 34);
            this.bRestoreFiles.TabIndex = 10;
            this.bRestoreFiles.Text = "Restore Files...";
            this.bRestoreFiles.UseVisualStyleBackColor = false;
            this.bRestoreFiles.Visible = false;
            this.bRestoreFiles.Click += new System.EventHandler(this.bRestoreFiles_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(732, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 34);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.Symbol = "";
            this.btnSearch.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSearch.SymbolSize = 18F;
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbShowUnknownFiles
            // 
            this.cbShowUnknownFiles.AutoSize = true;
            this.cbShowUnknownFiles.Location = new System.Drawing.Point(13, 492);
            this.cbShowUnknownFiles.Name = "cbShowUnknownFiles";
            this.cbShowUnknownFiles.Size = new System.Drawing.Size(80, 17);
            this.cbShowUnknownFiles.TabIndex = 14;
            this.cbShowUnknownFiles.Text = "checkBox1";
            this.cbShowUnknownFiles.UseVisualStyleBackColor = true;
            this.cbShowUnknownFiles.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.BackColor = System.Drawing.Color.White;
            this.progressBar.Location = new System.Drawing.Point(3, 507);
            this.progressBar.Maximum = 10000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(670, 34);
            this.progressBar.TabIndex = 11;
            // 
            // DeletedAll_File
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.cbShowUnknownFiles);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.fileView);
            this.Controls.Add(this.bRestoreFiles);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tbFilter);
            this.Name = "DeletedAll_File";
            this.Size = new System.Drawing.Size(866, 544);
            this.Leave += new System.EventHandler(this.DeletedFileViewer_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Button bRestoreFiles;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colModified;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colRecovery;
        private System.Windows.Forms.Timer UpdateTimer;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.CheckBox cbShowUnknownFiles;
        public System.Windows.Forms.ListView fileView;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
