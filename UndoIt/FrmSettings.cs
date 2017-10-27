using DevComponents.DotNetBar.Rendering;
using KFS.Disks;
using KFS.FileSystems;
using KickassUndelete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RecoveryThisFile
{
    public partial class FrmSettings : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                 int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        #region variable used for Settings
        private string timemodified;
        private string _language;
        private string _year;
        private string _years;

        private string english;
        private string french;
        private string spanish;
        private string german;
        private string japanese;
        #endregion


        public List<IFileSystemStore> FileSystemStore = new List<IFileSystemStore>();

        public Dictionary<List<IFileSystem>, Scanner> _scanners = new Dictionary<List<IFileSystem>, Scanner>();

        public List<string> ListPath = new List<string>();

        public DiskResult Result { get; set; }

        public bool Drive { get; set; }

        private string id;
        public string ID
        {
            get { return id; }
        }

        private string key;
        public string SelectedImageKey
        {
            get { return key; }
        }

        private string folder;

        public string NodePath
        {
            get { return lsvDisk.SelectedItems.ToString(); }
        }

        public string Disk { get { return name; } }
        public string Link { get { return link; } }

        public string pLocale { get; set; }
        public string pRsName { get; set; }

        private bool IsShow;
        private string dgHeader;
        private string dgMessage;
        private string dgOK;
        private string dgCancel;
        private string dgText;

        public FrmSettings(bool _isshow)
        {
            InitializeComponent();
            IsShow = _isshow;
            #region Settings
            week.Checked = Properties.Settings.Default.weekChecked;
            month.Checked = Properties.Settings.Default.monthChecked;
            all.Checked = Properties.Settings.Default.allChecked;
            cbYear.Checked = Properties.Settings.Default.yearChecked;
            comboBox1.SelectedItem = Properties.Settings.Default.lastYear;
            #endregion

            #region Disk

            LoadLogicalDisks();
            cbscan.Checked = true;
            GetFilseSystemStore();
            Result = DiskResult.Yes;
            #endregion

            CheckRegister();
            sideNavItem3.Checked = true;
            
            snpHelp.Controls.Add(expandablePanel3);
            expandablePanel3.Location = new Point(0,108);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void LoadLogicalDisks()
        {
            foreach (Disk disk in DiskLoader.LoadLogicalVolumes())
            {
                string diskName = disk.ToString().Substring(2);
                ListViewItem item = new ListViewItem();
                item.Text = disk.ToString().Replace(diskName, "\\");
                item.Tag = disk;
                item.ImageKey = "HDD";
                lsvDisk.Items.Add(item);
            }
        }

        private void labelX1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.TimeSetting)
            {
                case "week":
                    week.Checked = true;
                    break;
                case "month":
                    month.Checked = true;
                    break;
                case "year":
                    cbYear.Checked = true;
                    break;
                case "alltime":
                    all.Checked = true;
                    break;
                default:
                    break;
            }
            Result = DiskResult.Yes;
            this.Close();
        }

        private void sideHelp_Click(object sender, EventArgs e)
        {

        }

        #region Language and last Modified

        #region switch language
        private void Language()
        {
            BindingList<ItemLanguage> item = new BindingList<ItemLanguage>();
            item.Clear();
            item.Add(new ItemLanguage(english, "en-US", "RecoveryThisFile.Language.en"));
            item.Add(new ItemLanguage(french, "fr-FR", "RecoveryThisFile.Language.fr"));
            item.Add(new ItemLanguage(german, "de-DE", "RecoveryThisFile.Language.ge"));
            item.Add(new ItemLanguage(spanish, "es-US", "RecoveryThisFile.Language.sp"));
            item.Add(new ItemLanguage(japanese, "ja-JP", "RecoveryThisFile.Language.ja"));

            cbLanguage.DataSource = item;
            cbLanguage.DisplayMember = "ItemName";
            cbLanguage.ValueMember = "Locale";

        }

        private void swich_language(string locale, string rsName)
        {
            CultureInfo cul = new CultureInfo(locale);
            Assembly a = Assembly.Load("RecoveryThisFile");
            ResourceManager res_man = new ResourceManager(rsName, a);

            this.all.Text = "<font size=\"+2\">" + res_man.GetString("strOption", cul) + "</font>";
            this.month.Text = "<font size=\"+2\">" + res_man.GetString("strOption1", cul) + "</font>";
            this.week.Text = "<font size=\"+2\">" + res_man.GetString("strOption2", cul) + "</font>";

            this.lbgen1.Text = "<span align =\"left\"><b><font size=\"+3\">" + res_man.GetString("strOption3") + "</font></b></span>";

            /*  if (comboBox1.SelectedItem.ToString() != "1")
              {
                  this.labelX2.Text = "<font size =\"+2\">" + res_man.GetString("strOption4a", cul) + "</font>";
              }
              else
              {
                  this.labelX2.Text = "<font size =\"+2\">" + res_man.GetString("strOption4", cul) + "</font>";
              }*/

            this._years = res_man.GetString("strOption4a", cul);
            this._year = res_man.GetString("strOption4", cul);

            this.cbYear.Text = "<font size=\"+2\">" + res_man.GetString("strOption5", cul) + "</font>";

            this.btnOK.Text = res_man.GetString("strSave", cul);
            this.btnBrowse.Text = res_man.GetString("strB", cul);
            this.btnNo.Text = res_man.GetString("strCancel", cul);

            this.lbLanguage.Text = "<font size=\"+2\">" + res_man.GetString("strLanguage") + "</font>";
            this.cbscan.Text = res_man.GetString("strAlldisk", cul);

            english = res_man.GetString("strEnglish", cul);
            spanish = res_man.GetString("strSpanish", cul);
            french = res_man.GetString("strFrench", cul);
            german = res_man.GetString("strGerman", cul);
            japanese = res_man.GetString("strJapanese", cul);

            labelX4.Text = res_man.GetString("strHelp4", cul);
            labelX5.Text = res_man.GetString("strHelp5", cul);
            labelX5.Image = (System.Drawing.Image)res_man.GetObject("img1", cul);
            labelX6.Text = res_man.GetString("strHelp6", cul);
            labelX7.Text = res_man.GetString("strHelp7", cul);
            labelX8.Text = res_man.GetString("strHelp8", cul);
            labelX8.Image = (System.Drawing.Image)res_man.GetObject("img2", cul);
            labelX9.Text = res_man.GetString("strHelp9", cul);
            labelX10.Text = res_man.GetString("strHelp10", cul);
            labelX12.Text = res_man.GetString("strHelp12", cul);
            labelX12.Image = (System.Drawing.Image)res_man.GetObject("img3", cul);
            labelX11.Text = res_man.GetString("strHelp11", cul);
            labelX13.Text = res_man.GetString("strHelp13", cul);
            labelX16.Text = res_man.GetString("strHelp16", cul);
            labelX14.Text = res_man.GetString("strHelp14", cul);
            labelX15.Text = res_man.GetString("strHelp15", cul);

            dgHeader = res_man.GetString("strWarning", cul);
            dgMessage = "<span align=\"center\">" + res_man.GetString("strMessage16", cul) + "</span>";
            dgOK = res_man.GetString("strOK", cul);
            dgCancel = res_man.GetString("strCancel", cul);
            dgText = res_man.GetString("strAgain", cul);

            this.folder = res_man.GetString("strFolder", cul);

            expandablePanel3.TitleText = res_man.GetString("strHelpUS", cul);
            expandablePanel4.TitleText = res_man.GetString("strHelpAD", cul);
            expandablePanel1.TitleText = res_man.GetString("strHelpCS", cul);

            Office2007ColorTable table = ((Office2007Renderer)GlobalManager.Renderer).ColorTable;
            SideNavColorTable ct = table.SideNav;
            ct.PanelBackColor = Color.White;
            ct.SideNavItem.Default.BackColors = new Color[] { Color.FromArgb(15, 71, 150) };
            ct.SideNavItem.Selected.BackColors = new Color[] { Color.FromArgb(123, 155, 199) };
            ct.BorderColors = new Color[] { Color.FromArgb(15, 71, 150) };

            this.sideNavItem1.Text = "<font size=\"+4\" color=\"#FFFFFF\">" + res_man.GetString("strSubscription", cul) + "</font>";
            this.sideHelp.Text = "<font size=\"+4\" color=\"#FFFFFF\">" + res_man.GetString("strHelp", cul) + "</font>";
            this.sideNavItem3.Text = "<font size=\"+4\" color=\"#FFFFFF\">" + res_man.GetString("strGeneral", cul) + "</font>";
            /* Language();*/
        }

        private void setfont(float font)
        {
            labelX4.Font = new Font("Time News Roman", font);
            labelX5.Font = new Font("Time News Roman", font);
            labelX6.Font = new Font("Time News Roman", font);
            labelX7.Font = new Font("Time News Roman", font);
            labelX8.Font = new Font("Time News Roman", font);
            labelX9.Font = new Font("Time News Roman", font);
            labelX10.Font = new Font("Time News Roman", font);
            labelX12.Font = new Font("Time News Roman", font);
            labelX11.Font = new Font("Time News Roman", font);
            labelX13.Font = new Font("Time News Roman", font);
            labelX16.Font = new Font("Time News Roman", font);
            labelX14.Font = new Font("Time News Roman", font);
            labelX15.Font = new Font("Time News Roman", font);
           
        }

        private void cbLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbLanguage.SelectedValue != null)
            {
                _language = cbLanguage.SelectedValue.ToString();
                Properties.Settings.Default.index = cbLanguage.SelectedIndex.ToString();
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        #region choose last modified
        private void month_CheckedChanged(object sender, EventArgs e)
        {
            timemodified = "month";
            Properties.Settings.Default.monthChecked = month.Checked;
            Properties.Settings.Default.weekChecked = false;
            Properties.Settings.Default.allChecked = false;
            Properties.Settings.Default.Save();
        }

        private void cbYear_CheckedChanged(object sender, EventArgs e)
        {
            timemodified = "year";
            Properties.Settings.Default.yearChecked = cbYear.Checked;
            Properties.Settings.Default.weekChecked = false;
            Properties.Settings.Default.monthChecked = false;
            Properties.Settings.Default.allChecked = false;
            Properties.Settings.Default.Save();
        }

        private void all_CheckedChanged(object sender, EventArgs e)
        {
            timemodified = "alltime";
            Properties.Settings.Default.allChecked = all.Checked;
            Properties.Settings.Default.weekChecked = false;
            Properties.Settings.Default.monthChecked = false;
            Properties.Settings.Default.Save();
        }

        private void week_CheckedChanged(object sender, EventArgs e)
        {
            timemodified = "week";
            Properties.Settings.Default.weekChecked = week.Checked;
            Properties.Settings.Default.monthChecked = false;
            Properties.Settings.Default.allChecked = false;
            Properties.Settings.Default.Save();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "1")
            {
                labelX2.Text = "<font size =\"+2\">" + _years + "</font>";
            }
            else { labelX2.Text = "<font size =\"+2\">" + _year + "</font>"; }

            // Scanner.year = comboBox1.SelectedItem.ToString();
            Properties.Settings.Default.lastYear = comboBox1.SelectedItem.ToString();
            Scanner.year = Properties.Settings.Default.lastYear;
            Properties.Settings.Default.Save();
        }
        #endregion

        private void SettingsSave()
        {
            if (week.Checked)
            {
                timemodified = "week";
                Properties.Settings.Default.TimeSetting = timemodified;
            }
            else if (month.Checked)
            {
                timemodified = "month";
                Properties.Settings.Default.TimeSetting = timemodified;
            }
            else if (all.Checked)
            {
                timemodified = "alltime";
                Properties.Settings.Default.TimeSetting = timemodified;
            }
            else if (cbYear.Checked)
            {
                timemodified = "year";
                Properties.Settings.Default.TimeSetting = timemodified;
                Properties.Settings.Default.lastYear = comboBox1.SelectedItem.ToString();
            }

            var item = (ItemLanguage)cbLanguage.SelectedItem;

            swich_language(item.Locale, item.RsName);

            this.pLocale = item.Locale;
            this.pRsName = item.RsName;


            Properties.Settings.Default.language = _language;
            Properties.Settings.Default.Save();
            //  this.Close();
            GC.Collect();
        }

        #endregion

        #region Check Register
        private void CheckRegister()
        {
            bool check = RangeTree.Database.CheckData();
            if (check)
            {
                btnRegister.Visible = false;
                labelX17.Text = "<font size=\"+ 4\" color=\"#000000\"> Your subscription is active </font>";
                labelX17.Symbol = "\uf06a";
                labelX17.SymbolColor = Color.Green;
            }
            else
            {
                btnRegister.Visible = true;
                labelX17.Text = "<font size=\"+ 4\" color=\"#000000\"> Your have not registered </font>";
                labelX17.Symbol = "\uf071";
                labelX17.SymbolColor = Color.Orange;
            }
        }
        #endregion
        string link = "", disk, name;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                disk = folderDialog.SelectedPath.Substring(0, 3);
                int index = folderDialog.SelectedPath.LastIndexOf("\\");
                name = folderDialog.SelectedPath.Substring(index + 1);
                link = folderDialog.SelectedPath.ToString();
                /* foreach(TreeNode tnode in diskTree.Nodes)
                 {
                     if(tnode.Text == disk) { }
                 }*/
                tbFolder.Text = name;
                if (cbscan.Checked)
                    cbscan.Checked = false;
                else
                {
                    foreach(ListViewItem it in lsvDisk.Items)
                    {
                        if (it.ImageKey == "HDD1")
                        {
                            it.ImageKey = "HDD";
                        }
                    }
                }
            }
        }

        private void cbscan_CheckedChanged(object sender, EventArgs e)
        {
            if (cbscan.Checked)
            {
                if (!string.IsNullOrEmpty(tbFolder.Text))
                    tbFolder.Text = string.Empty;
                for(int i=0; i< lsvDisk.Items.Count; i++)
                {
                    lsvDisk.Items[i].ImageKey = "HDD1";
                }
            }
            else
            {
                for (int i = 0; i < lsvDisk.Items.Count; i++)
                {
                    lsvDisk.Items[i].Selected = false ;
                    if (lsvDisk.Items[i].ImageKey=="HDD1")
                    {
                        lsvDisk.Items[i].ImageKey = "HDD";
                    }
                }
            }
        }

        private void lsvDisk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFolder.Text))
            {
                tbFolder.Text = string.Empty;
                name = string.Empty;
            }

            if (lsvDisk.SelectedItems.Count != 0)
            {
                if (lsvDisk.SelectedItems[0].ImageKey == "HDD1")
                {
                    lsvDisk.SelectedItems[0].ImageKey = "HDD";
                }
                else
                {
                    lsvDisk.SelectedItems[0].ImageKey = "HDD1";
                }
            }
        }

        private void sideNavItem3_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register popup = new Register();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog(this);
            if (popup.Result == DialogResult.OK)
            {
                CheckRegister();
            }
            //popup.BringToFront();
            popup.Dispose();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.TimeSetting)
            {
                case "week":
                    week.Checked = true;
                    break;
                case "month":
                    month.Checked = true;
                    break;
                case "year":
                    cbYear.Checked = true;
                    break;
                case "alltime":
                    all.Checked = true;
                    break;
                default:
                    break;
            }
            Result = DiskResult.Yes;
            this.Close();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            var culture = Properties.Settings.Default.language;
            switch (culture)
            {
                case "en-US":
                    swich_language("en-US", "RecoveryThisFile.Language.en");
                    setfont(11);
                    Language();
                    cbLanguage.SelectedIndex = 0;
                    break;
                case "es-US":
                    swich_language("es-US", "RecoveryThisFile.Language.sp");
                    setfont(11);
                    Language();
                    cbLanguage.SelectedIndex = 3;
                    break;
                case "fr-FR":
                    swich_language("fr-FR", "RecoveryThisFile.Language.fr");
                    setfont(11);
                    Language();
                    cbLanguage.SelectedIndex = 1;
                    break;
                case "de-DE":
                    swich_language("de-DE", "RecoveryThisFile.Language.ge");
                    setfont(11);
                    Language();
                    cbLanguage.SelectedIndex = 2;
                    break;
                case "ja-JP":
                    swich_language("ja-JP", "RecoveryThisFile.Language.ja");
                    setfont(11);
                    Language();
                    cbLanguage.SelectedIndex = 4;
                    break;
            }
        }

        private void expandablePanel3_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            if (e.NewExpandedValue == false)
            {
                expandablePanel4.Location = new System.Drawing.Point(0, expandablePanel4.Location.Y - 1912);
                expandablePanel1.Location = new System.Drawing.Point(5, expandablePanel1.Location.Y - 1912);
            }
            else
            {
                expandablePanel4.Location = new System.Drawing.Point(0, expandablePanel4.Location.Y + 1912);
                expandablePanel1.Location = new System.Drawing.Point(5, expandablePanel1.Location.Y + 1912);
            }
        }

        private void sideNavItem1_Click(object sender, EventArgs e)
        {

        }

        private void FrmSettings_Shown(object sender, EventArgs e)
        {
            if (IsShow)
            {
                cbscan.Checked = false;
                DialogNew dgNew = new DialogNew(dgHeader, dgMessage, dgText, dgOK, dgCancel);
                dgNew.StartPosition = FormStartPosition.CenterScreen;
                dgNew.ShowDialog(this);
            }
        }

        ToolTip tt = new ToolTip();
        private void tbFolder_MouseHover(object sender, EventArgs e)
        {
            Control control = GetChildAtPoint(tbFolder.Location);
            if (control != null)
            {
                if (tbFolder.Text.Length > 0)
                {
                    tt.ToolTipTitle = folder;
                    tt.ToolTipIcon = ToolTipIcon.Info;
                    tt.UseFading = true;
                    tt.UseAnimation = true;
                    tt.IsBalloon = true;

                    tt.ShowAlways = true;

                    tt.AutoPopDelay = 1000;
                    tt.InitialDelay = 500;
                    tt.ReshowDelay = 500;

                    tt.SetToolTip(tbFolder, link);
                }
            }
        }

        private void tbfolderbr_MouseLeave(object sender, EventArgs e)
        {
            tt.Hide(tbFolder);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SettingsSave();
            GetFilseSystemStore();
            this.Close();
        }

        private void GetFilseSystemStore()
        {
            this.FileSystemStore.Clear();
            this.ListPath.Clear();
            if (cbscan.Checked)
            {
                foreach (ListViewItem item in lsvDisk.Items)
                {
                    if (item.ImageKey=="HDD1")
                    {
                        FileSystemStore.Add((IFileSystemStore)item.Tag);
                        ListPath.Add(item.Text.Replace("\\\\", "\\"));
                      //  System.Windows.Forms.MessageBox.Show(item.Text);
                    }
                }
                Drive = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(tbFolder.Text)) // chon 1 o dia
                {
                    foreach (ListViewItem item in lsvDisk.Items)
                    {
                        if (item.ImageKey=="HDD1")
                        {
                            key = item.ImageKey;
                            id = item.Text;
                            FileSystemStore.Add((IFileSystemStore)item.Tag);
                            ListPath.Add(item.Text.Replace("\\\\", "\\"));
                        }
                    }

                    if(FileSystemStore.Count == 1)
                    {
                        Drive = true;
                    }
                    else
                    {
                        Drive = false;
                    }
                }
                else
                {
                    foreach(ListViewItem item in lsvDisk.Items) // tim theo thu muc
                    {
                        if(item.Text == disk)
                        {
                            FileSystemStore.Add((IFileSystemStore)item.Tag);
                        }
                        item.Selected = false;
                    }
                    Drive = true;
                } 
            }
            Result = DiskResult.Yes;
        }

    }

    public class ItemLanguage
    {
        public string ItemName { get; set; }
        public string Locale { get; set; }
        public string RsName { get; set; }
        public ItemLanguage(string itemname, string locale, string rsname)
        {
            this.ItemName = itemname;
            this.Locale = locale;
            this.RsName = rsname;
        }
    }

}
