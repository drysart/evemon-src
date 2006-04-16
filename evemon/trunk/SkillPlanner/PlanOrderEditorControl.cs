using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using System.IO;
using System.Reflection;
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
                UpdateListColumns();
                PlanChanged();
            }
        }

        private void m_plan_Changed(object sender, EventArgs e)
        {
            PlanChanged();
        }

        private void PlanChanged()
        {
            tmrTick.Enabled = false;
            if (m_plan == null)
            {
                if (m_grandCharacterInfo != null)
                {
                    m_grandCharacterInfo.SkillChanged -= new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);
                }
                m_grandCharacterInfo = null;
                lvSkills.Items.Clear();
            }
            else
            {
                m_grandCharacterInfo = m_plan.GrandCharacterInfo;
                m_grandCharacterInfo.SkillChanged += new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);
                lvSkills.Items.Clear();
                DateTime lastEnd = DateTime.MinValue;
                foreach (PlanEntry pe in m_plan.Entries)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Tag = pe;
                    lvSkills.Items.Add(lvi);

                    GrandSkill gs = pe.Skill;
                    if (gs.InTraining)
                        tmrTick.Enabled = true;

                }
                UpdateListViewItems();
            }
        }

        void m_grandCharacterInfo_SkillChanged(object sender, SkillChangedEventArgs e)
        {
            UpdateListViewItems();
        }

        private void tmrTick_Tick(object sender, EventArgs e)
        {
            UpdateListViewItems();
        }

        //private const int SUBITEM_SKILLNAME = 0;
        //private const int SUBITEM_TRAININGTIME = 1;
        //private const int SUBITEM_EARLIESTSTART = 2;
        //private const int SUBITEM_EARLIESTEND = 3;
        //private const int SUBITEM_ENTRYTYPE = 4;
        //private const int SUBITEM_LEVELNUMERIC = 5;
        //private const int SUBITEM_MAX = 6;

        private void UpdateListViewItems()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    UpdateListViewItems();
                }));
                return;
            }

            EveAttributeScratchpad scratchpad = new EveAttributeScratchpad();

            lvSkills.BeginUpdate();
            try
            {
                DateTime start = DateTime.Now;
                for (int i = 0; i < lvSkills.Items.Count; i++)
                {
                    ListViewItem lvi = lvSkills.Items[i];
                    //ListViewItem olvi = lvSkills.Items[i];
                    //ListViewItem lvi = new ListViewItem();
                    //lvi.Tag = olvi.Tag;
                    //lvSkills.Items.Insert(lvSkills.Items.IndexOf(olvi), lvi);
                    //lvSkills.Items.Remove(olvi);

                    PlanEntry pe = (PlanEntry)lvi.Tag;
                    GrandSkill gs = pe.Skill;

                    while (lvi.SubItems.Count < lvSkills.Columns.Count+1)
                        lvi.SubItems.Add(String.Empty);

                    TimeSpan trainTime = gs.GetTrainingTimeOfLevelOnly(pe.Level, true, scratchpad);
                    int currentSP = gs.CurrentSkillPoints;
                    int reqBeforeThisLevel = gs.GetPointsRequiredForLevel(pe.Level - 1);
                    int reqToThisLevel = gs.GetPointsRequiredForLevel(pe.Level);
                    int pointsInThisLevel = currentSP - reqBeforeThisLevel;
                    if (pointsInThisLevel < 0)
                        pointsInThisLevel = 0;
                    double deltaPointsOfLevel = Convert.ToDouble(reqToThisLevel - reqBeforeThisLevel);
                    double pctComplete = pointsInThisLevel / deltaPointsOfLevel;

                    DateTime thisStart = start;
                    start += trainTime;
                    DateTime thisEnd = start;

                    for (int x = 0; x < lvSkills.Columns.Count; x++)
                    {
                        ColumnPreference.ColumnType ct = (ColumnPreference.ColumnType)lvSkills.Columns[x].Tag;
                        string res = String.Empty;
                        switch (ct)
                        {
                            case ColumnPreference.ColumnType.SkillName:
                                res = gs.Name + " " + GrandSkill.GetRomanSkillNumber(pe.Level);
                                break;
                            case ColumnPreference.ColumnType.TrainingTime:
                                res = GrandSkill.TimeSpanToDescriptiveText(trainTime, DescriptiveTextOptions.IncludeCommas);
                                break;
                            case ColumnPreference.ColumnType.EarliestStart:
                                res = thisStart.ToString();
                                break;
                            case ColumnPreference.ColumnType.EarliestEnd:
                                res = thisEnd.ToString();
                                break;
                            case ColumnPreference.ColumnType.PercentComplete:
                                res = pctComplete.ToString("0%");
                                break;
                        }
                        lvi.SubItems[x].Text = res;
                    }

                    lvi.SubItems[lvSkills.Columns.Count].Text = pe.EntryType.ToString();

                    if (pe.SkillName == "Learning")
                        scratchpad.AdjustLearningLevelBonus(1);
                    else if (gs.IsLearningSkill)
                        scratchpad.AdjustAttributeBonus(gs.AttributeModified, 1);
                }
            }
            finally
            {
                lvSkills.EndUpdate();
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
                    Match m = Regex.Match(lvi.Text, "^(.*) ([IV]+)$");
                    pe.SkillName = m.Groups[1].Value;
                    pe.Level = GrandSkill.GetIntForRoman(m.Groups[2].Value);
                    pe.EntryType = (PlanEntryType)Enum.Parse(typeof(PlanEntryType), lvi.SubItems[lvSkills.Columns.Count].Text, true);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ColumnPreference pref = m_plan.ColumnPreference;
            using (PlanOrderEditorColumnSelectWindow f = new PlanOrderEditorColumnSelectWindow(pref))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    UpdateListColumns();
                    Program.Settings.Save();
                }
            }
        }

        private void UpdateListColumns()
        {
            if (m_plan != null)
            {
                lvSkills.BeginUpdate();
                try
                {
                    lvSkills.Columns.Clear();
                    List<ColumnPreference.ColumnType> alreadyAdded = new List<ColumnPreference.ColumnType>();

                    foreach (string ts in m_plan.ColumnPreference.Order.Split(','))
                    {
                        try
                        {
                            ColumnPreference.ColumnType ct = (ColumnPreference.ColumnType)Enum.Parse(
                                typeof(ColumnPreference.ColumnType), ts, true);
                            if (m_plan.ColumnPreference[ct] && !alreadyAdded.Contains(ct))
                            {
                                ColumnHeader ch = new ColumnHeader();
                                ColumnPreference.ColumnDisplayAttribute cda = ColumnPreference.GetAttribute(ct);
                                ch.Text = cda.Header;
                                ch.Tag = ct;
                                lvSkills.Columns.Add(ch);
                                alreadyAdded.Add(ct);
                            }
                        }
                        catch { }
                    }

                    for (int i = 0; i < ColumnPreference.ColumnCount; i++)
                    {
                        ColumnPreference.ColumnType ct = (ColumnPreference.ColumnType)i;
                        if (m_plan.ColumnPreference[i] && !alreadyAdded.Contains(ct))
                        {
                            ColumnHeader ch = new ColumnHeader();
                            ColumnPreference.ColumnDisplayAttribute cda = ColumnPreference.GetAttribute(ct);
                            ch.Text = cda.Header;
                            ch.Tag = ct;
                            lvSkills.Columns.Add(ch);
                            alreadyAdded.Add(ct);
                        }
                    }
                //}
                //finally
                //{
                //    lvSkills.EndUpdate();
                //}
                UpdateListViewItems();
                //lvSkills.BeginUpdate();
                //try
                //{
                    for (int i = 0; i < lvSkills.Columns.Count; i++)
                    {
                        ColumnHeader ch = lvSkills.Columns[i];
                        ColumnPreference.ColumnDisplayAttribute cda = 
                            ColumnPreference.GetAttribute((ColumnPreference.ColumnType)ch.Tag);
                        ch.Width = cda.Width;
                    }
                }
                finally
                {
                    lvSkills.EndUpdate();
                }
            }
        }

        private void lvSkills_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            // Because this occurs before the reordering happens, we have to delay the
            // Order update a bit...
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    StringBuilder sb = new StringBuilder();
                    SortedDictionary<int, ColumnPreference.ColumnType> order = new SortedDictionary<int, ColumnPreference.ColumnType>();
                    for (int i = 0; i < lvSkills.Columns.Count; i++)
                    {
                        ColumnHeader ch = lvSkills.Columns[i];
                        order.Add(ch.DisplayIndex, (ColumnPreference.ColumnType)ch.Tag);
                    }
                    foreach (KeyValuePair<int, ColumnPreference.ColumnType> pair in order)
                    {
                        if (sb.Length != 0)
                            sb.Append(',');
                        sb.Append(pair.Value.ToString());
                    }

                    m_plan.ColumnPreference.Order = sb.ToString();
                    Program.Settings.Save();
                }));
            });
        }
    }

    [XmlRoot("ColumnPreference")]
    public class ColumnPreference
    {
        public class ColumnDisplayAttribute : Attribute
        {
            private string m_text;
            private string m_header;
            private int m_width = -2;

            public string Text
            {
                get { return m_text; }
                set { m_text = value; }
            }

            public string Header
            {
                get { return m_header; }
                set { m_header = value; }
            }

            public int Width
            {
                get { return m_width; }
                set { m_width = value; }
            }

            public ColumnDisplayAttribute(string text)
            {
                m_text = text;
                m_header = text;
            }

            public ColumnDisplayAttribute(string text, string header)
            {
                m_text = text;
                m_header = header;
            }
        }

        public enum ColumnType
        {
            [ColumnDisplay("Skill Name")]
            SkillName,
            [ColumnDisplay("Training Time")]
            TrainingTime,
            [ColumnDisplay("Earliest Start")]
            EarliestStart,
            [ColumnDisplay("Earliest End")]
            EarliestEnd,
            [ColumnDisplay("Percent Complete", "%")]
            PercentComplete
        }

        private bool[] m_prefs;

        public ColumnPreference()
        {
            m_prefs = new bool[ColumnPreference.ColumnCount];
            for (int i = 0; i < m_prefs.Length; i++)
                m_prefs[i] = false;

            this.SkillName = true;
            this.TrainingTime = true;
            this.EarliestStart = true;
            this.EarliestEnd = true;
        }

        private string m_order = String.Empty;

        [XmlAttribute("column_order")]
        public string Order
        {
            get { return m_order; }
            set { m_order = value; }
        }

        public static string GetDescription(int index)
        {
            return GetDescription((ColumnType)index);
        }

        public static string GetDescription(ColumnType ct)
        {
            ColumnDisplayAttribute cda = GetAttribute(ct);
            if (cda != null)
                return cda.Text;
            else
                return ct.ToString();
        }

        public static string GetHeader(int index)
        {
            return GetHeader((ColumnType)index);
        }

        public static string GetHeader(ColumnType ct)
        {
            ColumnDisplayAttribute cda = GetAttribute(ct);
            if (cda != null)
                return cda.Header;
            else
                return ct.ToString();
        }

        public static ColumnDisplayAttribute GetAttribute(ColumnType ct)
        {
            MemberInfo[] memInfo = typeof(ColumnType).GetMember(ct.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(ColumnDisplayAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return (ColumnDisplayAttribute)attrs[0];
            }
            return null;
        }

        [XmlIgnore]
        public static int ColumnCount
        {
            get { return Enum.GetValues(typeof(ColumnType)).Length; }
        }

        [XmlIgnore]
        public bool this[int index]
        {
            get { return m_prefs[index]; }
            set { m_prefs[index] = value; }
        }

        [XmlIgnore]
        public bool this[ColumnType index]
        {
            get { return this[(int)index]; }
            set { this[(int)index] = value; }
        }

        [XmlAttribute]
        public bool SkillName
        {
            get { return this[ColumnType.SkillName]; }
            set { this[ColumnType.SkillName] = value; }
        }

        [XmlAttribute]
        public bool TrainingTime
        {
            get { return this[ColumnType.TrainingTime]; }
            set { this[ColumnType.TrainingTime] = value; }
        }

        [XmlAttribute]
        public bool EarliestStart
        {
            get { return this[ColumnType.EarliestStart]; }
            set { this[ColumnType.EarliestStart] = value; }
        }

        [XmlAttribute]
        public bool EarliestEnd
        {
            get { return this[ColumnType.EarliestEnd]; }
            set { this[ColumnType.EarliestEnd] = value; }
        }

        [XmlAttribute]
        public bool PercentComplete
        {
            get { return this[ColumnType.PercentComplete]; }
            set { this[ColumnType.PercentComplete] = value; }
        }
    }
}
