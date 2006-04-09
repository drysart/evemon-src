using System;
using System.Collections.Generic;
using System.Text;

namespace EveCharacterMonitor.SkillPlanner
{
    public class PlanInfo
    {
        private string m_characterName;

        public string CharacterName
        {
            get { return m_characterName; }
            set { m_characterName = value; }
        }

        private List<PlannedSkill> m_plannedSkills = new List<PlannedSkill>();

        public List<PlannedSkill> PlannedSkills
        {
            get { return m_plannedSkills; }
        }

        public CombinedPlannedSkill GetCombinedSkill(string skillName)
        {
            CombinedPlannedSkill cps = new CombinedPlannedSkill();
            cps.Name = skillName;
            cps.RequiredLevel = 0;
            cps.PlannedLevel = 0;

            foreach (PlannedSkill ps in m_plannedSkills)
            {
                if (ps.Name == skillName && ps.Level > cps.PlannedLevel)
                    cps.PlannedLevel = ps.Level;
                CombinePrereqs(skillName, cps, ps);
            }

            return cps;
        }

        private void CombinePrereqs(string skillName, CombinedPlannedSkill cps, PlannedSkill ps)
        {
            foreach (PlannedSkill tps in ps.Prereqs)
            {
                if (tps.Name == skillName && tps.Level > cps.RequiredLevel)
                    cps.RequiredLevel = tps.Level;
                CombinePrereqs(skillName, cps, tps);
            }
        }

        /// <summary>
        /// Remove skills from the plan that are already on the character sheet
        /// with the requested skill level.
        /// </summary>
        /// <param name="ci">character sheet to check for completed skills on</param>
        public void CleanupPlan(CharacterInfo ci)
        {
            for (int i = 0; i < m_plannedSkills.Count; i++)
            {
                PlannedSkill tps = m_plannedSkills[i];
                if (tps.Level==0 || CheckForNeededRemove(ci, tps))
                {
                    m_plannedSkills.RemoveAt(i);
                    i--;
                }
                else
                {
                    CleanupPrereqs(ci, tps);
                }
            }
        }

        private void CleanupPrereqs(CharacterInfo ci, PlannedSkill tps)
        {
            for (int i = 0; i < tps.Prereqs.Count; i++)
            {
                PlannedSkill sps = tps.Prereqs[i];
                if (sps.Level==0 || CheckForNeededRemove(ci, sps))
                {
                    tps.Prereqs.RemoveAt(i);
                    i--;
                }
                else
                {
                    CleanupPrereqs(ci, sps);
                }
            }
        }

        private static bool CheckForNeededRemove(CharacterInfo ci, PlannedSkill tps)
        {
            Skill s = ci.GetSkill(tps.Name);
            if (s != null && s.Level >= tps.Level)
                return true;
            else
                return false;
        }

        internal void Plan(string skillName, int planToLevel, CharacterInfo ci)
        {
            foreach (PlannedSkill ps in m_plannedSkills)
            {
                if (ps.Name == skillName)
                {
                    ps.Level = planToLevel;
                    CleanupPlan(ci);
                    return;
                }
            }

            if (planToLevel > 0)
            {
                PlannedSkill nps = new PlannedSkill(skillName, planToLevel);
                m_plannedSkills.Add(nps);
                CleanupPlan(ci);
            }
        }
    }

    public class PlannedSkill
    {
        public PlannedSkill()
        {
        }

        public PlannedSkill(string skillName, int planToLevel)
        {
            m_skillName = skillName;
            m_plannedLevel = planToLevel;

            PlannerData pd = PlannerData.GetPlannerData();
            PlannerSkill ps = pd.GetSkill(skillName);
            foreach (PlannerPrereq pr in ps.Prereqs)
            {
                PlannedSkill ss = new PlannedSkill(pr.Name, pr.Level);
                m_prereqs.Add(ss);
            }
        }

        private string m_skillName;
        private int m_plannedLevel;

        public string Name
        {
            get { return m_skillName; }
            set { m_skillName = value; }
        }

        public int Level
        {
            get { return m_plannedLevel; }
            set { m_plannedLevel = value; }
        }

        private List<PlannedSkill> m_prereqs = new List<PlannedSkill>();

        public List<PlannedSkill> Prereqs
        {
            get { return m_prereqs; }
        }
    }

    public class CombinedPlannedSkill
    {
        private string m_skillName;

        public string Name
        {
            get { return m_skillName; }
            set { m_skillName = value; }
        }

        private int m_plannedLevel;

        public int PlannedLevel
        {
            get { return m_plannedLevel; }
            set { m_plannedLevel = value; }
        }

        private int m_requiredLevel;

        public int RequiredLevel
        {
            get { return m_requiredLevel; }
            set { m_requiredLevel = value; }
        }
    }
}
