using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveCharacterMonitor
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(Settings s)
            : this()
        {
            m_settings = s;
        }

        private Settings m_settings;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_settings.MinimizeToTray = cbMinimizeToTray.Checked;
            m_settings.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cbMinimizeToTray.Checked = m_settings.MinimizeToTray;
        }
    }
}