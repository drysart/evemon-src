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
using System.IO;
using System.Reflection;

using EVEMon.Common;

namespace EVEMon
{
    public partial class MainWindow : EVEMonForm
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

        private Image[] m_throbberImages;

        private IGBService.IGBServer m_igbServer;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.RememberPositionKey = "MainWindow";
            Program.MainWindow = this;

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
                if (cli != null)
                    AddTab(cli);
            }
            List<CharFileInfo> invalidFiles = new List<CharFileInfo>();
            foreach (CharFileInfo cfi in m_settings.CharFileList)
            {
                if (cfi != null)
                {
                    if (!AddTab(cfi, true))
                    {
                        invalidFiles.Add(cfi);
                    }
                }
            }
            foreach (CharFileInfo cfi in invalidFiles)
            {
                RemoveCharFileInfo(cfi);
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            UpdateManager um = UpdateManager.GetInstance();
            um.UpdateAvailable += new UpdateAvailableHandler(um_UpdateAvailable);
            um.Start();

#if !DEBUG
            InstanceManager im = InstanceManager.GetInstance();
            im.Signaled += new EventHandler<EventArgs>(im_Signaled);
#endif

            m_igbServer = new EVEMon.IGBService.IGBServer();
            m_settings.RunIGBServerChanged += new EventHandler<EventArgs>(m_settings_RunIGBServerChanged);
            m_settings_RunIGBServerChanged(null, null);

            TipWindow.ShowTip("startup",
                "Getting Started",
                "To begin using EVEMon, click the \"Add Character\" button in " +
                "the upper left corner of the window, enter your login information " +
                "and choose a character to monitor.");
        }

        private void m_settings_RunIGBServerChanged(object sender, EventArgs e)
        {
            if (m_settings.RunIGBServer)
            {
                m_igbServer.Start();
            }
            else
            {
                m_igbServer.Stop();
            }
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

        private bool m_updateShowing = false;

        private void um_UpdateAvailable(object sender, UpdateAvailableEventArgs e)
        {
            if (e.NewestVersion <= new Version(m_settings.IgnoreUpdateVersion))
                return;

            this.Invoke(new MethodInvoker(delegate
            {
                if (!m_updateShowing)
                {
                    m_updateShowing = true;
                    using (UpdateNotifyForm f = new UpdateNotifyForm(m_settings, e))
                    {
                        f.ShowDialog();
                        if (f.DialogResult == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    m_updateShowing = false;
                }
            }));
        }

        private bool AddTab(CharLoginInfo cli)
        {
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
            return true;
        }

        private bool AddTab(CharFileInfo cfi, bool silent)
        {
            SerializableCharacterInfo sci = SerializableCharacterInfo.CreateFromFile(cfi.Filename);
            if (sci == null)
            {
                if (!silent)
                {
                    MessageBox.Show("Unable to get character info from file.",
                        "Unable To Get Character Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }

            TabPage tp = new TabPage("(File) " + sci.Name);
            tp.UseVisualStyleBackColor = true;
            tp.Tag = cfi;
            tp.Padding = new Padding(5);
            CharacterMonitor cm = new CharacterMonitor(m_settings, cfi, sci);
            cm.Parent = tp;
            cm.Dock = DockStyle.Fill;
            cm.SkillTrainingCompleted += new SkillTrainingCompletedHandler(cm_SkillTrainingCompleted);
            cm.ShortInfoChanged += new EventHandler(cm_ShortInfoChanged);
            cm.Start();
            tcCharacterTabs.TabPages.Add(tp);
            SetRemoveEnable();
            return true;
        }

        private void cm_ShortInfoChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                SortedList<TimeSpan, GrandCharacterInfo> gcis = new SortedList<TimeSpan, GrandCharacterInfo>();
                foreach (TabPage tp in tcCharacterTabs.TabPages)
                {
                    CharacterMonitor cm = tp.Controls[0] as CharacterMonitor;
                    if (cm != null && cm.GrandCharacterInfo != null && cm.GrandCharacterInfo.CurrentlyTrainingSkill != null)
                    {
                        GrandCharacterInfo gci = cm.GrandCharacterInfo;
                        GrandSkill gs = gci.CurrentlyTrainingSkill;
                        TimeSpan ts = gs.EstimatedCompletion - DateTime.Now;
                        if (ts > TimeSpan.Zero)
                        {
                            while (gcis.ContainsKey(ts))
                                ts = ts + TimeSpan.FromMilliseconds(1);
                            gcis.Add(ts, gci);
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("EVEMon");
                foreach (GrandCharacterInfo gci in gcis.Values)
                {
                    sb.Append("\n");
                    sb.Append(gci.Name);
                    sb.Append(" - ");
                    sb.Append(gci.CurrentlyTrainingSkill.Name);
                    sb.Append(" ");
                    sb.Append(GrandSkill.GetRomanSkillNumber(gci.CurrentlyTrainingSkill.TrainingToLevel));
                    sb.Append(" - ");
                    sb.Append(GrandSkill.TimeSpanToDescriptiveText(
                        gci.CurrentlyTrainingSkill.EstimatedCompletion - DateTime.Now,
                        DescriptiveTextOptions.IncludeCommas));
                }
                SetMinimizedIconTooltipText(sb.ToString());

                if (m_settings.TitleToTime && gcis.Count > 0)
                {
                    StringBuilder tsb = new StringBuilder();
                    GrandCharacterInfo gci = gcis.Values[0];
                    tsb.Append(GrandSkill.TimeSpanToDescriptiveText(
                        gci.CurrentlyTrainingSkill.EstimatedCompletion - DateTime.Now,
                        DescriptiveTextOptions.Default));
                    tsb.Append(" - ");
                    tsb.Append(gci.Name);
                    tsb.Append(" - EVEMon");
                    this.Text = tsb.ToString();
                }

                //SortedList<TimeSpan, string> shortInfos = new SortedList<TimeSpan, string>();
                //foreach (TabPage tp in tcCharacterTabs.TabPages)
                //{
                //    CharacterMonitor cm = tp.Controls[0] as CharacterMonitor;
                //    if (cm != null && !String.IsNullOrEmpty(cm.ShortText) && cm.ShortTimeSpan > TimeSpan.Zero)
                //    {
                //        TimeSpan ts = cm.ShortTimeSpan;
                //        while (shortInfos.ContainsKey(ts))
                //            ts = ts + TimeSpan.FromMilliseconds(1);
                //        shortInfos.Add(ts, cm.ShortText);
                //    }
                //}
                //StringBuilder sb = new StringBuilder();
                //sb.Append("EVEMon");
                //for (int i = 0; i < shortInfos.Count; i++)
                //{
                //    string tKey = shortInfos[shortInfos.Keys[i]];
                //    if (sb.Length > 0)
                //        tKey = "\n" + tKey;
                //    sb.Append(tKey);
                //}
                //if (shortInfos.Count == 0)
                //    sb.Append("\nNo skills in training!");
                ////niMinimizeIcon.Text = sb.ToString();
                //SetMinimizedIconTooltipText(sb.ToString());

                //if (m_settings.TitleToTime && shortInfos.Count > 0)
                //{
                //    string s = shortInfos[shortInfos.Keys[0]];
                //    Match m = Regex.Match(s, "^(.*?): (.*)$");
                //    if (m.Success)
                //    {
                //        this.Text = m.Groups[2] + ": " + m.Groups[1] + " - EVEMon";
                //    }
                //    else
                //    {
                //        this.Text = s + " - EVEMon";
                //    }
                //}
                //else
                //{
                //    this.Text = "EVEMon";
                //}
            }));
        }

        private List<string> m_completedSkills = new List<string>();

        private void cm_SkillTrainingCompleted(object sender, SkillTrainingCompletedEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                if (m_settings.PlaySoundOnSkillComplete)
                    MP3Player.Play("SkillTrained.mp3", true);

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
                if (m_settings.EnableEmailAlert)
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
            tcCharacterTabs.TabPages.Remove(tp);
            if (tp.Tag is CharLoginInfo)
            {
                CharLoginInfo cli = tp.Tag as CharLoginInfo;
                m_settings.CharacterList.Remove(cli);
                m_settings.RemoveAllPlansFor(cli.CharacterName);
                m_settings.Save();
            }
            else if (tp.Tag is CharFileInfo)
            {
                CharFileInfo cfi = tp.Tag as CharFileInfo;
                RemoveCharFileInfo(cfi);
            }
            SetRemoveEnable();
        }

        private void RemoveCharFileInfo(CharFileInfo cfi)
        {
            m_settings.CharFileList.Remove(cfi);
            m_settings.RemoveAllPlansFor(cfi.Filename);
            m_settings.Save();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (LoginCharSelect f = new LoginCharSelect())
            {
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    if (f.IsLogin)
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
                    else if (f.IsFile)
                    {
                        CharFileInfo cfi = new CharFileInfo();
                        cfi.Filename = f.FileName;
                        cfi.MonitorFile = f.MonitorFile;
                        if (m_settings.AddFileCharacter(cfi))
                        {
                            AddTab(cfi, false);
                        }
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
            this.Activate();
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
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(AlertIconClick));
                return;
            }

            if (m_completedSkills.Count > 0)
            {
                niAlertIcon.Visible = false;
                tmrAlertRefresh.Enabled = false;
                using (SkillCompleteDialog f = new SkillCompleteDialog(m_completedSkills))
                {
                    f.ShowDialog();
                }
                m_completedSkills.Clear();
            }
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateManager um = UpdateManager.GetInstance();
            um.Stop();
            um.UpdateAvailable -= new UpdateAvailableHandler(um_UpdateAvailable);

            m_settings.RunIGBServerChanged -= new EventHandler<EventArgs>(m_settings_RunIGBServerChanged);

            m_igbServer.Stop();
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

        public GrandCharacterInfo GetGrandCharacterInfo(string charName)
        {
            foreach (TabPage tp in tcCharacterTabs.TabPages)
            {
                CharacterMonitor cm = tp.Controls[0] as CharacterMonitor;
                GrandCharacterInfo gci = cm.GrandCharacterInfo;
                if (gci != null && gci.Name == charName)
                    return gci;
            }
            return null;
        }

        private WeakReference<TrayTooltipWindow> m_tooltipWindow = null;

        private bool m_inMinIconMouseMove = false;

        private void niMinimizeIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (!m_inMinIconMouseMove)
            {
                m_inMinIconMouseMove = true;
                try
                {
                    TrayTooltipWindow ttw = null;
                    if (m_tooltipWindow != null)
                    {
                        ttw = m_tooltipWindow.Target;
                    }

                    if (ttw == null)
                    {
                        ttw = new TrayTooltipWindow();
                        m_tooltipWindow = new WeakReference<TrayTooltipWindow>(ttw);
                        ttw.FormClosed += delegate
                        {
                            m_tooltipWindow = null;
                            ttw.Dispose();
                        };
                        ttw.Text = m_tooltipText;
                        ttw.Show();
                    }
                    else
                    {
                        ttw.RefreshAlive();
                    }
                }
                finally
                {
                    m_inMinIconMouseMove = false;
                }
            }
        }

        private string m_tooltipText = "EVEMon";

        private void SetMinimizedIconTooltipText(string txt)
        {
            TrayTooltipWindow ttw = null;
            if (m_tooltipWindow != null)
            {
                ttw = m_tooltipWindow.Target;
            }

            if (ttw != null)
            {
                ttw.Text = txt;
            }
            m_tooltipText = txt;
        }

        private WeakReference<Schedule.ScheduleEditorWindow> m_scheduler;

        private void tsbSchedule_Click(object sender, EventArgs e)
        {
            Schedule.ScheduleEditorWindow sched = null;

            if (m_scheduler != null)
            {
                sched = m_scheduler.Target;
                if (sched != null)
                {
                    try
                    {
                        if (sched.Visible)
                            sched.BringToFront();
                        else
                            sched = null;
                    }
                    catch (ObjectDisposedException)
                    {
                        sched = null;
                        m_scheduler = null;
                    }
                }
            }
            if (sched == null)
            {
                sched = new EVEMon.Schedule.ScheduleEditorWindow(m_settings);
                sched.Show();
                m_scheduler = new WeakReference<EVEMon.Schedule.ScheduleEditorWindow>(sched);
            }
        }

    }
}