using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Serialization;

using EveCharacterMonitor.SkillPlanner;

namespace EveCharacterMonitor
{
    public partial class CharacterMonitor : UserControl
    {
        public CharacterMonitor()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
#if DEBUG
            btnDebugError.Visible = true;
#endif
        }

        private Settings m_settings;
        private CharLoginInfo m_cli;
        private EveSession m_session;
        private int m_charId;

        public CharacterMonitor(Settings s, CharLoginInfo cli)
            : this()
        {
            m_settings = s;
            m_cli = cli;
        }

        private Image[] m_throbberImages;

        public Image[] ThrobberImages
        {
            get { return m_throbberImages; }
            set { m_throbberImages = value; }
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

            m_grandCharacterInfo = new GrandCharacterInfo(m_charId, m_cli.CharacterName);
            m_grandCharacterInfo.BioInfoChanged += new EventHandler(m_grandCharacterInfo_BioInfoChanged);
            m_grandCharacterInfo.BalanceChanged += new EventHandler(m_grandCharacterInfo_BalanceChanged);
            m_grandCharacterInfo.AttributeChanged += new EventHandler(m_grandCharacterInfo_AttributeChanged);
            m_grandCharacterInfo.SkillChanged += new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);

            SerializableCharacterInfo sci = m_settings.GetCharacterInfo(m_cli.CharacterName);
            if (sci != null)
                m_grandCharacterInfo.AssignFromSerializableCharacterInfo(sci);

            tmrUpdate.Interval = 10;
            tmrUpdate.Enabled = true;
            tmrTick.Enabled = true;
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
                        // Find the existing listbox item...
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

                    if (gs.InTraining)
                    {
                        m_skillTrainingName = gs.Name + " " + GrandSkill.GetRomanSkillNumber(gs.TrainingToLevel);
                        lblTrainingSkill.Text = m_skillTrainingName;
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
            lblSkillHeader.Text = String.Format("{0} Known Skills ({1} Total SP):", m_grandCharacterInfo.KnownSkillCount, m_grandCharacterInfo.SkillPointTotal.ToString("#,##0"));
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
            SerializableCharacterInfo sci = m_grandCharacterInfo.ExportSerializableCharacterInfo();
            m_settings.SetCharacterCache(sci);
            m_settings.Save();
        }

        public void Stop()
        {
            if (m_session != null)
                m_session = null;
            tmrTick.Enabled = false;
            tmrUpdate.Enabled = false;
        }

        private DateTime m_tryImageAgainTime = DateTime.MaxValue;

        private void GotCharacterImage(EveSession sender, Image i)
        {
            pbCharImage.Image = i;
            if (i == null)
                m_tryImageAgainTime = DateTime.Now + TimeSpan.FromSeconds(10);
            else
                m_tryImageAgainTime = DateTime.MaxValue;
        }

        private DateTime m_lastUpdate = DateTime.MinValue;
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
            ThreadPool.QueueUserWorkItem(delegate
            {
                int gotCharId = -1;
                try
                {
                    m_session = EveSession.GetSession(m_cli.Username, m_cli.Password);
                    gotCharId = m_session.GetCharacterId(m_cli.CharacterName);
                }
                catch { }
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
            });
        }

        private void UpdateGrandCharacterInfo()
        {
            m_session.UpdateGrandCharacterInfoAsync(m_grandCharacterInfo,
                new UpdateGrandCharacterInfoCallback(delegate (EveSession s, int timeLeftInCache)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        m_lastUpdate = DateTime.Now;
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
                        now - m_estimatedCompletion);
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

        private void tmrTick_Tick(object sender, EventArgs e)
        {
            CalcSkillRemainText();

            GrandSkill trainingSkill = m_grandCharacterInfo.CurrentlyTrainingSkill;
            if (trainingSkill != null)
            {
                int idx = lbSkills.Items.IndexOf(trainingSkill);
                lbSkills.Invalidate(lbSkills.GetItemRectangle(idx));
                int sgidx = lbSkills.Items.IndexOf(trainingSkill.SkillGroup);
                lbSkills.Invalidate(lbSkills.GetItemRectangle(sgidx));
                UpdateSkillHeaderStats();
            }

            if (m_estimatedCompletion < DateTime.Now && m_skillTrainingName != m_lastCompletedSkill)
            {
                m_lastCompletedSkill = m_skillTrainingName;
                OnSkillTrainingComplete(m_cli.CharacterName, m_skillTrainingName);
            }
            if (m_tryImageAgainTime <= DateTime.Now && pbCharImage.Image == null)
            {
                GetCharacterImage();
            }
        }

        private void GetCharacterImage()
        {
            m_tryImageAgainTime = DateTime.MaxValue;
            m_session.GetCharaterImageAsync(m_charId, new GetCharacterImageCallback(GotCharacterImage));
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
            sfdSaveDialog.FileName = m_cli.CharacterName;
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
                        sg.Name, sg.Skills.Count, sg.Skills.Count>1 ? "s" : "", sg.GetTotalPoints().ToString("#,##0"));
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
                    using (XmlTextWriter xtw = new XmlTextWriter(outerStream, System.Text.Encoding.UTF8))
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
                using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("EveCharacterMonitor.output-" + saveFormat.ToString().ToLower() + ".xsl"))
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
                MessageBox.Show(
                    "Failed to save:\n" + ex.Message, "Could not save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private const int SKILL_HEADER_HEIGHT = 21;
        private const int SKILL_DETAIL_HEIGHT = 16;

        private void lbSkills_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            object item = lbSkills.Items[e.Index];
            Graphics g = e.Graphics;

            if (item is GrandSkillGroup)
            {
                GrandSkillGroup sg = (GrandSkillGroup)item;

                using (Brush b = new SolidBrush(Color.FromArgb(75, 75, 75)))
                {
                    g.FillRectangle(b, e.Bounds);
                }
                using (Font boldf = new Font(lbSkills.Font, FontStyle.Bold))
                {
                    Size titleSizeInt = TextRenderer.MeasureText(g, sg.Name, boldf, new Size(0, 0), TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                    Point titleTopLeftInt = new Point(e.Bounds.Left + 3,
                        e.Bounds.Top + ((e.Bounds.Height / 2) - (titleSizeInt.Height / 2)));
                    Point detailTopLeftInt = new Point(titleTopLeftInt.X + titleSizeInt.Width, titleTopLeftInt.Y);

                    string detailText = String.Format(", {0} Skill{1}, {2} Points",
                        sg.KnownCount, sg.KnownCount > 1 ? "s" : "", sg.GetTotalPoints().ToString("#,##0"));
                    TextRenderer.DrawText(g, sg.Name, boldf, titleTopLeftInt, Color.White);
                    TextRenderer.DrawText(g, detailText, lbSkills.Font, detailTopLeftInt, Color.White);
                }
            }
            else if (item is GrandSkill)
            {
                GrandSkill s = (GrandSkill)item;

                if ((e.Index % 2) == 0)
                    g.FillRectangle(Brushes.White, e.Bounds);
                else
                    g.FillRectangle(Brushes.LightGray, e.Bounds);

                using (Font boldf = new Font(lbSkills.Font, FontStyle.Bold))
                {
                    string skillName = s.Name + " " + GrandSkill.GetRomanSkillNumber(s.Level);
                    Size skillNameSizeInt = TextRenderer.MeasureText(g, skillName, boldf, new Size(0, 0), TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                    Point skillNameTopLeftInt = new Point(e.Bounds.Left + 6,
                        e.Bounds.Top + ((e.Bounds.Height / 2) - (skillNameSizeInt.Height / 2)));
                    Point detailTopLeftInt = new Point(skillNameTopLeftInt.X + skillNameSizeInt.Width, skillNameTopLeftInt.Y);

                    TextRenderer.DrawText(g, skillName, boldf, skillNameTopLeftInt, Color.Black);
                    TextRenderer.DrawText(g, " (Rank " + s.Rank.ToString() + ")", lbSkills.Font, detailTopLeftInt, Color.Black);

                    string skillPoints = String.Format("{0}/{1}", s.CurrentSkillPoints.ToString("#,##0"), s.GetPointsRequiredForLevel(5).ToString("#,##0"));
                    Size skillPointSizeInt = TextRenderer.MeasureText(g, skillPoints, lbSkills.Font, new Size(0, 0), TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                    Point skillPointTopLeftInt = new Point(e.Bounds.Right - skillPointSizeInt.Width - 6, skillNameTopLeftInt.Y);
                    TextRenderer.DrawText(g, skillPoints, lbSkills.Font, skillPointTopLeftInt, Color.Black);
                }
            }
        }

        private void lbSkills_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            object item = lbSkills.Items[e.Index];
            if (item is SerializableSkillGroup || item is GrandSkillGroup)
                e.ItemHeight = SKILL_HEADER_HEIGHT;
            else if (item is SerializableSkill || item is GrandSkill)
                e.ItemHeight = SKILL_DETAIL_HEIGHT;
        }

        private void btnPlan_Click(object sender, EventArgs e)
        {
            Plan p = null;
            using (PlanSelectWindow psw = new PlanSelectWindow(m_settings, m_grandCharacterInfo))
            {
                DialogResult dr = psw.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;
                p = psw.ResultPlan;
            }
            if (p == null)
            {
            AGAIN:
                bool doAgain = true;
                using (NewPlanWindow npw = new NewPlanWindow())
                {
                    DialogResult dr = npw.ShowDialog();
                    if (dr == DialogResult.Cancel)
                        return;
                    string planName = npw.Result;

                    p = new Plan();
                    try
                    {
                        m_settings.AddPlanFor(m_grandCharacterInfo.Name, p, planName);
                        doAgain = false;
                    }
                    catch (ApplicationException err)
                    {
                        DialogResult xdr = MessageBox.Show(err.Message, "Failed to Add Plan", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        if (xdr == DialogResult.Cancel)
                            return;
                    }
                }
                if (doAgain)
                    goto AGAIN;
            }

            p.ShowEditor(m_settings, m_grandCharacterInfo);
        }

        private void btnDebugError_Click(object sender, EventArgs e)
        {
            TipWindow.ShowTip("debugtip", "Sample Tip",
                "This is some example tip text. If you check the box you won't see " +
                "this window again. It's pretty amazing what you can do with technology " +
                "these days.");
        }

        private void lbSkills_MouseMove(object sender, MouseEventArgs e)
        {
            int index = lbSkills.IndexFromPoint(e.X, e.Y);
            object item;
            if (index < 0 || index >= lbSkills.Items.Count)
                item = null;
            else
                item = lbSkills.Items[index];

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
            }
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
                this.Invoke(new MethodInvoker(delegate
                {
                    SetErrorThrobber();
                }));
                return;
            }

            m_throbberRunning = false;
            m_throbberError = true;
        }

        private void tmrThrobber_Tick(object sender, EventArgs e)
        {
            tmrThrobber.Enabled = false;
            if (m_throbberRunning)
            {
                m_throbberFrame = ((m_throbberFrame + 1) % 8);
                pbThrobber.Image = m_throbberImages[m_throbberFrame + 1];
                tmrThrobber.Enabled = true;
            }
            else if (m_throbberError)
            {
                bool blinkState = (DateTime.Now.Millisecond > 500);
                if (!blinkState)
                    pbThrobber.Image = null;
                else
                    pbThrobber.Image = m_throbberImages[0];
                tmrThrobber.Enabled = true;
            }
            else
            {
                pbThrobber.Image = m_throbberImages[0];
                m_throbberFrame = 0;
            }
        }

        private void pbThrobber_Click(object sender, EventArgs e)
        {
            tmrUpdate_Tick(null, null);
        }

        private void ttToolTip_Popup(object sender, PopupEventArgs e)
        {
            if (e.AssociatedControl == pbThrobber)
            {
                if (!m_throbberRunning && !m_throbberError)
                {
                    ttToolTip.SetToolTip(pbThrobber,
                        String.Format("Last update: {0}\nNext update in: {1}\nClick to update now.",
                        m_lastUpdate.ToString(),
                        GrandSkill.TimeSpanToDescriptiveText(m_nextScheduledUpdateAt - DateTime.Now, DescriptiveTextOptions.Default)
                    ));
                }
            }
        }       
    }

    public delegate void SkillTrainingCompletedHandler(object sender, SkillTrainingCompletedEventArgs e);

    public class SkillTrainingCompletedEventArgs: EventArgs
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
