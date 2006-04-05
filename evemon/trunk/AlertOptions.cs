using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveCharacterMonitor
{
    public partial class AlertOptions : Form
    {
        public AlertOptions()
        {
            InitializeComponent();
        }

        public AlertOptions(Settings settings)
            : this()
        {
            m_settings = settings;
        }

        private Settings m_settings;

        private void AlertOptions_Load(object sender, EventArgs e)
        {
            cbSendEmail.Checked = m_settings.EnableEmailAlert;
            tbMailServer.Text = m_settings.EmailServer;
            tbFromAddress.Text = m_settings.EmailFromAddress;
            tbToAddress.Text = m_settings.EmailToAddress;

            UpdateDisables();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisables();
        }

        private void UpdateDisables()
        {
            tlpEmailSettings.Enabled = cbSendEmail.Checked;
            btnTestEmail.Enabled = cbSendEmail.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_settings.EnableEmailAlert = cbSendEmail.Checked;
            m_settings.EmailServer = tbMailServer.Text;
            m_settings.EmailFromAddress = tbFromAddress.Text;
            m_settings.EmailToAddress = tbToAddress.Text;
            m_settings.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnTestEmail_Click(object sender, EventArgs e)
        {
            if (!Emailer.SendTestMail(tbMailServer.Text, tbFromAddress.Text, tbToAddress.Text))
            {
                MessageBox.Show("The message failed to send.", "Mail Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("The message sent successfully. Please verify that the message was received.", "Mail Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}