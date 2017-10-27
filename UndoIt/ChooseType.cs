using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace RecoveryThisFile
{
    public partial class ChooseType : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        public string Type { get { return type; } }

        private string type;

        CultureInfo cul;
        Assembly a;
        ResourceManager res_man;

        public ChooseType()
        {
            InitializeComponent();
            CheckBoxImage_Load();
            AddCombobox_Items();
            type = Convert.ToString(FileType.doc);
            var culture = Properties.Settings.Default.language;
            switch (culture)
            {
                case "en-US":
                    switch_language("en-US", "RecoveryThisFile.Language.en");
                    break;
                case "es-US":
                    switch_language("es-US", "RecoveryThisFile.Language.sp");
                    break;
                case "fr-FR":
                    switch_language("fr-FR", "RecoveryThisFile.Language.fr");
                    break;
                case "de-DE":
                    switch_language("de-DE", "RecoveryThisFile.Language.ge");
                    break;
                case "ja-JP":
                    switch_language("ja-JP", "RecoveryThisFile.Language.ja");
                    break;
            }
        }

        public void switch_language(string locate, string rsname)
        {
            this.cul = new CultureInfo(locate);
            this.a = Assembly.Load("RecoveryThisFile");
            this.res_man = new ResourceManager(rsname, a);
            this.cbmore.Text = "<font color=\"#000000\"><span algn=\"center\">" + res_man.GetString("strChoose") + "</span></font>";
            this.label1.Text = res_man.GetString("strType", cul);
            this.button1.Text = res_man.GetString("strOK", cul);
            this.button2.Text = res_man.GetString("strCancel", cul);
            this.rdoVideos.Text = res_man.GetString("strvideos", cul);
            this.rdoPhotos.Text = res_man.GetString("strphotos", cul);
            this.rdoDocument.Text = res_man.GetString("strdocuments", cul);
            button2.ForeColor = Color.White;
            this.lbError.Text = res_man.GetString("strMessage14", cul);
        }

        private void ChooseType_Load(object sender, EventArgs e)
        {

        }
        private void CheckBoxImage_Load()
        {
            Thread.Sleep(200);
            panel2.Show();
        }

        #region Checkbox CheckedChanged event
        private void cbword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbword.Checked)
            {
                cbexcel.Checked = false;
                cbppt.Checked = false;
                cbmore.Checked = false;
                PDF.Checked = false;
                cbmp3.Checked = false;
                cbmp4.Checked = false;
                type = Convert.ToString(FileType.doc);
                Properties.Settings.Default.radio = "none";
            }
        }
        private void cbmore_CheckedChanged(object sender, EventArgs e)
        {
            if (cbmore.Checked)
            {
                cbType.Enabled = true;
                cbword.Checked = false;
                cbexcel.Checked = false;
                cbppt.Checked = false;
                PDF.Checked = false;
                cbmp3.Checked = false;
                cbmp4.Checked = false;
                type = cbType.SelectedItem.ToString();
                Properties.Settings.Default.radio = "none";
            }
            else
            {
                cbType.Enabled = false;
            }
        }

        private void cbexcel_CheckedChanged(object sender, EventArgs e)
        {
            if (cbexcel.Checked)
            {
                this.cbword.Checked = false;
                this.cbppt.Checked = false;
                this.cbmore.Checked = false;
                PDF.Checked = false;
                cbmp3.Checked = false;
                cbmp4.Checked = false;
                type = Convert.ToString(FileType.xls);
                Properties.Settings.Default.radio = "none";
            }
        }

        private void cbppt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbppt.Checked)
            {
                cbword.Checked = false;
                cbexcel.Checked = false;
                cbmore.Checked = false;
                PDF.Checked = false;
                cbmp3.Checked = false;
                cbmp4.Checked = false;
                type = Convert.ToString(FileType.ppt);
                Properties.Settings.Default.radio = "none";
            }
        }

        private void PDF_CheckedChanged(object sender, EventArgs e)
        {
            if (PDF.Checked)
            {
                cbword.Checked = false;
                cbexcel.Checked = false;
                cbmore.Checked = false;
                cbppt.Checked = false;
                cbmp3.Checked = false;
                cbmp4.Checked = false;
                type = Convert.ToString(FileType.pdf);
                Properties.Settings.Default.radio = "none";
            }
        }

        private void cbmp3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbmp3.Checked)
            {
                cbword.Checked = false;
                cbexcel.Checked = false;
                cbmore.Checked = false;
                cbppt.Checked = false;
                PDF.Checked = false;
                cbmp4.Checked = false;
                type = Convert.ToString(FileType.mp3);
                Properties.Settings.Default.radio = "none";
            }
        }

        private void cbmp4_CheckedChanged(object sender, EventArgs e)
        {
            if (cbmp4.Checked)
            {
                cbword.Checked = false;
                cbexcel.Checked = false;
                cbmore.Checked = false;
                cbppt.Checked = false;
                PDF.Checked = false;
                cbmp3.Checked = false;
                type = Convert.ToString(FileType.mp4);
                Properties.Settings.Default.radio = "none";
            }
        }
        #endregion

        #region ToolTip Event

        ToolTip tt = new ToolTip();
        private void ShowToolTip(Control ctrl, string title, string text)
        {
            if (ctrl.Text.Length > 0)
            {
                tt.ToolTipTitle = title;
                tt.ToolTipIcon = ToolTipIcon.None;
                tt.UseFading = true;
                tt.UseAnimation = true;
                tt.IsBalloon = true;
                tt.ShowAlways = true;

                tt.AutomaticDelay = 1000;
                tt.InitialDelay = 500;
                tt.ReshowDelay = 500;
                tt.SetToolTip(ctrl, text);
            }
        }

        private void HideToolTip(Control ctrl)
        {
            tt.Hide(ctrl);
        }

        private void cbword_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(cbword, "", Properties.Resources.strW);
        }

        private void cbword_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip(cbword);
        }

        private void cbexcel_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(cbexcel, "", Properties.Resources.strE);
        }

        private void cbexcel_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip(cbexcel);
        }

        private void cbppt_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(cbppt, "", Properties.Resources.strE);
        }

        private void cbppt_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip(cbppt);
        }

        private void PDF_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(PDF, "", Properties.Resources.strPD);
        }

        private void PDF_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip(PDF);
        }

        private void cbmp3_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(cbmp3, "", Properties.Resources.strP3);
        }

        private void cbmp3_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip(cbmp3);
        }

        private void cbmp4_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(cbmp4, "", Properties.Resources.strP4);
        }

        private void cbmp4_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip(cbmp4);
        }

        private void cbmore_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(cbmore, "", Properties.Resources.strB);
        }

        private void cbmore_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip(cbmore);
        }
        #endregion

        #region Combobox event
        private void AddCombobox_Items()
        {
            cbType.Items.Add(FileType.cs);
            cbType.Items.Add(FileType.dll);
            cbType.Items.Add(FileType.html);
            cbType.Items.Add(FileType.iso);
            cbType.Items.Add(FileType.ico);
            cbType.Items.Add(FileType.js);
            cbType.Items.Add(FileType.jpg);
            cbType.Items.Add(FileType.log);
            cbType.Items.Add(FileType.png);
            cbType.Items.Add(FileType.rar);
            cbType.Items.Add(FileType.txt);
            cbType.Items.Add(FileType.xml);
            cbType.SelectedIndex = 0;
        }

        private void cbType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbmore.Checked)
            {
                type = cbType.SelectedItem.ToString();
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (!rdoDocument.Checked && !rdoVideos.Checked && !rdoPhotos.Checked)
            {
                if ((cbmore.Checked || cbmp3.Checked || cbppt.Checked || cbword.Checked || cbexcel.Checked || PDF.Checked || cbmp4.Checked) == false)
                {
                    this.lbError.Visible = true;
                    this.lbError.ForeColor = Color.Red;
                }
                else if ((cbmore.Checked || cbmp3.Checked || cbppt.Checked || cbword.Checked || cbexcel.Checked || PDF.Checked || cbmp4.Checked) == true)
                {
                    this.lbError.Visible = false;
                    Properties.Settings.Default.radio = "none";
                    Properties.Settings.Default.Save();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else if (rdoVideos.Checked)
            {
                this.lbError.Visible = false;
                Properties.Settings.Default.radio = "video";
                Properties.Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (rdoPhotos.Checked)
            {
                this.lbError.Visible = false;
                Properties.Settings.Default.radio = "photo";
                Properties.Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (rdoDocument.Checked)
            {
                this.lbError.Visible = false;
                Properties.Settings.Default.radio = "document";
                Properties.Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPhotos.Checked)
            {
                cbexcel.Checked = false;
                cbword.Checked = false;
                cbppt.Checked = false;
                cbmp4.Checked = false;
                cbmp3.Checked = false;
                cbmore.Checked = false;
                PDF.Checked = false;
            }
        }

        private void rdoDocument_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDocument.Checked)
            {
                cbexcel.Checked = false;
                cbword.Checked = false;
                cbppt.Checked = false;
                cbmp4.Checked = false;
                cbmp3.Checked = false;
                cbmore.Checked = false;
                PDF.Checked = false;
            }
        }

        private void rdoVideos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoVideos.Checked)
            {
                cbexcel.Checked = false;
                cbword.Checked = false;
                cbppt.Checked = false;
                cbmp4.Checked = false;
                cbmp3.Checked = false;
                cbmore.Checked = false;
                PDF.Checked = false;
            }
        }
    }
}
