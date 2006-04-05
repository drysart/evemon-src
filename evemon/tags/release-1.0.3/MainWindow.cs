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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Settings m_settings;

        public MainWindow(Settings s)
            : this()
        {
            m_settings = s;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(m_settings.Username) &&
                !String.IsNullOrEmpty(m_settings.Password) &&
                !String.IsNullOrEmpty(m_settings.Character))
            {
                CharLoginInfo cli = new CharLoginInfo();
                cli.Username = m_settings.Username;
                cli.Password = m_settings.Password;
                cli.CharacterName = m_settings.Character;
                m_settings.AddCharacter(cli);
                //m_settings.CharacterList.Add(cli);
                m_settings.Username = String.Empty;
                m_settings.Password = String.Empty;
                m_settings.Character = String.Empty;
                m_settings.Save();
            }

            foreach (CharLoginInfo cli in m_settings.CharacterList)
            {
                AddTab(cli);
            }
        }

        private void AddTab(CharLoginInfo cli)
        {
            TabPage tp = new TabPage(cli.CharacterName);
            tp.UseVisualStyleBackColor = true;
            tp.Tag = cli;
            tp.Padding = new Padding(5);
            CharacterMonitor cm = new CharacterMonitor(cli);
            cm.Parent = tp;
            cm.Dock = DockStyle.Fill;
            cm.SkillTrainingCompleted += new SkillTrainingCompletedHandler(cm_SkillTrainingCompleted);
            cm.Start();
            tcCharacterTabs.TabPages.Add(tp);
            SetRemoveEnable();
        }

        private List<string> m_completedSkills = new List<string>();

        private void cm_SkillTrainingCompleted(object sender, SkillTrainingCompletedEventArgs e)
        {
            string sa = e.CharacterName + " has finished learning " + e.SkillName + ".";
            m_completedSkills.Add(sa);

            niAlertIcon.Text = "Skill Training Completed";
            niAlertIcon.BalloonTipTitle = "Skill Training Completed";
            if (m_completedSkills.Count==1)
                niAlertIcon.BalloonTipText = sa;
            else
                niAlertIcon.BalloonTipText = m_completedSkills.Count.ToString() + " skills completed. Click for more info.";
            niAlertIcon.BalloonTipIcon = ToolTipIcon.Info;
            niAlertIcon.Visible = true;
            niAlertIcon.ShowBalloonTip(30000);
            Emailer.SendAlertMail(m_settings, e.SkillName, e.CharacterName);

            tmrAlertRefresh.Enabled = false;
            tmrAlertRefresh.Interval = 60000;
            tmrAlertRefresh.Enabled = true;
        }

        private void SetRemoveEnable()
        {
            if (tcCharacterTabs.TabPages.Count > 1)
                tsbRemoveChar.Enabled = true;
            else
                tsbRemoveChar.Enabled = false;
        }

        private void RemoveTab(TabPage tp)
        {
            CharacterMonitor cm = tp.Controls[0] as CharacterMonitor;
            if (cm != null)
                cm.Stop();
            CharLoginInfo cli = tp.Tag as CharLoginInfo;
            tcCharacterTabs.TabPages.Remove(tp);
            m_settings.CharacterList.Remove(cli);
            m_settings.Save();
            SetRemoveEnable();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (LoginCharSelect f = new LoginCharSelect())
            {
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    CharLoginInfo cli = new CharLoginInfo();
                    cli.Username = f.Username;
                    cli.Password = f.Password;
                    cli.CharacterName = f.CharacterName;
                    if (m_settings.AddCharacter(cli))
                    {
                        AddTab(cli);
                    }
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TabPage activeTab = tcCharacterTabs.SelectedTab;
            RemoveTab(activeTab);
        }

        private void tsbOptions_Click(object sender, EventArgs e)
        {
            using (SettingsForm sf = new SettingsForm(m_settings))
            {
                sf.ShowDialog();
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && m_settings.MinimizeToTray)
            {
                niMinimizeIcon.Visible = true;
                this.Visible = false;
            }
            else
            {
                niMinimizeIcon.Visible = false;
            }
//            if (this.WindowState == FormWindowState.Normal)
                //lbSkills.Width = this.ClientSize.Width - (lbSkills.Left * 2);
        }

        private void niMinimizeIcon_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void tmrAlertRefresh_Tick(object sender, EventArgs e)
        {
            tmrAlertRefresh.Enabled = false;
            if (niAlertIcon.Visible)
            {
                niAlertIcon.ShowBalloonTip(30000);
                tmrAlertRefresh.Interval = 60000;
                tmrAlertRefresh.Enabled = true;
            }
        }

        private void niAlertIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            AlertIconClick();
        }

        private void niAlertIcon_Click(object sender, EventArgs e)
        {
            AlertIconClick();
        }

        private void niAlertIcon_MouseClick(object sender, MouseEventArgs e)
        {
            AlertIconClick();
        }

        private void AlertIconClick()
        {
            using (SkillCompleteDialog f = new SkillCompleteDialog(m_completedSkills))
            {
                f.ShowDialog();
            }
            m_completedSkills.Clear();
        }
    }
}