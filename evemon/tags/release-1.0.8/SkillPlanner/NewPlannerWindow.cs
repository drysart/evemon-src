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

        private GrandCharacterInfo m_grandCharacterInfo;

        public NewPlannerWindow(GrandCharacterInfo gci)
            : this()
        {
            m_grandCharacterInfo = gci;
        }

        private void NewPlannerWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            skillTreeDisplay1.RootSkill = m_grandCharacterInfo.GetSkill("Caldari Dreadnought");
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

        private void tvSkillView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = tvSkillView.SelectedNode;
            skillTreeDisplay1.RootSkill = tn.Tag as GrandSkill;
        }
    }
}