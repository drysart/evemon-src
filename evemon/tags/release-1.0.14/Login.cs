using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;

namespace EveCharacterMonitor
{
    public partial class Login : EVEMonForm
    {
        public Login()
        {
            InitializeComponent();
            GetStored();
        }

        private bool m_useStored = false;
        private Settings m_settings;

        public Login(bool useStored, Settings settings)
        {
            m_useStored = useStored;
            m_settings = settings;
            InitializeComponent();
            GetStored();
        }

        public string Username
        {
            get { return tbUserName.Text; }
        }

        public string Password
        {
            get { return tbPassword.Text; }
        }

        private string m_preferredChar = String.Empty;

        public string PreferredChar
        {
            get { return m_preferredChar; }
        }

        public bool Remember
        {
            get { return cbRemember.Checked; }
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void GetStored()
        {
            if (m_useStored && !String.IsNullOrEmpty(m_settings.Username) && !String.IsNullOrEmpty(m_settings.Password))
            {
                m_useStored = true;
                tbUserName.Text = m_settings.Username;
                tbPassword.Text = m_settings.Password;
                cbRemember.Checked = true;
            }
            else
            {
                m_useStored = false;
                tbUserName.Text = String.Empty;
                tbPassword.Text = String.Empty;
                cbRemember.Checked = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbUserName.Text))
            {
                MessageBox.Show("Please enter a user name.", "User Name Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (String.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (cbRemember.Checked)
            {
                m_settings.Username = tbUserName.Text;
                m_settings.Password = tbPassword.Text;
            }
            else
            {
                m_settings.Username = String.Empty;
                m_settings.Password = String.Empty;
            }

            m_settings.Character = String.Empty;
            m_settings.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public bool UseStored
        {
            get { return m_useStored; }
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            if (m_useStored)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}