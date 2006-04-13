using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using System.IO;
using System.Xml.Serialization;

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
                    lvi.Tag = pe;
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

        private void cmsContextMenu_Opening(object sender, CancelEventArgs e)
        {
            miRemoveFromPlan.Enabled = (lvSkills.SelectedItems.Count == 1);
        }

        private void miRemoveFromPlan_Click(object sender, EventArgs e)
        {
            if (lvSkills.SelectedItems.Count != 1)
                return;

            using (CancelChoiceWindow f = new CancelChoiceWindow())
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;
                if (dr == DialogResult.Yes)
                    RemoveFromPlan(GetPlanEntryForListViewItem(lvSkills.SelectedItems[0]), true);
                if (dr == DialogResult.No)
                    RemoveFromPlan(GetPlanEntryForListViewItem(lvSkills.SelectedItems[0]), false);
            }
        }

        private PlanEntry GetPlanEntryForListViewItem(ListViewItem lvi)
        {
            if (lvi == null)
                return null;
            return lvi.Tag as PlanEntry;
        }

        private void RemoveFromPlan(PlanEntry pe, bool includePrerequisites)
        {
            bool result = m_plan.RemoveEntry(pe.Skill, includePrerequisites, false);
            if (!result)
            {
                MessageBox.Show(this,
                    "The plan for this skill could not be cancelled because this skill is " +
                    "required for another skill you have planned.",
                    "Skill Needed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private enum SaveType
        {
            None = 0,
            Xml = 1,
            Text = 2
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            sfdSave.FileName = m_plan.GrandCharacterInfo.Name + " Skill Plan";
            sfdSave.FilterIndex = (int)SaveType.Xml;
            DialogResult dr = sfdSave.ShowDialog();
            if (dr == DialogResult.Cancel)
                return;

            string fileName = sfdSave.FileName;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    switch ((SaveType)sfdSave.FilterIndex)
                    {
                        case SaveType.Xml:
                            XmlSerializer xs = new XmlSerializer(typeof(Plan));
                            xs.Serialize(fs, m_plan);
                            break;
                        case SaveType.Text:
                            SaveAsText(fs);
                            break;
                        default:
                            return;
                    }
                }
            }
            catch (IOException err)
            {
                MessageBox.Show("There was an error writing out the file:\n\n" + err.Message,
                    "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAsText(Stream fs)
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("Skill Plan for {0}:", m_plan.GrandCharacterInfo.Name);
                sw.WriteLine();
                int i = 0;
                foreach (PlanEntry pe in m_plan.Entries)
                {
                    i++;
                    sw.WriteLine("{0:D3}: {1} {2}", i, pe.SkillName, GrandSkill.GetRomanSkillNumber(pe.Level));
                }
            }
        }

        private void tsbCopyForum_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[b]Skill Plan for ");
            sb.Append(m_plan.GrandCharacterInfo.Name);
            sb.AppendLine("[/b]");
            sb.AppendLine();
            int i = 0;
            foreach (PlanEntry pe in m_plan.Entries)
            {
                i++;
                GrandSkill gs = pe.Skill;
                sb.AppendLine(String.Format("{0}: [b]{1} {2}[/b] ({3})", 
                    i, pe.SkillName, GrandSkill.GetRomanSkillNumber(pe.Level), 
                    GrandSkill.TimeSpanToDescriptiveText(gs.GetTrainingTimeOfLevelOnly(pe.Level), DescriptiveTextOptions.FullText | DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.SpaceText)));
            }
            Clipboard.SetText(sb.ToString());
            MessageBox.Show("The skill plan has been copied to the clipboard in a " +
                "format suitable for forum posting.", "Plan Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
