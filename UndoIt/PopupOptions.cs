
using KickassUndelete;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace RecoveryThisFile
{
    public partial class PopupOptions : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        private string timemodified;
       // private string _language;
        private string _year;
        private string _years;

        public PopupOptions()
        {
            InitializeComponent();
            week.Checked = Properties.Settings.Default.weekChecked;
            month.Checked = Properties.Settings.Default.monthChecked;
            all.Checked = Properties.Settings.Default.allChecked;
            cbYear.Checked = Properties.Settings.Default.yearChecked;
           // Properties.Settings.Default.TimeSetting = timemodified;
            comboBox1.SelectedItem = Properties.Settings.Default.lastYear;

            var culture = Properties.Settings.Default.language;
            switch (culture)
            {
                case "en-US":
                    swich_language("en-US", "RecoveryThisFile.Language.en");
                    break;
                case "es-US":
                    swich_language("es-US", "RecoveryThisFile.Language.sp");
                    break;
                case "fr-FR":
                    swich_language("fr-FR", "RecoveryThisFile.Language.fr");
                    break;
                case "de-DE":
                    swich_language("de-DE", "RecoveryThisFile.Language.ge");
                    break;
                case "ja-JP":
                    swich_language("ja-JP", "RecoveryThisFile.Language.ja");
                    break;
            }
        }

        private void swich_language(string locale, string rsName)
        {
            CultureInfo cul = new CultureInfo(locale);
            Assembly a = Assembly.Load("RecoveryThisFile");
            ResourceManager res_man = new ResourceManager(rsName, a);

            this.all.Text = "<font size=\"+2\">" + res_man.GetString("strOption", cul) + "</font>";
            this.month.Text = "<font size=\"+2\">" + res_man.GetString("strOption1", cul) + "</font>";
            this.week.Text = "<font size=\"+2\">" + res_man.GetString("strOption2", cul) + "</font>";

            this.labelX1.Text = "<span align =\"center\"><b><font size=\"+3\">" + res_man.GetString("strOption3") + "</font></b></span>";

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
            this.labelItem2.Text = res_man.GetString("strOption6", cul);

            this.btnOK.Text = res_man.GetString("strSave", cul);
            this.btnCancel.Text = res_man.GetString("strCancel", cul);
            /* Language();*/
        }


        private void btnCancel_Click(object sender, EventArgs e)
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
            this.Close();
            GC.Collect();
        }

        private void week_CheckedChanged(object sender, EventArgs e)
        {
            timemodified = "week";      
            Properties.Settings.Default.weekChecked = week.Checked;
            Properties.Settings.Default.monthChecked = false;
            Properties.Settings.Default.allChecked = false;
            Properties.Settings.Default.Save();
        }

        private void month_CheckedChanged(object sender, EventArgs e)
        {
            timemodified = "month";   
            Properties.Settings.Default.monthChecked = month.Checked;
            Properties.Settings.Default.weekChecked = false;
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

        private void btnOK_Click(object sender, EventArgs e)
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
            Properties.Settings.Default.Save();
            this.Close();
            GC.Collect();    
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString() != "1")
            {
                labelX2.Text = "<font size =\"+2\">" + _years + "</font>";
            }
            else { labelX2.Text = "<font size =\"+2\">" + _year + "</font>"; }
           // Scanner.year = comboBox1.SelectedItem.ToString();
            Properties.Settings.Default.lastYear = comboBox1.SelectedItem.ToString();
            Scanner.year = Properties.Settings.Default.lastYear;
            Properties.Settings.Default.Save();
        }
    }
}