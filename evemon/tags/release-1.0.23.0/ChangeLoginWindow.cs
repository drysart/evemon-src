using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EVEMon.Common;

namespace EVEMon
{
    public partial class ChangeLoginWindow : EVEMon.Common.EVEMonForm
    {
        public ChangeLoginWindow()
        {
            InitializeComponent();
        }

        private string m_charName = "this character";

        public string CharacterName
        {
            get { return m_charName; }
            set { m_charName = value; }
        }

        private string m_username = String.Empty;

        public string Username
        {
            get { return m_username; }
        }

        private string m_password = String.Empty;

        public string Password
        {
            get { return m_password; }
        }

        private void ChangeLoginWindow_Load(object sender, EventArgs e)
        {
            label1.Text = String.Format("Enter EVE Online login information for {0}:", m_charName);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool isOk = false;
            bool isLoginOk = false;
            using (BusyDialog.GetScope())
            {
                try
                {
                    EveSession s = EveSession.GetSession(tbUsername.Text, tbPassword.Text);
                    isLoginOk = true;
                    if (s.GetCharacterId(m_charName) > 0)
                    {
                        isOk = true;
                    }
                }
                catch
                {
                    isLoginOk = false;
                }   
            }
            if (isOk)
            {
                m_username = tbUsername.Text;
                m_password = tbPassword.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (!isLoginOk)
                    MessageBox.Show("Could not log in to EVE Online with that username/password.", "Unable to Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Could not find " + m_charName + " on that EVE Online account.",
                        "Could Not Find Character", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {
            CheckDisables();
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            CheckDisables();
        }

        private void CheckDisables()
        {
            btnOk.Enabled = (!String.IsNullOrEmpty(tbUsername.Text) &&
                !String.IsNullOrEmpty(tbPassword.Text));
        }
    }
}

