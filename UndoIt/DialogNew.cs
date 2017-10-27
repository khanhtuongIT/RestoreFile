using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecoveryThisFile
{
    public partial class DialogNew : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        public DialogNew(string header, string message, string cbtext, string OK, string Cancel)
        {
            InitializeComponent();
            labelItem1.Text = header;
            lbmessage.Text = message;
            checkBoxX1.Text = cbtext;
            btnOK.Text = "<font size = \"+4\" >" + OK + "</font>";
            btnCancel.Text = "<font size = \"+4\" >" + Cancel + "</font>";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked)
            {
                Properties.Settings.Default.showagain = false;
                Properties.Settings.Default.Save();
            }
            this.Close();
        }
    }
}
