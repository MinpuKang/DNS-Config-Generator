using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNS_Config
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void mail_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string ToUsers = "kangminpu@126.com";
            string Title = "[Report]:DNS Config Advanced Report";
            //string Body = "&body=Hello Minpu \r\n Here are some suggestions:\r\n";
            string s_mail = "mailto:" + ToUsers + "?subject=" + Title;
            System.Diagnostics.Process.Start(s_mail);
        }
    }
}