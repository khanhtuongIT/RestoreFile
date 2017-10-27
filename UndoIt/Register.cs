using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;
using System.Resources;

namespace RecoveryThisFile
{
    public partial class Register : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        ResourceManager res_man;
        CultureInfo cul;

        public DialogResult Result { get; set; }
        private string message4;
        private string message5;
        private string message6;
        private string message7;
        private string message8;
        private string message9;

        public Register()
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

        private void switch_language(string locale, string rsname)
        {
            this.cul = new CultureInfo(locale);
            Assembly a = Assembly.Load("RecoveryThisFile");
            this.res_man = new ResourceManager(rsname, a);

            labelItem2.Text = res_man.GetString("strRegister", cul);
            labelX1.Text = res_man.GetString("strNumber", cul);
            labelX2.Text = res_man.GetString("strEmail", cul);
            labelX3.Text = res_man.GetString("strMessage10", cul);
            btnPurcharse.Text = res_man.GetString("strPurchase", cul);
            btnRegister.Text = res_man.GetString("strRegister", cul);
            btnCancel.Text = res_man.GetString("strCancel", cul);
            message9 = res_man.GetString("strMessage9", cul);
            message8 = res_man.GetString("strMessage8", cul);
            message7 = res_man.GetString("strMessage7", cul);
            message6 = res_man.GetString("strMessage6", cul);
            message5 = res_man.GetString("strMessage5", cul);
            message4 = res_man.GetString("strMessage4", cul);
        }


        private void btnPurcharse_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://recoverthisfile.com/?index&purchase");
            }catch { System.Diagnostics.Process.Start("iexplore.exe"); }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (tbKey.Text == "" || tbEmail.Text == "")
            {
                Message(message9, Color.Red);
            }
            else
            {
                string license_key = "";
                license_key = Check_Key.CreateLicenseKey(Convert.ToString(Version.RECOVERTHISFILE) + "_" + (int)Version.Version_Key + "_" + tbEmail.Text.ToUpper());
                if (tbKey.Text == license_key)
                {
                    string processID = ComputerInfo.GetProcessprID();
                    saveSetting(tbEmail.Text.ToUpper(), license_key, processID);

                    labelItem2.Symbol = "\uf13e";
                    Message(message8, Color.Green);
                    Result = DialogResult.OK;
                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        Thread.Sleep(2000);
                        this.Close();
                    }));
                    t.Start();
                    GC.Collect();
                }
                else
                {
                    Message("<span align=\"center\">"+message7+"</span>", Color.Red);
                }
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
             this.Close();
             GC.Collect();
        }

        private void tbEmail_Leave(object sender, EventArgs e)
        {
            try
            {
                if (tbEmail.Text == (string.Empty)|| string.IsNullOrWhiteSpace(tbEmail.Text))
                {
                    Message(message6, Color.Red);
                }
                else
                {
                    var mail = new System.Net.Mail.MailAddress(tbEmail.Text);
                    lbinfo.Text = "";
                }
            }
            catch (FormatException ex)
            {
                Message(message4, Color.Red);

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void tbKey_Leave(object sender, EventArgs e)
        {
            if (tbKey.Text == string.Empty || string.IsNullOrWhiteSpace(tbKey.Text))
            {
                Message(message5, Color.Red);
            }
        }

        private void Message(string message, Color color)
        {
            lbinfo.TextAlignment = StringAlignment.Center;
            lbinfo.ForeColor = color;
            lbinfo.Text = message;
        }

        private void saveSetting(string email, string key, string processorid)
        {
            Properties.Settings.Default.mail = email;
            Properties.Settings.Default.Key = key;
            Properties.Settings.Default.CPUID = processorid;
            Properties.Settings.Default.Save();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}