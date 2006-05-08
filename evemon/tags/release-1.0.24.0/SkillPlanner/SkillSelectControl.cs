using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class SkillSelectControl : UserControl
    {
        public SkillSelectControl()
        {
            InitializeComponent();
        }

        private GrandCharacterInfo m_grandCharacterInfo;
        private Plan m_plan;

        public GrandCharacterInfo GrandCharacterInfo
        {
            get { return m_grandCharacterInfo; }
            set { m_grandCharacterInfo = value; }
        }

        public Plan Plan
        {
            get { return m_plan; }
            set { m_plan = value; }
        }

        public event EventHandler<EventArgs> SelectedSkillChanged;

        private GrandSkill m_selectedSkill;

        public GrandSkill SelectedSkill
        {
            get { return m_selectedSkill; }
            private set { m_selectedSkill = value; OnSelectedSkillChanged(); }
        }

        private void OnSelectedSkillChanged()
        {
            if (SelectedSkillChanged != null)
                SelectedSkillChanged(this, new EventArgs());
        }

        private delegate bool SkillFilter(GrandSkill gs);

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSkillDisplay();
        }

        private void UpdateSkillFilter()
        {
            if (m_grandCharacterInfo == null || m_plan == null)
                return;

            SkillFilter sf;

            switch (cbFilter.SelectedIndex)
            {
                default:
                case 0: // All Skills
                    sf = delegate
                    {
                        return true;
                    };
                    break;
                case 1: // Known Skills
                    sf = delegate(GrandSkill gs)
                    {
                        return gs.Known;
                    };
                    break;
                case 2: // Not Known Skills
                    sf = delegate(GrandSkill gs)
                    {
                        return !gs.Known;
                    };
                    break;
                case 3: // Planned Skills
                    sf = delegate(GrandSkill gs)
                    {
                        return m_plan.IsPlanned(gs);
                    };
                    break;
                case 4: // Level I Ready Skills
                    sf = delegate(GrandSkill gs)
                    {
                        return (gs.Level == 0 && gs.PrerequisitesMet);
                    };
                    break;
            }

            tvSkillList.Nodes.Clear();
            foreach (GrandSkillGroup gsg in m_grandCharacterInfo.SkillGroups.Values)
            {
                TreeNode gtn = new TreeNode(gsg.Name);
                foreach (GrandSkill gs in gsg)
                {
                    if (sf(gs) && (gs.Public || cbShowNonPublic.Checked))
                    {
                        TreeNode stn = new TreeNode(gs.Name);
                        stn.Tag = gs;
                        gtn.Nodes.Add(stn);
                    }
                }
                if (gtn.Nodes.Count > 0)
                {
                    tvSkillList.Nodes.Add(gtn);
                }
            }
        }

        private void lblSearchTip_Click(object sender, EventArgs e)
        {
            tbSearch.Focus();
        }

        private void tbSearch_Enter(object sender, EventArgs e)
        {
            lblSearchTip.Visible = false;
        }

        private void tbSearch_Leave(object sender, EventArgs e)
        {
            lblSearchTip.Visible = String.IsNullOrEmpty(tbSearch.Text);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            SearchTextChanged();
        }

        private void SearchTextChanged()
        {
            string searchText = tbSearch.Text.ToLower().Trim();

            if (String.IsNullOrEmpty(searchText))
            {
                if (cbSorting.SelectedIndex == 0)
                {
                    lbSearchList.Visible = false;
                    tvSkillList.Visible = true;
                    lblNoMatches.Visible = false;
                    lvSortedSkillList.Visible = false;
                    return;
                }
                //else
                //{
                //    lbSearchList.Visible = false;
                //    lblNoMatches.Visible = false;
                //    tvSkillList.Visible = false;
                //    lvSortedSkillList.Visible = true;
                //}
            }

            SortedList<string, GrandSkill> filteredItems = new SortedList<string, GrandSkill>();
            foreach (TreeNode gtn in tvSkillList.Nodes)
            {
                foreach (TreeNode tn in gtn.Nodes)
                {
                    if (tn.Text.ToLower().Contains(searchText))
                    {
                        filteredItems.Add(tn.Text, tn.Tag as GrandSkill);
                    }
                }
            }

            SortedListSortKey sk;
            SortedListDisplayKey dk;
            MakeUniqueSortKey mu;
            string sortColName = String.Empty;

            switch (cbSorting.SelectedIndex)
            {
                default:
                case 0: // No sorting
                    lbSearchList.BeginUpdate();
                    try
                    {
                        lbSearchList.Items.Clear();
                        foreach (GrandSkill gs in filteredItems.Values)
                        {
                            lbSearchList.Items.Add(gs);
                        }

                        lbSearchList.Location = tvSkillList.Location;
                        lbSearchList.Size = tvSkillList.Size;
                        lbSearchList.Visible = true;
                        tvSkillList.Visible = false;
                        lvSortedSkillList.Visible = false;
                        lblNoMatches.Visible = (filteredItems.Count == 0);
                    }
                    finally
                    {
                        lbSearchList.EndUpdate();
                    }
                    return;
                case 1: // Training time to next level
                    sortColName = "Time";
                    sk = delegate(GrandSkill gs)
                    {
                        int curLevel = gs.Level;
                        if (curLevel == 5)
                            return TimeSpan.MaxValue;
                        int nextLevel = curLevel + 1;
                        return gs.GetPrerequisiteTime() + gs.GetTrainingTimeToLevel(nextLevel);
                    };
                    dk = delegate(GrandSkill gs, object v)
                    {
                        TimeSpan ts = (TimeSpan)v;
                        if (ts == TimeSpan.MaxValue)
                            return "-";
                        else
                        {
                            int nextLevel = gs.Level + 1;
                            return
                                GrandSkill.GetRomanSkillNumber(nextLevel) + ": " +
                                GrandSkill.TimeSpanToDescriptiveText(ts, DescriptiveTextOptions.Default);
                        }
                    };
                    mu = delegate(IComparable v)
                    {
                        TimeSpan ts = (TimeSpan)v;
                        ts = ts + TimeSpan.FromTicks(1);
                        return ts;
                    };
                    break;
                case 2: // Training time to level V
                    sortColName = "Time";
                    sk = delegate(GrandSkill gs)
                    {
                        int curLevel = gs.Level;
                        if (curLevel == 5)
                            return TimeSpan.MaxValue;
                        return gs.GetPrerequisiteTime() + gs.GetTrainingTimeToLevel(5);
                    };
                    dk = delegate(GrandSkill gs, object v)
                    {
                        TimeSpan ts = (TimeSpan)v;
                        if (ts == TimeSpan.MaxValue)
                            return "-";
                        else
                        {
                            int nextLevel = gs.Level + 1;
                            return
                                GrandSkill.GetRomanSkillNumber(5) + ": " +
                                GrandSkill.TimeSpanToDescriptiveText(ts, DescriptiveTextOptions.Default);
                        }
                    };
                    mu = delegate(IComparable v)
                    {
                        TimeSpan ts = (TimeSpan)v;
                        ts = ts + TimeSpan.FromTicks(1);
                        return ts;
                    };
                    break;
            }

            lvSortedSkillList.BeginUpdate();
            try
            {
                SortedList<IComparable, Pair<GrandSkill, string>> sortedItems = new SortedList<IComparable, Pair<GrandSkill, string>>();
                foreach (GrandSkill gs in filteredItems.Values)
                {
                    IComparable sortVal = sk(gs);
                    string dispVal = dk(gs, sortVal);
                    while (sortedItems.ContainsKey(sortVal))
                    {
                        sortVal = mu(sortVal);
                    }
                    sortedItems.Add(sortVal, new Pair<GrandSkill, string>(gs, dispVal));
                }

                chSortKey.Text = sortColName;
                lvSortedSkillList.Items.Clear();
                foreach (Pair<GrandSkill, string> p in sortedItems.Values)
                {
                    ListViewItem lvi = new ListViewItem(p.A.Name);
                    lvi.SubItems.Add(p.B);
                    lvi.Tag = p.A;
                    lvSortedSkillList.Items.Add(lvi);
                }

                chName.Width = -2;
                chSortKey.Width = -2;

                lvSortedSkillList.Location = tvSkillList.Location;
                lvSortedSkillList.Size = tvSkillList.Size;
                lvSortedSkillList.Visible = true;
                tvSkillList.Visible = false;
                lbSearchList.Visible = false;
                lblNoMatches.Visible = (sortedItems.Count == 0);
            }
            finally
            {
                lvSortedSkillList.EndUpdate();
            }
        }

        private delegate IComparable SortedListSortKey(GrandSkill gs);
        private delegate IComparable MakeUniqueSortKey(IComparable v);
        private delegate string SortedListDisplayKey(GrandSkill gs, object v);

        private void lbSearchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSearchList.SelectedIndex >= 0)
                this.SelectedSkill = lbSearchList.Items[lbSearchList.SelectedIndex] as GrandSkill;
            else
                this.SelectedSkill = null;
        }

        private void tvSkillList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = tvSkillList.SelectedNode;
            GrandSkill gs = tn.Tag as GrandSkill;
            if (gs != null)
                this.SelectedSkill = gs;
        }

        private void SkillSelectControl_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            cbSorting.SelectedIndex = 0;
        }

        private void cbShowNonPublic_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSkillDisplay();
        }

        private void cbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSkillDisplay();
        }

        private void UpdateSkillDisplay()
        {
            UpdateSkillFilter();
            SearchTextChanged();
        }

        private void lvSortedSkillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSortedSkillList.SelectedItems.Count == 0)
            {
                this.SelectedSkill = null;
            }
            else
            {
                ListViewItem lvi = lvSortedSkillList.SelectedItems[0];
                this.SelectedSkill = lvi.Tag as GrandSkill;
            }
        }
    }
}
