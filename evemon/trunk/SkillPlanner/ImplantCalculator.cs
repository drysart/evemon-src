using System;
using System.Drawing;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ImplantCalculator : EVEMonForm
    {
        public ImplantCalculator()
        {
            InitializeComponent();
        }

        public ImplantCalculator(GrandCharacterInfo gci, Plan p)
            : this()
        {
            m_grandCharacterInfo = gci;
            m_plan = p;
        }

        private GrandCharacterInfo m_grandCharacterInfo;
        private Plan m_plan;

        private void ImplantCalculator_Shown(object sender, EventArgs e)
        {
            LoadCurrent(true);
        }

        private bool m_disablePlanCalc = false;

        private void LoadCurrent(bool withImplants)
        {
            m_disablePlanCalc = true;
            try
            {
                LoadAttribute(EveAttribute.Intelligence, nudIntelligence, withImplants);
                LoadAttribute(EveAttribute.Charisma, nudCharisma, withImplants);
                LoadAttribute(EveAttribute.Perception, nudPerception, withImplants);
                LoadAttribute(EveAttribute.Memory, nudMemory, withImplants);
                LoadAttribute(EveAttribute.Willpower, nudWillpower, withImplants);
            }
            finally
            {
                m_disablePlanCalc = false;
            }

            CalculatePlanTimes();
        }

        private void LoadAttribute(EveAttribute attrib, NumericUpDown nud, bool withImplants)
        {
            int baseAttr = Convert.ToInt32(m_grandCharacterInfo.GetEffectiveAttribute(attrib, null, false, withImplants));
            nud.Value = baseAttr;
        }

        private void nudIntelligence_ValueChanged(object sender, EventArgs e)
        {
            AttributeUpdate(EveAttribute.Intelligence, Convert.ToInt32(nudIntelligence.Value),
                lblAdjustIntelligence, lblEffectiveIntelligence);
            CalculatePlanTimes();
        }

        private void nudCharisma_ValueChanged(object sender, EventArgs e)
        {
            AttributeUpdate(EveAttribute.Charisma, Convert.ToInt32(nudCharisma.Value),
                lblAdjustCharisma, lblEffectiveCharisma);
            CalculatePlanTimes();
        }

        private void nudPerception_ValueChanged(object sender, EventArgs e)
        {
            AttributeUpdate(EveAttribute.Perception, Convert.ToInt32(nudPerception.Value),
                lblAdjustPerception, lblEffectivePerception);
            CalculatePlanTimes();
        }

        private void nudMemory_ValueChanged(object sender, EventArgs e)
        {
            AttributeUpdate(EveAttribute.Memory, Convert.ToInt32(nudMemory.Value),
                lblAdjustMemory, lblEffectiveMemory);
            CalculatePlanTimes();
        }

        private void nudWillpower_ValueChanged(object sender, EventArgs e)
        {
            AttributeUpdate(EveAttribute.Willpower, Convert.ToInt32(nudWillpower.Value),
                lblAdjustWillpower, lblEffectiveWillpower);
            CalculatePlanTimes();
        }

        private void AttributeUpdate(EveAttribute attrib, int myValue, Label lblAdjust, Label lblEffective)
        {
            int learningLevel = m_grandCharacterInfo.SkillGroups["Learning"]["Learning"].Level;
            double learningAdjust = 1.0 + (0.02 * Convert.ToDouble(learningLevel));

            //int baseAttr = m_grandCharacterInfo.GetBaseAttribute(attrib);
            int baseAttr = Convert.ToInt32(m_grandCharacterInfo.GetEffectiveAttribute(attrib, null, false, false));

            int adjust = myValue - baseAttr;
            if (adjust >= 0)
            {
                lblAdjust.ForeColor = SystemColors.ControlText;
                lblAdjust.Text = "+" + adjust.ToString();
            }
            else
            {
                lblAdjust.ForeColor = Color.Red;
                lblAdjust.Text = adjust.ToString();
            }

            double effectiveAttr = Convert.ToDouble(myValue) * learningAdjust;
            lblEffective.Text = effectiveAttr.ToString("#0.00");
        }

        private void CalculatePlanTimes()
        {
            if (m_disablePlanCalc)
                return;

            // Current -- empty scratchpad is fine
            EveAttributeScratchpad currentScratchpad = new EveAttributeScratchpad();
            TimeSpan currentSpan = CalculatePlanTimes(currentScratchpad, lblCurrentSpan, lblCurrentDate);

            // Base -- subtract the values of all implants to get attributes back to base
            EveAttributeScratchpad baseScratchpad = new EveAttributeScratchpad();
            for (int an = 0; an < 5; an++)
            {
                EveAttribute thisAttrib = (EveAttribute)an;
                int implantAmount = 0;
                foreach (GrandEveAttributeBonus geab in m_grandCharacterInfo.AttributeBonuses)
                {
                    if (geab.EveAttribute == thisAttrib)
                    {
                        implantAmount += geab.Amount;
                    }
                }
                baseScratchpad.AdjustAttributeBonus(thisAttrib, 0 - implantAmount);
            }
            TimeSpan baseSpan = CalculatePlanTimes(baseScratchpad, lblBaseSpan, lblBaseDate);

            // This -- add difference from base
            EveAttributeScratchpad thisScratchpad = new EveAttributeScratchpad();
            for (int an = 0; an < 5; an++)
            {
                EveAttribute thisAttrib = (EveAttribute)an;
                int tBaseWI = Convert.ToInt32(m_grandCharacterInfo.GetEffectiveAttribute(thisAttrib, null, false, true));
                int tCur = 0;
                switch (thisAttrib)
                {
                    case EveAttribute.Intelligence:
                        tCur = Convert.ToInt32(nudIntelligence.Value);
                        break;
                    case EveAttribute.Charisma:
                        tCur = Convert.ToInt32(nudCharisma.Value);
                        break;
                    case EveAttribute.Perception:
                        tCur = Convert.ToInt32(nudPerception.Value);
                        break;
                    case EveAttribute.Memory:
                        tCur = Convert.ToInt32(nudMemory.Value);
                        break;
                    case EveAttribute.Willpower:
                        tCur = Convert.ToInt32(nudWillpower.Value);
                        break;
                }
                thisScratchpad.AdjustAttributeBonus(thisAttrib, tCur - tBaseWI);
            }
            TimeSpan thisSpan = CalculatePlanTimes(thisScratchpad, lblThisSpan, lblThisDate);

            if (thisSpan > baseSpan)
            {
                lblComparedToBase.ForeColor = Color.Red;
                lblComparedToBase.Text = GrandSkill.TimeSpanToDescriptiveText(thisSpan - baseSpan,
                    DescriptiveTextOptions.IncludeCommas) +
                    " slower than current base";
            }
            else
            {
                lblComparedToBase.ForeColor = SystemColors.ControlText;
                lblComparedToBase.Text = GrandSkill.TimeSpanToDescriptiveText(baseSpan - thisSpan,
                    DescriptiveTextOptions.IncludeCommas) +
                    " better than current base";
            }

            if (thisSpan > currentSpan)
            {
                lblComparedToCurrent.ForeColor = Color.Red;
                lblComparedToCurrent.Text = GrandSkill.TimeSpanToDescriptiveText(thisSpan - currentSpan,
                    DescriptiveTextOptions.IncludeCommas) +
                    " slower than current";
            }
            else
            {
                lblComparedToCurrent.ForeColor = SystemColors.ControlText;
                lblComparedToCurrent.Text = GrandSkill.TimeSpanToDescriptiveText(currentSpan - thisSpan,
                    DescriptiveTextOptions.IncludeCommas) +
                    " better than current";
            }
        }

        private TimeSpan CalculatePlanTimes(EveAttributeScratchpad scratchpad, Label lblSpan, Label lblDate)
        {
            TimeSpan ts = m_plan.GetTotalTime(scratchpad);
            DateTime dt = DateTime.Now + ts;

            lblSpan.Text = GrandSkill.TimeSpanToDescriptiveText(ts, DescriptiveTextOptions.IncludeCommas);
            lblDate.Text = dt.ToString();

            return ts;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LoadCurrent(false);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LoadCurrent(true);
        }
    }
}

