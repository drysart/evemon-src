using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class SuggestionWindow : EVEMonForm
    {
        public SuggestionWindow()
        {
            InitializeComponent();
        }

        public SuggestionWindow(Plan p)
            : this()
        {
            m_plan = p;
        }

        private Plan m_plan;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SuggestionWindow_Load(object sender, EventArgs e)
        {
            IEnumerable<PlanEntry> entries = m_plan.GetSuggestions();

            GrandCharacterInfo gci = m_plan.GrandCharacterInfo;
            EveAttributeScratchpad scratchpad = new EveAttributeScratchpad();
            TimeSpan postTime = TimeSpan.Zero;

            lbSkills.Items.Clear();
            foreach (PlanEntry pe in entries)
            {
                lbSkills.Items.Add(pe.SkillName + " " +
                    GrandSkill.GetRomanSkillNumber(pe.Level));

                GrandSkill gs = gci.GetSkill(pe.SkillName);
                postTime += gs.GetTrainingTimeOfLevelOnly(pe.Level, true, scratchpad);
                if (gs.Name == "Learning")
                    scratchpad.AdjustLearningLevelBonus(1);
                else
                    scratchpad.AdjustAttributeBonus(gs.AttributeModified, 1);
            }
            postTime += m_plan.GetTotalTime(scratchpad);
            TimeSpan preTime = m_plan.GetTotalTime(null);

            TimeSpan diff = preTime - postTime;

            lblBeforeTime.Text = GrandSkill.TimeSpanToDescriptiveText(preTime, DTO_OPTS);
            lblAfterTime.Text = GrandSkill.TimeSpanToDescriptiveText(postTime, DTO_OPTS);

            lblDiffTime.Text = GrandSkill.TimeSpanToDescriptiveText(diff, DTO_OPTS);
        }

        private const DescriptiveTextOptions DTO_OPTS = DescriptiveTextOptions.IncludeCommas;
    }
}