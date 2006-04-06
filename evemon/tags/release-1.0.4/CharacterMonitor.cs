using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

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
            m_session.GetCharaterImageAsync(m_charId, new GetCharacterImageCallback(GotCharacterImage));
        }

        public void Stop()
        {
            if (m_session != null)
                m_session = null;
            tmrTick.Enabled = false;
            tmrUpdate.Enabled = false;
        }

        private void GotCharacterImage(EveSession sender, Image i)
        {
            pbCharImage.Image = i;
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

        private void GotCharacterInfo(EveSession sess, CharacterInfo ci)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                lblCharacterName.Text = m_cli.CharacterName;
                if (ci == null)
                {
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
                    lblBioInfo.Text = ci.Gender + " " + ci.Race + " " + ci.BloodLine;
                    lblCorpInfo.Text = "Corporation: " + ci.CorpName;
                    lblBalance.Text = "Balance: " + ci.Balance.ToString("#,##0.00") + " ISK";
                    lblIntelligence.Text = ci.Intelligence.ToString() + " Intelligence";
                    lblCharisma.Text = ci.Charisma.ToString() + " Charisma";
                    lblPerception.Text = ci.Perception.ToString() + " Perception";
                    lblMemory.Text = ci.Memory.ToString() + " Memory";
                    lblWillpower.Text = ci.Willpower.ToString() + " Willpower";

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

        private void CalcSkillRemainText()
        {
            if (m_estimatedCompletion != DateTime.MaxValue)
            {
                lblTrainingRemain.Text = TimeSpanDescriptive(m_estimatedCompletion);
                if (m_estimatedCompletion > DateTime.Now)
                    lblTrainingEst.Text = m_estimatedCompletion.ToString();
                else
                    lblTrainingEst.Text = String.Empty;
            }
            else
            {
                lblTrainingRemain.Text = String.Empty;
                lblTrainingEst.Text = String.Empty;
            }
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
