using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
using EVEMon.Common;
using EVEMon.SkillPlanner;

namespace EVEMon
{
    public partial class CharacterMonitor : UserControl
    {
        public CharacterMonitor()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private Settings m_settings;
        private CharLoginInfo m_cli;
        private SerializableCharacterInfo m_sci;
        private CharFileInfo m_cfi;
        private EveSession m_session;
        private int m_charId;
        private FileSystemWatcher m_fsw = null;

        private Dictionary<GrandSkillGroup, bool> m_groupCollapsed = new Dictionary<GrandSkillGroup, bool>();

        public CharacterMonitor(Settings s, CharLoginInfo cli)
            : this()
        {
            m_settings = s;
            m_cli = cli;
            m_sci = null;
            m_cfi = null;
        }

        public CharacterMonitor(Settings s, CharFileInfo cfi, SerializableCharacterInfo sci)
            : this()
        {
            m_settings = s;
            m_cli = null;
            m_sci = sci;
            m_cfi = cfi;
        }

        private static Image[] m_throbberImages = null;

        public static Image[] ThrobberImages
        {
            get
            {
                if (m_throbberImages == null)
                    InitializeThrobberImages();
                return m_throbberImages;
            }
        }

        private const int THROBBERIMG_WIDTH = 24;
        private const int THROBBERIMG_HEIGHT = 24;

        private static void InitializeThrobberImages()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream s = asm.GetManifestResourceStream("EVEMon.throbber.png"))
            using (Image b = Image.FromStream(s, true, true))
            {
                m_throbberImages = new Image[9];
                for (int i = 0; i < 9; i++)
                {
                    Bitmap ib = new Bitmap(THROBBERIMG_WIDTH, THROBBERIMG_HEIGHT);
                    using (Graphics g = Graphics.FromImage(ib))
                    {
                        g.DrawImage(b, new Rectangle(0, 0, THROBBERIMG_WIDTH, THROBBERIMG_HEIGHT),
                            new Rectangle(i * THROBBERIMG_WIDTH, 0, THROBBERIMG_WIDTH, THROBBERIMG_HEIGHT), GraphicsUnit.Pixel);
                    }
                    m_throbberImages[i] = ib;
                }
            }
        }

        public event SkillTrainingCompletedHandler SkillTrainingCompleted;

        private void OnSkillTrainingComplete(string charName, string skillName)
        {
            if (String.IsNullOrEmpty(charName) || String.IsNullOrEmpty(skillName))
                return;

            if (SkillTrainingCompleted != null)
            {
                SkillTrainingCompletedEventArgs e = new SkillTrainingCompletedEventArgs(charName, skillName);
                SkillTrainingCompleted(this, e);
            }
        }

        public void Start()
        {
            m_session = null;
            m_charId = -1;
            //m_charId = m_session.GetCharacterId(m_cli.CharacterName);
            //if (m_charId < 0)
            //{
            //    m_session = null;
            //    throw new ApplicationException("Could not start character monitor");
            //}

            if (m_cli != null)
            {
                m_grandCharacterInfo = new GrandCharacterInfo(m_charId, m_cli.CharacterName);
            }
            else
            {
                m_charId = m_sci.CharacterId;
                m_grandCharacterInfo = new GrandCharacterInfo(m_sci.CharacterId, m_sci.Name);
                if (m_charId > 0)
                {
                    GetCharacterImage();
                }
                if (m_cfi.MonitorFile)
                {
                    m_fsw = new FileSystemWatcher(
                        Path.GetDirectoryName(m_cfi.Filename), Path.GetFileName(m_cfi.Filename));
                    m_fsw.Created += new FileSystemEventHandler(m_fsw_Created);
                    m_fsw.Changed += new FileSystemEventHandler(m_fsw_Changed);
                    m_fsw.EnableRaisingEvents = true;
                }
            }
            m_grandCharacterInfo.BioInfoChanged += new EventHandler(m_grandCharacterInfo_BioInfoChanged);
            m_grandCharacterInfo.BalanceChanged += new EventHandler(m_grandCharacterInfo_BalanceChanged);
            m_grandCharacterInfo.AttributeChanged += new EventHandler(m_grandCharacterInfo_AttributeChanged);
            m_grandCharacterInfo.SkillChanged += new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);

            if (m_cli != null)
            {
                SerializableCharacterInfo sci = m_settings.GetCharacterInfo(m_cli.CharacterName);
                if (sci != null)
                    m_grandCharacterInfo.AssignFromSerializableCharacterInfo(sci);

                tmrUpdate.Interval = 10;
                tmrUpdate.Enabled = true;
            }
            else
            {
                tmrUpdate.Enabled = false;
                pbThrobber.Visible = false;
                m_grandCharacterInfo.AssignFromSerializableCharacterInfo(m_sci);
                m_sci = null;
            }

            foreach (GrandSkillGroup gsg in m_grandCharacterInfo.SkillGroups.Values)
            {
                foreach (Pair<string, string> grp in m_settings.CollapsedGroups)
                {
                    if ((grp.A == m_grandCharacterInfo.Name) && (grp.B == gsg.Name))
                    {
                        m_groupCollapsed.Add(gsg, true);
                        foreach (GrandSkill gs in gsg)
                        {
                            if (gs.Known)
                                lbSkills.Items.RemoveAt(lbSkills.Items.IndexOf(gs));
                        }
                        gsg.isCollapsed = true;
                    }
                }
            }

            tmrTick.Enabled = true;

            m_settings.WorksafeChanged += new EventHandler<EventArgs>(m_settings_WorksafeChanged);
            m_settings_WorksafeChanged(null, null);
        }

        private void m_fsw_Created(object sender, FileSystemEventArgs e)
        {
            ReloadFromFile();
        }

        private void m_fsw_Changed(object sender, FileSystemEventArgs e)
        {
            ReloadFromFile();
        }

        private void ReloadFromFile()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(ReloadFromFile));
                return;
            }

            SerializableCharacterInfo sci = SerializableCharacterInfo.CreateFromFile(m_cfi.Filename);
            if (sci != null)
                m_grandCharacterInfo.AssignFromSerializableCharacterInfo(sci);
        }

        public void Stop()
        {
            if (m_session != null)
                m_session = null;
            tmrTick.Enabled = false;
            tmrUpdate.Enabled = false;
            if (m_fsw != null)
                m_fsw.EnableRaisingEvents = false;

            m_grandCharacterInfo.BioInfoChanged -= new EventHandler(m_grandCharacterInfo_BioInfoChanged);
            m_grandCharacterInfo.BalanceChanged -= new EventHandler(m_grandCharacterInfo_BalanceChanged);
            m_grandCharacterInfo.AttributeChanged -= new EventHandler(m_grandCharacterInfo_AttributeChanged);
            m_grandCharacterInfo.SkillChanged -= new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);

            m_settings.WorksafeChanged -= new EventHandler<EventArgs>(m_settings_WorksafeChanged);
        }

        private bool m_currentlyVisible = true;

        public bool CurrentlyVisible
        {
            get { return m_currentlyVisible; }
            set { m_currentlyVisible = value; }
        }

        private void m_settings_WorksafeChanged(object sender, EventArgs e)
        {
            pbCharImage.Visible = !m_settings.WorksafeMode;
            if (m_settings.WorksafeMode)
            {
                tableLayoutPanel1.SetColumnSpan(lblSkillHeader, 1);
                tableLayoutPanel1.SetColumn(lblSkillHeader, 1);
            }
            else
            {
                tableLayoutPanel1.SetColumn(lblSkillHeader, 0);
                tableLayoutPanel1.SetColumnSpan(lblSkillHeader, 2);
            }
            //lblCharacterName.Left = m_settings.WorksafeMode ? -3 : 134;
            //lblBioInfo.Left = m_settings.WorksafeMode ? -3 : 134;
            //lblCorpInfo.Left = m_settings.WorksafeMode ? -3 : 134;
            //lblBalance.Left = m_settings.WorksafeMode ? -3 : 134;
            //lblIntelligence.Left = m_settings.WorksafeMode ? -3 : 134;
            //lblCharisma.Left = m_settings.WorksafeMode ? -3 : 134;
            //lblPerception.Left = m_settings.WorksafeMode ? -3 : 134;
            //lblMemory.Left = m_settings.WorksafeMode ? -3 : 134;
            //lblWillpower.Left = m_settings.WorksafeMode ? -3 : 134;
        }

        void m_grandCharacterInfo_SkillChanged(object sender, SkillChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    m_grandCharacterInfo_SkillChanged(sender, e);
                }));
                return;
            }

            EnableButtons();
            lbSkills.BeginUpdate();
            try
            {
                foreach (GrandSkill gs in e.SkillList)
                {
                    GrandSkillGroup gsg = gs.SkillGroup;

                    if (gs.Known)
                    {
                        // Find the existing listbox item... if the group isn't collapsed
                        if (!m_groupCollapsed.ContainsKey(gsg) || m_groupCollapsed[gsg] == false)
                        {
                            int lbIndex = -1;
                            int shouldInsertAt = -1;
                            bool shouldInsertSkillGroup = true;
                            bool inMySkillGroup = false;
                            bool found = false;
                            for (int i = 0; i < lbSkills.Items.Count; i++)
                            {
                                object o = lbSkills.Items[i];
                                if (o == gs)
                                {
                                    shouldInsertSkillGroup = false;
                                    lbIndex = i;
                                    found = true;
                                    break;
                                }
                                else if (o == gsg)
                                {
                                    inMySkillGroup = true;
                                    shouldInsertSkillGroup = false;
                                }
                                else if (o is GrandSkillGroup && ((GrandSkillGroup)o).Name.CompareTo(gsg.Name) > 0)
                                {
                                    shouldInsertAt = i;
                                    shouldInsertSkillGroup = (!inMySkillGroup);
                                    break;
                                }
                                else if (inMySkillGroup && o is GrandSkill && ((GrandSkill)o).Name.CompareTo(gs.Name) > 0)
                                {
                                    shouldInsertAt = i;
                                    shouldInsertSkillGroup = false;
                                    break;
                                }
                            }

                            if (shouldInsertSkillGroup)
                            {
                                if (shouldInsertAt >= 0)
                                {
                                    lbSkills.Items.Insert(shouldInsertAt, gsg);
                                    shouldInsertAt++;
                                }
                                else
                                {
                                    lbSkills.Items.Add(gsg);
                                    shouldInsertAt = -1;
                                }
                            }
                            if (!found)
                            {
                                if (shouldInsertAt >= 0)
                                {
                                    lbSkills.Items.Insert(shouldInsertAt, gs);
                                    lbIndex = shouldInsertAt;
                                }
                                else
                                {
                                    lbSkills.Items.Add(gs);
                                    lbIndex = lbSkills.Items.Count - 1;
                                }
                            }

                            lbSkills.Invalidate(lbSkills.GetItemRectangle(lbIndex));
                        }
                    }

                    if (gs.InTraining)
                    {
                        m_skillTrainingName = gs.Name + " " + GrandSkill.GetRomanSkillNumber(gs.TrainingToLevel);
                        lblTrainingSkill.Text = m_skillTrainingName;
                        double spPerHour = 60 * (m_grandCharacterInfo.GetEffectiveAttribute(gs.PrimaryAttribute) +
                            (m_grandCharacterInfo.GetEffectiveAttribute(gs.SecondaryAttribute) / 2));
                        lblSPPerHour.Text = Convert.ToInt32(Math.Round(spPerHour)).ToString() + " SP/Hour";
                        m_estimatedCompletion = gs.EstimatedCompletion;
                        CalcSkillRemainText();
                        pnlTraining.Visible = true;
                    }
                    else if (m_grandCharacterInfo.CurrentlyTrainingSkill == null)
                    {
                        m_skillTrainingName = String.Empty;
                        m_estimatedCompletion = DateTime.MaxValue;
                        pnlTraining.Visible = false;
                    }
                }
            }
            finally
            {
                lbSkills.EndUpdate();
            }

            UpdateSkillHeaderStats();
            UpdateCachedCopy();
        }

        private void EnableButtons()
        {
            btnSave.Enabled = true;
            btnPlan.Enabled = true;
        }

        private void UpdateSkillHeaderStats()
        {
            if (this.Visible)
            {
                lblSkillHeader.Text = String.Format("{0} Known Skills ({1} Total SP):", m_grandCharacterInfo.KnownSkillCount, m_grandCharacterInfo.SkillPointTotal.ToString("#,##0"));
            }
        }

        void m_grandCharacterInfo_AttributeChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    m_grandCharacterInfo_AttributeChanged(sender, e);
                }));
                return;
            }

            EnableButtons();
            SetAttributeLabel(lblIntelligence, EveAttribute.Intelligence);
            SetAttributeLabel(lblCharisma, EveAttribute.Charisma);
            SetAttributeLabel(lblPerception, EveAttribute.Perception);
            SetAttributeLabel(lblMemory, EveAttribute.Memory);
            SetAttributeLabel(lblWillpower, EveAttribute.Willpower);

            UpdateCachedCopy();
        }

        private void SetAttributeLabel(Label lblWillpower, EveAttribute eveAttribute)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(eveAttribute.ToString());
            sb.Append(": ");
            sb.Append(m_grandCharacterInfo.GetEffectiveAttribute(eveAttribute).ToString("0.00"));
            double fromImplants = m_grandCharacterInfo.GetAttributeBonusFromImplants(eveAttribute);
            if (fromImplants > 0)
            {
                sb.Append(" (+");
                sb.Append(fromImplants.ToString("0.00"));
                sb.Append(" from implants)");
            }
            lblWillpower.Text = sb.ToString();
        }

        private void m_grandCharacterInfo_BalanceChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    m_grandCharacterInfo_BalanceChanged(sender, e);
                }));
                return;
            }

            EnableButtons();
            lblBalance.Text = "Balance: " + m_grandCharacterInfo.Balance.ToString("#,##0.00") + " ISK";

            UpdateCachedCopy();
        }

        private void m_grandCharacterInfo_BioInfoChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    m_grandCharacterInfo_BioInfoChanged(sender, e);
                }));
                return;
            }

            EnableButtons();
            if (m_grandCharacterInfo.IsCached)
                lblCharacterName.Text = m_grandCharacterInfo.Name + " (cached)";
            else
                lblCharacterName.Text = m_grandCharacterInfo.Name;
            lblBioInfo.Text = m_grandCharacterInfo.Gender + " " +
                m_grandCharacterInfo.Race + " " +
                m_grandCharacterInfo.Bloodline;
            lblCorpInfo.Text = "Corporation: " + m_grandCharacterInfo.CorporationName;

            UpdateCachedCopy();
        }

        private void UpdateCachedCopy()
        {
            if (m_session != null)
            {
                SerializableCharacterInfo sci = m_grandCharacterInfo.ExportSerializableCharacterInfo();
                m_settings.SetCharacterCache(sci);
                m_settings.Save();
            }
        }

        private void GotCharacterImage(EveSession sender, Image i)
        {
            string cacheDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EVEMon";
            string cacheFileName = cacheDir + "\\" + this.GrandCharacterInfo.CharacterId.ToString() + ".png";
            if (i == null)
            {
                if (File.Exists(cacheFileName))
                {
                    try
                    {
                        FileStream fs = new FileStream(cacheFileName, FileMode.Open);
                        i = Image.FromStream(fs, true);
                        fs.Close();
                        fs.Dispose();
                    }
                    catch (Exception e)
                    {
                        ExceptionHandler.LogException(e, false);
                    }
                }
                if (i == null)
                {
                    i = pbCharImage.InitialImage;
                    try
                    {
                        FileStream fs = new FileStream(cacheFileName, FileMode.Create);
                        i.Save(fs, ImageFormat.Png);
                        fs.Close();
                        fs.Dispose();
                    }
                    catch (Exception e)
                    {
                        ExceptionHandler.LogException(e, false);
                    }
                }
            }
            else
            {
                try
                {
                    FileStream fs = new FileStream(cacheFileName, FileMode.Create);
                    i.Save(fs, ImageFormat.Png);
                    fs.Close();
                    fs.Dispose();
                }
                catch (Exception e)
                {
                    ExceptionHandler.LogException(e, false);
                }
            }
            pbCharImage.Image = i;
            updating_pic = false;
        }

        //private DateTime m_lastUpdate = DateTime.MinValue;
        private DateTime m_nextScheduledUpdateAt = DateTime.MinValue;

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (m_throbberRunning)
                return;

            tmrUpdate.Enabled = false;
            StartThrobber();
            if (m_charId < 0)
                GetCharIdAndUpdate();
            else
                UpdateGrandCharacterInfo();
        }

        private void GetCharIdAndUpdate()
        {
#if DEBUG_SINGLETHREAD
            GetCharIdAndUpdateInternal(null);
#else
            ThreadPool.QueueUserWorkItem(new WaitCallback(GetCharIdAndUpdateInternal));
#endif
        }

        private void GetCharIdAndUpdateInternal(object state)
        {
            int gotCharId = -1;
            try
            {
                m_session = EveSession.GetSession(m_cli.Username, m_cli.Password);
                gotCharId = m_session.GetCharacterId(m_cli.CharacterName);
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e, false);
            }
            if (gotCharId < 0)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    ttToolTip.SetToolTip(pbThrobber, "Could not get character data!\nClick to try again.");
                    tmrUpdate.Interval = 1000 * 60 * 30;
                    tmrUpdate.Enabled = true;
                    SetErrorThrobber();
                }));
                return;
            }

            m_charId = gotCharId;
            m_grandCharacterInfo.CharacterId = gotCharId;
            GetCharacterImage();

            UpdateGrandCharacterInfo();
        }

        private void UpdateGrandCharacterInfo()
        {
            m_session.UpdateGrandCharacterInfoAsync(m_grandCharacterInfo, Program.MainWindow,
                new UpdateGrandCharacterInfoCallback(delegate(EveSession s, int timeLeftInCache)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        //m_lastUpdate = DateTime.Now;
                        m_nextScheduledUpdateAt = DateTime.Now + TimeSpan.FromMilliseconds(timeLeftInCache);
                        ttToolTip.SetToolTip(pbThrobber, "Click to update now.");
                        tmrUpdate.Interval = timeLeftInCache;
                        tmrUpdate.Enabled = true;
                        StopThrobber();
                    }));
                }));
        }

        private string m_skillTrainingName;
        private DateTime m_estimatedCompletion;
        private string m_lastCompletedSkill = String.Empty;

        private GrandCharacterInfo m_grandCharacterInfo;

        private string m_shortText = String.Empty;
        private TimeSpan m_shortTimeSpan = TimeSpan.Zero;

        public GrandCharacterInfo GrandCharacterInfo
        {
            get { return m_grandCharacterInfo; }
        }

        public string ShortText
        {
            get { return m_shortText; }
        }

        public TimeSpan ShortTimeSpan
        {
            get { return m_shortTimeSpan; }
        }

        public EventHandler ShortInfoChanged;

        private void CalcSkillRemainText()
        {
            
                DateTime now = DateTime.Now;
                if (m_estimatedCompletion != DateTime.MaxValue)
                {
                    lblTrainingRemain.Text = TimeSpanDescriptive(m_estimatedCompletion);
                    if (m_estimatedCompletion > now)
                    {
                        lblTrainingEst.Text = m_estimatedCompletion.ToString();
                        SetShortData(m_cli.CharacterName + ": " +
                            TimeSpanDescriptiveShort(m_estimatedCompletion),
                            m_estimatedCompletion - now);
                    }
                    else
                    {
                        lblTrainingEst.Text = String.Empty;
                        SetShortData(m_cli.CharacterName + ": Done", TimeSpan.Zero);
                    }
                }
                else
                {
                    lblTrainingRemain.Text = String.Empty;
                    lblTrainingEst.Text = String.Empty;
                    SetShortData(String.Empty, TimeSpan.Zero);
                }
            
        }

        public static string TimeSpanDescriptiveMedium(TimeSpan ts)
        {
            StringBuilder sb = new StringBuilder();
            if (ts < TimeSpan.Zero)
            {
                ts -= (ts + ts);
            }
            if (ts.Days > 0)
            {
                sb.Append(ts.Days.ToString());
                sb.Append("D");
            }
            ts -= TimeSpan.FromDays(ts.Days);
            if (ts.Hours > 0)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(ts.Hours.ToString());
                sb.Append("H");
            }
            ts -= TimeSpan.FromHours(ts.Hours);
            if (ts.Minutes > 0)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(ts.Minutes.ToString());
                sb.Append("M");
            }
            ts -= TimeSpan.FromMinutes(ts.Minutes);
            if (ts.Seconds > 0)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(ts.Seconds.ToString());
                sb.Append("S");
            }
            return sb.ToString();
        }

        public static string TimeSpanDescriptiveShort(DateTime t)
        {
            StringBuilder sb = new StringBuilder();
            if (t > DateTime.Now)
            {
                TimeSpan ts = t - DateTime.Now;
                if (ts.Days > 0)
                {
                    sb.Append(ts.Days.ToString());
                    sb.Append("d");
                }
                ts -= TimeSpan.FromDays(ts.Days);
                if (ts.Hours > 0)
                {
                    sb.Append(ts.Hours.ToString());
                    sb.Append("h");
                }
                ts -= TimeSpan.FromHours(ts.Hours);
                if (ts.Minutes > 0)
                {
                    sb.Append(ts.Minutes.ToString());
                    sb.Append("m");
                }
                ts -= TimeSpan.FromMinutes(ts.Minutes);
                if (ts.Seconds > 0)
                {
                    sb.Append(ts.Seconds.ToString());
                    sb.Append("s");
                }
                return sb.ToString();
            }
            else
            {
                return "Done";
            }
        }

        private void SetShortData(string newShortText, TimeSpan timeSpan)
        {
            bool fireEvent = false;
            if (newShortText != m_shortText || timeSpan != m_shortTimeSpan)
                fireEvent = true;
            m_shortText = newShortText;
            m_shortTimeSpan = timeSpan;
            if (fireEvent && ShortInfoChanged != null)
                ShortInfoChanged(this, new EventArgs());
        }

        private string TimeSpanDescriptive(DateTime t)
        {
            StringBuilder sb = new StringBuilder();
            if (t > DateTime.Now)
            {
                TimeSpan ts = t - DateTime.Now;
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
                return sb.ToString();
            }
            else
            {
                return "Completed.";
            }
        }

        private void UpdateNextUpdateLabel()
        {
            if (this.Visible)
            {
                if (m_throbberRunning)
                {
                    lblUpdateTimer.Visible = false;
                    return;
                }

                TimeSpan ts = m_nextScheduledUpdateAt - DateTime.Now;
                if (ts < TimeSpan.Zero || ts > TimeSpan.FromMinutes(60))
                {
                    lblUpdateTimer.Visible = false;
                }
                else
                {
                    lblUpdateTimer.Text =
                        String.Format("{0:d2}:{1:d2}", ts.Minutes, ts.Seconds);
                    lblUpdateTimer.Visible = true;
                }
            }
        }

        private int m_lastTickSPPaint = 0;

        private void tmrTick_Tick(object sender, EventArgs e)
        {

            CalcSkillRemainText();
            UpdateNextUpdateLabel();

            if (m_currentlyVisible)
            {
                GrandSkill trainingSkill = m_grandCharacterInfo.CurrentlyTrainingSkill;
                if (trainingSkill != null)
                {
                    if (trainingSkill.CurrentSkillPoints != m_lastTickSPPaint)
                    {
                        m_lastTickSPPaint = trainingSkill.CurrentSkillPoints;
                        int idx = lbSkills.Items.IndexOf(trainingSkill);
                        if (idx >= 0)
                            lbSkills.Invalidate(lbSkills.GetItemRectangle(idx));
                        int sgidx = lbSkills.Items.IndexOf(trainingSkill.SkillGroup);
                        lbSkills.Invalidate(lbSkills.GetItemRectangle(sgidx));
                        UpdateSkillHeaderStats();
                    }
                }
            }

            if (m_estimatedCompletion < DateTime.Now && m_skillTrainingName != m_lastCompletedSkill)
            {
                m_lastCompletedSkill = m_skillTrainingName;
                OnSkillTrainingComplete(m_cli.CharacterName, m_skillTrainingName);
            }
            if (updating_pic == false && pbCharImage.Image == null)
            {
                updating_pic = true;
                GetCharacterImage();
            }
        }

        private bool updating_pic = false;

        private void GetCharacterImage()
        {
            if (pbCharImage.Image == null)
            {
                string cacheDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EVEMon";
                string cacheFileName = cacheDir + "\\" + this.GrandCharacterInfo.CharacterId.ToString() + ".png";
                if (File.Exists(cacheFileName))
                {
                    try
                    {
                        FileStream fs = new FileStream(cacheFileName, FileMode.Open);
                        pbCharImage.Image = Image.FromStream(fs, true);
                        fs.Close();
                        fs.Dispose();
                    }
                    catch (Exception e)
                    {
                        ExceptionHandler.LogException(e, false);
                    }
                    updating_pic = false;
                }
                if (pbCharImage.Image == null)
                {
                    EveSession.GetCharacterImageAsync(this.GrandCharacterInfo.CharacterId, new GetImageCallback(GotCharacterImage));
                }
            }
        }

        private void UpdateCharacterImage()
        {
            pbCharImage.Image = null;
            EveSession.GetCharacterImageAsync(this.GrandCharacterInfo.CharacterId, new GetImageCallback(GotCharacterImage));
        }

        private enum SaveFormat
        {
            None = 0,
            Text = 1,
            Html = 2,
            Xml = 3
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sfdSaveDialog.FileName = m_grandCharacterInfo.Name;
            sfdSaveDialog.FilterIndex = (int)SaveFormat.Xml;
            DialogResult dr = sfdSaveDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                SaveFile((SaveFormat)sfdSaveDialog.FilterIndex, sfdSaveDialog.FileName);
            }
        }

        private void SaveTextFile(string fileName)
        {
            SerializableCharacterInfo ci = m_grandCharacterInfo.ExportSerializableCharacterInfo();
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                MethodInvoker writeSep = new MethodInvoker(delegate
                {
                    sw.WriteLine("=======================================================================");
                });
                MethodInvoker writeSubSep = new MethodInvoker(delegate
                {
                    sw.WriteLine("-----------------------------------------------------------------------");
                });
                sw.WriteLine("BASIC INFO");
                writeSep();
                sw.WriteLine("     Name: {0}", ci.Name);
                sw.WriteLine("   Gender: {0}", ci.Gender);
                sw.WriteLine("     Race: {0}", ci.Race);
                sw.WriteLine("Bloodline: {0}", ci.BloodLine);
                sw.WriteLine("  Balance: {0} ISK", ci.Balance.ToString("#,##0.00"));
                sw.WriteLine();
                sw.WriteLine("Intelligence: {0}", ci.Attributes.AdjustedIntelligence.ToString("#0.00").PadLeft(5));
                sw.WriteLine("    Charisma: {0}", ci.Attributes.AdjustedCharisma.ToString("#0.00").PadLeft(5));
                sw.WriteLine("  Perception: {0}", ci.Attributes.AdjustedPerception.ToString("#0.00").PadLeft(5));
                sw.WriteLine("      Memory: {0}", ci.Attributes.AdjustedMemory.ToString("#0.00").PadLeft(5));
                sw.WriteLine("   Willpower: {0}", ci.Attributes.AdjustedWillpower.ToString("#0.00").PadLeft(5));
                sw.WriteLine();
                if (ci.AttributeBonuses.Bonuses.Count > 0)
                {
                    sw.WriteLine("IMPLANTS");
                    writeSep();
                    foreach (SerializableEveAttributeBonus tb in ci.AttributeBonuses.Bonuses)
                    {
                        sw.WriteLine("+{0} {1} : {2}", tb.Amount, tb.EveAttribute.ToString().PadRight(13), tb.Name);
                    }
                    sw.WriteLine();
                }
                sw.WriteLine("SKILLS");
                writeSep();
                foreach (SerializableSkillGroup sg in ci.SkillGroups)
                {
                    sw.WriteLine("{0}, {1} Skill{2}, {3} Points",
                        sg.Name, sg.Skills.Count, sg.Skills.Count > 1 ? "s" : "", sg.GetTotalPoints().ToString("#,##0"));
                    foreach (SerializableSkill s in sg.Skills)
                    {
                        string skillDesc = s.Name + " " + GrandSkill.GetRomanSkillNumber(s.Level) + " (" + s.Rank.ToString() + ")";
                        sw.WriteLine(": {0} {1}/{2} Points",
                            skillDesc.PadRight(40), s.SkillPoints.ToString("#,##0"), s.SkillLevel5.ToString("#,##0"));
                        if (ci.SkillInTraining != null && ci.SkillInTraining.SkillName == s.Name)
                        {
                            sw.WriteLine(":  (Currently training to level {0}, completes {1})",
                                GrandSkill.GetRomanSkillNumber(ci.SkillInTraining.TrainingToLevel),
                                ci.SkillInTraining.EstimatedCompletion.ToString());
                        }
                    }
                    writeSubSep();
                }
            }
        }

        private void SaveFile(SaveFormat saveFormat, string fileName)
        {
            if (saveFormat == SaveFormat.Text)
            {
                SaveTextFile(fileName);
                return;
            }
            try
            {
                Stream outerStream;
                XPathDocument xpdoc;
                if (saveFormat == SaveFormat.Xml)
                    outerStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                else
                    outerStream = new MemoryStream(32767);
                try
                {
                    using (XmlTextWriter xtw = new XmlTextWriter(outerStream, Encoding.UTF8))
                    {
                        if (saveFormat == SaveFormat.Xml)
                        {
                            xtw.Indentation = 1;
                            xtw.IndentChar = '\t';
                            xtw.Formatting = Formatting.Indented;
                        }
                        XmlSerializer xs = new XmlSerializer(typeof(SerializableCharacterInfo));
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
                        xs.Serialize(xtw, m_grandCharacterInfo.ExportSerializableCharacterInfo(), ns);
                        xtw.Flush();

                        if (saveFormat == SaveFormat.Xml)
                            return;

                        MemoryStream ms = (MemoryStream)outerStream;
                        ms.Position = 0;
                        using (StreamReader tr = new StreamReader(ms))
                        {
                            xpdoc = new XPathDocument(tr);
                        }
                    }
                }
                finally
                {
                    outerStream.Dispose();
                }

                XslCompiledTransform xstDoc2 = new XslCompiledTransform();
                using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("EVEMon.output-" + saveFormat.ToString().ToLower() + ".xsl"))
                using (XmlTextReader xtr = new XmlTextReader(s))
                {
                    xstDoc2.Load(xtr);
                }

                using (StreamWriter sw = new StreamWriter(fileName, false))
                using (XmlTextWriter xtw = new XmlTextWriter(sw))
                {
                    xtw.Indentation = 1;
                    xtw.IndentChar = '\t';
                    xtw.Formatting = Formatting.Indented;
                    xstDoc2.Transform(xpdoc, null, xtw);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, true);
                MessageBox.Show(
                    "Failed to save:\n" + ex.Message, "Could not save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbSkills_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            object item = lbSkills.Items[e.Index];
            
            if (item is GrandSkillGroup)
            {
                ((GrandSkillGroup)item).Draw(e);
            }
            else if (item is GrandSkill)
            {
                ((GrandSkill)item).Draw(e);
            }
        }

        private void lbSkills_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            object item = lbSkills.Items[e.Index];
            if (item is GrandSkillGroup)
                e.ItemHeight = GrandSkillGroup.Height;
            else if (item is GrandSkill)
                e.ItemHeight = GrandSkill.Height;
        }

        private void lbSkills_MouseWheel(object sender, MouseEventArgs e)
        {
            // Update the drawing based upon the mouse wheel scrolling.
            int numberOfItemLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            int Lines = numberOfItemLinesToMove;
            int direction = numberOfItemLinesToMove / Math.Abs(numberOfItemLinesToMove);
            int[] numberOfPixelsToMove = new int[numberOfItemLinesToMove * direction];
            for (int i = 1; i <= Math.Abs(numberOfItemLinesToMove); i++)
            {
                object item = null;
                if (direction == Math.Abs(direction))
                {
                    // Going up
                    if (lbSkills.TopIndex - i >= 0)
                        item = lbSkills.Items[lbSkills.TopIndex - i];
                }
                else
                {
                    // Going down
                    int h = 0; // height of items from current topindex inclusive
                    for (int j = lbSkills.TopIndex + i - 1; j < lbSkills.Items.Count; j++)
                    {
                        if (lbSkills.Items[j] is GrandSkillGroup)
                            h += GrandSkillGroup.Height;
                        else if (lbSkills.Items[j] is GrandSkill)
                            h += GrandSkill.Height;
                    }
                    if (h > lbSkills.ClientSize.Height)
                        item = lbSkills.Items[lbSkills.TopIndex + i - 1];
                }
                if (item != null)
                {
                    if (item is GrandSkillGroup)
                        numberOfPixelsToMove[i - 1] = GrandSkillGroup.Height * direction;
                    else if (item is GrandSkill)
                        numberOfPixelsToMove[i - 1] = GrandSkill.Height * direction;
                }
                else
                    Lines -= direction;
            }
            if (Lines != 0)
            {
                // The Array 'numberOfPixelsToMove' contains the number of pixels the
                // list box 'lbSkills' needs to scroll for each line... the question
                // is, how to tell it that and get it to do so smoothly.
                /* // This doesn't work...
                for (int i = 0; i < Math.Abs(Lines); i++)
                {
                    System.Drawing.Drawing2D.Matrix translateMatrix = new System.Drawing.Drawing2D.Matrix();
                    translateMatrix.Translate(0, numberOfPixelsToMove[i]);
                    mousePath.Transform(translateMatrix);
                } */
                lbSkills.Invalidate();
                // invalidate is a temporary fix that does give limited functionality for purpose.
            }
        }

        private void btnPlan_Click(object sender, EventArgs e)
        {
            Plan p = null;
            using (PlanSelectWindow psw = new PlanSelectWindow(m_settings, m_grandCharacterInfo))
            {
                if (m_cfi != null)
                    psw.CharKey = m_cfi.Filename;
                DialogResult dr = psw.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;
                p = psw.ResultPlan;
            }
            if (p == null || p.Name == null)
            {
                bool doAgain = true;
                while (doAgain)
                {
                    using (NewPlanWindow npw = new NewPlanWindow())
                    {
                        DialogResult dr = npw.ShowDialog();
                        if (dr == DialogResult.Cancel)
                            return;
                        string planName = npw.Result;

                        if (p == null)
                        {
                            p = new Plan();
                        }
                        try
                        {
                            if (m_cfi == null)
                                m_settings.AddPlanFor(m_grandCharacterInfo.Name, p, planName);
                            else
                                m_settings.AddPlanFor(m_cfi.Filename, p, planName);
                            doAgain = false;
                        }
                        catch (ApplicationException err)
                        {
                            ExceptionHandler.LogException(err, true);
                            DialogResult xdr = MessageBox.Show(err.Message, "Failed to Add Plan", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            if (xdr == DialogResult.Cancel)
                                return;
                        }
                    }
                }
            }

            p.ShowEditor(m_settings, m_grandCharacterInfo);
        }

        private void btnMoreOptions_Click(object sender, EventArgs e)
        {
            cmsMoreOptions.Show(btnMoreOptions,
                btnMoreOptions.PointToClient(MousePosition), ToolStripDropDownDirection.Default);
        }
        
        private void lbSkills_MouseMove(object sender, MouseEventArgs e)
        {
            /*int index = lbSkills.IndexFromPoint(e.X, e.Y);
            object item;
            if (index < 0 || index >= lbSkills.Items.Count)
                item = null;
            else
                item = lbSkills.Items[index];

            ttToolTip.IsBalloon = true;

            if (item is GrandSkillGroup)
            {
                //GrandSkillGroup sg = (GrandSkillGroup)item;
                //SkillGroup description is not in the skills.xml
                //They do exist though, see: http://www.eve-online.com/itemdatabase/skillsaccessories/skills/default.asp
                
                //nothing to display in the tooltip, turn it off
                ttToolTip.Active = false;
            }
            else if (item is GrandSkill)
            {
                GrandSkill s = (GrandSkill)item;

                ttToolTip.Active = true;
                ttToolTip.SetToolTip(lbSkills, s.Description.ToString());
            }
            else
            {
                ttToolTip.Active = false;
            }*/
        }

        private void lbSkills_MouseClick(object sender, MouseEventArgs e)
        {
            int index = lbSkills.IndexFromPoint(e.X, e.Y);
            object item;
            if (index < 0 || index >= lbSkills.Items.Count)
                item = null;
            else
                item = lbSkills.Items[index];

            ttToolTip.IsBalloon = true;
            ttToolTip.UseAnimation = true;
            ttToolTip.UseFading = true;
            ttToolTip.AutoPopDelay = 10000;

            if (e.Button == MouseButtons.Right)
            {
                //invoke skill planner maybe
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (item is GrandSkillGroup)
                {
                    GrandSkillGroup sg = (GrandSkillGroup)item;
                    Rectangle itemRect = lbSkills.GetItemRectangle(lbSkills.Items.IndexOf(item));
                    Rectangle buttonRect = sg.GetButtonRectangle(itemRect);
                    if (buttonRect.Contains(e.Location))
                    {
                        ToggleGroupExpandCollapse(sg);
                        return;
                    }

                    int TotalPoints = 0;
                    //int PointsRemaining = 0;
                    double percentDonePoints = 0.0;
                    double percentDoneSkills = 0.0;
                    //TimeSpan timeSpent = 0;
                    //TimeSpan totalTime = 0;

                    foreach (GrandSkill s in sg)
                    {
                        TotalPoints += s.GetPointsRequiredForLevel(5);
                    }

                    if (sg.GetTotalPoints() < TotalPoints)
                    {
                        percentDonePoints = (Convert.ToDouble(sg.GetTotalPoints()) / Convert.ToDouble(TotalPoints) * 100);
                        percentDoneSkills = Convert.ToDouble(sg.KnownCount) / Convert.ToDouble(sg.Count);
                        //PointsRemaining = TotalPoints - sg.GetTotalPoints();

                        string SkillGroupStats = String.Format("Points Completed: {0}/{1} ({2}%)\nSkills Known: {3}/{4} ({5})",
                            sg.GetTotalPoints().ToString("#,##0"), TotalPoints.ToString("#,##0"),
                            percentDonePoints.ToString("N3"), sg.KnownCount.ToString("#"),
                            sg.Count.ToString("#"), percentDoneSkills.ToString("P0"));

                        ttToolTip.Active = true;
                        ttToolTip.SetToolTip(lbSkills, SkillGroupStats);
                    }
                    else // we must have learned all the skills in this group to level 5
                    {//I wish I could test this :)
                        string Done = String.Format("Skill Group completed: {0}/{1} (100%)\nSkills: {2}/{3} (100%)",
                        sg.GetTotalPoints().ToString("#,##0"), TotalPoints.ToString("#,##0"), sg.KnownCount.ToString("#"), sg.Count.ToString("#"));

                        ttToolTip.Active = true;
                        ttToolTip.SetToolTip(lbSkills, Done);
                    }
                }
                else if (item is GrandSkill)
                {
                    GrandSkill s = (GrandSkill)item;
                    double percentDone = 0.0;
                    int NextLevel = 0;
                    int CurrentSP = s.CurrentSkillPoints;
                    int reqToThisLevel = s.GetPointsRequiredForLevel(s.Level);
                    int reqToNextLevel = 0;
                    int pointsInThisLevel = 0;
                    double deltaPointsOfLevel = 0.0;
                    int PointsRemain = 0;

                    if (CurrentSP > s.GetPointsRequiredForLevel(s.Level))
                    { //We must have completed some, but not all, of level II, III or IV
                        NextLevel = s.Level + 1;

                        pointsInThisLevel = CurrentSP - reqToThisLevel;
                        reqToNextLevel = s.GetPointsRequiredForLevel(NextLevel);
                        deltaPointsOfLevel = Convert.ToDouble(reqToNextLevel - reqToThisLevel);
                        percentDone = pointsInThisLevel / deltaPointsOfLevel;
                        PointsRemain = s.GetPointsRequiredForLevel(NextLevel) - s.CurrentSkillPoints;
                        string CurrentlyDone = String.Format("Partially Completed lvl {0}: {1}/{2} ({3})", GrandSkill.GetRomanSkillNumber(NextLevel), s.CurrentSkillPoints.ToString("#,##0"), s.GetPointsRequiredForLevel(NextLevel).ToString("#,##0"), percentDone.ToString("P0"));
                        string ToNextLevel = String.Format("To Level {0}: {1} Skill Points remaining", GrandSkill.GetRomanSkillNumber(NextLevel), PointsRemain.ToString("#,##0"));
                        ttToolTip.Active = true;
                        ttToolTip.SetToolTip(lbSkills, CurrentlyDone + "\n" + ToNextLevel + "\nTraining Time remaining: " + GrandSkill.TimeSpanToDescriptiveText(s.GetTrainingTimeToLevel(NextLevel), DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.UppercaseText) + "\n" + s.DescriptionNl.ToString() + "\nPrimary: " + s.PrimaryAttribute.ToString() + ", Secondary: " + s.SecondaryAttribute.ToString());
                    }
                    else if (CurrentSP == s.GetPointsRequiredForLevel(s.Level))// We've completed all the skill points for the current level
                    {
                        if (s.Level != 5)
                        {
                            NextLevel = s.Level + 1;
                            pointsInThisLevel = CurrentSP - reqToThisLevel;
                            reqToNextLevel = s.GetPointsRequiredForLevel(NextLevel);
                            deltaPointsOfLevel = Convert.ToDouble(reqToNextLevel - reqToThisLevel);
                            percentDone = pointsInThisLevel / deltaPointsOfLevel;
                            PointsRemain = s.GetPointsRequiredForLevel(NextLevel) - s.CurrentSkillPoints;
                            string CurrentlyDone = String.Format("Completed lvl {0}: {1}/{2} ({3})", GrandSkill.GetRomanSkillNumber(s.Level), s.CurrentSkillPoints.ToString("#,##0"), s.GetPointsRequiredForLevel(s.Level).ToString("#,##0"), percentDone.ToString("P0"));
                            string ToNextLevel = String.Format("To Level {0}: {1} Skill Points required", GrandSkill.GetRomanSkillNumber(NextLevel), PointsRemain.ToString("#,##0"));
                            ttToolTip.Active = true;
                            ttToolTip.SetToolTip(lbSkills,
                                CurrentlyDone + "\n" + ToNextLevel + "\nTraining Time: " + GrandSkill.TimeSpanToDescriptiveText(s.GetTrainingTimeToLevel(NextLevel), DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.UppercaseText) + "\n" + s.DescriptionNl.ToString() + "\nPrimary: " + s.PrimaryAttribute.ToString() + ", Secondary: " + s.SecondaryAttribute.ToString());
                        }
                        else// training completed
                        {
                            ttToolTip.Active = true;
                            ttToolTip.SetToolTip(lbSkills,
                                String.Format("Level V Complete: {0}/{1} (100%)\nNo further training required\n{2}\nPrimary: {3}, Secondary: {4}",
                                s.CurrentSkillPoints.ToString("#,##0"),
                                s.GetPointsRequiredForLevel(5).ToString("#,##0"),
                                s.DescriptionNl.ToString(), s.PrimaryAttribute.ToString(), s.SecondaryAttribute.ToString()));
                        }
                    }
                    else// training hasn't got past level 1 yet
                    {
                        NextLevel = s.Level + 1;//thus should always be 1

                        //pointsInThisLevel = reqToThisLevel - CurrentSP;
                        //reqToNextLevel = s.GetPointsRequiredForLevel(NextLevel);
                        //deltaPointsOfLevel = Convert.ToDouble(reqToNextLevel - reqToThisLevel);
                        percentDone = Convert.ToDouble(CurrentSP) / Convert.ToDouble(s.GetPointsRequiredForLevel(NextLevel));
                        PointsRemain = s.GetPointsRequiredForLevel(NextLevel) - s.CurrentSkillPoints;
                        string CurrentlyDone = String.Format("Partially Completed lvl {0}: {1}/{2} ({3})", GrandSkill.GetRomanSkillNumber(NextLevel), s.CurrentSkillPoints.ToString("#,##0"), s.GetPointsRequiredForLevel(NextLevel).ToString("#,##0"), percentDone.ToString("P0"));
                        string ToNextLevel = String.Format("To Level {0}: {1} Skill Points remaining", GrandSkill.GetRomanSkillNumber(NextLevel), PointsRemain.ToString("#,##0"));
                        ttToolTip.Active = true;
                        ttToolTip.SetToolTip(lbSkills, CurrentlyDone + "\n" + ToNextLevel + "\nTraining Time remaining: " + GrandSkill.TimeSpanToDescriptiveText(s.GetTrainingTimeToLevel(NextLevel), DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.UppercaseText) + "\n" + s.DescriptionNl.ToString() + "\nPrimary: " + s.PrimaryAttribute.ToString() + ", Secondary: " + s.SecondaryAttribute.ToString());
                    }
                }
            }
        }

        private void ToggleGroupExpandCollapse(GrandSkillGroup gsg)
        {
            bool toCollapse;
            if (gsg.isCollapsed)
                toCollapse = false;
            else
                toCollapse = true;
            m_groupCollapsed[gsg] = toCollapse;
            gsg.isCollapsed = m_groupCollapsed[gsg];
            if (toCollapse)
            {
                // Remove the skills in the group from the list
                foreach (GrandSkill gs in gsg)
                {
                    if (gs.Known)
                        lbSkills.Items.RemoveAt(lbSkills.Items.IndexOf(gs));
                }

                Pair<string, string> grp = new Pair<string,string> (m_grandCharacterInfo.Name, gsg.Name);
                m_settings.CollapsedGroups.Add(grp);
            }
            else
            {
                List<GrandSkill> skillList = new List<GrandSkill>();
                foreach (GrandSkill gs in gsg)
                {
                    skillList.Add(gs);
                }
                SkillChangedEventArgs args = new SkillChangedEventArgs(skillList.ToArray());
                m_grandCharacterInfo_SkillChanged(this, args);
                foreach (Pair<string, string> grp in m_settings.CollapsedGroups) {
                    if ((grp.A == m_grandCharacterInfo.Name) && (grp.B == gsg.Name))
                    {
                        m_settings.CollapsedGroups.Remove(grp);
                        break;
                    }
                }
                //void m_grandCharacterInfo_SkillChanged(object sender, SkillChangedEventArgs e)
            }
            lbSkills.Invalidate(lbSkills.GetItemRectangle(lbSkills.Items.IndexOf(gsg)));
        }

        private bool m_throbberRunning = false;
        private bool m_throbberError = true;
        private int m_throbberFrame = 0;

        private void StartThrobber()
        {
            m_throbberRunning = true;
            tmrThrobber.Enabled = true;
            ttToolTip.SetToolTip(pbThrobber, "Retrieving data from EVE Online...");
        }

        private void StopThrobber()
        {
            m_throbberRunning = false;
            m_throbberError = false;
        }

        private void SetErrorThrobber()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(SetErrorThrobber));
                return;
            }

            m_throbberRunning = false;
            m_throbberError = true;
        }

        private void tmrThrobber_Tick(object sender, EventArgs e)
        {
            if (m_currentlyVisible)
            {
                tmrThrobber.Enabled = false;
                if (m_throbberRunning)
                {
                    m_throbberFrame = ((m_throbberFrame + 1) % 8);
                    pbThrobber.Image = ThrobberImages[m_throbberFrame + 1];
                    tmrThrobber.Enabled = true;
                }
                else if (m_throbberError)
                {
                    bool blinkState = (DateTime.Now.Millisecond > 500);
                    if (!blinkState)
                        pbThrobber.Image = null;
                    else
                        pbThrobber.Image = ThrobberImages[0];
                    tmrThrobber.Enabled = true;
                }
                else
                {
                    pbThrobber.Image = ThrobberImages[0];
                    m_throbberFrame = 0;
                }
            }
        }

        private void pbThrobber_Click(object sender, EventArgs e)
        {
            if (!m_throbberError)
            {
                tmrUpdate_Tick(null, null);
                UpdateNextUpdateLabel();
            }
            else
            {
                cmsThrobberMenu.Show(MousePosition);
            }
        }

        private void ttToolTip_Popup(object sender, PopupEventArgs e)
        {
            //if (e.AssociatedControl == pbThrobber)
            //{
            //    if (!m_throbberRunning && !m_throbberError)
            //    {
            //        ttToolTip.SetToolTip(pbThrobber,
            //            String.Format("Last update: {0}\nNext update in: {1}\nClick to update now.",
            //            m_lastUpdate.ToString(),
            //            GrandSkill.TimeSpanToDescriptiveText(m_nextScheduledUpdateAt - DateTime.Now, DescriptiveTextOptions.Default)
            //        ));
            //    }
            //}
        }

        private void lbSkills_MouseEnter(object sender, EventArgs e)
        {
            ttToolTip.Active = false;
        }

        private void lbSkills_MouseLeave(object sender, EventArgs e)
        {
            ttToolTip.Active = false;
        }

        private void pbThrobber_MouseEnter(object sender, EventArgs e)
        {
            ttToolTip.Active = true;
        }

        private void llToggleAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<GrandSkillGroup> toggles = new List<GrandSkillGroup>();
            bool? setCollapsed = null;
            foreach (object o in lbSkills.Items)
            {
                if (o is GrandSkillGroup)
                {
                    GrandSkillGroup gsg = (GrandSkillGroup)o;
                    bool isCollapsed = (m_groupCollapsed.ContainsKey(gsg) && m_groupCollapsed[gsg]);
                    if (setCollapsed == null)
                    {
                        setCollapsed = !isCollapsed;
                    }
                    if (isCollapsed != setCollapsed)
                    {
                        toggles.Add(gsg);
                    }
                }
            }
            lbSkills.BeginUpdate();
            try
            {
                foreach (GrandSkillGroup toggroup in toggles)
                {
                    ToggleGroupExpandCollapse(toggroup);
                }
            }
            finally
            {
                lbSkills.EndUpdate();
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CharacterMonitor_Load(object sender, EventArgs e)
        {

        }

        private void miHitEveO_Click(object sender, EventArgs e)
        {
            tmrUpdate_Tick(this, new EventArgs());
        }

        private void miChangeInfo_Click(object sender, EventArgs e)
        {
            using (ChangeLoginWindow f = new ChangeLoginWindow())
            {
                f.CharacterName = m_grandCharacterInfo.Name;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    m_cli.Username = f.Username;
                    m_cli.Password = f.Password;
                    m_session = EveSession.GetSession(f.Username, f.Password);
                    m_charId = -1;
                    tmrUpdate_Tick(this, new EventArgs());

                    m_settings.Save();
                }
            }
        }

        private void miManualImplants_Click(object sender, EventArgs e)
        {
            using (ManualImplantWindow f = new ManualImplantWindow(m_grandCharacterInfo.AttributeBonuses))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    m_grandCharacterInfo.SuppressEvents();
                    try
                    {
                        for (int i = 0; i < m_grandCharacterInfo.AttributeBonuses.Count; i++)
                        {
                            GrandEveAttributeBonus geab = m_grandCharacterInfo.AttributeBonuses[i];
                            if (geab.Manual)
                            {
                                m_grandCharacterInfo.AttributeBonuses.RemoveAt(i);
                                i--;
                            }
                        }
                        foreach (GrandEveAttributeBonus b in f.ResultBonuses)
                        {
                            m_grandCharacterInfo.AttributeBonuses.Add(b);
                        }
                    }
                    finally
                    {
                        m_grandCharacterInfo.ResumeEvents();
                    }
                }
            }
        }

        public void ForceUpdate()
        {
            CalcSkillRemainText();
        }

        private void mi_UpdatePicture_Click(object sender, EventArgs e)
        {
            UpdateCharacterImage();
        }

        private void pbCharImage_Click(object sender, EventArgs e)
        {
            cmsPictureOptions.Show(MousePosition);
        }
    }

    public delegate void SkillTrainingCompletedHandler(object sender, SkillTrainingCompletedEventArgs e);

    public class SkillTrainingCompletedEventArgs : EventArgs
    {
        private string m_skillName;

        public string SkillName
        {
            get { return m_skillName; }
        }

        private string m_characterName;

        public string CharacterName
        {
            get { return m_characterName; }
        }

        public SkillTrainingCompletedEventArgs(string charName, string skillName)
        {
            m_skillName = skillName;
            m_characterName = charName;
        }
    }
}
