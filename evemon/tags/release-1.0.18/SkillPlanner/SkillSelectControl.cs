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
                    if (sf(gs))
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

            tbSearch_TextChanged(this, new EventArgs());
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
            string searchText = tbSearch.Text.ToLower().Trim();

            if (String.IsNullOrEmpty(searchText))
            {
                lbSearchList.Visible = false;
                tvSkillList.Visible = true;
                lblNoMatches.Visible = false;
                return;
            }

            bool hasMatch = false;
            lbSearchList.BeginUpdate();
            try
            {
                lbSearchList.Items.Clear();
                List<string> filteredItems = new List<string>();
                foreach (TreeNode gtn in tvSkillList.Nodes)
                {
                    foreach (TreeNode tn in gtn.Nodes)
                    {
                        if (tn.Text.ToLower().Contains(searchText))
                        {
                            filteredItems.Add(tn.Text);
                            //lbSearchList.Items.Add(tn.Text);
                            //hasMatch = true;
                        }
                    }
                }
                filteredItems.Sort();
                foreach (string s in filteredItems)
                {
                    lbSearchList.Items.Add(s);
                }

                lbSearchList.Location = tvSkillList.Location;
                lbSearchList.Size = tvSkillList.Size;
                lbSearchList.Visible = true;
                tvSkillList.Visible = false;
                lblNoMatches.Visible = (filteredItems.Count==0);
            }
            finally
            {
                lbSearchList.EndUpdate();
            }
        }

        private void lbSearchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedSkill = m_grandCharacterInfo.GetSkill((string)lbSearchList.Items[lbSearchList.SelectedIndex]);
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
        }
    }
}
