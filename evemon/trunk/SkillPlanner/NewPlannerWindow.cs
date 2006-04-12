using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
                Pair<string,Plan> np = new Pair<string,Plan>();
                np.A = m_grandCharacterInfo.Name;
                np.B = m_plan;
                m_settings.Plans.Add(np);
            }
            skillTreeDisplay1.Plan = m_plan;
        }

        private void NewPlannerWindow_Load(object sender, EventArgs e)
        {

        }

        private void skillTreeDisplay1_Load(object sender, EventArgs e)
        {
            cbSkillFilter.SelectedIndex = 0;
        }

        private void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSkillFilter.SelectedIndex)
            {
                case 0: // Show All Skills
                case 1: // Show Known Skills
                case 2: // Show Planned Skills
                default:
                    break;
            }

            tvSkillView.Nodes.Clear();
            foreach (GrandSkillGroup gsg in m_grandCharacterInfo.SkillGroups.Values)
            {
                TreeNode gtn = new TreeNode(gsg.Name);
                foreach (GrandSkill gs in gsg)
                {
                    TreeNode stn = new TreeNode(gs.Name);
                    stn.Tag = gs;
                    gtn.Nodes.Add(stn);
                }
                if (gtn.Nodes.Count > 0)
                {
                    tvSkillView.Nodes.Add(gtn);
                }
            }
        }

        private GrandSkill m_selectedSkill = null;

        private void tvSkillView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = tvSkillView.SelectedNode;
            m_selectedSkill = tn.Tag as GrandSkill;
            skillTreeDisplay1.RootSkill = m_selectedSkill;
        }

        private void skillTreeDisplay1_SkillClicked(object sender, SkillClickedEventArgs e)
        {
            if (e.Skill == m_selectedSkill)
            {
                if (e.Button == MouseButtons.Right)
                {
                    bool isPlanned = false;
                    isPlanned |= SetMenuItemState(miPlanTo1, e.Skill, 1);
                    isPlanned |= SetMenuItemState(miPlanTo2, e.Skill, 2);
                    isPlanned |= SetMenuItemState(miPlanTo3, e.Skill, 3);
                    isPlanned |= SetMenuItemState(miPlanTo4, e.Skill, 4);
                    isPlanned |= SetMenuItemState(miPlanTo5, e.Skill, 5);
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

        private void PlanTo(int level)
        {
            MessageBox.Show(this, "Planning not yet implemented.", "Not Yet Implemented", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}