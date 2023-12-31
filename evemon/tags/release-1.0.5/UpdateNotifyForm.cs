using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveCharacterMonitor
{
    public partial class UpdateNotifyForm : Form
    {
        public UpdateNotifyForm()
        {
            InitializeComponent();
        }

        private Settings m_settings;
        private UpdateAvailableEventArgs m_args;

        public UpdateNotifyForm(Settings settings, UpdateAvailableEventArgs args)
            :this()
        {
            m_args = args;
            m_settings = settings;
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Are you sure you want to ignore this update? You will not " +
                "be prompted again until a newer version is released.",
                "Ignore Update?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
                return;
            m_settings.IgnoreUpdateVersion = m_args.NewestVersion.ToString();
            m_settings.Save();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(m_args.UpdateUrl);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnLater_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateNotifyForm_Load(object sender, EventArgs e)
        {
        }

        private void UpdateNotifyForm_Shown(object sender, EventArgs e)
        {
            textBox1.Text = m_args.UpdateMessage;
            label1.Text = String.Format(label1.Text, m_args.CurrentVersion, m_args.NewestVersion);
        }
    }
}