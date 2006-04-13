using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Globalization;

using System.Runtime.InteropServices;

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
                if (cli!=null)
                    AddTab(cli);
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            UpdateManager um = UpdateManager.GetInstance();
            um.UpdateAvailable += new UpdateAvailableHandler(um_UpdateAvailable);
            um.Start();

            InstanceManager im = InstanceManager.GetInstance();
            im.Signaled += new EventHandler<EventArgs>(im_Signaled);
        }

        void im_Signaled(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    im_Signaled(sender, e);
                }));
                return;
            }

            if (!this.Visible)
                niMinimizeIcon_Click(this, new EventArgs());
            else if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            else
                this.BringToFront();

            FlashWindow(this.Handle, true);
        }

        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        private void um_UpdateAvailable(object sender, UpdateAvailableEventArgs e)
        {
            if (e.NewestVersion <= new Version(m_settings.IgnoreUpdateVersion))
                return;

            this.Invoke(new MethodInvoker(delegate
            {
                using (UpdateNotifyForm f = new UpdateNotifyForm(m_settings, e))
                {
                    f.ShowDialog();
                    if (f.DialogResult == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }));
        }

        private void AddTab(CharLoginInfo cli)
        {
        AGAIN:
            bool result;
            try
            {
                result = cli.Validate();
            }
            catch (NullReferenceException)
            {
                result = false;
            }
            if (!result)
            {
                DialogResult dr = MessageBox.Show(
                    "Unable to show character monitor for " + cli.CharacterName + ", " +
                    "could not validate username/password/character combination.",
                    "Could Not Validate Character",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error);
                if (dr == DialogResult.Retry)
                {
                    //EveSession.GetSession(cli.Username, cli.Password).ReLogin();
                    goto AGAIN;
                }
                return;
            }

            TabPage tp = new TabPage(cli.CharacterName);
            tp.UseVisualStyleBackColor = true;
            tp.Tag = cli;
            tp.Padding = new Padding(5);
            CharacterMonitor cm = new CharacterMonitor(m_settings, cli);
            cm.Parent = tp;
            cm.Dock = DockStyle.Fill;
            cm.SkillTrainingCompleted += new SkillTrainingCompletedHandler(cm_SkillTrainingCompleted);
            cm.ShortInfoChanged += new EventHandler(cm_ShortInfoChanged);
            cm.Start();
            tcCharacterTabs.TabPages.Add(tp);
            SetRemoveEnable();
        }

        private void cm_ShortInfoChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                SortedList<TimeSpan, string> shortInfos = new SortedList<TimeSpan, string>();
                foreach (TabPage tp in tcCharacterTabs.TabPages)
                {
                    CharacterMonitor cm = tp.Controls[0] as CharacterMonitor;
                    if (cm != null && !String.IsNullOrEmpty(cm.ShortText))
                    {
                        TimeSpan ts = cm.ShortTimeSpan;
                        while (shortInfos.ContainsKey(ts))
                            ts = ts + TimeSpan.FromMilliseconds(1);
                        shortInfos.Add(ts, cm.ShortText);
                    }
                }
                int ttCharsLeft = 64;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < shortInfos.Count; i++)
                {
                    string tKey = shortInfos[shortInfos.Keys[i]];
                    if (sb.Length > 0)
                        tKey = "\n" + tKey;
                    ttCharsLeft -= tKey.Length;
                    if (ttCharsLeft > 0)
                        sb.Append(tKey);
                    else
                        break;
                }
                if (sb.Length == 0)
                    sb.Append("EVEMon - No skills in training!");
                niMinimizeIcon.Text = sb.ToString();

                if (m_settings.TitleToTime && shortInfos.Count > 0)
                {
                    string s = shortInfos[shortInfos.Keys[0]];
                    Match m = Regex.Match(s, "^(.*?): (.*)$");
                    if (m.Success)
                    {
                        this.Text = m.Groups[2] + ": " + m.Groups[1] + " - EVEMon";
                    }
                    else
                    {
                        this.Text = s + " - EVEMon";
                    }
                }
                else
                {
                    this.Text = "EVEMon";
                }
            }));
        }

        private List<string> m_completedSkills = new List<string>();

        private void cm_SkillTrainingCompleted(object sender, SkillTrainingCompletedEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                string sa = e.CharacterName + " has finished learning " + e.SkillName + ".";
                m_completedSkills.Add(sa);

                niAlertIcon.Text = "Skill Training Completed";
                niAlertIcon.BalloonTipTitle = "Skill Training Completed";
                if (m_completedSkills.Count == 1)
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
            }));
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
            cm.SkillTrainingCompleted -= new SkillTrainingCompletedHandler(cm_SkillTrainingCompleted);
            cm.ShortInfoChanged -= new EventHandler(cm_ShortInfoChanged);
            CharLoginInfo cli = tp.Tag as CharLoginInfo;
            tcCharacterTabs.TabPages.Remove(tp);
            m_settings.CharacterList.Remove(cli);
            m_settings.RemoveAllPlansFor(cli.CharacterName);
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
            niAlertIcon.Visible = false;
            tmrAlertRefresh.Enabled = false;
            using (SkillCompleteDialog f = new SkillCompleteDialog(m_completedSkills))
            {
                f.ShowDialog();
            }
            m_completedSkills.Clear();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateManager um = UpdateManager.GetInstance();
            um.Stop();
            um.UpdateAvailable -= new UpdateAvailableHandler(um_UpdateAvailable);
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            using (AboutWindow f = new AboutWindow())
            {
                f.ShowDialog();
            }
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            tmrClock.Enabled = false;
            DateTime now = DateTime.Now.ToUniversalTime();
            DateTimeFormatInfo fi = CultureInfo.CurrentCulture.DateTimeFormat;
            lblStatus.Text = "Current EVE Time: " + now.ToString(fi.ShortDatePattern+" HH:mm");
            tmrClock.Interval = ((60-now.Minute) * 1000) + (1000-now.Millisecond);
            tmrClock.Enabled = true;
        }
    }
}