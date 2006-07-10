using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

using EVEMon.Common;

namespace EVEMon
{
    public partial class AboutWindow : EVEMonForm
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = String.Format(lblVersion.Text, currentVersion.ToString());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://evemon.evercrest.com/");
        }
    }
}
