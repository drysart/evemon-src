using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using System.Xml.Serialization;
using EveCharacterMonitor.SkillPlanner;

namespace EveCharacterMonitor
{
    [XmlRoot("logindata2")]
    public class Settings
    {
        public Settings()
        {
        }

        private string m_username;

        public string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        private string m_password;

        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        private string m_character;

        public string Character
        {
            get { return m_character; }
            set { m_character = value; }
        }

        private List<CharLoginInfo> m_characterList = new List<CharLoginInfo>();

        public List<CharLoginInfo> CharacterList
        {
            get { return m_characterList; }
            set { m_characterList = value; }
        }

        private bool m_enableEmailAlert = false;

        public bool EnableEmailAlert
        {
            get { return m_enableEmailAlert; }
            set { m_enableEmailAlert = value; }
        }

        private string m_emailServer;

        public string EmailServer
        {
            get { return m_emailServer; }
            set { m_emailServer = value; }
        }
        
        private bool m_emailServerRequiresSsl = false;

        public bool EmailServerRequiresSsl
        {
            get { return m_emailServerRequiresSsl; }
            set { m_emailServerRequiresSsl = value; }
        }

        private bool m_emailAuthRequired = false;

        public bool EmailAuthRequired
        {
            get { return m_emailAuthRequired; }
            set { m_emailAuthRequired = value; }
        }

        private string m_emailUsername;
        private string m_emailPassword;

        public string EmailAuthUsername
        {
            get { return m_emailUsername; }
            set { m_emailUsername = value; }
        }

        public string EmailAuthPassword
        {
            get { return m_emailPassword; }
            set { m_emailPassword = value; }
        }

        private string m_emailFromAddress;

        public string EmailFromAddress
        {
            get { return m_emailFromAddress; }
            set { m_emailFromAddress = value; }
        }

        private string m_emailToAddress;

        public string EmailToAddress
        {
            get { return m_emailToAddress; }
            set { m_emailToAddress = value; }
        }

        private bool m_minimizeToTray = true;

        public bool MinimizeToTray
        {
            get { return m_minimizeToTray; }
            set { m_minimizeToTray = value; }
        }

        private string m_ignoreUpdateVersion = "0.0.0.0";

        public string IgnoreUpdateVersion
        {
            get { return m_ignoreUpdateVersion; }
            set {
                Version v = new Version("0.0.0.0");
                try
                {
                    v = new Version(value);
                }
                catch { }
                m_ignoreUpdateVersion = v.ToString();
            }
        }

        private bool m_titleToTime = true;

        public bool TitleToTime
        {
            get { return m_titleToTime; }
            set { m_titleToTime = value; }
        }

        private List<Pair<string, Plan>> m_plans = new List<Pair<string, Plan>>();

        public List<Pair<string, Plan>> Plans
        {
            get { return m_plans; }
        }

        private const string PLAN_DEFAULT = "Default Plan";

        public IEnumerable<string> GetPlansForCharacter(string charName)
        {
            foreach (Pair<string, Plan> x in m_plans)
            {
                if (x.A == charName)
                    yield return PLAN_DEFAULT;
                else if (x.A.StartsWith(charName + "::"))
                    yield return x.A.Substring(charName.Length + 2);
            }
        }

        public Plan GetPlanByName(string charName, string planName)
        {
            foreach (Pair<string, Plan> x in m_plans)
            {
                if (planName == PLAN_DEFAULT && x.A == charName)
                {
                    x.B.Name = PLAN_DEFAULT;
                    return x.B;
                }
                else if (x.A == charName + "::" + planName)
                {
                    x.B.Name = planName;
                    return x.B;
                }
            }
            return null;
        }

        public void AddPlanFor(string charName, Plan plan, string planName)
        {
            if (GetPlanByName(charName, planName) != null)
                throw new ApplicationException("That plan already exists.");

            Pair<string, Plan> p = new Pair<string, Plan>();
            if (planName == PLAN_DEFAULT)
                p.A = charName;
            else
                p.A = charName + "::" + planName;
            p.B = plan;
            m_plans.Add(p);

            plan.Name = planName;
            this.Save();
        }

        public void RemovePlanFor(string charName, string planName)
        {
            for (int i = 0; i < m_plans.Count; i++)
            {
                if (planName == PLAN_DEFAULT && m_plans[i].A == charName)
                {
                    Plan p = m_plans[i].B;
                    p.CloseEditor();
                    m_plans.RemoveAt(i);
                    i--;
                }
                else if (m_plans[i].A == charName + "::" + planName)
                {
                    Plan p = m_plans[i].B;
                    p.CloseEditor();
                    m_plans.RemoveAt(i);
                    i--;
                }
            }
            this.Save();
        }

        public void RemoveAllPlansFor(string charName)
        {
            for (int i = 0; i < m_plans.Count; i++)
            {
                if (m_plans[i].A.StartsWith(charName + "::") || m_plans[i].A == charName)
                {
                    Plan p = m_plans[i].B;
                    p.CloseEditor();
                    m_plans.RemoveAt(i);
                    i--;
                }
            }
            this.Save();
        }

        private List<SerializableCharacterInfo> m_cachedCharacterInfo = new List<SerializableCharacterInfo>();

        public List<SerializableCharacterInfo> CachedCharacterInfo
        {
            get { return m_cachedCharacterInfo; }
        }

        public SerializableCharacterInfo GetCharacterInfo(string charName)
        {
            foreach (SerializableCharacterInfo sci in m_cachedCharacterInfo)
            {
                if (sci.Name == charName)
                    return sci;
            }
            return null;
        }

        public void RemoveCharacterCache(string charName)
        {
            for (int i = 0; i < m_cachedCharacterInfo.Count; i++)
            {
                if (m_cachedCharacterInfo[i].Name == charName)
                    m_cachedCharacterInfo.RemoveAt(i);
            }
        }

        public void SetCharacterCache(SerializableCharacterInfo sci)
        {
            RemoveCharacterCache(sci.Name);
            sci.IsCached = true;
            m_cachedCharacterInfo.Add(sci);
        }

        private List<string> m_confirmedTips = new List<string>();

        public List<string> ConfirmedTips
        {
            get { return m_confirmedTips; }
        }

        private PlanTextOptions m_defaultCopyOptions = new PlanTextOptions();
        private PlanTextOptions m_defaultSaveOptions = new PlanTextOptions();

        public PlanTextOptions DefaultCopyOptions
        {
            get { return m_defaultCopyOptions; }
            set { m_defaultCopyOptions = value; }
        }

        public PlanTextOptions DefaultSaveOptions
        {
            get { return m_defaultSaveOptions; }
            set { m_defaultSaveOptions = value; }
        }

        private const string STORE_FILE_NAME = "evecharactermonitor-logindata{0}.xml";

        private static string StoreFileName(string key)
        {
            return String.Format(STORE_FILE_NAME, key);
        }

        public static Settings LoadFromKey(string key)
        {
            try
            {
                if (File.Exists(Settings.SettingsFileName))
                {
                    using (FileStream fs = new FileStream(Settings.SettingsFileName, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Settings));
                        Settings result = (Settings)xs.Deserialize(fs);
                        result.SetKey(key);
                        return result;
                    }
                }
                else
                {
                    return LoadFromKeyFromIsoStorage(key);
                }
            }
            catch
            {
                return LoadFromKeyFromIsoStorage(key);
            }
        }

        private static Settings LoadFromKeyFromIsoStorage(string key)
        {
            try
            {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
                using (IsolatedStorageFileStream s = new IsolatedStorageFileStream(StoreFileName(key), FileMode.Open))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(s);

                    if (xdoc.DocumentElement.Name == "logindata")
                    {
                        Settings result = new Settings();
                        result.SetKey(key);
                        result.Username = ((XmlElement)xdoc.SelectSingleNode("/logindata/username")).GetAttribute("value");
                        result.Password = ((XmlElement)xdoc.SelectSingleNode("/logindata/password")).GetAttribute("value");
                        XmlNode cn = xdoc.SelectSingleNode("/logindata/character");
                        if (cn != null)
                        {
                            result.Character = (cn as XmlElement).GetAttribute("value");
                        }
                        s.Close();
                        store.Close();
                        result.Save();
                        return result;
                    }
                    else if (xdoc.DocumentElement.Name == "logindata2")
                    {
                        s.Seek(0, SeekOrigin.Begin);
                        XmlSerializer xs = new XmlSerializer(typeof(Settings));
                        Settings result = (Settings)xs.Deserialize(s);
                        result.SetKey(key);
                        return result;
                    }
                    else
                    {
                        Settings result = new Settings();
                        result.SetKey(key);
                        return result;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Settings s = new Settings();
                s.SetKey(key);
                return s;
            }
        }

        private string m_key;

        private void SetKey(string key)
        {
            m_key = key;
        }

        private bool m_neverSave = false;

        public void NeverSave()
        {
            m_neverSave = true;
        }

        [XmlIgnore]
        public static string SettingsFileName
        {
            get
            {
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                        + "/EVEMon";
                if (!Directory.Exists(appDataDir))
                    Directory.CreateDirectory(appDataDir);
                string fn = appDataDir + "/settings.xml";
#if DEBUG
                fn = appDataDir + "/settings-debug.xml";
#endif
                return fn;
            }
        }

        public void Save()
        {
            if (!m_neverSave)
            {
                string fn = Settings.SettingsFileName;

                //using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
                //using (IsolatedStorageFileStream s = new IsolatedStorageFileStream(StoreFileName(m_key), FileMode.Create, store))
                using (FileStream s = new FileStream(fn, FileMode.Create, FileAccess.Write))
                {
                    SaveTo(s);
                }
            }
        }

        internal bool AddCharacter(CharLoginInfo cli)
        {
            foreach (CharLoginInfo tx in m_characterList)
            {
                if (cli.CharacterName == tx.CharacterName)
                    return false;
            }
            m_characterList.Add(cli);
            this.Save();
            return true;
        }

        internal static void ResetKey(string p)
        {
            Settings s = new Settings();
            s.SetKey(p);
            s.Save();
        }

        public void SaveTo(Stream s)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Settings));
            xs.Serialize(s, this);
        }
    }

    public class CharLoginInfo
    {
        private string m_username;

        public string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        private string m_password;

        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        private string m_characterName;

        public string CharacterName
        {
            get { return m_characterName; }
            set { m_characterName = value; }
        }

        public bool Validate()
        {
            EveSession s = EveSession.GetSession(m_username, m_password);
            return (s.GetCharacterId(m_characterName) > 0);
        }
    }
}
