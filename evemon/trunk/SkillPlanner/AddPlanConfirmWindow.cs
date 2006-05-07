using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class AddPlanConfirmWindow : EVEMonForm
    {
        public AddPlanConfirmWindow()
        {
            InitializeComponent();
        }

        public AddPlanConfirmWindow(IEnumerable<PlanEntry> entries)
            : this()
        {
            m_entries = entries;
        }

        private IEnumerable<PlanEntry> m_entries;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddPlanConfirmWindow_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (PlanEntry pe in m_entries)
            {
                listBox1.Items.Add(pe.SkillName + " " + GrandSkill.GetRomanSkillNumber(pe.Level));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private static bool ShouldAdd(Plan p, GrandSkill gs, int level, IEnumerable<PlanEntry> list)
        {
            if (gs.Level < level && !p.IsPlanned(gs, level))
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

        private static void AddPrerequisiteEntries(Plan p, GrandSkill gs, List<PlanEntry> planEntries)
        {
            foreach (GrandSkill.Prereq pp in gs.Prereqs)
            {
                GrandSkill pgs = pp.Skill;
                AddPrerequisiteEntries(p, pgs, planEntries);
                for (int i = 1; i <= pp.RequiredLevel; i++)
                {
                    if (ShouldAdd(p, pgs, i, planEntries))
                    {
                        PlanEntry pe = new PlanEntry();
                        pe.SkillName = pgs.Name;
                        pe.Level = i;
                        pe.EntryType = PlanEntryType.Prerequisite;
                        planEntries.Add(pe);
                    }
                }
            }
        }

        private static void ConfirmSkillAdd(Plan p, List<PlanEntry> planEntries)
        {
            using (AddPlanConfirmWindow f = new AddPlanConfirmWindow(planEntries))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    p.AddList(planEntries);
                }
            }
        }

        public static void AddSkillsWithConfirm(Plan p, IEnumerable<Pair<string, int>> skillsToAdd)
        {
            List<PlanEntry> planEntries = new List<PlanEntry>();
            foreach (Pair<string, int> ts in skillsToAdd)
            {
                GrandSkill gs = p.GrandCharacterInfo.GetSkill(ts.A);
                if (ShouldAdd(p, gs, ts.B, planEntries))
                {
                    AddPrerequisiteEntries(p, gs, planEntries);
                    for (int i = 1; i <= ts.B; i++)
                    {
                        if (ShouldAdd(p, gs, i, planEntries))
                        {
                            PlanEntry pe = new PlanEntry();
                            pe.SkillName = gs.Name;
                            pe.Level = i;
                            pe.EntryType = (i == ts.B) ? PlanEntryType.Planned : PlanEntryType.Prerequisite;
                            planEntries.Add(pe);
                        }
                    }
                }
            }
            if (planEntries.Count > 0)
            {
                ConfirmSkillAdd(p, planEntries);
            }
            else
            {
                MessageBox.Show("All the required skills are already in your plan.",
                        "Already Planned", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}