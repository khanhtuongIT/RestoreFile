using System;
using System.Drawing;

namespace RecoveryThisFile
{
    public partial class Dialog : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        public Dialog(string message, bool enable1, bool enable2, bool enable3,string Label)
        {
            InitializeComponent();
            labelItem1.Text = Label;
            lbMessage.Text = message;
            btnOK.Visible = enable1;
            btnOK1.Visible = enable2;
            btnCancel.Visible = enable3;
        }

        public Dialog(string message, bool enable1, bool enable2, bool enable3, string Label, bool enable, string label1, string label2)
        {
            InitializeComponent();
            labelItem1.Text = Label;
            lbMessage.Text = message;
            btnOK.Visible = enable1;
            btnOK.Text = "<font size = \"+4\" >" + label1 + "</font>"; ;
            btnOK1.Visible = enable2;
            btnCancel.Visible = enable3;
            btnCancel.Text = "<font size = \"+4\" >" + label2 + "</font>";
          
        }

        public Dialog(string _message)
        {
            InitializeComponent();
            this.Height = 168;
            lbMessage.Text = _message;
            lbMessage.Height = 70;
            lbMessage.TextAlignment = StringAlignment.Center;
            lbMessage.TextLineAlignment = StringAlignment.Near;
            btnOK.Visible = true;
            btnOK1.Visible = false;
            btnCancel.Visible = true;
        }

        private void Dialog_Load(object sender, EventArgs e)
        {

        }
    }
}