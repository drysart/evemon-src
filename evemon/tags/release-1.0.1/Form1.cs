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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private EveCharacterReader m_ecr;

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupCharReader();
        }

        private string m_lastFinishedSkill = String.Empty;
        private bool m_alertAcknowledged = true;

        void m_ecr_SkillInTrainingChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                SkillInTraining sit = m_ecr.SkillInTraining;
                if (sit == null)
                {
                    pnlSkillInTraining.Visible = false;
                    lbSkills.Height = this.ClientSize.Height - lbSkills.Top - 12;
                }
                else
                {
                    pnlSkillInTraining.Visible = true;
                    lbSkills.Height = this.ClientSize.Height - lbSkills.Top - pnlSkillInTraining.Height - 24;

                    string[] levels = new string[5] { "I", "II", "III", "IV", "V" };
                    lblSkillInTraining.Text = sit.SkillName + " " + levels[sit.TrainingToLevel - 1];
                    StringBuilder sb = new StringBuilder();
                    if (sit.EstimatedCompletion > DateTime.Now)
                    {
                        TimeSpan ts = sit.EstimatedCompletion - DateTime.Now;
                        if (ts.Days > 0)
                        {
                            sb.Append(ts.Days.ToString());
                            sb.Append(" day");
                            if (ts.Days > 1)
                                sb.Append("s");
                        }
                        ts -= TimeSpan.FromDays(ts.Days);
                        if (ts.Hours > 0)
                        {
                            if (sb.Length > 0)
                                sb.Append(", ");
                            sb.Append(ts.Hours.ToString());
                            sb.Append(" hour");
                            if (ts.Hours > 1)
                                sb.Append("s");
                        }
                        ts -= TimeSpan.FromHours(ts.Hours);
                        if (ts.Minutes > 0)
                        {
                            if (sb.Length > 0)
                                sb.Append(", ");
                            sb.Append(ts.Minutes.ToString());
                            sb.Append(" minute");
                            if (ts.Minutes > 1)
                                sb.Append("s");
                        }
                        ts -= TimeSpan.FromMinutes(ts.Minutes);
                        if (ts.Seconds > 0)
                        {
                            if (sb.Length > 0)
                                sb.Append(", ");
                            sb.Append(ts.Seconds.ToString());
                            sb.Append(" second");
                            if (ts.Seconds > 1)
                                sb.Append("s");
                        }
                        lblTrainingCompletion.Text = sit.EstimatedCompletion.ToString();
                    }
                    else
                    {
                        sb.Append("Completed.");
                        lblTrainingCompletion.Text = String.Empty;
                        if (m_lastFinishedSkill != sit.SkillName + sit.TrainingToLevel.ToString())
                        {
                            m_lastFinishedSkill = sit.SkillName + sit.TrainingToLevel.ToString();
                            UnacknowledgeAlert();
                            niAlert.BalloonTipTitle = "Skill Training Completed";
                            niAlert.BalloonTipText = lblCharacterName.Text + " has finished " +
                                "learning " + lblSkillInTraining.Text + ".";
                            niAlert.Visible = true;
                            niAlert.ShowBalloonTip(30000);
                        }
                    }
                    lblTrainingTimeLeft.Text = sb.ToString();
                }
            }));
        }

        private void SelectCharacter(string charname)
        {
            this.Text = charname + " - EVE Character Monitor";
            m_ecr.ActiveCharacter = charname;
            lblCharacterName.Text = charname;
        }

        void m_ecr_ImageChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                pictureBox1.Image = m_ecr.CharacterImage;
            }));
        }

        bool firstLogin = true;
        private string m_preferChar = String.Empty;
        private bool m_rememberChar = false;

        void m_ecr_NeedLogin(EveCharacterReader sender, NeedLoginEventArgs e)
        {
            using (Login lf = new Login(firstLogin))
            {
                if (!lf.UseStored || String.IsNullOrEmpty(lf.Username) || String.IsNullOrEmpty(lf.Password))
                    lf.ShowDialog();
                if (lf.DialogResult == DialogResult.OK || lf.UseStored)
                {
                    e.Username = lf.Username;
                    e.Password = lf.Password;
                    if (lf.UseStored)
                    {
                        m_preferChar = lf.PreferredChar;
                    }
                    m_rememberChar = lf.Remember;
                }
            }
            firstLogin = false;
        }

        private void tmrTrainingTimer_Tick(object sender, EventArgs e)
        {
            m_ecr_SkillInTrainingChanged(this, null);
        }

        private void SetupCharReader()
        {
            try
            {
                if (m_ecr != null)
                {
                    m_ecr.Stop();
                    m_ecr.Dispose();
                    m_ecr = null;
                }
                m_ecr = new EveCharacterReader();
                m_ecr.NeedLogin += new NeedLoginDelegate(m_ecr_NeedLogin);
                m_ecr.ImageChanged += new EventHandler(m_ecr_ImageChanged);
                m_ecr.SkillInTrainingChanged += new EventHandler(m_ecr_SkillInTrainingChanged);
                m_ecr.CharacterInfoUpdated += new EventHandler(m_ecr_CharacterInfoUpdated);
                m_ecr.Start();

                string res = null;
                IEnumerable<string> charsEnum = m_ecr.Characters;
                using (CharSelect csf = new CharSelect(charsEnum, m_preferChar))
                {
                    if (csf.Result==null)
                        csf.ShowDialog();
                    if (csf.Result!=null)
                        res = csf.Result;
                    else
                    {
                        throw new ApplicationException("no char selected");
                    }
                }
                if (m_rememberChar)
                    RememberCharacter(res);
                SelectCharacter(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void RememberCharacter(string res)
        {
            //Load
            XmlDocument xdoc = null;
            try
            {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
                using (IsolatedStorageFileStream s = new IsolatedStorageFileStream(Login.StoreFileName(), FileMode.Open))
                {
                    xdoc = new XmlDocument();
                    xdoc.Load(s);
                }
            }
            catch (FileNotFoundException)
            {
                return;
            }

            // Update
            XmlNode cn = xdoc.SelectSingleNode("/logindata/character");
            if (cn != null)
                (cn as XmlElement).SetAttribute("value", res);
            else
            {
                XmlElement el = xdoc.CreateElement("character");
                el.SetAttribute("value", res);
                xdoc.DocumentElement.AppendChild(el);
            }

            // Save
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
            using (IsolatedStorageFileStream s = new IsolatedStorageFileStream(Login.StoreFileName(), FileMode.Create, store))
            using (StreamWriter sw = new StreamWriter(s))
            {
                sw.Write(xdoc.InnerXml);
            }
        }

        void m_ecr_CharacterInfoUpdated(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                lblBioInfo.Text = m_ecr.Gender + " " + m_ecr.Race + " " + m_ecr.BloodLine;
                lblCorpInfo.Text = "Corporation: " + m_ecr.CorpName;
                lblBalance.Text = "Balance: " + m_ecr.Balance.ToString("#,##0.00 ISK");

                lblIntelligence.Text = m_ecr.Intelligence.ToString() + " Intelligence";
                lblCharisma.Text = m_ecr.Charisma.ToString() + " Charisma";
                lblPerception.Text = m_ecr.Perception.ToString() + " Perception";
                lblMemory.Text = m_ecr.Memory.ToString() + " Memory";
                lblWillpower.Text = m_ecr.Willpower.ToString() + " Willpower";

                lbSkills.Items.Clear();
                foreach (SkillGroup sg in m_ecr.SkillGroups)
                {
                    lbSkills.Items.Add(sg);
                    foreach (Skill s in sg.Skills)
                    {
                        lbSkills.Items.Add(s);
                    }
                }
            }));
        }

        private void lnkChange_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SetupCharReader();
            try { this.Show(); }
            catch (ObjectDisposedException) { }
        }

        private void UnacknowledgeAlert()
        {
            m_alertAcknowledged = false;
            tmrAlert.Enabled = true;
        }

        private void AcknowledgeAlert()
        {
            tmrAlert.Enabled = false;
            m_alertAcknowledged = true;
            niAlert.Visible = false;
        }

        private void niAlert_Click(object sender, EventArgs e)
        {
            AcknowledgeAlert();
        }

        private void niAlert_MouseClick(object sender, MouseEventArgs e)
        {
            AcknowledgeAlert();
        }

        private void niAlert_BalloonTipClicked(object sender, EventArgs e)
        {
            AcknowledgeAlert();
        }

        private void tmrAlert_Tick(object sender, EventArgs e)
        {
            if (m_alertAcknowledged)
            {
                tmrAlert.Enabled = false;
                return;
            }
            niAlert.ShowBalloonTip(30000);
        }
    }
}