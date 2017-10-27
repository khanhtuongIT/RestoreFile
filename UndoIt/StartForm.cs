
using KickassUndelete;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RecoveryThisFile
{
    public partial class StartForm : Form
    {
        private static StartForm _instance;
        public static StartForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StartForm();
                return _instance;
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        FrmSettings drive = new FrmSettings(false);

        private string message;
        private string warning;
        private string exit;
        private string stop;

        ResourceManager res_man;
        CultureInfo cul;

        public StartForm()
        {
            InitializeComponent();
            var culture = Properties.Settings.Default.language;
            this.panel1.Controls.Add(labelX2);
            SendMessage(this.Handle, WM_SETREDRAW, false, 0);
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
            SendMessage(this.Handle, WM_SETREDRAW, true, 0);
        }

        private void switch_language(string locale, string rsname)
        {
            cul = new CultureInfo(locale);
            Assembly a = Assembly.Load("RecoveryThisFile");
            res_man = new ResourceManager(rsname, a);

            lbSettings.Text = res_man.GetString("strSettings", cul);
            warning = res_man.GetString("strWarning", cul);
            message = res_man.GetString("strMessage2", cul);
            exit = res_man.GetString("strExit", cul);
            stop = res_man.GetString("strStop", cul);

           
            btnShowAll.Image = (Image)res_man.GetObject("scanall2", cul);
            btnShowAll.HoverImage = (Image)res_man.GetObject("scanall-hv", cul);
            btnName.Image = (Image)res_man.GetObject("name", cul);
            btnName.HoverImage = (Image)res_man.GetObject("name-hv", cul);
            btnSearch.Image = (Image)res_man.GetObject("type", cul);
            btnSearch.HoverImage = (Image)res_man.GetObject("type-hv", cul);
            labelX2.Text = res_man.GetString("strHeader", cul);
           
        }

        private void Show(Panel pnl)
        {
            ButtonHide();
            pnl.Show();
        }

        private void Hide(Panel pnl)
        {
            ButtonHide();
            pnl.Hide();
            ButtonShow();
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            EnterName enDialog = new EnterName();
            enDialog.StartPosition = FormStartPosition.CenterParent;
            enDialog.ShowDialog();
            if (enDialog.DialogResult == DialogResult.OK)
            {
                if (drive.FileSystemStore.Count > 0)
                {
                    SendMessage(this.Handle, WM_SETREDRAW, false, 0);
                    if (drive.Drive == true)
                    {
                        Scanner.FileName = enDialog.filname;
                        FrmFileName.Instance.FilePath = drive.NodePath;
                        FrmFileName.Instance.drive = true;
                        FrmFileName.Instance.FileName = enDialog.filname;
                        FrmFileName.Instance.FileSystemStore = drive.FileSystemStore;
                        FrmFileName.Instance.fileView.Visible = false;

                        FrmFileName.Instance.SetFileSystem(drive.FileSystemStore.ElementAt(0));
                        DeleteFileViewName.IsChecked = false;
                        if (string.IsNullOrEmpty(drive.Disk))
                        {
                            if (drive.SelectedImageKey == "HDD1")
                            {
                                DeleteFileViewName.ID = "";
                                FrmFileName.Instance.ID = "";
                                Scanner.NodePath = drive.ID;
                                if (FrmFileName.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Count > 0)
                                {
                                    FrmFileName.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Clear();
                                }
                                FrmFileName.Instance._scanners[(drive.FileSystemStore.ElementAt(0)).FS].StartScanType();
                                FrmFileName.Instance._deletedViewers[(drive.FileSystemStore.ElementAt(0)).FS].Filter();
                            }
                            else
                            {
                                DeleteFileViewName.ID = drive.ID + "\\";
                                FrmFileName.Instance.ID = drive.ID + "\\";
                                Scanner.NodePath = drive.ID;
                                if (FrmFileName.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Count > 0)
                                {
                                    FrmFileName.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Clear();
                                }
                                FrmFileName.Instance._scanners[(drive.FileSystemStore.ElementAt(0)).FS].StartScanType();
                                FrmFileName.Instance._deletedViewers[(drive.FileSystemStore.ElementAt(0)).FS].Filter();
                            }

                            FrmFileName.Instance._scanners[drive.FileSystemStore.ElementAt(0).FS].StartScanName();
                        }
                        else
                        {
                            DeleteFileViewName.ID = drive.Disk;
                            Scanner.NodePath = drive.Link;
                            FrmFileType.Instance._scanners[(drive.FileSystemStore.ElementAt(0)).FS].StartScanType();
                            FrmFileType.Instance._deletedViewers[(drive.FileSystemStore.ElementAt(0)).FS].Filter();
                        }
                        FrmFileName.Instance.btnScan.Text = stop;
                        FrmFileName.Instance.btnScan.Tag = "stop";
                        FrmFileName.Instance.bRestoreFiles.Hide();
                    }
                    else
                    {
                        FrmFileName.Instance.FileSystemStore = drive.FileSystemStore;
                        FrmFileName.Instance.listPath = drive.ListPath;
                        DeleteFileViewType.IsChecked = true;
                        FrmFileName.Instance.FileName = enDialog.filname;
                        FrmFileName.Instance.drive = false;
                        FrmFileName.Instance.progressBar1.Maximum = (10000 * drive.FileSystemStore.Count);
                        FrmFileName.Instance.progressBar1.Visible = true;
                        FrmFileName.Instance.IV = 0;
                        FrmFileName.Instance.JV = 0;
                        FrmFileName.Process_Status = 0;
                        DeleteFileViewName.ID = "";
                        FrmFileName.Instance.btnScan.Text = stop;
                        FrmFileName.Instance.btnScan.Tag = "stop";
                        FrmFileName.Instance.bRestoreFiles.Hide();
                        FrmFileName.Instance.timer1.Start();
                    }
                    DeleteViewerLoad(pnlName, FrmFileName.Instance);
                    Show(pnlName);
                    pnlAll.Hide();
                    pnlType.Hide();
                    SendMessage(this.Handle, WM_SETREDRAW, true, 0);
                    this.Refresh();
                }
                else
                {
                    CountDrive(Properties.Settings.Default.showagain);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {      
            ChooseType ctDialog = new ChooseType();
            ctDialog.StartPosition = FormStartPosition.CenterScreen;
            ctDialog.ShowDialog();
            if (ctDialog.DialogResult == DialogResult.OK)
            {
                if (drive.FileSystemStore.Count > 0)
                {
                    SendMessage(this.Handle, WM_SETREDRAW, false, 0);
                    if (drive.Drive == true)
                    {

                        Scanner.FileType = ctDialog.Type;
                        FrmFileType.Instance.fileView.Visible = false;
                        FrmFileType.Instance.progressBar1.Visible = false;

                        FrmFileType.Instance.SetFileSystem(drive.FileSystemStore.ElementAt(0));
                        DeletedAll_File.IsChecked = false;
                        if (string.IsNullOrEmpty(drive.Disk))
                        {
                            if (drive.SelectedImageKey == "HDD1")
                            {
                                DeleteFileViewType.ID = "";
                                Scanner.NodePath = drive.ID;
                                if (FrmFileType.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Count > 0)
                                {
                                    FrmFileType.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Clear();
                                }
                                FrmFileType.Instance._scanners[(drive.FileSystemStore.ElementAt(0)).FS].StartScanType();
                            }
                            else
                            {
                                DeleteFileViewType.ID = drive.ID + "\\";
                                Scanner.NodePath = drive.ID;
                                if (FrmFileType.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Count > 0)
                                {
                                    FrmFileType.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Clear();
                                }
                                FrmFileType.Instance._scanners[(drive.FileSystemStore.ElementAt(0)).FS].StartScanType();
                                FrmFileType.Instance._deletedViewers[(drive.FileSystemStore.ElementAt(0)).FS].Filter();
                            }
                        }
                        else
                        {
                            DeleteFileViewType.ID = drive.Disk;
                            Scanner.NodePath = drive.Link;
                            FrmFileType.Instance._scanners[(drive.FileSystemStore.ElementAt(0)).FS].StartScanType();
                            FrmFileType.Instance._deletedViewers[(drive.FileSystemStore.ElementAt(0)).FS].Filter();
                        }

                        FrmFileType.Instance.btnScan.Text = stop;
                        FrmFileType.Instance.btnScan.Tag = "stop";
                        FrmFileType.Instance.bRestoreFiles.Hide();
                    }
                    else
                    {
                        FrmFileType.Instance.FileSystemStore = drive.FileSystemStore;
                        FrmFileType.Instance.listPath = drive.ListPath;
                        DeleteFileViewType.IsChecked = true;
                        FrmFileType.Instance.Type = ctDialog.Type;
                        FrmFileType.Instance.drive = false;
                        FrmFileType.Instance.progressBar1.Maximum = (10000 * drive.FileSystemStore.Count);
                        FrmFileType.Instance.progressBar1.Visible = true;
                        FrmFileType.Instance.IV = 0;
                        FrmFileType.Instance.JV = 0;
                        FrmFileType.Process_Status = 0;
                        DeleteFileViewType.ID = "";
                        FrmFileType.Instance.btnScan.Text = stop;
                        FrmFileType.Instance.btnScan.Tag = "stop";
                        FrmFileType.Instance.bRestoreFiles.Hide();
                        FrmFileType.Instance.timer1.Start();
                    }

                    DeleteViewerLoad(pnlType, FrmFileType.Instance);
                    Show(pnlType);
                    pnlName.Hide();
                    pnlAll.Hide();
                    SendMessage(this.Handle, WM_SETREDRAW, true, 0);
                    this.Refresh();
                }
                else
                {
                    CountDrive(Properties.Settings.Default.showagain);
                }
            }
          
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            if (drive.Result == DiskResult.Yes)
            {
                if (drive.FileSystemStore.Count > 0) {
                    SendMessage(this.Handle, WM_SETREDRAW, false, 0);
                    if (drive.FileSystemStore != null)
                    {
                        if (drive.Drive == true)
                        {
                            FrmShowAllcs.Instance.drive = true;
                            FrmShowAllcs.Instance.FileSystemStore = drive.FileSystemStore;
                            FrmShowAllcs.Instance.progressBar1.Visible = false;
                            FrmShowAllcs.Instance.fileView.Visible = false;
                            FrmShowAllcs.Instance.panel3.Visible = false;

                            FrmShowAllcs.Instance.SetFileSystem(drive.FileSystemStore.ElementAt(0));
                            DeletedAll_File.IsChecked = false;
                            if (string.IsNullOrEmpty(drive.Disk))
                            {
                                if (drive.SelectedImageKey == "HDD1")
                                {
                                    try
                                    {
                                        DeletedAll_File.ID = "";
                                        Scanner.NodePath = drive.ID;
                                        if (FrmShowAllcs.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Count > 0)
                                        {
                                            FrmShowAllcs.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Clear();
                                        }
                                        FrmShowAllcs.Instance._scanners[(drive.FileSystemStore.ElementAt(0)).FS].StartScan();
                                        //  System.Windows.Forms.MessageBox.Show(drive.ID);
                                    }
                                    catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
                                }
                            }
                            else
                            {
                                DeletedAll_File.ID = drive.Disk;
                                Scanner.NodePath = drive.Link;
                                if (FrmShowAllcs.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Count > 0)
                                {
                                    FrmShowAllcs.Instance._deletedViewers[drive.FileSystemStore.ElementAt(0).FS]._files.Clear();
                                }
                                FrmShowAllcs.Instance._scanners[(drive.FileSystemStore.ElementAt(0)).FS].StartScan();
                                FrmShowAllcs.Instance._deletedViewers[(drive.FileSystemStore.ElementAt(0)).FS].Filter2();
                            }

                            FrmShowAllcs.Instance.btnScan.Text = stop; ;
                            FrmShowAllcs.Instance.btnScan.Tag = "stop";
                            FrmShowAllcs.Instance.bRestoreFiles.Hide();
                        }
                        else
                        {
                            FrmShowAllcs.Instance.FileSystemStore = drive.FileSystemStore;
                            FrmShowAllcs.Instance.listPath = drive.ListPath;
                            DeletedAll_File.IsChecked = true;
                            FrmShowAllcs.Instance.drive = false;
                            FrmShowAllcs.Instance.progressBar1.Maximum = (10000 * drive.FileSystemStore.Count);
                            FrmShowAllcs.Instance.progressBar1.Visible = true;
                            FrmShowAllcs.Instance.IV = 0;
                            FrmShowAllcs.Instance.JV = 0;
                            FrmShowAllcs.Process_Status = 0;
                            DeletedAll_File.ID = "";
                            FrmShowAllcs.Instance.btnScan.Text = stop;
                            FrmShowAllcs.Instance.btnScan.Tag = "stop";
                            FrmShowAllcs.Instance.bRestoreFiles.Hide();
                            FrmShowAllcs.Instance.timer2.Start();

                        }
                        DeleteViewerLoad(pnlAll, FrmShowAllcs.Instance);
                        Show(pnlAll);
                        pnlName.Hide();
                        pnlType.Hide();
                        SendMessage(this.Handle, WM_SETREDRAW, true, 0);
                    }
                    this.Refresh();
                }
                else
                {
                    CountDrive(Properties.Settings.Default.showagain);
                }
            }

        }

        private void CountDrive(bool _check)
        {
            FrmSettings setting = new FrmSettings(_check);
            setting.StartPosition = FormStartPosition.CenterScreen;
            setting.ShowDialog(this);
            if (setting.DialogResult == DialogResult.OK)
            {
                ChangeLanguage(setting.pLocale, setting.pRsName);
                drive.FileSystemStore = setting.FileSystemStore;
                drive.ListPath = setting.ListPath;
            }
        }
        private void help_Click(object sender, EventArgs e)
        {
            Help popup = new Help();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog(this);
            //popup.BringToFront();
            popup.Dispose();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            if ((string)FrmShowAllcs.Instance.btnScan.Tag == "stop" 
                || (string)FrmFileName.Instance.btnScan.Tag == "stop" 
                || (string)FrmFileType.Instance.btnScan.Tag == "stop")
            {
                using (Dialog dialog = new Dialog(message, true, false, true, exit))
                {
                    dialog.StartPosition = FormStartPosition.CenterScreen;
                    DialogResult result = dialog.ShowDialog();
                    switch (result)
                    {
                        case DialogResult.OK:
                            Invoke(new Action(() =>
                            {
                                if ((string)FrmFileType.Instance.btnScan.Tag == "stop") { FrmFileType.Instance.StopScanningWhenFormLeave(sender, e); }
                                else if ((string)FrmFileName.Instance.btnScan.Tag == "stop") { FrmFileName.Instance.StopScanningWhenFormLeave(sender, e); }
                                else if ((string)FrmShowAllcs.Instance.btnScan.Tag == "stop") { FrmShowAllcs.Instance.StopScanningWhenFormLeave(sender, e); }
                                GC.Collect();
                                Application.ExitThread();
                            }));
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                    dialog.Dispose();
                }
                Application.ExitThread();
            }
            else
            {
                Application.ExitThread();
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

        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void gbAll_Click(object sender, EventArgs e)
        {
            if ((string)FrmShowAllcs.Instance.btnScan.Tag == "stop")
            {
                using (Dialog dialog = new Dialog(message, true, false, true, warning))
                {
                    dialog.StartPosition = FormStartPosition.CenterParent;
                    DialogResult result = dialog.ShowDialog();
                    switch (result)
                    {
                        case DialogResult.OK:
                            FrmShowAllcs.Instance.StopScanningWhenFormLeave(sender, e);
                            Hide(pnlAll);
                            pnlName.Hide();
                            pnlType.Hide();
                            ButtonShow();
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
            }
            else
            {
                Hide(pnlAll);
                pnlName.Hide();
                pnlType.Hide();
                ButtonShow();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if ((string)FrmFileName.Instance.btnScan.Tag == "stop")
            {
                using (Dialog dialog = new Dialog(message, true, false, true, warning))
                {
                    dialog.StartPosition = FormStartPosition.CenterParent;
                    DialogResult result = dialog.ShowDialog();
                    switch (result)
                    {
                        case DialogResult.OK:
                            FrmFileName.Instance.StopScanningWhenFormLeave(sender, e);
                            Hide(pnlName);
                            pnlAll.Hide();
                            pnlType.Hide();
                            ButtonShow();
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
            }
            else
            {
                Hide(pnlName);
                pnlAll.Hide();
                pnlType.Hide();
                ButtonShow();
            }
        } 

        private void gbType(object sender, EventArgs e)
        {
            if ((string)FrmFileType.Instance.btnScan.Tag == "stop")
            {
                using (Dialog dialog = new Dialog(message, true, false, true, warning))
                {
                    dialog.StartPosition = FormStartPosition.CenterParent;
                    DialogResult result = dialog.ShowDialog();
                    switch (result)
                    {
                        case DialogResult.OK:
                            FrmFileType.Instance.StopScanningWhenFormLeave(sender, e);
                            Hide(pnlType);
                            pnlAll.Hide();
                            pnlName.Hide();
                            ButtonShow();
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
            }
            else
            {
                Hide(pnlType);
                pnlAll.Hide();
                pnlName.Hide();
                ButtonShow();
            }
        }

        private void ButtonHide()
        {
            btnShowAll.Hide();
            btnName.Hide();
            btnSearch.Hide();
        }

        private void ButtonShow()
        {
            btnShowAll.Show();
            btnName.Show();
            btnSearch.Show();
        }

        private void RefreshPanleAll()
        {
            this.pnlAll.Controls.Clear();
        }
        private void DeleteViewerLoad(Panel panel, UserControl uControl)
        {
 
            if (!panel.Controls.Contains(uControl))
            {
                int MARGIN = 35;
                panel.Controls.Add(uControl);
                uControl.Left = MARGIN;
                uControl.Top = 50;
                uControl.Width = panel.Width - MARGIN * 2;
                uControl.Height = panel.Height - MARGIN * 2;
                uControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                uControl.BringToFront();
            }
            else uControl.BringToFront();
        }

       
        private void StartForm_Load(object sender, EventArgs e)
        {
            
        }

        private void pnlAll_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void lbSettings_Click(object sender, EventArgs e)
        {
            drive.StartPosition = FormStartPosition.CenterScreen;
            drive.ShowDialog(this);
            if (drive.DialogResult == DialogResult.OK)
            {
                ChangeLanguage(drive.pLocale, drive.pRsName);
            }
        }

        private void ChangeLanguage(string pLocale,string pRsName)
        {
            switch_language(pLocale, pRsName);
            FrmFileName.Instance.switch_language(pLocale, pRsName);
            FrmFileType.Instance.switch_language(pLocale, pRsName);
            FrmShowAllcs.Instance.switch_language(pLocale, pRsName);
        }
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void StartForm_Shown(object sender, EventArgs e)
        {
            DowloadLatestVersion();
            Application.DoEvents();
        }

        public void DowloadLatestVersion()
        {
            string latest_verion;
            try
            {
                System.Xml.Linq.XDocument xdocument = System.Xml.Linq.XDocument.Load("http://RecoverThisfile.com/infor/version.xml");
                System.Xml.Linq.XElement xelement = xdocument.Element("CurrentVersion");
                latest_verion = xelement.Value;
            }
            catch { latest_verion = Check_Key.version_app; }

            if (Check_Key.version_app.CompareTo(latest_verion) < 0)
            {
                string message_version = "<span align=\"center\">" + res_man.GetString("strMessage13", cul) + "</span>";
                using (Dialog dialog = new Dialog(message_version, true, false, true, "Recover this file", true, res_man.GetString("strUpdate", cul), res_man.GetString("strLater", cul)))
                {
                    dialog.StartPosition = FormStartPosition.CenterParent;
                    DialogResult result = dialog.ShowDialog(this);
                    switch (result)
                    {
                        case DialogResult.OK:
                            try { System.Diagnostics.Process.Start("http://recoverthisfile.com"); }
                            catch { System.Diagnostics.Process.Start("iexplore.exe"); }
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                    dialog.Dispose();
                }
            }
        }
    }
}
