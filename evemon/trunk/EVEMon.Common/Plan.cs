using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    [XmlRoot("plan")]
    public class Plan
    {
        public Plan()
        {
            //m_entries = new MonitoredList<PlanEntry>();
            m_entries.Changed += new EventHandler<ChangedEventArgs<PlanEntry>>(m_entries_Changed);
            m_entries.Cleared += new EventHandler<ClearedEventArgs<PlanEntry>>(m_entries_Cleared);
        }

        void m_entries_Cleared(object sender, ClearedEventArgs<PlanEntry> e)
        {
            foreach (PlanEntry pe in e.Items)
            {
                pe.Plan = null;
            }
        }

        void m_entries_Changed(object sender, ChangedEventArgs<PlanEntry> e)
        {
            m_uniqueSkillCount = -1;
            m_attributeSuggestion = null;
            switch (e.ChangeType)
            {
                case ChangeType.Added:
                    e.Item.Plan = this;
                    break;
                case ChangeType.Removed:
                    e.Item.Plan = null;
                    break;
            }
            OnChange();
        }

        private ColumnPreference m_columnPreference = new ColumnPreference();

        public ColumnPreference ColumnPreference
        {
            get { return m_columnPreference; }
            set { m_columnPreference = value; }
        }

        public event EventHandler<EventArgs> Changed;

        private delegate void FireEventInvoker();

        private object m_eventLock = new object();
        private int m_suppression = 0;
        private Queue<FireEventInvoker> m_firedEvents = new Queue<FireEventInvoker>();
        private Dictionary<string, bool> m_eventsInQueue = new Dictionary<string, bool>();

        public void SuppressEvents()
        {
            lock (m_eventLock)
            {
                m_suppression++;
            }
        }

        public void ResumeEvents()
        {
            lock (m_eventLock)
            {
                m_suppression--;
                if (m_suppression <= 0)
                {
                    m_suppression = 0;
                    while (m_firedEvents.Count > 0)
                    {
                        FireEventInvoker fei = m_firedEvents.Dequeue();
                        fei();
                    }
                    m_eventsInQueue.Clear();
                }
            }
        }

        private void FireEvent(FireEventInvoker fei, string key)
        {
            lock (m_eventLock)
            {
                if (m_suppression > 0)
                {
                    if (String.IsNullOrEmpty(key) || !m_eventsInQueue.ContainsKey(key))
                    {
                        m_firedEvents.Enqueue(fei);
                        if (!String.IsNullOrEmpty(key))
                            m_eventsInQueue.Add(key, true);
                    }
                }
                else
                    fei();
            }
        }

        private void OnChange()
        {
            FireEvent(delegate
            {
                if (Changed != null)
                    Changed(this, new EventArgs());
            }, "change");
        }

        private MonitoredList<PlanEntry> m_entries = new MonitoredList<PlanEntry>();

        [XmlArrayItem("entry")]
        public MonitoredList<PlanEntry> Entries
        {
            get { return m_entries; }
        }

        private bool? m_attributeSuggestion = null;

        [XmlIgnore]
        public bool HasAttributeSuggestion
        {
            get
            {
                if (m_attributeSuggestion == null)
                    CheckForAttributeSuggestion();
                return m_attributeSuggestion.Value;
            }
        }

        public IEnumerable<PlanEntry> GetSuggestions()
        {
            List<PlanEntry> result = new List<PlanEntry>();

            TimeSpan baseTime = GetTotalTime(null);
            CheckForTimeBenefit("Instant Recall", "Eidetic Memory", baseTime, result);
            CheckForTimeBenefit("Analytical Mind", "Logic", baseTime, result);
            CheckForTimeBenefit("Spatial Awareness", "Clarity", baseTime, result);
            CheckForTimeBenefit("Iron Will", "Focus", baseTime, result);
            CheckForTimeBenefit("Empathy", "Presence", baseTime, result);

            return result;
        }

        private void CheckForAttributeSuggestion()
        {
            if (m_grandCharacterInfo == null)
            {
                m_attributeSuggestion = false;
                return;
            }

            TimeSpan baseTime = GetTotalTime(null);

            m_attributeSuggestion = CheckForTimeBenefit("Analytical Mind", "Logic", baseTime);
            if (m_attributeSuggestion==true)
                return;
            m_attributeSuggestion = CheckForTimeBenefit("Spatial Awareness", "Clarity", baseTime);
            if (m_attributeSuggestion == true)
                return;
            m_attributeSuggestion = CheckForTimeBenefit("Iron Will", "Focus", baseTime);
            if (m_attributeSuggestion == true)
                return;
            m_attributeSuggestion = CheckForTimeBenefit("Instant Recall", "Eidetic Memory", baseTime);
            if (m_attributeSuggestion == true)
                return;
            m_attributeSuggestion = CheckForTimeBenefit("Empathy", "Presence", baseTime);
        }

        private bool CheckForTimeBenefit(string skillA, string skillB, TimeSpan baseTime)
        {
            return CheckForTimeBenefit(skillA, skillB, baseTime, null);
        }

        private bool CheckForTimeBenefit(string skillA, string skillB, TimeSpan baseTime, List<PlanEntry> entries)
        {
            GrandSkill gsa = m_grandCharacterInfo.SkillGroups["Learning"][skillA];
            GrandSkill gsb = m_grandCharacterInfo.SkillGroups["Learning"][skillB];

            TimeSpan bestTime = baseTime;
            TimeSpan addedTrainingTime = TimeSpan.Zero;
            GrandSkill bestGs = null;
            int bestLevel = -1;
            int added = 0;
            for (int i = 0; i < 10; i++)
            {
                int level;
                GrandSkill gs;
                if (i < 5)
                {
                    gs = gsa;
                    level = i + 1;
                }
                else
                {
                    gs = gsb;
                    level = i - 4;
                }

                if (gs.Level < level && !this.IsPlanned(gs, level))
                {
                    EveAttributeScratchpad scratchpad = new EveAttributeScratchpad();
                    scratchpad.AdjustAttributeBonus(gs.AttributeModified, added++);
                    addedTrainingTime += gs.GetTrainingTimeOfLevelOnly(level, true, scratchpad);
                    scratchpad.AdjustAttributeBonus(gs.AttributeModified, 1);

                    TimeSpan thisTime = GetTotalTime(scratchpad) + addedTrainingTime;
                    if (thisTime < bestTime)
                    {
                        bestTime = thisTime;
                        bestGs = gs;
                        bestLevel = level;
                    }
                }
            }

            if (entries != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    int level;
                    GrandSkill gs;
                    if (i < 5)
                    {
                        gs = gsa;
                        level = i + 1;
                    }
                    else
                    {
                        gs = gsb;
                        level = i - 4;
                    }
                    if (gs.Level < level && !this.IsPlanned(gs, level))
                    {
                        if ((level <= bestLevel && gs == bestGs) || (bestGs == gsb && gs == gsa))
                        {
                            PlanEntry pe = new PlanEntry();
                            pe.SkillName = gs.Name;
                            pe.Level = level;
                            pe.EntryType = PlanEntryType.Planned;
                            entries.Add(pe);
                        }
                    }
                }
            }

            return (bestGs != null);
        }

        [XmlIgnore]
        public TimeSpan TotalTrainingTime
        {
            get { return GetTotalTime(null); }
        }

        public TimeSpan GetTotalTime(EveAttributeScratchpad scratchpad)
        {
            TimeSpan ts = TimeSpan.Zero;
            if (scratchpad==null)
                scratchpad = new EveAttributeScratchpad();
            foreach (PlanEntry pe in m_entries)
            {
                ts += pe.Skill.GetTrainingTimeOfLevelOnly(pe.Level, true, scratchpad);
                scratchpad.ApplyALevelOf(pe.Skill);
            }
            return ts;
        }

        private int m_uniqueSkillCount = -1;

        [XmlIgnore]
        public int UniqueSkillCount
        {
            get
            {
                if (m_uniqueSkillCount == -1)
                    CountUniqueSkills();
                return m_uniqueSkillCount;
            }
        }

        private void CountUniqueSkills()
        {
            Dictionary<string, bool> counted = new Dictionary<string, bool>();
            m_uniqueSkillCount = 0;
            foreach (PlanEntry pe in m_entries)
            {
                if (!counted.ContainsKey(pe.SkillName))
                {
                    m_uniqueSkillCount++;
                    counted[pe.SkillName] = true;
                }
            }
        }

        private GrandCharacterInfo m_grandCharacterInfo = null;

        [XmlIgnore]
        public GrandCharacterInfo GrandCharacterInfo
        {
            get { return m_grandCharacterInfo; }
            set {
                this.SuppressEvents();
                try
                {
                    m_attributeSuggestion = null;
                    if (m_grandCharacterInfo != null)
                        m_grandCharacterInfo.SkillChanged -= new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);
                    m_grandCharacterInfo = value;
                    if (m_grandCharacterInfo != null)
                        m_grandCharacterInfo.SkillChanged += new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);
                    CheckForCompletedSkills();
                    CheckForMissingPrerequisites();
                }
                finally
                {
                    this.ResumeEvents();
                }
            }
        }

        public void CheckForMissingPrerequisites()
        {
            this.SuppressEvents();
            try
            {
                for (int i = 0; i < m_entries.Count; i++)
                {
                    bool jumpBack = false;
                    PlanEntry pe = m_entries[i];
                    GrandSkill gs = pe.Skill;
                    foreach (GrandSkill.Prereq pp in gs.Prereqs)
                    {
                        GrandSkill pgs = pp.Skill;
                        int prIndex = GetIndexOf(pgs.Name, pp.RequiredLevel);
                        if (prIndex == -1 && pgs.Level < pp.RequiredLevel)
                        {
                            // Not in the plan, and needs to be trained...
                            PlanEntry npe = new PlanEntry();
                            npe.SkillName = pgs.Name;
                            npe.Level = pp.RequiredLevel;
                            npe.EntryType = PlanEntryType.Prerequisite;
                            m_entries.Insert(i, npe);
                            jumpBack = true;
                            i -= 1;
                            break;
                        }
                        else if (prIndex > i)
                        {
                            // In the plan, but in invalid order...
                            PlanEntry mve = m_entries[prIndex];
                            m_entries.RemoveAt(prIndex);
                            m_entries.Insert(i, mve);
                            jumpBack = true;
                            i -= 1;
                            break;
                        }
                    }
                    if (!jumpBack)
                    {
                        if (pe.Level-1 > gs.Level)
                        {
                            // The previous level is needed in the plan...
                            int prIndex = GetIndexOf(gs.Name, pe.Level - 1);
                            if (prIndex == -1)
                            {
                                // ...it's not in the plan.
                                PlanEntry ppe = new PlanEntry();
                                ppe.SkillName = gs.Name;
                                ppe.Level = pe.Level - 1;
                                m_entries.Insert(i, ppe);
                                jumpBack = true;
                                i -= 1;
                            }
                            else if (prIndex > i)
                            {
                                // ..it's in the plan, but it's after this.
                                PlanEntry mve = m_entries[prIndex];
                                m_entries.RemoveAt(prIndex);
                                m_entries.Insert(i, mve);
                                jumpBack = true;
                                i -= 1;
                            }
                        }
                    }
                }
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        private int GetIndexOf(string skillName, int level)
        {
            for (int i = 0; i < m_entries.Count; i++)
            {
                PlanEntry pe = m_entries[i];
                if (pe.SkillName == skillName && pe.Level == level)
                    return i;
            }
            return -1;
        }

        private string m_planName;

        [XmlIgnore]
        public string Name
        {
            get { return m_planName; }
            set { m_planName = value; }
        }

        private void CheckForCompletedSkills()
        {
            this.SuppressEvents();
            try
            {
                for (int i = 0; i < m_entries.Count; i++)
                {
                    PlanEntry pe = m_entries[i];
                    GrandSkill gs = m_grandCharacterInfo.GetSkill(pe.SkillName);
                    if (gs == null || pe.Level>5 || pe.Level<1)
                        throw new ApplicationException("The plan contains invalid skills.");
                    if (gs.Level >= pe.Level)
                    {
                        m_entries.RemoveAt(i);
                        i--;
                    }
                }
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        private void m_grandCharacterInfo_SkillChanged(object sender, SkillChangedEventArgs e)
        {
            CheckForCompletedSkills();
        }

        public bool IsPlanned(GrandSkill gs)
        {
            foreach (PlanEntry pe in m_entries)
            {
                if (pe.Skill == gs)
                    return true;
            }
            return false;
        }

        public bool IsPlanned(GrandSkill gs, int level)
        {
            foreach (PlanEntry pe in m_entries)
            {
                if (pe.SkillName == gs.Name && pe.Level == level)
                    return true;
            }
            return false;
        }

        public void AddList(List<PlanEntry> planEntries)
        {
            this.SuppressEvents();
            try
            {
                foreach (PlanEntry pe in planEntries)
                {
                    m_entries.Add(pe);
                }
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public bool RemoveEntry(PlanEntry pe)
        {
            this.SuppressEvents();
            try
            {
                foreach (PlanEntry tpe in m_entries)
                {
                    GrandSkill tSkill = tpe.Skill;
                    int thisMinNeeded;
                    if (pe.Skill == tpe.Skill && tpe.Level > pe.Level)
                    {
                        return false;
                    }
                    if (tSkill.HasAsPrerequisite(pe.Skill, out thisMinNeeded))
                    {
                        if (thisMinNeeded >= pe.Level)
                            return false;
                    }
                }
                m_entries.Remove(pe);
                return true;
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public bool RemoveEntry(GrandSkill gs, bool removePrerequisites, bool nonPlannedOnly)
        {
            this.SuppressEvents();
            try
            {
                int minNeeded = 0;
                // Verify nothing else in the plan requires this...
                foreach (PlanEntry pe in m_entries)
                {
                    GrandSkill tSkill = pe.Skill;
                    int thisMinNeeded;
                    if (tSkill.HasAsPrerequisite(gs, out thisMinNeeded))
                    {
                        if (thisMinNeeded == 5)  // All are needed, fail now
                            return false;
                        if (thisMinNeeded > minNeeded)
                            minNeeded = thisMinNeeded;
                    }
                }
                // Remove this skill...
                bool anyRemoved = false;
                for (int i = 0; i < m_entries.Count; i++)
                {
                    PlanEntry tpe = m_entries[i];
                    bool canRemove = (nonPlannedOnly && tpe.EntryType == PlanEntryType.Prerequisite) || !nonPlannedOnly;
                    if (tpe.Skill == gs && tpe.Level > minNeeded && canRemove)
                    {
                        anyRemoved = true;
                        m_entries.RemoveAt(i);
                        i--;
                    }
                }
                if (!anyRemoved)
                    return false;
                if (!removePrerequisites)
                    return true;

                // Remove prerequisites
                foreach (GrandSkill.Prereq pp in gs.Prereqs)
                {
                    RemoveEntry(pp.Skill, true, true);
                }
                return true;
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public PlanEntry GetEntry(string name, int level)
        {
            foreach (PlanEntry pe in m_entries)
            {
                if (pe.SkillName == name && pe.Level == level)
                    return pe;
            }
            return null;
        }

        public PlanEntry GetEntryWithRoman(string name)
        {
            int level = 0;
            for (int i = 1; i <= 5; i++)
            {
                string tRomanSuffix = " " + GrandSkill.GetRomanSkillNumber(i);
                if (name.EndsWith(tRomanSuffix))
                {
                    level = i;
                    name = name.Substring(0, name.Length - tRomanSuffix.Length);
                    break;
                }
            }
            return GetEntry(name, level);
        }

        private static IPlannerWindowFactory m_plannerWindowFactory;

        [XmlIgnore]
        public static IPlannerWindowFactory PlannerWindowFactory
        {
            get { return m_plannerWindowFactory; }
            set { m_plannerWindowFactory = value; }
        }

        private WeakReference<Form> m_plannerWindow;

        public void ShowEditor(Settings s, GrandCharacterInfo gci)
        {
            if (m_plannerWindow != null)
            {
                Form npw = m_plannerWindow.Target;
                if (npw != null)
                {
                    try
                    {
                        if (npw.Visible)
                        {
                            npw.BringToFront();
                            return;
                        }
                        else
                        {
                            npw.Show();
                            return;
                        }
                    }
                    catch (ObjectDisposedException e)
                    {
                        ExceptionHandler.LogException(e, false);
                    }
                }
                m_plannerWindow = null;
            }

            Form newWin = m_plannerWindowFactory.CreateWindow(s, gci, this);
            //NewPlannerWindow newWin = new NewPlannerWindow(s, gci, this);
            newWin.Show();
            m_plannerWindow = new WeakReference<Form>(newWin);
        }

        public void CloseEditor()
        {
            if (m_plannerWindow != null)
            {
                Form npw = m_plannerWindow.Target;
                if (npw != null)
                {
                    try
                    {
                        npw.Close();
                    }
                    catch (ObjectDisposedException e)
                    {
                        ExceptionHandler.LogException(e, false);
                    }
                }
                m_plannerWindow = null;
            }
        }

        internal void ClearEntries()
        {
            this.SuppressEvents();
            try
            {
                while (m_entries.Count > 0)
                    m_entries.RemoveAt(m_entries.Count - 1);
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public bool VerifySkills()
        {
            foreach (PlanEntry pe in m_entries)
            {
                GrandSkill gs = pe.Skill;
                if (gs == null)
                    return false;
            }
            return true;
        }

        public void SaveAsText(System.IO.StreamWriter sw, PlanTextOptions pto, bool includeForumMarkup)
        {
            SaveAsText(sw, pto, includeForumMarkup ? MarkupType.Forum : MarkupType.None);
        }

        public void SaveAsText(System.IO.StreamWriter sw, PlanTextOptions pto, MarkupType markupType)
        {
            MethodInvoker writeLine = delegate
            {
                if (markupType == MarkupType.Html)
                    sw.WriteLine("<br>");
                else
                    sw.WriteLine();
            };
            MethodInvoker boldStart = delegate
            {
                switch (markupType)
                {
                    case MarkupType.None:
                        break;
                    case MarkupType.Forum:
                        sw.Write("[b]");
                        break;
                    case MarkupType.Html:
                        sw.Write("<b>");
                        break;
                }
            };
            MethodInvoker boldEnd = delegate
            {
                switch (markupType)
                {
                    case MarkupType.None:
                        break;
                    case MarkupType.Forum:
                        sw.Write("[/b]");
                        break;
                    case MarkupType.Html:
                        sw.Write("</b>");
                        break;
                }
            };

            if (pto.IncludeHeader)
            {
                boldStart();
                sw.Write("Skill Plan for {0}", this.GrandCharacterInfo.Name);
                boldEnd();
                writeLine();
                writeLine();
            }

            EveAttributeScratchpad scratchpad = new EveAttributeScratchpad();
            TimeSpan totalTrainingTime = TimeSpan.Zero;
            DateTime curDt = DateTime.Now;
            int num = 0;
            foreach (PlanEntry pe in this.Entries)
            {
                num++;
                if (pto.EntryNumber)
                {
                    sw.Write("{0}: ", num);
                }
                boldStart();
                sw.Write(pe.SkillName);
                sw.Write(' ');
                sw.Write(GrandSkill.GetRomanSkillNumber(pe.Level));
                boldEnd();

                TimeSpan trainingTime = pe.Skill.GetTrainingTimeOfLevelOnly(pe.Level, true, scratchpad);
                scratchpad.ApplyALevelOf(pe.Skill);
                DateTime startDate = curDt;
                curDt += trainingTime;
                DateTime finishDate = curDt;
                totalTrainingTime += trainingTime;

                if (pto.EntryTrainingTimes || pto.EntryStartDate || pto.EntryFinishDate)
                {
                    sw.Write(" (");
                    bool needComma = false;
                    if (pto.EntryTrainingTimes)
                    {
                        sw.Write(GrandSkill.TimeSpanToDescriptiveText(trainingTime,
                            DescriptiveTextOptions.FullText|DescriptiveTextOptions.IncludeCommas|DescriptiveTextOptions.SpaceText));
                        needComma = true;
                    }
                    if (pto.EntryStartDate)
                    {
                        if (needComma)
                            sw.Write("; ");
                        sw.Write("Start: ");
                        sw.Write(startDate.ToString());
                        needComma = true;
                    }
                    if (pto.EntryFinishDate)
                    {
                        if (needComma)
                            sw.Write("; ");
                        sw.Write("Finish: ");
                        sw.Write(finishDate.ToString());
                        needComma = true;
                    }
                    sw.Write(')');
                }
                writeLine();
            }
            if (pto.FooterCount || pto.FooterTotalTime || pto.FooterDate)
            {
                writeLine();
                bool needComma = false;
                if (pto.FooterCount)
                {
                    boldStart();
                    sw.Write(num.ToString());
                    boldEnd();
                    sw.Write(" skill");
                    if (num != 1)
                        sw.Write('s');
                    needComma = true;
                }
                if (pto.FooterTotalTime)
                {
                    if (needComma)
                        sw.Write("; ");
                    sw.Write("Total time: ");
                    boldStart();
                    sw.Write(GrandSkill.TimeSpanToDescriptiveText(totalTrainingTime,
                            DescriptiveTextOptions.FullText | DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.SpaceText));
                    boldEnd();
                    needComma = true;
                }
                if (pto.FooterDate)
                {
                    if (needComma)
                        sw.Write("; ");
                    sw.Write("Completion: ");
                    boldStart();
                    sw.Write(curDt.ToString());
                    boldEnd();
                    needComma = true;
                }
                writeLine();
            }
        }
    }

    public enum MarkupType
    {
        None,
        Forum,
        Html
    }

    public enum PlanEntryType
    {
        Planned,
        Prerequisite
    }

    [XmlRoot("planentry")]
    public class PlanEntry: ICloneable
    {
        private Plan m_owner;
        private string m_skillName;
        private int m_level;
        private PlanEntryType m_entryType;
        private string m_notes;

        [XmlIgnore]
        public Plan Plan
        {
            get { return m_owner; }
            set {
                if (m_owner != value)
                {
                    if (m_owner!=null)
                        m_owner.Changed -= new EventHandler<EventArgs>(m_owner_Changed);
                    m_owner = value;
                    if (m_owner != null)
                        m_owner.Changed += new EventHandler<EventArgs>(m_owner_Changed);
                }
            }
        }

        private IEnumerable<PlanEntryPrerequisite> m_prerequisiteCache = null;

        void m_owner_Changed(object sender, EventArgs e)
        {
            m_prerequisiteCache = null;
        }

        public string SkillName
        {
            get { return m_skillName; }
            set { m_skillName = value; }
        }

        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }

        public PlanEntryType EntryType
        {
            get { return m_entryType; }
            set { m_entryType = value; }
        }

        public string Notes
        {
            get { return m_notes; }
            set { m_notes = value; }
        }

        [XmlIgnore]
        public GrandSkill Skill
        {
            get
            {
                if (m_owner == null || m_owner.GrandCharacterInfo == null)
                    return null;
                return m_owner.GrandCharacterInfo.GetSkill(m_skillName);
            }
        }

        [XmlIgnore]
        public IEnumerable<PlanEntryPrerequisite> Prerequisites
        {
            get
            {
                if (m_prerequisiteCache == null)
                {
                    Dictionary<string, bool> contains = new Dictionary<string, bool>();
                    List<PlanEntryPrerequisite> result = new List<PlanEntryPrerequisite>();
                    BuildPrereqs(this.Skill, this.Level, result, contains);
                    m_prerequisiteCache = result;
                }
                return m_prerequisiteCache;
            }
        }

        private void BuildPrereqs(GrandSkill gs, int level, List<PlanEntryPrerequisite> result, Dictionary<string, bool> contains)
        {
            for (int l = level; l >= 1; l--)
            {
                string tSkill = gs.Name + " " + GrandSkill.GetRomanSkillNumber(l);
                if (!contains.ContainsKey(tSkill))
                {
                    PlanEntryPrerequisite pep = new PlanEntryPrerequisite(gs, l,
                        m_owner.GetEntry(gs.Name, l));
                    contains[tSkill] = true;
                    result.Insert(0, pep);
                }
            }
            foreach (GrandSkill.Prereq pp in gs.Prereqs)
            {
                string tSkill = pp.Skill + " " + GrandSkill.GetRomanSkillNumber(pp.RequiredLevel);
                if (!contains.ContainsKey(tSkill))
                {
                    PlanEntryPrerequisite pep = new PlanEntryPrerequisite(pp.Skill, pp.RequiredLevel,
                        m_owner.GetEntry(pp.Skill.Name, pp.RequiredLevel));
                    contains[tSkill] = true;
                    result.Insert(0, pep);
                    BuildPrereqs(pp.Skill, pp.RequiredLevel, result, contains);    
                }
            }
        }

        #region ICloneable Members

        public object Clone()
        {
            PlanEntry pe = new PlanEntry();
            pe.SkillName = this.SkillName;
            pe.Level = this.Level;
            pe.EntryType = this.EntryType;
            pe.Notes = this.Notes;
            return pe;
        }

        #endregion
    }

    public interface IPlannerWindowFactory
    {
        Form CreateWindow(Settings s, GrandCharacterInfo gci, Plan p);
    }

    public class PlanEntryPrerequisite
    {
        private GrandSkill m_skill;
        private int m_level;
        private PlanEntry m_entry;

        public GrandSkill Skill
        {
            get { return m_skill; }
        }

        public int Level
        {
            get { return m_level; }
        }

        public PlanEntry PlanEntry
        {
            get { return m_entry; }
        }

        public bool IsKnown
        {
            get { return m_skill.Level >= m_level; }
        }

        public bool IsPlanned
        {
            get { return m_entry != null; }
        }

        internal PlanEntryPrerequisite(GrandSkill skill, int level, PlanEntry entry)
        {
            m_skill = skill;
            m_level = level;
            m_entry = entry;
        }
    }
}
