using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class PlanSortWindow : EVEMon.Common.EVEMonForm
    {
        public PlanSortWindow()
        {
            InitializeComponent();
        }

        private void PlanSortWindow_Load(object sender, EventArgs e)
        {
            cbSortType.BeginUpdate();
            try
            {
                cbSortType.Items.Clear();
                int count = Enum.GetValues(typeof(PlanSortType)).Length;
                for (int i = 0; i < count; i++)
                {
                    string txt = String.Empty;
                    PlanSortDescriptionAttribute descAttr =
                        EnumAttributeReader<PlanSortType, PlanSortDescriptionAttribute>.GetAttribute((PlanSortType)i);
                    if (descAttr != null)
                    {
                        txt = descAttr.DisplayText;
                    }
                    else
                    {
                        txt = ((PlanSortType)i).ToString();
                    }
                    cbSortType.Items.Add(txt);
                }
            }
            finally
            {
                cbSortType.EndUpdate();
            }
            cbSortType.SelectedIndex = (int)PlanSortType.FastestFirst;
        }

        private void cbSortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlanSortDescriptionAttribute descAttr =
                EnumAttributeReader<PlanSortType, PlanSortDescriptionAttribute>.GetAttribute((PlanSortType)cbSortType.SelectedIndex);
            if (descAttr != null)
            {
                lblDescription.Text = descAttr.Description;
            }
            else
            {
                lblDescription.Text = String.Empty;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private PlanSortType m_resultSortType;
        private bool m_resultLearningFirst;

        public PlanSortType SortType
        {
            get { return m_resultSortType; }
        }

        public bool LearningFirst
        {
            get { return m_resultLearningFirst; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_resultSortType = (PlanSortType)cbSortType.SelectedIndex;
            m_resultLearningFirst = cbArrangeLearning.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    public enum PlanSortType
    {
        [PlanSortDescription("No Sorting", "Do not sort skills in the plan.")]
        NoChange,
        [PlanSortDescription("Fastest Skills First", "Skills with the shortest remaining training time will be ordered first.")]
        FastestFirst
    }

    public class PlanSortDescriptionAttribute : Attribute
    {
        private string m_displayText = String.Empty;
        private string m_description = String.Empty;

        public string DisplayText
        {
            get { return m_displayText; }
            set { m_displayText = value; }
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public PlanSortDescriptionAttribute(string displayText, string description)
        {
            m_displayText = displayText;
            m_description = description;
        }
    }


    public class PlanSorter
    {
        private static Pair<string, int>[] sm_idealLearningSkillOrder;

        static PlanSorter()
        {
            // Order borrowed with slight modification from
            // http://myeve.eve-online.com/ingameboard.asp?a=topic&threadID=242786&page=1#2

            List<Pair<string, int>> idealBuilder = new List<Pair<string, int>>();
            for (int i = 0; i < 4; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Instant Recall", i + 1));
                idealBuilder.Add(new Pair<string, int>("Analytical Mind", i + 1));
                idealBuilder.Add(new Pair<string, int>("Learning", i + 1));
            }
            idealBuilder.Add(new Pair<string, int>("Instant Recall", 5));
            for (int i = 0; i < 4; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Eidetic Memory", i + 1));
            }
            idealBuilder.Add(new Pair<string, int>("Analytical Mind", 5));
            for (int i = 0; i < 4; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Logic", i + 1));
            }
            idealBuilder.Add(new Pair<string, int>("Learning", 5));
            for (int i = 0; i < 5; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Spatial Awareness", i + 1));
                idealBuilder.Add(new Pair<string, int>("Iron Will", i + 1));
                idealBuilder.Add(new Pair<string, int>("Empathy", i + 1));
            }
            for (int i = 0; i < 4; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Focus", i + 1));
                idealBuilder.Add(new Pair<string, int>("Clarity", i + 1));
                idealBuilder.Add(new Pair<string, int>("Presence", i + 1));
            }
            idealBuilder.Add(new Pair<string, int>("Eidetic Memory", 5));
            idealBuilder.Add(new Pair<string, int>("Logic", 5));
            idealBuilder.Add(new Pair<string, int>("Focus", 5));
            idealBuilder.Add(new Pair<string, int>("Clarity", 5));
            idealBuilder.Add(new Pair<string, int>("Presence", 5));
            sm_idealLearningSkillOrder = idealBuilder.ToArray();
        }

        public static void SortPlan(Plan p, PlanSortType sortType, bool learningFirst)
        {
            PlanSorter ps = new PlanSorter(p, sortType, learningFirst);
            ps.Sort();
        }

        private Plan m_plan;
        private PlanSortType m_sortType;
        private bool m_learningFirst;

        private PlanSorter(Plan p, PlanSortType sortType, bool learningFirst)
        {
            m_plan = p;
            m_sortType = sortType;
            m_learningFirst = learningFirst;
        }

        private List<PlanEntry> m_originalOrder = new List<PlanEntry>();
        private List<PlanEntry> m_skillsToInsert = new List<PlanEntry>();

        private void Sort()
        {
            foreach (PlanEntry pe in m_plan.Entries)
            {
                PlanEntry xpe = (PlanEntry)pe.Clone();
                m_originalOrder.Add(xpe);
                m_skillsToInsert.Add(xpe);
            }
            m_plan.SuppressEvents();
            try
            {
                m_plan.Entries.Clear();
                if (m_learningFirst)
                    ArrangeLearningSkills();
                ArrangeInSortTypeOrder();
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, true);
                m_plan.Entries.Clear();
                foreach (PlanEntry xpe in m_originalOrder)
                {
                    PlanEntry pe = (PlanEntry)xpe.Clone();
                    m_plan.Entries.Add(pe);
                }
                MessageBox.Show("Sorting failed: " + ex.Message, "Sorting Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                m_plan.ResumeEvents();
            }
        }

        private void ArrangeInSortTypeOrder()
        {
            switch (m_sortType)
            {
                case PlanSortType.NoChange:
                    while (m_skillsToInsert.Count > 0)
                    {
                        PlanEntry pe = (PlanEntry)m_skillsToInsert[0].Clone();
                        m_plan.Entries.Add(pe);
                        m_skillsToInsert.RemoveAt(0);
                    }
                    break;
                case PlanSortType.FastestFirst:
                    ArrangeFastestFirst();
                    break;
            }
        }

        private void ArrangeFastestFirst()
        {
            Dictionary<string, int> trainedLevels = new Dictionary<string, int>();

            while (m_skillsToInsert.Count > 0)
            {
                TimeSpan fastestSpan = TimeSpan.MaxValue;
                PlanEntry fastestPe = null;
                for (int i = 0; i < m_skillsToInsert.Count; i++)
                {
                    PlanEntry thisPe = m_skillsToInsert[i];
                    GrandSkill thisSkill = m_plan.GrandCharacterInfo.GetSkill(thisPe.SkillName);
                    TimeSpan thisSpan = thisSkill.GetTrainingTimeOfLevelOnly(thisPe.Level, true);
                    if (thisSpan < fastestSpan)
                    {
                        bool canInsert = true;
                        foreach (GrandSkill.Prereq pp in thisSkill.Prereqs)
                        {
                            if (pp.Skill.Level < pp.RequiredLevel)
                            {
                                if (!trainedLevels.ContainsKey(pp.Name) ||
                                    trainedLevels[pp.Name] < pp.RequiredLevel)
                                {
                                    canInsert = false;
                                    break;
                                }
                            }
                        }
                        if (canInsert)
                        {
                            fastestSpan = thisSpan;
                            fastestPe = thisPe;
                        }
                    }
                }
                if (fastestPe == null)
                    throw new ApplicationException("no suitable skill -- should never happen!");

                m_plan.Entries.Add((PlanEntry)fastestPe.Clone());
                m_skillsToInsert.Remove(fastestPe);
                trainedLevels[fastestPe.SkillName] = fastestPe.Level;
            }
        }

        private void ArrangeLearningSkills()
        {
            for (int i = 0; i < sm_idealLearningSkillOrder.Length; i++)
            {
                string skillname = sm_idealLearningSkillOrder[i].A;
                int level = sm_idealLearningSkillOrder[i].B;
                for (int z = 0; z < m_skillsToInsert.Count; z++)
                {
                    if (m_skillsToInsert[z].SkillName == skillname &&
                        m_skillsToInsert[z].Level == level)
                    {
                        PlanEntry pe = (PlanEntry)m_skillsToInsert[z].Clone();
                        m_plan.Entries.Add(pe);
                        m_skillsToInsert.RemoveAt(z);
                        break;
                    }
                }
            }
        }
    }
}

