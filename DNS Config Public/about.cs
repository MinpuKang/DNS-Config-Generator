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



        private void author_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 指定要打开的网站地址
            string websiteUrl = "https://github.com/MinpuKang";

            // 使用默认的 Web 浏览器打开网站
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = websiteUrl,
                UseShellExecute = true
            });
        }

        private void linkLabel_issue_report_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 指定要打开的网站地址
            string websiteUrl = "https://github.com/MinpuKang/DNS-Config-Generator/issues";

            // 使用默认的 Web 浏览器打开网站
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = websiteUrl,
                UseShellExecute = true
            });
        }
    }
}