using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class PlanOrderEditorControl : UserControl
    {
        public PlanOrderEditorControl()
        {
            InitializeComponent();
        }

        private GrandCharacterInfo m_grandCharacterInfo;
        private Plan m_plan;

        public Plan Plan
        {
            get { return m_plan; }
            set
            {
                if (m_plan != null)
                    m_plan.Changed -= new EventHandler<EventArgs>(m_plan_Changed);
                m_plan = value;
                if (m_plan != null)
                    m_plan.Changed += new EventHandler<EventArgs>(m_plan_Changed);
                PlanChanged();
            }
        }

        private void m_plan_Changed(object sender, EventArgs e)
        {
            PlanChanged();
        }

        private void PlanChanged()
        {
            if (m_plan == null)
            {
                m_grandCharacterInfo = null;
                lvSkills.Items.Clear();
            }
            else
            {
                m_grandCharacterInfo = m_plan.GrandCharacterInfo;
                lvSkills.Items.Clear();
                DateTime lastEnd = DateTime.MinValue;
                foreach (PlanEntry pe in m_plan.Entries)
                {
                    GrandSkill gs = pe.Skill;
                    string[] data = new string[6];
                    data[0] = pe.SkillName + " " + GrandSkill.GetRomanSkillNumber(pe.Level);
                    data[1] = GrandSkill.TimeSpanToDescriptiveText(gs.GetTrainingTimeOfLevelOnly(pe.Level), DescriptiveTextOptions.IncludeCommas);
                    if (lastEnd == DateTime.MinValue)
                    {
                        lastEnd = DateTime.Now + gs.GetPrerequisiteTime() + gs.GetTrainingTimeToLevel(pe.Level - 1);
                    }
                    data[2] = lastEnd.ToString();
                    lastEnd += gs.GetTrainingTimeOfLevelOnly(pe.Level);
                    data[3] = lastEnd.ToString();
                    data[4] = pe.EntryType.ToString();
                    data[5] = pe.Level.ToString();
                    ListViewItem lvi = new ListViewItem(data);
                    lvSkills.Items.Add(lvi);
                }
            }
        }

        private void lvSkills_ListViewItemsDragging(object sender, ListViewDragEventArgs e)
        {
            List<PlanEntry> newOrder = new List<PlanEntry>();
            List<PlanEntry> oldItems = new List<PlanEntry>();
            List<PlanEntry> movingItems = new List<PlanEntry>();
            foreach (ListViewItem lvi in lvSkills.Items)
            {
                newOrder.Add(m_plan.GetEntryWithRoman(lvi.Text));
            }
            for (int i = 0; i < e.MovingCount; i++)
            {
                PlanEntry ope = newOrder[e.MovingFrom + i];
                PlanEntry pe = new PlanEntry();
                pe.SkillName = ope.SkillName;
                pe.Level = ope.Level;
                pe.EntryType = ope.EntryType;
                movingItems.Add(pe);
                oldItems.Add(ope);
            }
            for (int i = e.MovingCount - 1; i >= 0; i--)
            {
                newOrder.Insert(e.MovingTo, movingItems[i]);
            }
            foreach (PlanEntry pe in oldItems)
            {
                newOrder.Remove(pe);
            }

            // TODO: test for ordering
            Dictionary<string, int> known = new Dictionary<string,int>();
            foreach (GrandSkillGroup gsg in m_grandCharacterInfo.SkillGroups.Values)
            {
                foreach (GrandSkill gs in gsg)
                {
                    known[gs.Name] = gs.Level;
                }
            }
            for (int i = 0; i < newOrder.Count; i++)
            {
                GrandSkill gs = m_grandCharacterInfo.GetSkill(newOrder[i].SkillName);
                string failure = String.Empty;
                if (!CheckPrereqs(gs, newOrder[i].Level, known, ref failure))
                {
                    if (e.MovingFrom > e.MovingTo)
                    {
                        MessageBox.Show(
                            "You can not move " + newOrder[i].SkillName + " " +
                            GrandSkill.GetRomanSkillNumber(newOrder[i].Level) +
                            " before its required prerequisite skill " +
                            failure + ".", "Failed Prerequisite", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        MessageBox.Show(
                            "You can not move " + failure + 
                            " after " +
                            newOrder[i].SkillName + " " + GrandSkill.GetRomanSkillNumber(newOrder[i].Level) + " which requires it as a prerequisite.", "Failed Prerequisite", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (!known.ContainsKey(gs.Name))
                        known[gs.Name] = 0;
                    if (known[gs.Name] < newOrder[i].Level)
                        known[gs.Name] = newOrder[i].Level;
                }
            }
        }

        private bool CheckPrereqs(GrandSkill gs, int checkLevel, Dictionary<string, int> known, ref string failMessage)
        {
            //if (gs.PrerequisitesMet)
            //    return true;
            foreach (GrandSkill.Prereq pp in gs.Prereqs)
            {
                GrandSkill pgs = pp.Skill;
                if (pgs.Level < pp.RequiredLevel)
                {
                    if (!(known.ContainsKey(pgs.Name) && known[pgs.Name] >= pp.RequiredLevel))
                    {
                        failMessage = pgs.Name + " " + GrandSkill.GetRomanSkillNumber(pp.RequiredLevel);
                        return false;
                    }
                }
            }
            if (known[gs.Name] < checkLevel - 1)
            {
                failMessage = gs.Name + " " + GrandSkill.GetRomanSkillNumber(checkLevel - 1);
                return false;
            }
            return true;
        }

        private void lvSkills_ListViewItemsDragged(object sender, EventArgs e)
        {
            m_plan.SuppressEvents();
            try
            {
                m_plan.Entries.Clear();
                foreach (ListViewItem lvi in lvSkills.Items)
                {
                    PlanEntry pe = new PlanEntry();
                    pe.SkillName = Regex.Match(lvi.Text, "^(.*) ([IV]+)$").Groups[1].Value;
                    pe.Level = Convert.ToInt32(lvi.SubItems[5].Text);
                    pe.EntryType = (PlanEntryType)Enum.Parse(typeof(PlanEntryType), lvi.SubItems[4].Text, true);
                    m_plan.Entries.Add(pe);
                }
            }
            finally
            {
                m_plan.ResumeEvents();
            }
        }
    }
}
