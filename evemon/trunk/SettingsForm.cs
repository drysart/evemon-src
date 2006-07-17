using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using EVEMon.Common;

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
            s.WorksafeMode = cbWorksafeMode.Checked;
            s.RunIGBServer = cbRunIGBServer.Checked;
            s.RelocateEveWindow = cbRelocateEveWindow.Checked;
            s.RelocateTargetScreen = cbScreenList.SelectedIndex;
            s.PlaySoundOnSkillComplete = cbPlaySoundOnSkillComplete.Checked;
            s.EnableEmailAlert = cbSendEmail.Checked;
            s.EmailServer = tbMailServer.Text;
            s.EmailServerRequiresSsl = cbEmailServerRequireSsl.Checked;
            s.EmailAuthRequired = cbEmailAuthRequired.Checked;
            s.EmailAuthUsername = tbEmailUsername.Text;
            s.EmailAuthPassword = tbEmailPassword.Text;
            s.EmailFromAddress = tbFromAddress.Text;
            s.EmailToAddress = tbToAddress.Text;

            s.UseCustomProxySettings = rbCustomProxy.Checked;
            ProxySetting httpSetting = ((ProxySetting)btnProxyHttpAuth.Tag).Clone();
            httpSetting.Host = tbProxyHttpHost.Text;
            try
            {
                httpSetting.Port = Convert.ToInt32(tbProxyHttpPort.Text);
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e, true);
                httpSetting.Port = 0;
            }
            s.HttpProxy = httpSetting;

            m_settings.StatusUpdateInterval = (int)numericStatusInterval.Value;
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
            cbScreenList.Items.Clear();
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                cbScreenList.Items.Add(String.Format("Screen {0}", i + 1));
            }

            int maxWidth = 0;
            int maxHeight = 0;
            foreach (TabPage tp in tabControl1.TabPages)
            {
                tabControl1.SelectedTab = tp;
                Size prefSize = tp.Controls[0].GetPreferredSize(new Size(Int32.MaxValue, Int32.MaxValue));
                prefSize.Width += tp.Padding.Left + tp.Padding.Right;
                prefSize.Height += tp.Padding.Top + tp.Padding.Bottom;
                maxWidth = Math.Max(maxWidth, prefSize.Width);
                maxHeight = Math.Max(maxHeight, prefSize.Height);
            }
            int extraHeight = tabControl1.ClientSize.Height - tabControl1.TabPages[0].Height;
            int extraWidth = tabControl1.ClientSize.Width - tabControl1.TabPages[0].Width;
            tabControl1.ClientSize = new Size(maxWidth + extraWidth, maxHeight + extraHeight);
            tabControl1.SelectedTab = tabControl1.TabPages[0];

            cbMinimizeToTray.Checked = m_settings.MinimizeToTray;
            cbTitleToTime.Checked = m_settings.TitleToTime;
            cbWorksafeMode.Checked = m_settings.WorksafeMode;
            cbRunIGBServer.Checked = m_settings.RunIGBServer;
            cbRelocateEveWindow.Checked = m_settings.RelocateEveWindow;
            if (m_settings.RelocateTargetScreen < cbScreenList.Items.Count)
                cbScreenList.SelectedIndex = m_settings.RelocateTargetScreen;
            else
                cbScreenList.SelectedIndex = 0;
            cbPlaySoundOnSkillComplete.Checked = m_settings.PlaySoundOnSkillComplete;
            cbSendEmail.Checked = m_settings.EnableEmailAlert;
            tbMailServer.Text = m_settings.EmailServer;
            cbEmailServerRequireSsl.Checked = m_settings.EmailServerRequiresSsl;
            cbEmailAuthRequired.Checked = m_settings.EmailAuthRequired;
            tbEmailUsername.Text = m_settings.EmailAuthUsername;
            tbEmailPassword.Text = m_settings.EmailAuthPassword;
            tbFromAddress.Text = m_settings.EmailFromAddress;
            tbToAddress.Text = m_settings.EmailToAddress;

            rbDefaultProxy.Checked = (m_settings.UseCustomProxySettings == false);
            rbCustomProxy.Checked = (m_settings.UseCustomProxySettings == true);
            tbProxyHttpHost.Text = m_settings.HttpProxy.Host;
            tbProxyHttpPort.Text = m_settings.HttpProxy.Port.ToString();
            btnProxyHttpAuth.Tag = m_settings.HttpProxy.Clone();

            numericStatusInterval.Value = m_settings.StatusUpdateInterval;

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

            bool isValid = true;
            isValid = isValid && ValidateProxySetting(tbProxyHttpHost.Text, tbProxyHttpPort.Text);

            btnOk.Enabled = isValid;
        }

        private bool ValidateProxySetting(string host, string port)
        {
            if (rbCustomProxy.Checked)
            {
                if (String.IsNullOrEmpty(host))
                    return false;

                int junk;
                if (!Int32.TryParse(port, out junk) || junk < 0)
                {
                    return false;
                }
            }
            return true;
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

        private void btnIdentifyScreens_Click(object sender, EventArgs e)
        {
            EVEMon.WindowRelocator.IdentifyScreenForm.Display();
        }

        private void cbRelocateEveWindow_CheckedChanged(object sender, EventArgs e)
        {
            flpScreenSelect.Enabled = cbRelocateEveWindow.Checked;
        }

        private void rbCustomProxy_CheckedChanged(object sender, EventArgs e)
        {
            vfpCustomProxySettings.Enabled = rbCustomProxy.Checked;
            UpdateDisables();
        }

        private void tbProxyHttpPort_TextChanged(object sender, EventArgs e)
        {
            UpdateDisables();
        }

        private void btnProxyHttpAuth_Click(object sender, EventArgs e)
        {
            ProxySetting ps = ((ProxySetting)btnProxyHttpAuth.Tag).Clone();
            using (ProxyAuthenticationWindow f = new ProxyAuthenticationWindow())
            {
                f.ProxySetting = ps;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    btnProxyHttpAuth.Tag = f.ProxySetting.Clone();
                }
            }
        }
    }
}
