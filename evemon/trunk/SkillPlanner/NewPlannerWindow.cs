using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class NewPlannerWindow : EVEMonForm
    {
        public NewPlannerWindow()
        {
            InitializeComponent();
        }

        private Settings m_settings;
        private GrandCharacterInfo m_grandCharacterInfo;
        private Plan m_plan;

        public NewPlannerWindow(Settings s, GrandCharacterInfo gci, Plan p)
            : this()
        {
            m_settings = s;
            m_grandCharacterInfo = gci;

            m_plan = p;
            m_plan.GrandCharacterInfo = m_grandCharacterInfo;
            skillTreeDisplay1.Plan = m_plan;

            skillSelectControl1.GrandCharacterInfo = m_grandCharacterInfo;
            skillSelectControl1.Plan = m_plan;
            planEditor.Plan = m_plan;
        }

        private void NewPlannerWindow_Shown(object sender, EventArgs e)
        {
            m_plan.Changed += new EventHandler<EventArgs>(m_plan_Changed);
            m_settings.WorksafeChanged += new EventHandler<EventArgs>(m_settings_WorksafeChanged);

            m_settings_WorksafeChanged(null, null);

            shipSelectControl1_SelectedShipChanged(null, null);
            itemBrowserControl1.Plan = m_plan;

            TipWindow.ShowTip("planner",
                "Welcome to the Skill Planner",
                "Select skills to add to your plan using the list on the left. To " +
                "view the list of skills you've added to your plan, choose " +
                "\"View Plan\" from the dropdown in the upper left.");
            m_showing = true;
        }

        private bool m_showing = false;

        private void NewPlannerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_plan.Changed -= new EventHandler<EventArgs>(m_plan_Changed);
            m_settings.WorksafeChanged -= new EventHandler<EventArgs>(m_settings_WorksafeChanged);

            foreach (WeakReference<ImplantCalculator> ric in m_calcWindows)
            {
                ImplantCalculator ic = ric.Target;
                if (ic != null && ic.Visible)
                    ic.Close();
            }
            m_calcWindows.Clear();
        }

        private void m_settings_WorksafeChanged(object sender, EventArgs e)
        {
            skillTreeDisplay1.WorksafeMode = m_settings.WorksafeMode;
        }

        private void m_plan_Changed(object sender, EventArgs e)
        {
            m_settings.Save();
            UpdateStatusBar();
        }

        private void NewPlannerWindow_Load(object sender, EventArgs e)
        {
            this.Text = m_grandCharacterInfo.Name + " [" + m_plan.Name + "] - EVEMon Skill Planner";
            UpdatePlanControl();
        }

        private void skillTreeDisplay1_Load(object sender, EventArgs e)
        {
            //cbSkillFilter.SelectedIndex = 0;
        }

        //private delegate bool SkillFilter(GrandSkill gs);

        //private void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    tbSkillFilter.Text = String.Empty;
        //    lbFilteredSkills.Visible = false;

        //    SkillFilter sf;
        //    switch (cbSkillFilter.SelectedIndex)
        //    {
        //        case 4: // View Plan
        //            SwitchToPlanEditor(true);
        //            return;
        //        case 1: // Show Known Skills
        //            sf = delegate(GrandSkill xx)
        //            {
        //                return xx.Known;
        //            };
        //            break;
        //        case 2: // Show Planned Skills
        //            sf = delegate(GrandSkill xx)
        //            {
        //                return m_plan.IsPlanned(xx);
        //            };
        //            break;
        //        case 3: // Show Available, Untrained Skills
        //            sf = delegate(GrandSkill xx)
        //            {
        //                return (xx.PrerequisitesMet && !xx.Known);
        //            };
        //            break;
        //        case 0:
        //        default:
        //            sf = delegate(GrandSkill xx)
        //            {
        //                return true;
        //            };
        //            break;
        //    }

        //    SwitchToPlanEditor(false);

        //    tvSkillView.Nodes.Clear();
        //    foreach (GrandSkillGroup gsg in m_grandCharacterInfo.SkillGroups.Values)
        //    {
        //        TreeNode gtn = new TreeNode(gsg.Name);
        //        foreach (GrandSkill gs in gsg)
        //        {
        //            if (sf(gs))
        //            {
        //                TreeNode stn = new TreeNode(gs.Name);
        //                stn.Tag = gs;
        //                gtn.Nodes.Add(stn);
        //            }
        //        }
        //        if (gtn.Nodes.Count > 0)
        //        {
        //            tvSkillView.Nodes.Add(gtn);
        //        }
        //    }

        //    UpdatePlanControl();
        //}

        //private void SwitchToPlanEditor(bool switchOn)
        //{
        //    planEditor.Visible = switchOn;
        //    tvSkillView.Visible = !switchOn;
        //    skillTreeDisplay1.Visible = !switchOn;
        //    pbSearchImage.Visible = !switchOn;
        //    tbSkillFilter.Visible = !switchOn;
        //    lblSearchNote.Visible = (!switchOn && String.IsNullOrEmpty(tbSkillFilter.Text));
        //    lbFilteredSkills.Visible = false;

        //    if (switchOn)
        //    {
        //        pnlPlanControl.Visible = false;
        //        planEditor.Plan = m_plan;
        //    }
        //    else
        //        planEditor.Plan = null;

        //    //Point p = this.PointToClient(splitContainer1.Panel1.PointToScreen(tvSkillView.Location));
        //    Point p = this.PointToClient(splitContainer1.Panel1.PointToScreen(pbSearchImage.Location));
        //    planEditor.Location = p;
        //    planEditor.Size = new Size(
        //        this.ClientSize.Width - (planEditor.Location.X * 2),
        //        tvSkillView.Height + tbSkillFilter.Height + (tvSkillView.Top - tbSkillFilter.Bottom));
        //    planEditor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        //}

        private GrandSkill m_selectedSkill = null;

        //private void tvSkillView_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    TreeNode tn = tvSkillView.SelectedNode;
        //    GrandSkill gs = tn.Tag as GrandSkill;
        //    SelectSkill(gs);
        //}

        private void SelectSkill(GrandSkill gs)
        {
            m_selectedSkill = gs;
            skillTreeDisplay1.RootSkill = m_selectedSkill;

            UpdatePlanControl();
        }

        [Flags]
        private enum PlanSelectShowing
        {
            None = 1,
            One = 2,
            Two = 4,
            Three = 8,
            Four = 16,
            Five = 32
        }

        private PlanSelectShowing m_planSelectShowing;
        private int m_planSelectSelected;

        private void UpdatePlanSelect()
        {
            PlanSelectShowing thisPss = PlanSelectShowing.None;
            int plannedTo = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (m_selectedSkill.Level < i)
                {
                    int x = 1 << i;
                    thisPss = (PlanSelectShowing)((int)thisPss + x);
                }
                if (m_plan.IsPlanned(m_selectedSkill, i))
                    plannedTo = i;
            }
            if (thisPss != m_planSelectShowing || plannedTo != m_planSelectSelected)
            {
                cbPlanSelect.SelectedIndexChanged -= new EventHandler(cbPlanSelect_SelectedIndexChanged);

                cbPlanSelect.Items.Clear();
                if ((thisPss & PlanSelectShowing.None) != 0)
                {
                    cbPlanSelect.Items.Add("Not Planned");
                    if (plannedTo == 0)
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                }
                if ((thisPss & PlanSelectShowing.One) != 0)
                {
                    cbPlanSelect.Items.Add("Level I");
                    if (plannedTo == 1)
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                }
                if ((thisPss & PlanSelectShowing.Two) != 0)
                {
                    cbPlanSelect.Items.Add("Level II");
                    if (plannedTo == 2)
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                }
                if ((thisPss & PlanSelectShowing.Three) != 0)
                {
                    cbPlanSelect.Items.Add("Level III");
                    if (plannedTo == 3)
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                }
                if ((thisPss & PlanSelectShowing.Four) != 0)
                {
                    cbPlanSelect.Items.Add("Level IV");
                    if (plannedTo == 4)
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                }
                if ((thisPss & PlanSelectShowing.Five) != 0)
                {
                    cbPlanSelect.Items.Add("Level V");
                    if (plannedTo == 5)
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                }

                m_planSelectShowing = thisPss;
                m_planSelectSelected = plannedTo;
                cbPlanSelect.SelectedIndexChanged += new EventHandler(cbPlanSelect_SelectedIndexChanged);
            }
        }

        private void cbPlanSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = (string)cbPlanSelect.Items[cbPlanSelect.SelectedIndex];
            int setLevel = 0;
            if (s.StartsWith("Level "))
            {
                string r = s.Substring(6);
                setLevel = GrandSkill.GetIntForRoman(r);
            }
            m_planSelectSelected = setLevel;
            PlanTo(setLevel);
        }

        private void UpdatePlanControl()
        {
            if (planEditor.Visible)
            {
                UpdateStatusBar();
                return;
            }

            if (m_selectedSkill == null)
            {
                pnlPlanControl.Visible = false;
            }
            else
            {
                lblSkillName.Text = m_selectedSkill.Name;
                lblDescription.Text = m_selectedSkill.Description;
                lblAttributes.Text = "Primary: " + m_selectedSkill.PrimaryAttribute.ToString() + ", " +
                    "Secondary: " + m_selectedSkill.SecondaryAttribute.ToString();

                //int plannedTo = 0;
                bool anyPlan = false;
                bool tPlan;
                tPlan = SetPlanLabel(lblLevel1Time, 1);
                //if (tPlan)
                    //plannedTo = 1;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel2Time, 2);
                //if (tPlan)
                    //plannedTo = 2;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel3Time, 3);
                //if (tPlan)
                    //plannedTo = 3;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel4Time, 4);
                //if (tPlan)
                    //plannedTo = 4;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel5Time, 5);
                //if (tPlan)
                    //plannedTo = 5;
                anyPlan = anyPlan || tPlan;
                //btnCancelPlan.Enabled = anyPlan;

                UpdatePlanSelect();

                //if (plannedTo > 0)
                //{
                //    lblPlanDescription.Text = "Currently planned to level " +
                //        GrandSkill.GetRomanSkillNumber(plannedTo);
                //}
                //else
                //{
                //    lblPlanDescription.Text = "Not currently planned.";
                //}
                pnlPlanControl.Visible = true;
            }

            UpdateStatusBar();
        }

        private bool m_suggestionTipUp = false;

        private void UpdateStatusBar()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    UpdateStatusBar();
                }));
                return;
            }

            TimeSpan res = m_plan.GetTotalTime(null);
            slblStatusText.Text = String.Format("{0} Skill{1} Planned ({2} Unique Skill{3}). Total training time: {4}",
                m_plan.Entries.Count,
                m_plan.Entries.Count == 1 ? "" : "s",
                m_plan.UniqueSkillCount,
                m_plan.UniqueSkillCount == 1 ? "" : "s",
                GrandSkill.TimeSpanToDescriptiveText(res, DescriptiveTextOptions.FullText | DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.SpaceText));

            if (m_plan.HasAttributeSuggestion)
            {
                tslSuggestion.Visible = true;
                if (m_showing && !m_suggestionTipUp)
                {
                    m_suggestionTipUp = true;
                    TipWindow.ShowTip("suggestion",
                        "Plan Suggestion",
                        "EVEMon has analyzed your plan and has come up with a " +
                        "suggestion of learning skills that you can add that will " +
                        "lower the overall training time of the plan. To view this " +
                        "suggestion and the resulting change in plan time, click the " +
                        "\"Suggestion\" link in the planner status bar.");
                    m_suggestionTipUp = false;
                }
            }
            else
            {
                tslSuggestion.Visible = false;
            }
        }

        private bool SetPlanLabel(Label label, int level)
        {
            bool isPlanned = m_plan.IsPlanned(m_selectedSkill, level);
            StringBuilder sb = new StringBuilder();
            sb.Append("Level ");
            sb.Append(GrandSkill.GetRomanSkillNumber(level));
            sb.Append(": ");
            if (m_selectedSkill.Level >= level)
            {
                sb.Append("Already known");
                //button.Enabled = false;
            }
            else
            {
                TimeSpan tts = m_selectedSkill.GetTrainingTimeOfLevelOnly(level);
                if (m_selectedSkill.Level == level - 1)
                    tts = m_selectedSkill.GetTrainingTimeToLevel(level);
                sb.Append(GrandSkill.TimeSpanToDescriptiveText(tts, DescriptiveTextOptions.IncludeCommas));
                TimeSpan prts = m_selectedSkill.GetTrainingTimeToLevel(level-1) +
                    m_selectedSkill.GetPrerequisiteTime();
                if (prts > TimeSpan.Zero)
                {
                    sb.Append(" (plus ");
                    sb.Append(GrandSkill.TimeSpanToDescriptiveText(prts, DescriptiveTextOptions.IncludeCommas));
                    sb.Append(")");
                }
                //button.Enabled = !isPlanned;
                //if (m_selectedSkill.InTraining && m_selectedSkill.TrainingToLevel == level)
                //    button.Enabled = false;
            }
            label.Text = sb.ToString();
            return isPlanned;
        }

        private void skillTreeDisplay1_SkillClicked(object sender, SkillClickedEventArgs e)
        {
            if (e.Skill == m_selectedSkill)
            {
                if (e.Button == MouseButtons.Right)
                {
                    bool isPlanned = false;
                    isPlanned = SetMenuItemState(miPlanTo1, e.Skill, 1) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo2, e.Skill, 2) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo3, e.Skill, 3) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo4, e.Skill, 4) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo5, e.Skill, 5) || isPlanned;
                    miCancelPlanMenu.Enabled = isPlanned;
                    cmsSkillContext.Show(skillTreeDisplay1, e.Location);
                }
            }
        }

        private const DescriptiveTextOptions DTO_OPTS = DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.UppercaseText;

        private bool SetMenuItemState(ToolStripMenuItem mi, GrandSkill gs, int level)
        {
            bool isKnown = false;
            bool isPlanned = false;
            bool isTraining = false;
            if (level <= gs.Level)
            {
                isKnown = true;
            }
            if (!isKnown)
            {
                if (gs.InTraining && gs.TrainingToLevel == level)
                {
                    isTraining = true;
                }
                else
                {
                    foreach (PlanEntry pe in m_plan.Entries)
                    {
                        if (pe.Skill == gs && pe.Level == level)
                        {
                            isPlanned = true;
                            break;
                        }
                    }
                }
            }

            if (!isKnown && !isTraining)
            {
                if (isPlanned)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanSkillNumber(level) + " (Already Planned)";
                    mi.Enabled = false;
                    return true;
                }
                else
                {
                    TimeSpan ts = gs.GetPrerequisiteTime() + gs.GetTrainingTimeToLevel(level);
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanSkillNumber(level) + " (" +
                        GrandSkill.TimeSpanToDescriptiveText(ts, DTO_OPTS) + ")";
                    mi.Enabled = true;
                }
            }
            else
            {
                if (isKnown)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanSkillNumber(level) + " (Already Known)";
                    mi.Enabled = false;
                }
                else if (isTraining)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanSkillNumber(level) + " (Currently Training)";
                    mi.Enabled = false;
                }
            }
            return false;
        }

        private void miPlanTo1_Click(object sender, EventArgs e)
        {
            PlanTo(1);
        }

        private void miPlanTo2_Click(object sender, EventArgs e)
        {
            PlanTo(2);
        }

        private void miPlanTo3_Click(object sender, EventArgs e)
        {
            PlanTo(3);
        }

        private void miPlanTo4_Click(object sender, EventArgs e)
        {
            PlanTo(4);
        }

        private void miPlanTo5_Click(object sender, EventArgs e)
        {
            PlanTo(5);
        }

        private bool ShouldAdd(GrandSkill gs, int level, IEnumerable<PlanEntry> list)
        {
            if (gs.Level < level && !m_plan.IsPlanned(gs, level))
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

        private void PlanTo(int level)
        {
            if (level == 0)
            {
                CancelPlan();
                return;
            }

            //MessageBox.Show(this, "Planning not yet implemented.", "Not Yet Implemented", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            List<PlanEntry> planEntries = new List<PlanEntry>();
            AddPrerequisiteEntries(m_selectedSkill, planEntries);
            for (int i = 1; i <= level; i++)
            {
                if (ShouldAdd(m_selectedSkill, i, planEntries))
                {
                    PlanEntry pe = new PlanEntry();
                    pe.SkillName = m_selectedSkill.Name;
                    if (i == level)
                    {
                        pe.EntryType = PlanEntryType.Planned;
                        //pe.PrerequisiteForName = String.Empty;
                        //pe.PrerequisiteForLevel = -1;
                    }
                    else
                    {
                        pe.EntryType = PlanEntryType.Prerequisite;
                        //pe.PrerequisiteForName = m_selectedSkill.Name;
                        //pe.PrerequisiteForLevel = level + 1;
                    }
                    pe.Level = i;
                    planEntries.Add(pe);
                }
            }
            if (planEntries.Count > 0)
            {
                ConfirmSkillAdd(planEntries);
            }
            else
            {
                for (int i = 5; i > level; i--)
                {
                    PlanEntry pe = m_plan.GetEntry(m_selectedSkill.Name, i);
                    if (pe != null)
                    {
                        if (!m_plan.RemoveEntry(pe))
                        {
                            MessageBox.Show(this,
                                "The plan for this skill could not be set below level " +
                                GrandSkill.GetRomanSkillNumber(i) + " because this skill is " +
                                "required at that level for another skill you have planned.",
                                "Skill Needed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;
                        }
                    }
                }
            }
            UpdatePlanControl();
        }

        private void ConfirmSkillAdd(List<PlanEntry> planEntries)
        {
            using (AddPlanConfirmWindow f = new AddPlanConfirmWindow(planEntries))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    m_plan.AddList(planEntries);
                }
            }
        }

        private void AddPrerequisiteEntries(GrandSkill gs, List<PlanEntry> planEntries)
        {
            foreach (GrandSkill.Prereq pp in gs.Prereqs)
            {
                GrandSkill pgs = pp.Skill;
                AddPrerequisiteEntries(pgs, planEntries);
                for (int i = 1; i <= pp.RequiredLevel; i++)
                {
                    if (ShouldAdd(pgs, i, planEntries))
                    {
                        PlanEntry pe = new PlanEntry();
                        pe.SkillName = pgs.Name;
                        pe.Level = i;
                        pe.EntryType = PlanEntryType.Prerequisite;
                        //pe.PrerequisiteForName = gs.Name;
                        //pe.PrerequisiteForLevel = 1;
                        planEntries.Add(pe);
                    }
                }
            }
        }

        private void tmrSkillTick_Tick(object sender, EventArgs e)
        {
            UpdatePlanControl();
        }

        private void btnPlanTo1_Click(object sender, EventArgs e)
        {
            PlanTo(1);
        }

        private void btnPlanTo2_Click(object sender, EventArgs e)
        {
            PlanTo(2);
        }

        private void btnPlanTo3_Click(object sender, EventArgs e)
        {
            PlanTo(3);
        }

        private void btnPlanTo4_Click(object sender, EventArgs e)
        {
            PlanTo(4);
        }

        private void btnPlanTo5_Click(object sender, EventArgs e)
        {
            PlanTo(5);
        }

        private void btnCancelPlan_Click(object sender, EventArgs e)
        {
            CancelPlan();
        }

        private void CancelPlan()
        {
            using (CancelChoiceWindow f = new CancelChoiceWindow())
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;
                if (dr == DialogResult.Yes)
                    CancelPlan(true);
                if (dr == DialogResult.No)
                    CancelPlan(false);
            }
        }

        private void CancelPlan(bool includePrerequisites)
        {
            bool result = m_plan.RemoveEntry(m_selectedSkill, includePrerequisites, false);
            if (!result)
            {
                MessageBox.Show(this,
                    "The plan for this skill could not be cancelled because this skill is " +
                    "required for another skill you have planned.",
                    "Skill Needed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                UpdatePlanControl();
            }
        }

        private void miCancelAll_Click(object sender, EventArgs e)
        {
            CancelPlan(true);
        }

        private void miCancelThis_Click(object sender, EventArgs e)
        {
            CancelPlan(false);
        }

        private void tsbDeletePlan_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Are you sure you want to delete this plan?",
                "Delete Plan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.Yes)
                return;

            m_settings.RemovePlanFor(m_grandCharacterInfo.Name, m_plan.Name);
        }

        private void tslSuggestion_Click(object sender, EventArgs e)
        {
            using (SuggestionWindow f = new SuggestionWindow(m_plan))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;
            }
            // XXX: apply plan change

           // List<PlanEntry> nonLearningEntries = new List<PlanEntry>();
            m_plan.SuppressEvents();
            try
            {
                //foreach (PlanEntry pe in m_plan.Entries)
                //{
                //    GrandSkill gs = pe.Skill;
                //    if (!gs.IsLearningSkill && gs.Name != "Learning")
                //        nonLearningEntries.Add(pe);
                //}
                //m_plan.ClearEntries();
                //foreach (PlanEntry pe in nonLearningEntries)
                //{
                //    m_plan.Entries.Add(pe);
                //}
                IEnumerable<PlanEntry> entries = m_plan.GetSuggestions();
                int i = 0;
                foreach (PlanEntry pe in entries)
                {
                    m_plan.Entries.Insert(i, pe);
                    i++;
                }

                //Ensures any existing learning skills in the plan get moved to
                //account for higher levels being inserted at the front...
                m_plan.CheckForMissingPrerequisites();

                // Arrange the learning skills in the plan in optimal order
                PlanSorter.SortPlan(m_plan, PlanSortType.NoChange, true);
            }
            finally
            {
                m_plan.ResumeEvents();
            }
        }

        private void tsbCopyForum_Click(object sender, EventArgs e)
        {
            PlanTextOptions pto = (PlanTextOptions)m_settings.DefaultCopyOptions.Clone();
            using (CopySaveOptionsWindow f = new CopySaveOptionsWindow(pto, m_plan, true))
            {
                f.ShowDialog();
                if (f.DialogResult == DialogResult.Cancel)
                    return;
                if (f.SetAsDefault)
                {
                    m_settings.DefaultCopyOptions = pto;
                    m_settings.Save();
                }
            }

            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            {
                m_plan.SaveAsText(sw, pto, true);
                sw.Flush();
                string s = Encoding.Default.GetString(ms.ToArray());
                Clipboard.SetText(s);
            }

            MessageBox.Show("The skill plan has been copied to the clipboard in a " +
                "format suitable for forum posting.", "Plan Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private enum SaveType
        {
            None = 0,
            Emp = 1,
            Xml = 2,
            Text = 3
        }

        private void tsbSaveAs_Click(object sender, EventArgs e)
        {
            sfdSave.FileName = m_plan.GrandCharacterInfo.Name + " Skill Plan";
            sfdSave.FilterIndex = (int)SaveType.Emp;
            DialogResult dr = sfdSave.ShowDialog();
            if (dr == DialogResult.Cancel)
                return;

            string fileName = sfdSave.FileName;
            try
            {
                PlanTextOptions pto = null;
                if ((SaveType)sfdSave.FilterIndex == SaveType.Text)
                {
                    pto = (PlanTextOptions)m_settings.DefaultSaveOptions.Clone();
                    using (CopySaveOptionsWindow f = new CopySaveOptionsWindow(pto, m_plan, false))
                    {
                        f.ShowDialog();
                        if (f.DialogResult == DialogResult.Cancel)
                            return;
                        if (f.SetAsDefault)
                        {
                            m_settings.DefaultSaveOptions = pto;
                            m_settings.Save();
                        }
                    }
                }

                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    switch ((SaveType)sfdSave.FilterIndex)
                    {
                        case SaveType.Emp:
                            using (GZipStream gzs = new GZipStream(fs, CompressionMode.Compress))
                            {
                                SerializePlanTo(gzs);
                            }
                            break;
                        case SaveType.Xml:
                            SerializePlanTo(fs);
                            break;
                        case SaveType.Text:
                            SaveAsText(fs, pto);
                            break;
                        default:
                            return;
                    }
                }
            }
            catch (IOException err)
            {
                ExceptionHandler.LogException(err, true);
                MessageBox.Show("There was an error writing out the file:\n\n" + err.Message,
                    "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SerializePlanTo(Stream s)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Plan));
            xs.Serialize(s, m_plan);
        }

        private void SaveAsText(Stream fs, PlanTextOptions pto)
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                m_plan.SaveAsText(sw, pto, false);
            }
        }

        //private void tbSkillFilter_TextChanged(object sender, EventArgs e)
        //{
        //    if (String.IsNullOrEmpty(tbSkillFilter.Text) || tbSkillFilter.Text.Trim().Length==0)
        //    {
        //        tvSkillView.Visible = true;
        //        lbFilteredSkills.Visible = false;
        //        lblNoResults.Visible = false;
        //        return;
        //    }

        //    string searchStr = tbSkillFilter.Text.ToLower().Trim();

        //    List<string> filterResults = new List<string>();
        //    foreach (TreeNode tn in tvSkillView.Nodes)
        //    {
        //        foreach (TreeNode stn in tn.Nodes)
        //        {
        //            if (stn.Text.ToLower().Contains(searchStr))
        //                filterResults.Add(stn.Text);
        //        }
        //    }

        //    filterResults.Sort();
        //    lbFilteredSkills.Items.Clear();
        //    foreach (string s in filterResults)
        //    {
        //        lbFilteredSkills.Items.Add(s);
        //    }

        //    lbFilteredSkills.Location = tvSkillView.Location;
        //    lbFilteredSkills.Size = tvSkillView.Size;
        //    tvSkillView.Visible = false;
        //    lbFilteredSkills.Visible = true;
        //    lblNoResults.Visible = (filterResults.Count==0);
        //}

        //private void lbFilteredSkills_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (lbFilteredSkills.SelectedItems.Count > 0)
        //    {
        //        GrandSkill gs = m_grandCharacterInfo.GetSkill((string)lbFilteredSkills.SelectedItem);
        //        SelectSkill(gs);
        //    }
        //}

        //private void lblSearchNote_Click(object sender, EventArgs e)
        //{
        //    tbSkillFilter.Focus();
        //}

        //private void tbSkillFilter_Enter(object sender, EventArgs e)
        //{
        //    lblSearchNote.Visible = false;
        //}

        //private void tbSkillFilter_Leave(object sender, EventArgs e)
        //{
        //    lblSearchNote.Visible = String.IsNullOrEmpty(tbSkillFilter.Text);
        //}

        private void skillSelectControl1_Load(object sender, EventArgs e)
        {

        }

        private void skillSelectControl1_SelectedSkillChanged(object sender, EventArgs e)
        {
            SelectSkill(skillSelectControl1.SelectedSkill);
        }

        private List<WeakReference<ImplantCalculator>> m_calcWindows = new List<WeakReference<ImplantCalculator>>();

        private void tsbImplantCalculator_Click(object sender, EventArgs e)
        {
            // Remove closed windows
            for (int i = 0; i < m_calcWindows.Count; i++)
            {
                bool needRemove = true;
                ImplantCalculator thisIc = m_calcWindows[i].Target;
                if (thisIc != null)
                {
                    needRemove = !thisIc.Visible;
                }
                if (needRemove)
                {
                    m_calcWindows.RemoveAt(i);
                    i--;
                }
            }

            ImplantCalculator ic = new ImplantCalculator(m_grandCharacterInfo, m_plan);
            m_calcWindows.Add(new WeakReference<ImplantCalculator>(ic));

            ic.Show();
        }

        private void shipSelectControl1_Load(object sender, EventArgs e)
        {

        }

        private void shipSelectControl1_SelectedShipChanged(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(256, 256);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 256, 256));
            }
            pbShipImage.Image = b;

            if (shipSelectControl1.SelectedShip != null)
            {
                Ship s = shipSelectControl1.SelectedShip;
                int shipId = s.Id;
                EveSession.GetImageAsync(
                    "http://www.eve-online.com/bitmaps/icons/itemdb/shiptypes/256_256/" +
                    shipId.ToString() + ".png", true, delegate(EveSession ss, Image i)
                    {
                        GotShipImage(shipId, i);
                    });

                lblShipClass.Text = s.Type + " > " + s.Race;
                lblShipName.Text = s.Name;
                lblShipDescription.Text = Regex.Replace(s.Description, "<. +?>", String.Empty, RegexOptions.Singleline);

                bool allKnown = true;
                allKnown = SetShipSkillLabel(0, lblShipSkill1, s.RequiredSkills) && allKnown;
                allKnown = SetShipSkillLabel(1, lblShipSkill2, s.RequiredSkills) && allKnown;
                allKnown = SetShipSkillLabel(2, lblShipSkill3, s.RequiredSkills) && allKnown;

                if (!allKnown)
                {
                    List<Pair<GrandSkill, int>> reqSkills = new List<Pair<GrandSkill, int>>();
                    foreach (ShipRequiredSkill srs in s.RequiredSkills)
                    {
                        Pair<GrandSkill, int> p = new Pair<GrandSkill, int>();
                        p.A = m_grandCharacterInfo.GetSkill(srs.Name);
                        p.B = srs.Level;
                        reqSkills.Add(p);
                    }
                    TimeSpan trainTime = m_grandCharacterInfo.GetTrainingTimeToMultipleSkills(reqSkills);
                    lblShipTimeRequired.Text = "Training Time: " +
                        GrandSkill.TimeSpanToDescriptiveText(trainTime, DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.SpaceText);
                    btnShipSkillsAdd.Enabled = true;
                }
                else
                {
                    lblShipTimeRequired.Text = String.Empty;
                    btnShipSkillsAdd.Enabled = false;
                }

                lbShipProperties.BeginUpdate();
                try
                {
                    lbShipProperties.Items.Clear();
                    foreach (ShipProperty prop in s.Properties)
                    {
                        lbShipProperties.Items.Add(prop);
                    }
                }
                finally
                {
                    lbShipProperties.EndUpdate();
                }

                foreach (Control c in scShipSelect.Panel2.Controls)
                {
                    c.Visible = true;
                }
            }
            else
            {
                foreach (Control c in scShipSelect.Panel2.Controls)
                {
                    c.Visible = false;
                }
            }
        }

        private bool SetShipSkillLabel(int rnum, Label skillLabel, List<ShipRequiredSkill> list)
        {
            if (list.Count > rnum)
            {
                GrandSkill gs = m_grandCharacterInfo.GetSkill(list[rnum].Name);
                string knownText = String.Empty;
                if (gs.Level >= list[rnum].Level)
                    knownText = " (Known)";
                skillLabel.Text = list[rnum].Name + " " +
                    GrandSkill.GetRomanSkillNumber(list[rnum].Level) + knownText;
                return (knownText.Length > 0);
            }
            else
            {
                skillLabel.Text = String.Empty;
                return true;
            }
        }

        private void GotShipImage(int shipId, Image i)
        {
            if (i == null)
                return;
            if (shipSelectControl1.SelectedShip == null)
                return;
            if (shipId != shipSelectControl1.SelectedShip.Id)
                return;
            pbShipImage.Image = i;
        }

        private void btnShipSkillsAdd_Click(object sender, EventArgs e)
        {
            Ship s = shipSelectControl1.SelectedShip;
            if (s == null)
                return;

            List<Pair<string, int>> skillsToAdd = new List<Pair<string, int>>();
            foreach (ShipRequiredSkill srs in s.RequiredSkills)
            {
                skillsToAdd.Add(new Pair<string, int>(srs.Name, srs.Level));
            }
            AddPlanConfirmWindow.AddSkillsWithConfirm(m_plan, skillsToAdd);

            //List<PlanEntry> planEntries = new List<PlanEntry>();
            //foreach (ShipRequiredSkill srs in s.RequiredSkills)
            //{
            //    GrandSkill gs = m_grandCharacterInfo.GetSkill(srs.Name);
            //    if (ShouldAdd(gs, srs.Level, planEntries))
            //    {
            //        AddPrerequisiteEntries(gs, planEntries);
            //        for (int i = 1; i <= srs.Level; i++)
            //        {
            //            if (ShouldAdd(gs, i, planEntries))
            //            {
            //                PlanEntry pe = new PlanEntry();
            //                pe.SkillName = gs.Name;
            //                pe.Level = i;
            //                pe.EntryType = (i == srs.Level) ? PlanEntryType.Planned : PlanEntryType.Prerequisite;
            //                planEntries.Add(pe);
            //            }
            //        }
            //    }
            //}
            //if (planEntries.Count > 0)
            //{
            //    ConfirmSkillAdd(planEntries);
            //}
            //else
            //{
            //    MessageBox.Show("All the required skills are already in your plan.",
            //        "Already Planned", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void lblShipDescription_Click(object sender, EventArgs e)
        {

        }

        private void scShipSelect_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlShipDescription_ClientSizeChanged(object sender, EventArgs e)
        {
            int w = pnlShipDescription.ClientSize.Width;
            lblShipDescription.MaximumSize = new Size(w, Int32.MaxValue);
            if (lblShipDescription.PreferredHeight > pnlShipDescription.ClientSize.Height)
            {
                pnlShipDescription.Visible = false;
                pnlShipDescription.PerformLayout();
                int xw = pnlShipDescription.ClientSize.Width;
                lblShipDescription.MaximumSize = new Size(xw, Int32.MaxValue);
                pnlShipDescription.Visible = true;
            }
        }
    }

    public class PlannerWindowFactory : IPlannerWindowFactory
    {
        #region IPlannerWindowFactory Members

        public Form CreateWindow(Settings s, GrandCharacterInfo gci, Plan p)
        {
            return new NewPlannerWindow(s, gci, p);
        }

        #endregion
    }
}
