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
    public partial class LoginCharSelect : EVEMonForm
    {
        public LoginCharSelect()
        {
            InitializeComponent();
        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {
            SetNoCharacter();
        }

        private bool m_charOk = false;
        private bool m_fileOk = false;

        private bool CharOk
        {
            get { return m_charOk; }
            set { m_charOk = value; CheckValidation(); }
        }

        private bool FileOk
        {
            get { return m_fileOk; }
            set { m_fileOk = value; CheckValidation(); }
        }

        private void CheckValidation()
        {
            switch (cbCharacterType.SelectedIndex)
            {
                case 0:
                    btnOk.Enabled = m_charOk;
                    break;
                case 1:
                    btnOk.Enabled = m_fileOk;
                    break;
                default:
                    btnOk.Enabled = false;
                    break;
            }
        }

        private void SetNoCharacter()
        {
            tbCharName.Text = "(None)";
            CharOk = false;
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            SetNoCharacter();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (cbCharacterType.SelectedIndex)
            {
                case 0:
                    m_isLogin = true;
                    m_isFile = false;
                    m_username = tbUsername.Text;
                    m_password = tbPassword.Text;
                    m_characterName = tbCharName.Text;
                    break;
                case 1:
                    m_isLogin = false;
                    m_isFile = true;
                    m_fileName = tbFilename.Text;
                    m_monitorFile = cbMonitorFile.Checked;
                    break;
                default:
                    return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool m_isLogin;
        private string m_username;
        private string m_password;
        private string m_characterName;
        private bool m_isFile;
        private string m_fileName;
        private bool m_monitorFile;

        public bool IsLogin
        {
            get { return m_isLogin; }
        }

        public string Username
        {
            get { return m_username; }
        }

        public string Password
        {
            get { return m_password; }
        }

        public string CharacterName
        {
            get { return m_characterName; }
        }

        public bool IsFile
        {
            get { return m_isFile; }
        }

        public string FileName
        {
            get { return m_fileName; }
        }

        public bool MonitorFile
        {
            get { return m_monitorFile; }
        }

        private void btnCharSelect_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbUsername.Text) || String.IsNullOrEmpty(tbPassword.Text))
            {
                SetNoCharacter();
                MessageBox.Show("Enter your login information first.",
                    "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            EveSession s = null;
            string errm;
            using (IDisposable d = BusyDialog.GetScope())
            {
                s = EveSession.GetSession(tbUsername.Text, tbPassword.Text, out errm);
            }
            if (s == null)
            {
                SetNoCharacter();
                //MessageBox.Show("Your login information could not be verified. Please " +
                //    "ensure it is entered correctly.\n\nThe error message returned was: "+errm, "Unable to Log In",
                //    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show(errm, "Unable to Log In",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            List<Pair<string, int>> chars = s.GetCharacterList();
            if (chars.Count == 0)
            {
                SetNoCharacter();
                MessageBox.Show("No characters were found on that account.",
                    "No Characters Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            using (CharSelect f = new CharSelect(s))
            {
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    tbCharName.Text = f.Result;
                    CharOk = true;
                }
            }
        }

        private void LoginCharSelect_Load(object sender, EventArgs e)
        {
            gbSavedXML.Location = gbEveLogin.Location;
            this.ClientSize = new Size(gbSavedXML.Width + (gbSavedXML.Left * 2),
                this.ClientSize.Height);
            cbCharacterType.SelectedIndex = 0;
        }

        private void cbCharacterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbEveLogin.Visible = (cbCharacterType.SelectedIndex == 0);
            gbSavedXML.Visible = (cbCharacterType.SelectedIndex == 1);
            CheckValidation();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dr = ofdOpenXml.ShowDialog();
            if (dr == DialogResult.OK)
            {
                tbFilename.Text = ofdOpenXml.FileName;
                FileOk = true;

                //anders - find the real character.  This probably burns too much time to stay here, but I like it
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.Load(ofdOpenXml.FileName);
                System.Xml.XmlElement cElement = SerializableCharacterInfo.FindCharacterElement(xdoc);
                tbFileCharName.Text = cElement.Attributes["name"].Value;
            }
        }
    }
}