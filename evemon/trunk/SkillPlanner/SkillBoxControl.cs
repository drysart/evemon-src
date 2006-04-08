using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using EveCharacterMonitor;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class SkillBoxControl : UserControl
    {
        public SkillBoxControl()
        {
            InitializeComponent();
        }

        private bool m_primary;
        private PlannerSkill m_plannerSkill;
        private CharacterInfo m_characterInfo;
        private PlanInfo m_planInfo;
        private int m_prereqLevel;

        public SkillBoxControl(PlannerSkill ps, CharacterInfo ci, PlanInfo pi, bool primary, int prereqLevel)
            : this()
        {
            m_primary = primary;
            m_prereqLevel = prereqLevel;
            if (primary)
                this.ContextMenuStrip = cmsPrimaryMenu;
            this.PlannerSkill = ps;
            this.CharacterInfo = ci;
            this.PlanInfo = pi;
        }

        public PlannerSkill PlannerSkill
        {
            get { return m_plannerSkill; }
            set { m_plannerSkill = value; PopulateDisplay(); }
        }

        public CharacterInfo CharacterInfo
        {
            get { return m_characterInfo; }
            set { m_characterInfo = value; PopulateDisplay(); }
        }

        public PlanInfo PlanInfo
        {
            get { return m_planInfo; }
            set { m_planInfo = value; PopulateDisplay(); }
        }

        private const string TTXT_PLANNED_LEVEL = "The level you're planning on training this skill to.";
        private const string TTXT_REQUIRED_LEVEL = "The level you're required to have in this skill to learn your planned skill.";
        private const string TTXT_TIME_INVESTED = "How much time you've spent training this skill to its current level.";
        private const string TTXT_THIS_TIME = "How much time it will take to learn this skill, not including any prerequisite skill training.";
        private const string TTXT_TOTAL_TIME = "How much time it will take to learn this skill, including all necessary prerequisite skill training.";

        private void PopulateDisplay()
        {
            if (m_plannerSkill==null || m_characterInfo == null || m_planInfo == null)
            {
                lblSkillName.Text = String.Empty;
                lblCurrentLevel.Text = String.Empty;
                lblRequiredLevel.Text = String.Empty;
                lblTimeRequired.Text = String.Empty;
                lblTotalTimeRequired.Text = String.Empty;
            }
            else
            {
                Skill s = m_characterInfo.GetSkill(m_plannerSkill.Name);
                // XXX : get only RequiredLevel for the current prereq
                CombinedPlannedSkill cps = m_planInfo.GetCombinedSkill(m_plannerSkill.Name);

                lblSkillName.Text = m_plannerSkill.Name;
                int fromLevel = 0;
                if (s != null)
                {
                    fromLevel = s.Level;
                    lblCurrentLevel.Text = "Current Level: " + Skill.RomanSkillLevel[s.Level];
                }
                else
                {
                    fromLevel = 0;
                    lblCurrentLevel.Text = "Current Level: (none)";
                }
                int toLevel = 0;
                if (m_primary)
                {
                    toLevel = cps.PlannedLevel;
                    lblRequiredLevel.Text = "Planned Level: " + Skill.RomanSkillLevel[cps.PlannedLevel];
                    ttToolTip.SetToolTip(lblRequiredLevel, TTXT_PLANNED_LEVEL);
                }
                else
                {
                    toLevel = m_prereqLevel;
                    lblRequiredLevel.Text = "Required Level: " + Skill.RomanSkillLevel[m_prereqLevel];
                    ttToolTip.SetToolTip(lblRequiredLevel, TTXT_REQUIRED_LEVEL);
                }
                TimeSpan timeReq = m_plannerSkill.CalculateTimeToSkill(fromLevel, toLevel, m_characterInfo.Attributes);
                if (timeReq == TimeSpan.Zero)
                {
                    lblTimeRequired.Text = String.Empty;
                    ttToolTip.SetToolTip(lblTimeRequired, String.Empty);
                    lblTotalTimeRequired.Text = String.Empty;
                    ttToolTip.SetToolTip(lblTotalTimeRequired, String.Empty);
                }
                else if (timeReq < TimeSpan.Zero)
                {
                    lblTimeRequired.Text = "Time Invested: " + CharacterMonitor.TimeSpanDescriptiveMedium(timeReq);
                    ttToolTip.SetToolTip(lblTimeRequired, TTXT_TIME_INVESTED);
                    lblTotalTimeRequired.Text = String.Empty;
                    ttToolTip.SetToolTip(lblTotalTimeRequired, String.Empty);
                }
                else
                {
                    lblTimeRequired.Text = "This Time: " + CharacterMonitor.TimeSpanDescriptiveMedium(timeReq);
                    ttToolTip.SetToolTip(lblTimeRequired, TTXT_THIS_TIME);
                    TimeSpan ts = CalculateTotalTime(cps.PlannedLevel);
                    lblTotalTimeRequired.Text = "Total Time: " + CharacterMonitor.TimeSpanDescriptiveMedium(ts);
                    ttToolTip.SetToolTip(lblTotalTimeRequired, TTXT_TOTAL_TIME);
                }
            }
        }

        private TimeSpan CalculateTotalTime(int toLevel)
        {
            CombinedPlannedSkill cps = m_planInfo.GetCombinedSkill(m_plannerSkill.Name);
            Skill s = m_characterInfo.GetSkill(m_plannerSkill.Name);
            int fromLevel = 0;
            if (s != null)
                fromLevel = s.Level;

            TimeSpan res = m_plannerSkill.CalculateTimeToSkill(fromLevel, toLevel, m_characterInfo.Attributes);

            foreach (PlannerPrereq pp in m_plannerSkill.Prereqs)
            {
                res += CalculateTimeSpanFor(pp);
            }
            return res;
        }

        private TimeSpan CalculateTimeSpanFor(PlannerPrereq thisp)
        {
            Skill s = m_characterInfo.GetSkill(thisp.Name);
            int fromLevel = 0;
            if (s != null)
                fromLevel = s.Level;
            int toLevel = thisp.Level;

            PlannerData pd = PlannerData.GetPlannerData();
            return pd.GetSkill(thisp.Name).CalculateTimeToSkill(fromLevel, toLevel, m_characterInfo.Attributes);
        }

        private void cmsPrimaryMenu_Opening(object sender, CancelEventArgs e)
        {
            SetupPlanMenuOption(miPlanTo1, 1);
            SetupPlanMenuOption(miPlanTo2, 2);
            SetupPlanMenuOption(miPlanTo3, 3);
            SetupPlanMenuOption(miPlanTo4, 4);
            SetupPlanMenuOption(miPlanTo5, 5);

            CombinedPlannedSkill cps = m_planInfo.GetCombinedSkill(m_plannerSkill.Name);
            miCancelPlan.Enabled = (cps.PlannedLevel > 0);
        }

        private void SetupPlanMenuOption(ToolStripMenuItem mi, int level)
        {
            Skill s = m_characterInfo.GetSkill(m_plannerSkill.Name);
            bool enable;
            if (s==null)
                enable = true;
            else
                enable = (s.Level < level);

            mi.Enabled = enable;
            if (!enable)
                mi.Text = "Plan to Level " + Skill.RomanSkillLevel[level];
            else
                mi.Text = "Plan to Level " + Skill.RomanSkillLevel[level] + " (" +
                    CharacterMonitor.TimeSpanDescriptiveMedium(CalculateTotalTime(level)) + ")";
        }

        private void miPlanTo1_Click(object sender, EventArgs e)
        {
            SetPlanTo(1);
        }

        private void miPlanTo2_Click(object sender, EventArgs e)
        {
            SetPlanTo(2);
        }

        private void miPlanTo3_Click(object sender, EventArgs e)
        {
            SetPlanTo(3);
        }

        private void miPlanTo4_Click(object sender, EventArgs e)
        {
            SetPlanTo(4);
        }

        private void miPlanTo5_Click(object sender, EventArgs e)
        {
            SetPlanTo(5);
        }

        private void SetPlanTo(int p)
        {
            m_planInfo.Plan(m_plannerSkill.Name, p, m_characterInfo);
            PopulateDisplay();
        }

        private void miCancelPlan_Click(object sender, EventArgs e)
        {
            SetPlanTo(0);
        }
    }
}
