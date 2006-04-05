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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            GetStored();
        }

        private bool m_useStored = false;

        public Login(bool useStored)
        {
            m_useStored = useStored;
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

        public const string STORE_FILE_NAME = "evecharactermonitor-logindata{0}.xml";

        private void Login_Load(object sender, EventArgs e)
        {
        }

        public static string StoreFileName()
        {
            string ca = String.Empty;
            if (Environment.GetCommandLineArgs().Length>1)
                ca = Environment.GetCommandLineArgs()[1];
            return String.Format(STORE_FILE_NAME, ca);
        }

        private void GetStored()
        {
            try
            {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
                using (IsolatedStorageFileStream s = new IsolatedStorageFileStream(StoreFileName(), FileMode.Open))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(s);

                    tbUserName.Text = ((XmlElement)xdoc.SelectSingleNode("/logindata/username")).GetAttribute("value");
                    tbPassword.Text = ((XmlElement)xdoc.SelectSingleNode("/logindata/password")).GetAttribute("value");
                    XmlNode cn = xdoc.SelectSingleNode("/logindata/character");
                    if (cn != null)
                    {
                        m_preferredChar = (cn as XmlElement).GetAttribute("value");
                    }
                    cbRemember.Checked = true;

                }
            }
            catch (FileNotFoundException)
            {
                m_useStored = false;
                tbUserName.Text = String.Empty;
                tbPassword.Text = String.Empty;
                cbRemember.Checked = false;
            }
            catch (Exception ex)
            {
                m_useStored = false;
                MessageBox.Show(ex.Message);
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
                XmlDocument xdoc = new XmlDocument();
                xdoc.AppendChild(xdoc.CreateElement("logindata"));
                XmlElement uel = xdoc.CreateElement("username");
                uel.SetAttribute("value", tbUserName.Text);
                xdoc.DocumentElement.AppendChild(uel);
                XmlElement pel = xdoc.CreateElement("password");
                pel.SetAttribute("value", tbPassword.Text);
                xdoc.DocumentElement.AppendChild(pel);

                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
                using (IsolatedStorageFileStream s = new IsolatedStorageFileStream(StoreFileName(), FileMode.Create, store))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(xdoc.InnerXml);
                }
            }
            else
            {
                try
                {
                    using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
                    {
                        store.DeleteFile(StoreFileName());
                    }
                }
                catch { }
            }

            m_preferredChar = String.Empty;
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