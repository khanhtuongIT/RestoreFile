using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace RecoveryThisFile
{
    public partial class EnterName : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        public string filname { get { return tbName.Text; } }
    
        CultureInfo cul;
        Assembly a;
        ResourceManager res_man;

        public EnterName()
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

        public void switch_language(string locate, string rsname)
        {
            this.cul = new CultureInfo(locate);
            this.a = Assembly.Load("RecoveryThisFile");
            this.res_man = new ResourceManager(rsname, a);
            this.btnOK.Text = res_man.GetString("strOK", cul);
            this.btnCancel.Text = res_man.GetString("strCancel", cul);
            labelX1.Text = res_man.GetString("strFilename", cul);
            lbError.Text = res_man.GetString("strMessage15", cul);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                lbError.Visible = true;
                lbError.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lbError.Visible = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void lbError_Click(object sender, EventArgs e)
        {

        }
    }
}
