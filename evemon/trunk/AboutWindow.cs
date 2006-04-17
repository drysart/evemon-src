using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Reflection;

namespace EveCharacterMonitor
{
    public partial class AboutWindow : EVEMonForm
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {
            Version currentVersion = new Version("0.0.0.0");
            foreach (Attribute a in Assembly.GetExecutingAssembly().GetCustomAttributes(false))
            {
                if (a is AssemblyFileVersionAttribute)
                {
                    AssemblyFileVersionAttribute ava = a as AssemblyFileVersionAttribute;
                    currentVersion = new Version(ava.Version);
                }
            }

            lblVersion.Text = String.Format(lblVersion.Text, currentVersion.ToString());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}