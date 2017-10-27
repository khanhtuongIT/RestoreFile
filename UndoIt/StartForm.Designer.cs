namespace RecoveryThisFile
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.Header = new System.Windows.Forms.Panel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.Minimize = new System.Windows.Forms.PictureBox();
            this.CloseF = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.lbSettings = new DevComponents.DotNetBar.LabelX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.btnShowAll = new DevComponents.DotNetBar.ButtonX();
            this.btnName = new DevComponents.DotNetBar.ButtonX();
            this.pnlAll = new System.Windows.Forms.Panel();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.pnlName = new System.Windows.Forms.Panel();
            this.pbBack2 = new System.Windows.Forms.PictureBox();
            this.pnlType = new System.Windows.Forms.Panel();
            this.pbBack3 = new System.Windows.Forms.PictureBox();
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseF)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            this.pnlName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack2)).BeginInit();
            this.pnlType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack3)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.Header.Controls.Add(this.labelX1);
            this.Header.Controls.Add(this.Minimize);
            this.Header.Controls.Add(this.CloseF);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.ForeColor = System.Drawing.Color.Black;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(895, 31);
            this.Header.TabIndex = 5;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Header_MouseDown);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.labelX1.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelX1.Location = new System.Drawing.Point(0, 0);
            this.labelX1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.Color.Transparent;
            this.labelX1.Size = new System.Drawing.Size(829, 31);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "<font color=\"#FFFFFF\"><span align=\"left\"><font size=\"+4\"> Recover This File</font" +
    "></span></font>";
            this.labelX1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelX1_MouseDown);
            // 
            // Minimize
            // 
            this.Minimize.BackColor = System.Drawing.Color.Transparent;
            this.Minimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Minimize.BackgroundImage")));
            this.Minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Minimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.Minimize.ForeColor = System.Drawing.Color.Black;
            this.Minimize.Image = ((System.Drawing.Image)(resources.GetObject("Minimize.Image")));
            this.Minimize.Location = new System.Drawing.Point(829, 0);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(33, 31);
            this.Minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Minimize.TabIndex = 2;
            this.Minimize.TabStop = false;
            this.Minimize.Click += new System.EventHandler(this.Minimize_Click);
            // 
            // CloseF
            // 
            this.CloseF.BackColor = System.Drawing.Color.Transparent;
            this.CloseF.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseF.BackgroundImage")));
            this.CloseF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseF.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseF.ForeColor = System.Drawing.Color.Black;
            this.CloseF.Image = ((System.Drawing.Image)(resources.GetObject("CloseF.Image")));
            this.CloseF.Location = new System.Drawing.Point(862, 0);
            this.CloseF.Name = "CloseF";
            this.CloseF.Size = new System.Drawing.Size(33, 31);
            this.CloseF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CloseF.TabIndex = 1;
            this.CloseF.TabStop = false;
            this.CloseF.Click += new System.EventHandler(this.Close_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Controls.Add(this.lbSettings);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnShowAll);
            this.panel1.Controls.Add(this.btnName);
            this.panel1.Controls.Add(this.pnlAll);
            this.panel1.Controls.Add(this.pnlName);
            this.panel1.Controls.Add(this.pnlType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(895, 701);
            this.panel1.TabIndex = 6;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.White;
            this.labelX2.Location = new System.Drawing.Point(83, 170);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(722, 77);
            this.labelX2.TabIndex = 12;
            this.labelX2.Text = "Please choose the way you want to scan and restore";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelX2.WordWrap = true;
            // 
            // lbSettings
            // 
            this.lbSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            // 
            // 
            // 
            this.lbSettings.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbSettings.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSettings.ForeColor = System.Drawing.Color.White;
            this.lbSettings.Location = new System.Drawing.Point(2685, 671);
            this.lbSettings.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.lbSettings.Name = "lbSettings";
            this.lbSettings.Size = new System.Drawing.Size(0, 30);
            this.lbSettings.Symbol = "";
            this.lbSettings.SymbolSize = 15F;
            this.lbSettings.TabIndex = 10;
            this.lbSettings.Text = "Settings";
            this.lbSettings.Click += new System.EventHandler(this.lbSettings_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FocusCuesEnabled = false;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.btnSearch.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.btnSearch.Location = new System.Drawing.Point(592, 253);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.btnSearch.Size = new System.Drawing.Size(220, 204);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 5;
            this.btnSearch.TextColor = System.Drawing.Color.White;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnShowAll.BackColor = System.Drawing.Color.Transparent;
            this.btnShowAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnShowAll.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.btnShowAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowAll.FocusCuesEnabled = false;
            this.btnShowAll.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAll.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.btnShowAll.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.btnShowAll.Location = new System.Drawing.Point(81, 253);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.btnShowAll.Size = new System.Drawing.Size(220, 204);
            this.btnShowAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014;
            this.btnShowAll.TabIndex = 4;
            this.btnShowAll.TextColor = System.Drawing.Color.White;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnName
            // 
            this.btnName.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnName.BackColor = System.Drawing.Color.Transparent;
            this.btnName.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.btnName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnName.FocusCuesEnabled = false;
            this.btnName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnName.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.btnName.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.btnName.Location = new System.Drawing.Point(338, 253);
            this.btnName.Name = "btnName";
            this.btnName.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.btnName.Size = new System.Drawing.Size(220, 204);
            this.btnName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnName.TabIndex = 3;
            this.btnName.TextColor = System.Drawing.Color.White;
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // pnlAll
            // 
            this.pnlAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.pnlAll.Controls.Add(this.pbBack);
            this.pnlAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAll.Location = new System.Drawing.Point(1790, 0);
            this.pnlAll.Name = "pnlAll";
            this.pnlAll.Size = new System.Drawing.Size(895, 701);
            this.pnlAll.TabIndex = 6;
            this.pnlAll.Visible = false;
            this.pnlAll.SizeChanged += new System.EventHandler(this.pnlAll_SizeChanged);
            // 
            // pbBack
            // 
            this.pbBack.Image = global::RecoveryThisFile.Properties.Resources.goback;
            this.pbBack.Location = new System.Drawing.Point(4, 7);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(45, 40);
            this.pbBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBack.TabIndex = 0;
            this.pbBack.TabStop = false;
            this.pbBack.Click += new System.EventHandler(this.gbAll_Click);
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.pnlName.Controls.Add(this.pbBack2);
            this.pnlName.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlName.Location = new System.Drawing.Point(895, 0);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(895, 701);
            this.pnlName.TabIndex = 7;
            this.pnlName.Visible = false;
            // 
            // pbBack2
            // 
            this.pbBack2.Image = global::RecoveryThisFile.Properties.Resources.goback;
            this.pbBack2.Location = new System.Drawing.Point(6, 7);
            this.pbBack2.Name = "pbBack2";
            this.pbBack2.Size = new System.Drawing.Size(45, 40);
            this.pbBack2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBack2.TabIndex = 1;
            this.pbBack2.TabStop = false;
            this.pbBack2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pnlType
            // 
            this.pnlType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.pnlType.Controls.Add(this.pbBack3);
            this.pnlType.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlType.Location = new System.Drawing.Point(0, 0);
            this.pnlType.Name = "pnlType";
            this.pnlType.Size = new System.Drawing.Size(895, 701);
            this.pnlType.TabIndex = 8;
            this.pnlType.Visible = false;
            // 
            // pbBack3
            // 
            this.pbBack3.Image = global::RecoveryThisFile.Properties.Resources.goback;
            this.pbBack3.Location = new System.Drawing.Point(6, 7);
            this.pbBack3.Name = "pbBack3";
            this.pbBack3.Size = new System.Drawing.Size(45, 40);
            this.pbBack3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBack3.TabIndex = 2;
            this.pbBack3.TabStop = false;
            this.pbBack3.Click += new System.EventHandler(this.gbType);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(192)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(895, 732);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Header);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "StartForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartForm";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.Shown += new System.EventHandler(this.StartForm_Shown);
            this.Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseF)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlAll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            this.pnlName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBack2)).EndInit();
            this.pnlType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBack3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Header;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.PictureBox Minimize;
        private System.Windows.Forms.PictureBox CloseF;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnName;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.Panel pnlAll;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Panel pnlType;
        private System.Windows.Forms.PictureBox pbBack3;
        private System.Windows.Forms.PictureBox pbBack2;
        private DevComponents.DotNetBar.LabelX lbSettings;
        private DevComponents.DotNetBar.ButtonX btnShowAll;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}