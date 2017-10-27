using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Reflection;
using System.IO;
using RecoveryThisFile.RangeTree;
using System.Resources;
using System.Globalization;
using System.Drawing;

namespace RecoveryThisFile
{
    public partial class Help : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        CultureInfo cul;
        Assembly a;
        ResourceManager res_man;

        public Help()
        {
            InitializeComponent();
            var culture = Properties.Settings.Default.language;
            switch (culture)
            {
                case "en-US":
                    switch_language("en-US", "RecoveryThisFile.Language.en");
                    break;
                case "es-US":
                    switch_language("es-US", "RecoveryThisFile.Language.sp");
                    setfont(12);
                    break;
                case "fr-FR":
                    switch_language("fr-FR", "RecoveryThisFile.Language.fr");
                    setfont(11);
                    break;
                case "de-DE":
                    switch_language("de-DE", "RecoveryThisFile.Language.ge");
                    setfont(12);
                    break;
                case "ja-JP":
                    switch_language("ja-JP", "RecoveryThisFile.Language.ja");
                    setfont(12);
                    break;
            }
        }

        private void switch_language(string locate, string rsname)
        {
            this.cul = new CultureInfo(locate);
            this.a = Assembly.Load("RecoveryThisFile");
            this.res_man = new ResourceManager(rsname, a);

            labelX4.Text = res_man.GetString("strHelp4", cul);
            labelX5.Text = res_man.GetString("strHelp5", cul);
            labelX5.Image = (Image)res_man.GetObject("img1",cul);
            labelX6.Text = res_man.GetString("strHelp6", cul);
            labelX7.Text = res_man.GetString("strHelp7", cul);
            labelX8.Text = res_man.GetString("strHelp8", cul);
            labelX8.Image = (Image)res_man.GetObject("img2",cul);
            labelX9.Text = res_man.GetString("strHelp9", cul);
            labelX10.Text = res_man.GetString("strHelp10", cul);
            labelX12.Text = res_man.GetString("strHelp12", cul);
            labelX12.Image = (Image)res_man.GetObject("img3",cul);
            labelX11.Text = res_man.GetString("strHelp11", cul);
            labelX13.Text = res_man.GetString("strHelp13", cul);
            labelX16.Text = res_man.GetString("strHelp16", cul);
            labelX14.Text = res_man.GetString("strHelp14", cul);
            labelX15.Text = res_man.GetString("strHelp15", cul);
            expandablePanel3.TitleText = res_man.GetString("strHelpUS", cul);
            expandablePanel4.TitleText = res_man.GetString("strHelpAD", cul);
            expandablePanel1.TitleText = res_man.GetString("strHelpCS", cul);
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
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Mouse Down Event
        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void logo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
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
        #endregion

        private void Help_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void expandablePanel3_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            if(e.NewExpandedValue == false)
            {
                expandablePanel4.Location = new System.Drawing.Point(5, expandablePanel4.Location.Y - 1912);
                expandablePanel1.Location = new System.Drawing.Point(5, expandablePanel1.Location.Y - 1912);
            }
            else
            {
                expandablePanel4.Location = new System.Drawing.Point(5, expandablePanel4.Location.Y + 1912);
                expandablePanel1.Location = new System.Drawing.Point(5, expandablePanel1.Location.Y + 1912);
            }
        }

        private void labelX15_MarkupLinkClick(object sender, DevComponents.DotNetBar.MarkupLinkClickEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(e.HRef);
            try { System.Diagnostics.Process.Start(info); }
            catch { System.Diagnostics.Process.Start("iexplore.exe", info.FileName); }
        }

        private void expandablePanel5_Click(object sender, EventArgs e)
        {

        }

        private void labelX13_Click(object sender, EventArgs e)
        {

        }

        private void expandablePanel3_Click(object sender, EventArgs e)
        {

        }

        private void labelX6_Click(object sender, EventArgs e)
        {

        }

        private void labelX8_Click(object sender, EventArgs e)
        {

        }

        private void labelX4_Click(object sender, EventArgs e)
        {

        }

        private void expandablePanel4_Click(object sender, EventArgs e)
        {

        }

        private void Help_Load(object sender, EventArgs e)
        {

        }
    }
}
