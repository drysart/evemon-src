using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Xml.Serialization;

using EveCharacterMonitor;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class NewPlannerWindow : Form
    {
        public NewPlannerWindow()
        {
            InitializeComponent();
        }

        private Settings m_settings;
        private GrandCharacterInfo m_grandCharacterInfo;
        private Plan m_plan;

        public NewPlannerWindow(Settings s, GrandCharacterInfo gci)
            : this()
        {
            m_settings = s;
            m_grandCharacterInfo = gci;

            m_plan = m_settings.GetPlanByName(m_grandCharacterInfo.Name);
            if (m_plan == null)
            {
                m_plan = new Plan();
                m_settings.AddPlanFor(m_grandCharacterInfo.Name, m_plan);

            }
            m_plan.GrandCharacterInfo = m_grandCharacterInfo;
            m_plan.Changed += new EventHandler<EventArgs>(m_plan_Changed);
            skillTreeDisplay1.Plan = m_plan;
        }

        private void NewPlannerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_plan.Changed -= new EventHandler<EventArgs>(m_plan_Changed);
        }

        void m_plan_Changed(object sender, EventArgs e)
        {
            //MessageBox.Show("saving");
            m_settings.Save();
            UpdateStatusBar();
        }

        private void NewPlannerWindow_Load(object sender, EventArgs e)
        {
            this.Text = m_grandCharacterInfo.Name + " - EVEMon Skill Planner";
            UpdatePlanControl();
        }

        private void skillTreeDisplay1_Load(object sender, EventArgs e)
        {
            cbSkillFilter.SelectedIndex = 0;
        }

        private delegate bool SkillFilter(GrandSkill gs);

        private void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            SkillFilter sf;
            switch (cbSkillFilter.SelectedIndex)
            {
                case 4: // View Plan
                    SwitchToPlanEditor(true);
                    return;
                case 1: // Show Known Skills
                    sf = delegate(GrandSkill xx)
                    {
                        return xx.Known;
                    };
                    break;
                case 2: // Show Planned Skills
                    sf = delegate(GrandSkill xx)
                    {
                        return m_plan.IsPlanned(xx);
                    };
                    break;
                case 3: // Show Available, Untrained Skills
                    sf = delegate(GrandSkill xx)
                    {
                        return (xx.PrerequisitesMet && !xx.Known);
                    };
                    break;
                case 0:
                default:
                    sf = delegate(GrandSkill xx)
                    {
                        return true;
                    };
                    break;
            }

            SwitchToPlanEditor(false);

            tvSkillView.Nodes.Clear();
            foreach (GrandSkillGroup gsg in m_grandCharacterInfo.SkillGroups.Values)
            {
                TreeNode gtn = new TreeNode(gsg.Name);
                foreach (GrandSkill gs in gsg)
                {
                    if (sf(gs))
                    {
                        TreeNode stn = new TreeNode(gs.Name);
                        stn.Tag = gs;
                        gtn.Nodes.Add(stn);
                    }
                }
                if (gtn.Nodes.Count > 0)
                {
                    tvSkillView.Nodes.Add(gtn);
                }
            }
        }

        private void SwitchToPlanEditor(bool switchOn)
        {
            planEditor.Visible = switchOn;
            tvSkillView.Visible = !switchOn;
            skillTreeDisplay1.Visible = !switchOn;

            if (switchOn)
            {
                pnlPlanControl.Visible = false;
                planEditor.Plan = m_plan;
            }
            else
                planEditor.Plan = null;

            planEditor.Location = tvSkillView.Location;
            planEditor.Size = new Size(
                this.ClientSize.Width - (planEditor.Location.X * 2),
                tvSkillView.Height);
            planEditor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        }

        private GrandSkill m_selectedSkill = null;

        private void tvSkillView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = tvSkillView.SelectedNode;
            m_selectedSkill = tn.Tag as GrandSkill;
            skillTreeDisplay1.RootSkill = m_selectedSkill;

            UpdatePlanControl();
        }

        private void UpdatePlanControl()
        {
            if (planEditor.Visible)
                return;

            if (m_selectedSkill == null)
            {
                pnlPlanControl.Visible = false;
            }
            else
            {
                lblSkillName.Text = m_selectedSkill.Name;
                int plannedTo = 0;
                bool anyPlan = false;
                bool tPlan;
                tPlan = SetPlanLabel(lblLevel1Time, btnPlanTo1, 1);
                if (tPlan)
                    plannedTo = 1;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel2Time, btnPlanTo2, 2);
                if (tPlan)
                    plannedTo = 2;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel3Time, btnPlanTo3, 3);
                if (tPlan)
                    plannedTo = 3;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel4Time, btnPlanTo4, 4);
                if (tPlan)
                    plannedTo = 4;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel5Time, btnPlanTo5, 5);
                if (tPlan)
                    plannedTo = 5;
                anyPlan = anyPlan || tPlan;
                btnCancelPlan.Enabled = anyPlan;

                if (plannedTo > 0)
                {
                    lblPlanDescription.Text = "Currently planned to level " +
                        GrandSkill.GetRomanSkillNumber(plannedTo);
                }
                else
                {
                    lblPlanDescription.Text = "Not currently planned.";
                }
                pnlPlanControl.Visible = true;
            }

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            slblStatusText.Text = String.Format("{0} Skill{1} Planned ({2} Unique Skill{3})",
                m_plan.Entries.Count,
                m_plan.Entries.Count == 1 ? "" : "s",
                m_plan.UniqueSkillCount,
                m_plan.UniqueSkillCount == 1 ? "" : "s");
        }

        private bool SetPlanLabel(Label label, Button button, int level)
        {
            bool isPlanned = m_plan.IsPlanned(m_selectedSkill, level);
            StringBuilder sb = new StringBuilder();
            sb.Append("Level ");
            sb.Append(GrandSkill.GetRomanSkillNumber(level));
            sb.Append(": ");
            if (m_selectedSkill.Level >= level)
            {
                sb.Append("Already known");
                button.Enabled = false;
            }
            else
            {
                TimeSpan tts = m_selectedSkill.GetTrainingTimeOfLevelOnly(level);
                if (m_selectedSkill.Level == level - 1)
                    tts = m_selectedSkill.GetTrainingTimeToLevel(level);
                sb.Append(GrandSkill.TimeSpanToDescriptiveText(tts, DescriptiveTextOptions.IncludeCommas));
                TimeSpan prts = m_selectedSkill.GetTrainingTimeToLevel(level-1) +
                    m_selectedSkill.GetPrerequisiteTime();
                if (prts > TimeSpan.Zero)
                {
                    sb.Append(" (plus ");
                    sb.Append(GrandSkill.TimeSpanToDescriptiveText(prts, DescriptiveTextOptions.IncludeCommas));
                    sb.Append(")");
                }
                button.Enabled = !isPlanned;
                //if (m_selectedSkill.InTraining && m_selectedSkill.TrainingToLevel == level)
                //    button.Enabled = false;
            }
            label.Text = sb.ToString();
            return isPlanned;
        }

        private void skillTreeDisplay1_SkillClicked(object sender, SkillClickedEventArgs e)
        {
            if (e.Skill == m_selectedSkill)
            {
                if (e.Button == MouseButtons.Right)
                {
                    bool isPlanned = false;
                    isPlanned = SetMenuItemState(miPlanTo1, e.Skill, 1) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo2, e.Skill, 2) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo3, e.Skill, 3) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo4, e.Skill, 4) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo5, e.Skill, 5) || isPlanned;
                    miCancelPlanMenu.Enabled = isPlanned;
                    cmsSkillContext.Show(skillTreeDisplay1, e.Location);
                }
            }
        }

        private const DescriptiveTextOptions DTO_OPTS = DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.UppercaseText;

        private bool SetMenuItemState(ToolStripMenuItem mi, GrandSkill gs, int level)
        {
            bool isKnown = false;
            bool isPlanned = false;
            bool isTraining = false;
            if (level <= gs.Level)
            {
                isKnown = true;
            }
            if (!isKnown)
            {
                if (gs.InTraining && gs.TrainingToLevel == level)
                {
                    isTraining = true;
                }
                else
                {
                    foreach (PlanEntry pe in m_plan.Entries)
                    {
                        if (pe.Skill == gs && pe.Level == level)
                        {
                            isPlanned = true;
                            break;
                        }
                    }
                }
            }

            if (!isKnown && !isTraining)
            {
                if (isPlanned)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanSkillNumber(level) + " (Already Planned)";
                    mi.Enabled = false;
                    return true;
                }
                else
                {
                    TimeSpan ts = gs.GetPrerequisiteTime() + gs.GetTrainingTimeToLevel(level);
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanSkillNumber(level) + " (" +
                        GrandSkill.TimeSpanToDescriptiveText(ts, DTO_OPTS) + ")";
                    mi.Enabled = true;
                }
            }
            else
            {
                if (isKnown)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanSkillNumber(level) + " (Already Known)";
                    mi.Enabled = false;
                }
                else if (isTraining)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanSkillNumber(level) + " (Currently Training)";
                    mi.Enabled = false;
                }
            }
            return false;
        }

        private void miPlanTo1_Click(object sender, EventArgs e)
        {
            PlanTo(1);
        }

        private void miPlanTo2_Click(object sender, EventArgs e)
        {
            PlanTo(2);
        }

        private void miPlanTo3_Click(object sender, EventArgs e)
        {
            PlanTo(3);
        }

        private void miPlanTo4_Click(object sender, EventArgs e)
        {
            PlanTo(4);
        }

        private void miPlanTo5_Click(object sender, EventArgs e)
        {
            PlanTo(5);
        }

        private bool ShouldAdd(GrandSkill gs, int level, IEnumerable<PlanEntry> list)
        {
            if (gs.Level < level && !m_plan.IsPlanned(gs, level))
            {
                foreach (PlanEntry pe in list)
                {
                    if (pe.SkillName == gs.Name && pe.Level == level)
                        return false;
                }
                return true;
            }
            return false;
        }

        private void PlanTo(int level)
        {
            //MessageBox.Show(this, "Planning not yet implemented.", "Not Yet Implemented", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            List<PlanEntry> planEntries = new List<PlanEntry>();
            AddPrerequisiteEntries(m_selectedSkill, planEntries);
            for (int i = 1; i <= level; i++)
            {
                if (ShouldAdd(m_selectedSkill, i, planEntries))
                {
                    PlanEntry pe = new PlanEntry();
                    pe.SkillName = m_selectedSkill.Name;
                    if (i == level)
                    {
                        pe.EntryType = PlanEntryType.Planned;
                        //pe.PrerequisiteForName = String.Empty;
                        //pe.PrerequisiteForLevel = -1;
                    }
                    else
                    {
                        pe.EntryType = PlanEntryType.Prerequisite;
                        //pe.PrerequisiteForName = m_selectedSkill.Name;
                        //pe.PrerequisiteForLevel = level + 1;
                    }
                    pe.Level = i;
                    planEntries.Add(pe);
                }
            }
            using (AddPlanConfirmWindow f = new AddPlanConfirmWindow(planEntries))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    m_plan.AddList(planEntries);
                }
            }
            UpdatePlanControl();
        }

        private void AddPrerequisiteEntries(GrandSkill gs, List<PlanEntry> planEntries)
        {
            foreach (GrandSkill.Prereq pp in gs.Prereqs)
            {
                GrandSkill pgs = pp.Skill;
                AddPrerequisiteEntries(pgs, planEntries);
                for (int i = 1; i <= pp.RequiredLevel; i++)
                {
                    if (ShouldAdd(pgs, i, planEntries))
                    {
                        PlanEntry pe = new PlanEntry();
                        pe.SkillName = pgs.Name;
                        pe.Level = i;
                        pe.EntryType = PlanEntryType.Prerequisite;
                        //pe.PrerequisiteForName = gs.Name;
                        //pe.PrerequisiteForLevel = 1;
                        planEntries.Add(pe);
                    }
                }
            }
        }

        private void tmrSkillTick_Tick(object sender, EventArgs e)
        {
            UpdatePlanControl();
        }

        private void btnPlanTo1_Click(object sender, EventArgs e)
        {
            PlanTo(1);
        }

        private void btnPlanTo2_Click(object sender, EventArgs e)
        {
            PlanTo(2);
        }

        private void btnPlanTo3_Click(object sender, EventArgs e)
        {
            PlanTo(3);
        }

        private void btnPlanTo4_Click(object sender, EventArgs e)
        {
            PlanTo(4);
        }

        private void btnPlanTo5_Click(object sender, EventArgs e)
        {
            PlanTo(5);
        }

        private void btnCancelPlan_Click(object sender, EventArgs e)
        {
            CancelPlan();
        }

        private void CancelPlan()
        {
            using (CancelChoiceWindow f = new CancelChoiceWindow())
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;
                if (dr == DialogResult.Yes)
                    CancelPlan(true);
                if (dr == DialogResult.No)
                    CancelPlan(false);
            }
        }

        private void CancelPlan(bool includePrerequisites)
        {
            bool result = m_plan.RemoveEntry(m_selectedSkill, includePrerequisites, false);
            if (!result)
            {
                MessageBox.Show(this,
                    "The plan for this skill could not be cancelled because this skill is " +
                    "required for another skill you have planned.",
                    "Skill Needed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void miCancelAll_Click(object sender, EventArgs e)
        {
            CancelPlan(true);
        }

        private void miCancelThis_Click(object sender, EventArgs e)
        {
            CancelPlan(false);
        }
    }
}