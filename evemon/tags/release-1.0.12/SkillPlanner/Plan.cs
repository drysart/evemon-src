using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;
using EveCharacterMonitor;

namespace EveCharacterMonitor.SkillPlanner
{
    [XmlRoot("plan")]
    public class Plan
    {
        public Plan()
        {
            //m_entries = new MonitoredList<PlanEntry>();
            m_entries.Changed += new EventHandler<ChangedEventArgs<PlanEntry>>(m_entries_Changed);
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
            }
            OnChange();
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
                if (pe.Skill.Name == "Learning")
                    scratchpad.AdjustLearningLevelBonus(1);
                else if (pe.Skill.IsLearningSkill)
                    scratchpad.AdjustAttributeBonus(pe.Skill.AttributeModified, 1);
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
                m_attributeSuggestion = null;
                if (m_grandCharacterInfo!=null)
                    m_grandCharacterInfo.SkillChanged -= new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);
                m_grandCharacterInfo = value;
                if (m_grandCharacterInfo != null)
                    m_grandCharacterInfo.SkillChanged += new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);
                CheckForCompletedSkills();
            }
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
            foreach (PlanEntry pe in m_entries)
            {
                if (pe.SkillName == name && pe.Level == level)
                    return pe;
            }
            return null;
        }

        private WeakReference<NewPlannerWindow> m_plannerWindow;

        public void ShowEditor(Settings s, GrandCharacterInfo gci)
        {
            if (m_plannerWindow != null)
            {
                NewPlannerWindow npw = m_plannerWindow.Target;
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
                    catch (ObjectDisposedException)
                    {
                    }
                }
                m_plannerWindow = null;
            }

            NewPlannerWindow newWin = new NewPlannerWindow(s, gci, this);
            newWin.Show();
            m_plannerWindow = new WeakReference<NewPlannerWindow>(newWin);
        }

        public void CloseEditor()
        {
            if (m_plannerWindow != null)
            {
                NewPlannerWindow npw = m_plannerWindow.Target;
                if (npw != null)
                {
                    try
                    {
                        npw.Close();
                    }
                    catch (ObjectDisposedException)
                    {
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
    }

    public enum PlanEntryType
    {
        Planned,
        Prerequisite
    }

    [XmlRoot("planentry")]
    public class PlanEntry
    {
        private Plan m_owner;
        private string m_skillName;
        private int m_level;
        private PlanEntryType m_entryType;

        [XmlIgnore]
        public Plan Plan
        {
            get { return m_owner; }
            set { m_owner = value; }
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

        /*
         *  This needs to support multiple targets.
         * 
        private string m_prerequisiteForName;
        private int m_prerequisiteForLevel;

        public string PrerequisiteForName
        {
            get { return m_prerequisiteForName; }
            set { m_prerequisiteForName = value; }
        }

        public int PrerequisiteForLevel
        {
            get { return m_prerequisiteForLevel; }
            set { m_prerequisiteForLevel = value; }
        }
         */

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
    }
}
