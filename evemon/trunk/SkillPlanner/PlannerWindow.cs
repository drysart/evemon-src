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
    public partial class PlannerWindow : Form
    {
        public PlannerWindow()
        {
            InitializeComponent();
        }

        private Settings m_settings;
        private CharacterInfo m_characterInfo;
        private PlannerData m_plannerData;

        public PlannerWindow(Settings s, CharacterInfo ci)
            : this()
        {
            m_settings = s;
            m_characterInfo = ci;
            m_plannerData = PlannerData.GetPlannerData();
        }

        private void PlannerWindow_Load(object sender, EventArgs e)
        {
            cbSkillFilter.SelectedIndex = 0;
        }

        private void tvSkillView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = tvSkillView.SelectedNode;
            OnSkillSelect(tn.Tag as PlannerSkill);
        }

        private void OnSkillSelect(PlannerSkill ps)
        {
            while (pnlSkillDisplay.Controls.Count > 0)
            {
                Control c = pnlSkillDisplay.Controls[0];
                pnlSkillDisplay.Controls.Remove(c);
                c.Dispose();
            }
            if (ps == null)
                return;

            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.ColumnCount = 3;
            tlp.RowCount = 3;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            Label l = new Label();
            l.Text = "test test";
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.AutoSize = true;
            tlp.Controls.Add(l, 1, 1);
            tlp.Dock = DockStyle.Fill;

            pnlSkillDisplay.Controls.Add(tlp);
        }

        private Skill GetCharacterSkillForPlannerSkill(PlannerSkill ps)
        {
            return m_characterInfo.GetSkill(ps.Name);
        }

        private delegate bool SkillFilterDelegate(PlannerSkill el);

        private void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            SkillFilterDelegate sfd;

            switch (cbSkillFilter.SelectedIndex)
            {
                case 1: // Known Skills
                    sfd = delegate(PlannerSkill el)
                    {
                        return (GetCharacterSkillForPlannerSkill(el) != null);
                    };
                    break;
                case 2: // Planned Skills
                case 0: // All Skills
                default:
                    sfd = delegate(PlannerSkill el)
                    {
                        return true;
                    };
                    break;
            }

            tvSkillView.Nodes.Clear();

            foreach (PlannerSkillGroup psg in m_plannerData.SkillGroups)
            {
                TreeNode gtn = new TreeNode(psg.Name);
                foreach (PlannerSkill ps in psg.Skills)
                {
                    if (sfd(ps))
                    {
                        TreeNode stn = new TreeNode(ps.Name);
                        stn.Tag = ps;
                        gtn.Nodes.Add(stn);
                    }
                }
                if (gtn.Nodes.Count > 0)
                {
                    tvSkillView.Nodes.Add(gtn);
                }
            }
        }
    }
}