using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EVEMon
{
    public partial class LoginCharSelect : EVEMonForm
    {
        public LoginCharSelect()
        {
            InitializeComponent();
        }

        //private List<Pair<string, int>> m_chars = null;

        //private void cbCharacter_DropDown(object sender, EventArgs e)
        //{
        //    if (String.IsNullOrEmpty(tbUsername.Text) || String.IsNullOrEmpty(tbPassword.Text))
        //    {
        //        cbCharacter.Items.Clear();
        //        cbCharacter.Items.Add("<enter login first>");
        //        return;
        //    }

        //    EveSession s = EveSession.GetSession(tbUsername.Text, tbPassword.Text);
        //    if (s == null)
        //    {
        //        cbCharacter.Items.Clear();
        //        cbCharacter.Items.Add("<invalid login>");
        //        return;
        //    }

        //    cbCharacter.Items.Clear();
        //    if (m_chars==null)
        //        m_chars = s.GetCharacterList();
        //    foreach (Pair<string, int> p in m_chars)
        //    {
        //        cbCharacter.Items.Add(p.A);
        //    }
        //}

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {
            SetNoCharacter();
        }

        private void SetNoCharacter()
        {
            tbCharName.Text = "(None)";
            btnOk.Enabled = false;
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            SetNoCharacter();
        }

        //private void cbCharacter_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbCharacter.SelectedIndex == -1)
        //    {
        //        btnOk.Enabled = false;
        //        return;
        //    }
        //    string sv = cbCharacter.Items[cbCharacter.SelectedIndex] as string;
        //    if (String.IsNullOrEmpty(sv) || sv.StartsWith("<"))
        //    {
        //        cbCharacter.SelectedIndex = -1;
        //    }
        //    btnOk.Enabled = (cbCharacter.SelectedIndex >= 0);
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_username = tbUsername.Text;
            m_password = tbPassword.Text;
            m_characterName = tbCharName.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private string m_username;
        private string m_password;
        private string m_characterName;

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

        private void btnCharSelect_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbUsername.Text) || String.IsNullOrEmpty(tbPassword.Text))
            {
                SetNoCharacter();
                MessageBox.Show("Enter your login information first.",
                    "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            EveSession s = EveSession.GetSession(tbUsername.Text, tbPassword.Text);
            if (s == null)
            {
                SetNoCharacter();
                MessageBox.Show("Your login information could not be verified. Please " +
                    "ensure it is entered correctly.", "Unable to Log In",
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
                tbCharName.Text = f.Result;
                btnOk.Enabled = true;
            }
        }
    }
}