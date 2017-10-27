namespace RecoveryThisFile
{
    partial class DialogNew
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
            this.metroStatusBar1 = new DevComponents.DotNetBar.Metro.MetroStatusBar();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lbmessage = new DevComponents.DotNetBar.LabelX();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStatusBar1
            // 
            this.metroStatusBar1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.metroStatusBar1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.metroStatusBar1.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150)))));
            this.metroStatusBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroStatusBar1.ContainerControlProcessDialogKey = true;
            this.metroStatusBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroStatusBar1.DragDropSupport = true;
            this.metroStatusBar1.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroStatusBar1.ForeColor = System.Drawing.Color.Black;
            this.metroStatusBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1});
            this.metroStatusBar1.Location = new System.Drawing.Point(1, 1);
            this.metroStatusBar1.Name = "metroStatusBar1";
            this.metroStatusBar1.ResizeHandleVisible = false;
            this.metroStatusBar1.ShowToolTips = false;
            this.metroStatusBar1.Size = new System.Drawing.Size(499, 36);
            this.metroStatusBar1.TabIndex = 10;
            // 
            // labelItem1
            // 
            this.labelItem1.BackColor = System.Drawing.Color.Transparent;
            this.labelItem1.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.labelItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItem1.FontBold = true;
            this.labelItem1.ForeColor = System.Drawing.Color.White;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.labelItem1.Stretch = true;
            this.labelItem1.Symbol = "";
            this.labelItem1.SymbolColor = System.Drawing.Color.White;
            this.labelItem1.SymbolSize = 20F;
            this.labelItem1.Text = "WARNING";
            this.labelItem1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelItem1.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(1, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 36);
            this.panel1.TabIndex = 13;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.FocusCuesEnabled = false;
            this.btnOK.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Color;
            this.btnOK.Location = new System.Drawing.Point(93, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnOK.Size = new System.Drawing.Size(110, 28);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.Symbol = " ";
            this.btnOK.SymbolColor = System.Drawing.Color.Chartreuse;
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "<font size=\"+3\">Ok</font>";
            this.btnOK.TextColor = System.Drawing.Color.White;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FocusCuesEnabled = false;
            this.btnCancel.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Color;
            this.btnCancel.Location = new System.Drawing.Point(291, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnCancel.Size = new System.Drawing.Size(110, 28);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.Symbol = " ";
            this.btnCancel.SymbolColor = System.Drawing.Color.Red;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "<font size=\"+3\">Cancel</font>";
            this.btnCancel.TextColor = System.Drawing.Color.White;
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxX1.ForeColor = System.Drawing.Color.Black;
            this.checkBoxX1.Location = new System.Drawing.Point(53, 100);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(436, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 14;
            this.checkBoxX1.Text = "checkBoxX1";
            // 
            // lbmessage
            // 
            this.lbmessage.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lbmessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbmessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbmessage.ForeColor = System.Drawing.Color.Black;
            this.lbmessage.Location = new System.Drawing.Point(9, 44);
            this.lbmessage.Name = "lbmessage";
            this.lbmessage.Size = new System.Drawing.Size(480, 51);
            this.lbmessage.TabIndex = 15;
            this.lbmessage.Text = "labelX1";
            // 
            // DialogNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderColor = new DevComponents.DotNetBar.Metro.BorderColors(System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150))))), System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150))))), System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150))))), System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(71)))), ((int)(((byte)(150))))));
            this.BorderThickness = new DevComponents.DotNetBar.Metro.Thickness(3D, 3D, 3D, 3D);
            this.ClientSize = new System.Drawing.Size(501, 170);
            this.Controls.Add(this.lbmessage);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.metroStatusBar1);
            this.Name = "DialogNew";
            this.Text = "DialogNew";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Metro.MetroStatusBar metroStatusBar1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.LabelX lbmessage;
    }
}