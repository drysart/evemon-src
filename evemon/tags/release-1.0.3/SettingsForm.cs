using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EveCharacterMonitor
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            if (Application.RenderWithVisualStyles)
                m_renderer = new VisualStyleRenderer(VisualStyleElement.Window.Dialog.Normal);
        }

        public SettingsForm(Settings s)
            : this()
        {
            m_settings = s;
        }

        private VisualStyleRenderer m_renderer;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!Application.RenderWithVisualStyles)
                return;

            m_renderer.DrawBackground(e.Graphics, this.ClientRectangle);
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
            m_settings.EnableEmailAlert = cbSendEmail.Checked;
            m_settings.EmailServer = tbMailServer.Text;
            m_settings.EmailFromAddress = tbFromAddress.Text;
            m_settings.EmailToAddress = tbToAddress.Text;
            m_settings.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cbMinimizeToTray.Checked = m_settings.MinimizeToTray;
            cbSendEmail.Checked = m_settings.EnableEmailAlert;
            tbMailServer.Text = m_settings.EmailServer;
            tbFromAddress.Text = m_settings.EmailFromAddress;
            tbToAddress.Text = m_settings.EmailToAddress;
            UpdateDisables();
        }

        private void cbSendEmail_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisables();
        }

        private void UpdateDisables()
        {
            tlpEmailSettings.Enabled = cbSendEmail.Checked;
            btnTestEmail.Enabled = cbSendEmail.Checked;
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