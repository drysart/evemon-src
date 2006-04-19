using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EVEMon
{
    public partial class SettingsForm : EVEMonForm
    {
        public SettingsForm()
        {
            InitializeComponent();

            //if (Application.RenderWithVisualStyles)
            //    m_renderer = new VisualStyleRenderer(VisualStyleElement.Window.Dialog.Normal);
        }

        public SettingsForm(Settings s)
            : this()
        {
            m_settings = s;
        }

        //private VisualStyleRenderer m_renderer;
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    if (!Application.RenderWithVisualStyles)
        //        return;

        //    m_renderer.DrawBackground(e.Graphics, this.ClientRectangle);
        //}

        private Settings m_settings;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ApplyToSettings(Settings s)
        {
            s.MinimizeToTray = cbMinimizeToTray.Checked;
            s.TitleToTime = cbTitleToTime.Checked;
            s.EnableEmailAlert = cbSendEmail.Checked;
            s.EmailServer = tbMailServer.Text;
            s.EmailServerRequiresSsl = cbEmailServerRequireSsl.Checked;
            s.EmailAuthRequired = cbEmailAuthRequired.Checked;
            s.EmailAuthUsername = tbEmailUsername.Text;
            s.EmailAuthPassword = tbEmailPassword.Text;
            s.EmailFromAddress = tbFromAddress.Text;
            s.EmailToAddress = tbToAddress.Text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ApplyToSettings(m_settings);
            m_settings.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cbMinimizeToTray.Checked = m_settings.MinimizeToTray;
            cbTitleToTime.Checked = m_settings.TitleToTime;
            cbSendEmail.Checked = m_settings.EnableEmailAlert;
            tbMailServer.Text = m_settings.EmailServer;
            cbEmailServerRequireSsl.Checked = m_settings.EmailServerRequiresSsl;
            cbEmailAuthRequired.Checked = m_settings.EmailAuthRequired;
            tbEmailUsername.Text = m_settings.EmailAuthUsername;
            tbEmailPassword.Text = m_settings.EmailAuthPassword;
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
            tlpEmailAuthTable.Enabled = cbEmailAuthRequired.Checked;
        }

        private void btnTestEmail_Click(object sender, EventArgs e)
        {
            Settings ts = new Settings();
            ts.NeverSave();
            ApplyToSettings(ts);
            if (!Emailer.SendTestMail(ts))
            {
                MessageBox.Show("The message failed to send.", "Mail Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("The message sent successfully. Please verify that the message was received.", "Mail Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbEmailAuthRequired_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisables();
        }
    }
}