using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Serialization;

namespace EveCharacterMonitor
{
    public partial class CharacterMonitor : UserControl
    {
        public CharacterMonitor()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private CharLoginInfo m_cli;
        private EveSession m_session;
        private int m_charId;

        public CharacterMonitor(CharLoginInfo cli)
            : this()
        {
            m_cli = cli;
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
            m_session = EveSession.GetSession(m_cli.Username, m_cli.Password);
            m_charId = m_session.GetCharacterId(m_cli.CharacterName);
            if (m_charId < 0)
            {
                m_session = null;
                throw new ApplicationException("Could not start character monitor");
            }
            tmrUpdate.Interval = 10;
            tmrUpdate.Enabled = true;
            tmrTick.Enabled = true;
            GetCharacterImage();
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

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            tmrUpdate.Enabled = false;
            m_session.GetCharacterInfoAsync(m_charId, new GetCharacterInfoCallback(GotCharacterInfo));
        }

        private static string[] m_skillLevelRoman = new string[6] { "(none)", "I", "II", "III", "IV", "V" };

        private string m_skillTrainingName;
        private DateTime m_estimatedCompletion;
        private string m_lastCompletedSkill = String.Empty;
        private CharacterInfo m_characterInfo;

        private void GotCharacterInfo(EveSession sess, CharacterInfo ci)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                m_characterInfo = ci;
                lblCharacterName.Text = m_cli.CharacterName;
                if (ci == null)
                {
                    btnSave.Enabled = false;
                    lblBioInfo.Text = String.Empty;
                    lblCorpInfo.Text = "(cannot retrieve character info)";
                    lblBalance.Text = String.Empty;
                    lblIntelligence.Text = String.Empty;
                    lblCharisma.Text = String.Empty;
                    lblPerception.Text = String.Empty;
                    lblMemory.Text = String.Empty;
                    lblWillpower.Text = String.Empty;
                    lbSkills.Items.Clear();
                    m_skillTrainingName = String.Empty;
                    m_estimatedCompletion = DateTime.MaxValue;
                    pnlTraining.Visible = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    lblBioInfo.Text = ci.Gender + " " + ci.Race + " " + ci.BloodLine;
                    lblCorpInfo.Text = "Corporation: " + ci.CorpName;
                    lblBalance.Text = "Balance: " + ci.Balance.ToString("#,##0.00") + " ISK";
                    SetAttributeLabel(ci, lblIntelligence, EveAttribute.Intelligence);
                    SetAttributeLabel(ci, lblCharisma, EveAttribute.Charisma);
                    SetAttributeLabel(ci, lblPerception, EveAttribute.Perception);
                    SetAttributeLabel(ci, lblMemory, EveAttribute.Memory);
                    SetAttributeLabel(ci, lblWillpower, EveAttribute.Willpower);
                    //lblIntelligence.Text = ci.Attributes.AdjustedIntelligence.ToString("#.00") + " Intelligence";
                    //lblCharisma.Text = ci.Attributes.AdjustedCharisma.ToString("#.00") + " Charisma";
                    //lblPerception.Text = ci.Attributes.AdjustedPerception.ToString("#.00") + " Perception";
                    //lblMemory.Text = ci.Attributes.AdjustedMemory.ToString("#.00") + " Memory";
                    //lblWillpower.Text = ci.Attributes.AdjustedWillpower.ToString("#.00") + " Willpower";

                    lbSkills.Items.Clear();
                    foreach (SkillGroup sg in ci.SkillGroups)
                    {
                        lbSkills.Items.Add(sg);
                        foreach (Skill s in sg.Skills)
                        {
                            lbSkills.Items.Add(s);
                        }
                    }

                    if (ci.SkillInTraining == null)
                    {
                        m_skillTrainingName = String.Empty;
                        m_estimatedCompletion = DateTime.MaxValue;
                        pnlTraining.Visible = false;
                    }
                    else
                    {
                        m_skillTrainingName = ci.SkillInTraining.SkillName + " " + m_skillLevelRoman[ci.SkillInTraining.TrainingToLevel];
                        lblTrainingSkill.Text = m_skillTrainingName;
                        m_estimatedCompletion = ci.SkillInTraining.EstimatedCompletion;
                        CalcSkillRemainText();
                        pnlTraining.Visible = true;
                    }
                }
                tmrUpdate.Interval = 5 * 60 * 1000;
                tmrUpdate.Enabled = true;
            }));
        }

        private void SetAttributeLabel(CharacterInfo ci, Label label, EveAttribute eveAttribute)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ci.Attributes.GetAttributeAdjustment(eveAttribute, EveAttributeAdjustment.AllWithLearning).ToString("#.00"));
            sb.Append(' ');
            sb.Append(eveAttribute.ToString());
            double implantValue = ci.Attributes.GetAttributeAdjustment(eveAttribute, EveAttributeAdjustment.Implants | EveAttributeAdjustment.Learning);
            if (implantValue > 0)
            {
                sb.Append(" (");
                sb.Append(implantValue.ToString("#.##"));
                sb.Append(" from implants)");
            }
            label.Text = sb.ToString();
        }

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

        private string TimeSpanDescriptiveShort(DateTime t)
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
                if ((SaveFormat)sfdSaveDialog.FilterIndex != SaveFormat.Xml)
                {
                    MessageBox.Show("Saving to formats other than XML is not yet " +
                        "supported. Try again next version.", "Not Yet Implemented",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                SaveFile((SaveFormat)sfdSaveDialog.FilterIndex, sfdSaveDialog.FileName);
            }
        }

        private void SaveFile(SaveFormat saveFormat, string fileName)
        {
            //IntelligenceBonus ib = new IntelligenceBonus();
            //ib.Name = "Test Cybernetic Subprocessor";
            //ib.Amount = 3;
            //m_characterInfo.AttributeBonuses.Bonuses.Add(ib);
            //CharismaBonus cb = new CharismaBonus();
            //cb.Name = "Test Charisma Bonii";
            //cb.Amount = 3;
            //m_characterInfo.AttributeBonuses.Bonuses.Add(cb);

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
                        XmlSerializer xs = new XmlSerializer(typeof(CharacterInfo));
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
                        xs.Serialize(xtw, m_characterInfo, ns);
                    }
                    if (saveFormat == SaveFormat.Xml)
                        return;

                    MemoryStream ms = (MemoryStream)outerStream;
                    ms.Position = 0;
                    using (StreamReader tr = new StreamReader(ms))
                    {
                        xpdoc = new XPathDocument(tr);
                    }
                }
                finally
                {
                    outerStream.Dispose();
                }

                XslCompiledTransform xstDoc2 = new XslCompiledTransform();
                //System.Xml.Xsl.XslTransform xstDoc = new System.Xml.Xsl.XslTransform();
                using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("EveCharacterMonitor.output-" + saveFormat.ToString().ToLower() + ".xsl"))
                using (XmlTextReader xtr = new XmlTextReader(s))
                {
                    //xstDoc.Load(xtr);
                    xstDoc2.Load(xtr);
                }

                using (StreamWriter sw = new StreamWriter(fileName, false))
                using (XmlTextWriter xtw = new XmlTextWriter(sw))
                {
                    xtw.Indentation = 1;
                    xtw.IndentChar = '\t';
                    xtw.Formatting = Formatting.Indented;
                    //xstDoc.Transform(xpdoc, null, xtw);
                    xstDoc2.Transform(xpdoc, null, xtw);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to save:\n" + ex.Message, "Could not save", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
