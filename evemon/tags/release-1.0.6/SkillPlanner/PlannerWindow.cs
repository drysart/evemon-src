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
        private PlanInfo m_planInfo;

        public PlannerWindow(Settings s, CharacterInfo ci)
            : this()
        {
            m_settings = s;
            m_characterInfo = ci;
            m_plannerData = PlannerData.GetPlannerData();
            m_planInfo = s.GetSkillPlanForCharacter(ci.Name);

            if (m_planInfo==null)
            {
                m_planInfo = new PlanInfo();
                m_planInfo.CharacterName = ci.Name;
                s.SkillPlans.Add(m_planInfo);
            }

            m_planInfo.CleanupPlan(ci);
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

            //Panel containerPanel = new Panel();
            //containerPanel.AutoScroll = true;
            //containerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            Control xc = GetSkillControlRecursive(ps, true, 0);

            //SkillBoxControl sbc = new SkillBoxControl(ps, m_characterInfo, m_planInfo, true);
            //tlp.Dock = DockStyle.Fill;
            //sbc.Location = new Point(0, 0);
            //containerPanel.ClientSize = sbc.Size;
            //containerPanel.Controls.Add(sbc);

            //tlp.Controls.Add(containerPanel, 1, 1);
            tlp.Controls.Add(xc, 1, 1);

            tlp.Location = new Point(0, 0);
            tlp.Size = pnlSkillDisplay.ClientSize;
            //tlp.Anchor = AnchorStyles.Top | AnchorStyles.Left; // | AnchorStyles.Right | AnchorStyles.Bottom;
            tlp.AutoSize = true;
            tlp.AutoSizeMode = AutoSizeMode.GrowOnly;
            //tlp.Dock = DockStyle.Fill;
            pnlSkillDisplay.Controls.Add(tlp);
            //xc.Padding = new Padding(3);
            //pnlSkillDisplay.Controls.Add(xc);
            //FixupLocation();
        }

        private const int LR_PADDING = 10;
        private const int UD_PADDING = 5;
        private const bool LAYOUT_HORIZONTAL = false;

        private Control GetSkillControlRecursive(PlannerSkill ps, bool primary, int prereqLevel)
        {
            SkillBoxControl sbc = new SkillBoxControl(ps, m_characterInfo, m_planInfo, primary, prereqLevel);
            if (ps.Prereqs.Count == 0)
                return sbc;

            Panel cPanel = new Panel();
            cPanel.BackColor = Color.Transparent;

            List<LineDrawControl> m_lines = new List<LineDrawControl>();
            if (LAYOUT_HORIZONTAL)
            {
                int curTop = 0;
                foreach (PlannerPrereq prereq in ps.Prereqs)
                {
                    LineDrawControl ldc = new LineDrawControl();
                    cPanel.Controls.Add(ldc);
                    m_lines.Add(ldc);

                    PlannerSkill tps = m_plannerData.GetSkill(prereq.Name);
                    Control c = GetSkillControlRecursive(tps, false, prereq.Level);
                    c.Top = curTop;
                    c.Left = sbc.Width + LR_PADDING;
                    curTop += c.Height + UD_PADDING;
                    cPanel.Controls.Add(c);
                    if (cPanel.Width < c.Right)
                        cPanel.Width = c.Right;

                    ldc.SetPointA(c.Left + sbc.Width / 2, c.Top + c.Height / 2);
                }
                curTop -= UD_PADDING;
                cPanel.Height = curTop;
                sbc.Top = (curTop / 2) - (sbc.Height / 2);
                sbc.Left = 0;
            }
            else
            {
                int curLeft = 0;
                foreach (PlannerPrereq prereq in ps.Prereqs)
                {
                    LineDrawControl ldc = new LineDrawControl();
                    cPanel.Controls.Add(ldc);
                    m_lines.Add(ldc);

                    PlannerSkill tps = m_plannerData.GetSkill(prereq.Name);
                    Control c = GetSkillControlRecursive(tps, false, prereq.Level);
                    c.Top = sbc.Height + LR_PADDING;
                    c.Left = curLeft;
                    curLeft += c.Width + UD_PADDING;
                    cPanel.Controls.Add(c);
                    if (cPanel.Height < c.Bottom)
                        cPanel.Height = c.Bottom;

                    ldc.SetPointA(c.Left + c.Width / 2, c.Top + sbc.Height / 2);
                }
                curLeft -= UD_PADDING;
                cPanel.Width = curLeft;
                sbc.Top = 0;
                sbc.Left = (curLeft / 2) - (sbc.Width / 2);
            }
            cPanel.Controls.Add(sbc);
            foreach (LineDrawControl xldc in m_lines)
            {
                xldc.SetPointB(sbc.Left + sbc.Width / 2, sbc.Top + sbc.Height / 2);
                xldc.SendToBack();
            }

            return cPanel;
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
                case 3: // Order Planned Skills
                    SetupPlannedSkillOrder();
                    return;
                case 1: // Show Known Skills
                    sfd = delegate(PlannerSkill el)
                    {
                        return (GetCharacterSkillForPlannerSkill(el) != null);
                    };
                    break;
                case 2: // Show Planned Skills
                    sfd = delegate(PlannerSkill el)
                    {
                        foreach (PlannedSkill ps in m_planInfo.PlannedSkills)
                        {
                            if (ps.Name == el.Name)
                                return true;
                        }
                        return false;
                    };
                    break;
                case 0: // Show All Skills
                default:
                    sfd = delegate(PlannerSkill el)
                    {
                        return true;
                    };
                    break;
            }

            lbPlannedList.Visible = false;
            tvSkillView.Visible = true;
            pnlSkillDisplay.Visible = true;
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

        private void SetupPlannedSkillOrder()
        {
            lbPlannedList.Location = tvSkillView.Location;
            lbPlannedList.Size = tvSkillView.Size;
            lbPlannedList.Anchor = tvSkillView.Anchor;

            tvSkillView.Visible = false;
            pnlSkillDisplay.Visible = false;
            lbPlannedList.Visible = true;

            lbPlannedList.Items.Clear();
            foreach (PlannedSkill ps in m_planInfo.PlannedSkills)
            {
                lbPlannedList.Items.Add(ps.Name);
            }
        }

        private void pnlSkillDisplay_ClientSizeChanged(object sender, EventArgs e)
        {
            if (pnlSkillDisplay.Controls.Count == 0)
                return;
            pnlSkillDisplay.SuspendLayout();
            Control c = pnlSkillDisplay.Controls[0];
            c.Size = pnlSkillDisplay.ClientSize;
            pnlSkillDisplay.ResumeLayout();
        }

        private void FixupLocation()
        {
        }
    }
}