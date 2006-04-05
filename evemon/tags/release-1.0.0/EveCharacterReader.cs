using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace EveCharacterMonitor
{
    public class EveCharacterReader: IDisposable
    {
        private string m_username;
        private string m_password;
        private Dictionary<string, int> m_characters = new Dictionary<string, int>();
        private Timer m_updateTimer;
        private Image m_characterImage;

        public EveCharacterReader()
        {
        }

        public void Start()
        {
            if (!ValidateLogin())
            {
#if DEBUG
                throw new ApplicationException("failed to validate login: " + m_username + "/" + m_password);
#else
                throw new ApplicationException("failed to validate login.");
#endif
            }
        }

        public void Stop()
        {
            lock (this)
            {
                if (m_updateTimer != null)
                {
                    m_updateTimer.Dispose();
                    m_updateTimer = null;
                }
                if (m_characterImage != null)
                {
                    m_characterImage.Dispose();
                    this.CharacterImage = null;
                }
            }
        }

        public void Dispose()
        {
            this.Stop();
        }

        public IEnumerable<string> Characters
        {
            get
            {
                foreach (string s in m_characters.Keys)
                    yield return s;
            }
        }

        private string m_activeCharacter;

        public string ActiveCharacter
        {
            get { return m_activeCharacter; }
            set
            {
                if (value != m_activeCharacter)
                {
                    lock (this)
                    {
                        if (String.IsNullOrEmpty(value) || m_characters.ContainsKey(value))
                        {
                            if (m_updateTimer!=null)
                                m_updateTimer.Change(-1, -1);
                            m_activeCharacter = value;
                            if (m_updateTimer==null)
                                m_updateTimer = new Timer(new TimerCallback(TimerTick));
                            m_updateTimer.Change(0, Timeout.Infinite);
                            if (!String.IsNullOrEmpty(m_activeCharacter))
                                BeginGetUrl("http://img.eve.is/serv.asp?s=512&c=" + m_characters[m_activeCharacter],
                                    new GetUrlCallback(EndGetImage));
                        }
                        else
                            throw new ArgumentOutOfRangeException("value");
                    }
                }
            }
        }

        public Image CharacterImage
        {
            get { return m_characterImage; }
            private set {
                m_characterImage = value;
                if (ImageChanged != null)
                    ImageChanged(this, new EventArgs());
            }
        }

        private void EndGetImage(byte[] iData)
        {
            if (m_characterImage != null)
            {
                m_characterImage.Dispose();
                this.CharacterImage = null;
            }
            if (iData != null)
            {
                MemoryStream ms = new MemoryStream(iData);  // Owned by the Image
                try
                {
                    Image i = Image.FromStream(ms, true, true);
                    this.CharacterImage = i;
                }
                catch
                {
                    this.CharacterImage = null;
                    ms.Dispose();
                }
            }
        }

        private SkillInTraining m_skillInTraining;

        public SkillInTraining SkillInTraining
        {
            get { return m_skillInTraining; }
            private set
            {
                if (m_skillInTraining == null && value == null)
                    return;
                m_skillInTraining = value;
                if (SkillInTrainingChanged != null)
                    SkillInTrainingChanged(this, new EventArgs());
            }
        }

        public event EventHandler SkillInTrainingChanged;

        private void TimerTick(object state)
        {
            lock (this)
            {
                if (!String.IsNullOrEmpty(m_activeCharacter))
                {
                    SkillInTraining sit = null;

                    string htmld = GetUrl("http://myeve.eve-online.com/character/skilltree.asp?characterID=" +
                        m_characters[m_activeCharacter].ToString(), null);
                    int cti = htmld.IndexOf("Currently training to: ");
                    if (cti != -1)
                    {
                        sit = new SkillInTraining();
                        string bsubstr = ReverseString(htmld.Substring(cti - 400, 400));
                        string s1 = Regex.Match(bsubstr, @"knaR>i< / (.+?)>""xp11:ezis-tnof").Groups[1].Value;
                        sit.SkillName = ReverseString( s1 );
                        string fsubstr = htmld.Substring(cti, 800);
                        sit.TrainingToLevel = Convert.ToInt32(Regex.Match(fsubstr, @"Currently training to: <\/font><strong>level (\d) </st").Groups[1].Value);
                        string timeLeft = Regex.Match(fsubstr, @"Time left: <\/font><strong>(.+?)<\/strong>").Groups[1].Value;
                        sit.EstimatedCompletion = DateTime.Now + ConvertTimeStringToTimeSpan(timeLeft);
                        sit.CurrentPoints = Convert.ToInt32(Regex.Match(fsubstr, @"SP done: </font><strong>(\d+) of \d+</strong>").Groups[1].Value);
                        sit.NeededPoints = Convert.ToInt32(Regex.Match(fsubstr, @"SP done: </font><strong>\d+ of (\d+)</strong>").Groups[1].Value);
                    }

                    this.SkillInTraining = sit;

                    string data = GetUrl("http://myeve.eve-online.com/character/xml.asp?characterID=" +
                        m_characters[m_activeCharacter].ToString(), null);
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(data);

                    int timeLeftInCache = ProcessCharacterXml(xdoc, sit);

                    //m_updateTimer.Change(timeLeftInCache + 1000, -1);
                    m_updateTimer.Change(5*60*1000, -1);
                }
                else
                {
                    m_updateTimer.Dispose();
                    m_updateTimer = null;
                }
            }
        }

        private TimeSpan ConvertTimeStringToTimeSpan(string timeLeft)
        {
            TimeSpan result = new TimeSpan();
            if (timeLeft.Contains("second"))
                result += TimeSpan.FromSeconds(
                    Convert.ToInt32(Regex.Match(timeLeft, @"(\d+) seconds?").Groups[1].Value));
            if (timeLeft.Contains("minute"))
                result += TimeSpan.FromMinutes(
                    Convert.ToInt32(Regex.Match(timeLeft, @"(\d+) minutes?").Groups[1].Value));
            if (timeLeft.Contains("hour"))
                result += TimeSpan.FromHours(
                    Convert.ToInt32(Regex.Match(timeLeft, @"(\d+) hours?").Groups[1].Value));
            if (timeLeft.Contains("day"))
                result += TimeSpan.FromDays(
                    Convert.ToInt32(Regex.Match(timeLeft, @"(\d+) days?").Groups[1].Value));
            return result;
        }

        private string ReverseString(string p)
        {
            char[] ca = new char[p.Length];
            for (int i = 0; i < p.Length; i++)
                ca[p.Length - i - 1] = p[i];
            return new String(ca);
        }

        private string m_race;

        public string Race
        {
            get { return m_race; }
        }

        private string m_bloodLine;

        public string BloodLine
        {
            get { return m_bloodLine; }
        }

        private string m_gender;

        public string Gender
        {
            get { return m_gender; }
        }

        private string m_corpName;

        public string CorpName
        {
            get { return m_corpName; }
        }

        private decimal m_balance;

        public Decimal Balance
        {
            get { return m_balance; }
        }

        private int m_intelligence;

        public int Intelligence
        {
            get { return m_intelligence; }
        }

        private int m_charisma;

        public int Charisma
        {
            get { return m_charisma; }
        }

        private int m_perception;

        public int Perception
        {
            get { return m_perception; }
        }

        private int m_memory;

        public int Memory
        {
            get { return m_memory; }
        }

        private int m_willpower;

        public int Willpower
        {
            get { return m_willpower; }
        }

        private List<SkillGroup> m_skillGroups = new List<SkillGroup>();

        public List<SkillGroup> SkillGroups
        {
            get { return m_skillGroups; }
            set { m_skillGroups = value; }
        }

        private int ProcessCharacterXml(XmlDocument xdoc, SkillInTraining sit)
        {
            XmlElement charRoot = xdoc.DocumentElement.SelectSingleNode("/charactersheet/characters/character[@name='" + m_activeCharacter + "']") as XmlElement;
            m_race = charRoot.SelectSingleNode("race").InnerText;
            m_bloodLine = charRoot.SelectSingleNode("bloodLine").InnerText;
            m_gender = charRoot.SelectSingleNode("gender").InnerText;
            m_corpName = charRoot.SelectSingleNode("corporationName").InnerText;
            m_balance = Convert.ToDecimal(charRoot.SelectSingleNode("balance").InnerText);

            m_intelligence = Convert.ToInt32(charRoot.SelectSingleNode("attributes/intelligence").InnerText);
            m_charisma = Convert.ToInt32(charRoot.SelectSingleNode("attributes/charisma").InnerText);
            m_perception = Convert.ToInt32(charRoot.SelectSingleNode("attributes/perception").InnerText);
            m_memory = Convert.ToInt32(charRoot.SelectSingleNode("attributes/memory").InnerText);
            m_willpower = Convert.ToInt32(charRoot.SelectSingleNode("attributes/willpower").InnerText);

            m_skillGroups.Clear();
            foreach (XmlElement sgel in charRoot.SelectNodes("skills/skillGroup"))
            {
                SkillGroup sg = new SkillGroup();
                sg.Name = sgel.GetAttribute("groupName");
                sg.Id = Convert.ToInt32(sgel.GetAttribute("groupID"));

                foreach (XmlElement skel in sgel.SelectNodes("skill"))
                {
                    Skill s = new Skill();
                    s.Name = skel.GetAttribute("typeName");
                    s.Id = Convert.ToInt32(skel.GetAttribute("typeID"));
                    s.Rank = Convert.ToInt32(skel.SelectSingleNode("rank").InnerText);
                    s.SkillPoints = Convert.ToInt32(skel.SelectSingleNode("skillpoints").InnerText);
                    s.Level = Convert.ToInt32(skel.SelectSingleNode("level").InnerText);
                    sg.Skills.Add(s);

                    if (s.Name == "Analytical Mind" || s.Name=="Logic")
                        m_intelligence += s.Level;
                    if (s.Name == "Empathy" || s.Name == "Presence")
                        m_charisma += s.Level;
                    if (s.Name == "Instant Recall" || s.Name == "Eidetic Memory")
                        m_memory += s.Level;
                    if (s.Name == "Iron Will" || s.Name == "Focus")
                        m_willpower += s.Level;
                    if (s.Name == "Spatial Awareness" || s.Name == "Clarity")
                        m_perception += s.Level;
                }

                m_skillGroups.Add(sg);
            }

            if (CharacterInfoUpdated != null)
                CharacterInfoUpdated(this, new EventArgs());

            return Convert.ToInt32(charRoot.GetAttribute("timeLeftInCache"));
        }

        public event EventHandler CharacterInfoUpdated;

        private bool OnNeedLogin()
        {
            if (NeedLogin == null)
                return false;

            NeedLoginEventArgs e = new NeedLoginEventArgs();
            NeedLogin(this, e);
            if (String.IsNullOrEmpty(e.Username) || String.IsNullOrEmpty(e.Password))
                return false;
            m_username = e.Username;
            m_password = e.Password;
            return true;
        }

        private WebClient m_webcli;

        private WebClient CreateWebClient()
        {
            if (m_webcli == null)
            {
                m_webcli = new WebClient();
                CredentialCache cc = new CredentialCache();
                m_webcli.Credentials = cc;
            }
            return m_webcli;
        }

        private CookieContainer m_cookies;

        private delegate void GetUrlCallback(byte[] data);

        private IAsyncResult BeginGetUrl(string url, GetUrlCallback cb)
        {
            if (m_cookies == null)
                m_cookies = new CookieContainer();

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1";
            wr.Referer = "http://myeve.eve-online.com/news.asp";
            wr.CookieContainer = m_cookies;
            wr.AllowAutoRedirect = true;
            object[] x = new object[2];
            x[0] = wr;
            x[1] = cb;
            return wr.BeginGetResponse(new AsyncCallback(EndGetUrl), x);
        }

        private void EndGetUrl(IAsyncResult ar)
        {
            object[] x = ar.AsyncState as object[];
            HttpWebRequest wr = x[0] as HttpWebRequest;
            GetUrlCallback cb = x[1] as GetUrlCallback;

            try
            {
                HttpWebResponse resp = (HttpWebResponse)wr.EndGetResponse(ar);
                using (Stream s = resp.GetResponseStream())
                {
                    byte[] res = new byte[resp.ContentLength];
                    int cl = Convert.ToInt32(resp.ContentLength);
                    int toGo = cl;
                    while (toGo > 0)
                    {
                        int bRead = s.Read(res, cl - toGo, toGo);
                        toGo -= bRead;
                    }
                    s.Close();
                    resp.Close();
                    cb(res);
                }
            }
            catch
            {
                cb(null);
            }
        }

        private string GetUrl(string url, string refer)
        {
            if (String.IsNullOrEmpty(refer))
                refer = "http://myeve.eve-online.com/news.asp";

            if (m_cookies == null)
                m_cookies = new CookieContainer();

            AGAIN:
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1";
            wr.Referer = refer;
            wr.CookieContainer = m_cookies;
            wr.AllowAutoRedirect = false;
            HttpWebResponse resp = (HttpWebResponse)wr.GetResponse();
            if (resp.StatusCode == HttpStatusCode.Redirect)
            {
                string loc = resp.GetResponseHeader("Location");
                Uri x = new Uri(url);
                Uri newUri = new Uri(x, loc);
                refer = url;
                url = newUri.ToString();
                resp.Close();
                goto AGAIN;
            }

            string res = String.Empty;
            using (Stream rs = resp.GetResponseStream())
            using (StreamReader sreader = new StreamReader(rs, Encoding.GetEncoding("iso-8859-1")))
            {
                res = sreader.ReadToEnd();
                sreader.Close();
                rs.Close();
                resp.Close();
            }

            if (res.Contains("document.onload=window.location.href='"))
            {
                Match m = Regex.Match(res, @"document\.onload=window\.location\.href='(.*?)';");
                refer = url;
                url = m.Groups[1].Value;
                goto AGAIN;
            }

            return res;
        }

        private bool ValidateLogin()
        {
            RESTART:
            WebClient wc = CreateWebClient();
            if (String.IsNullOrEmpty(m_username) || String.IsNullOrEmpty(m_password))
            {
                if (!OnNeedLogin())
                    return false;
            }

            GetUrl("https://myeve.eve-online.com/login.asp?username=" + m_username + "&password=" + m_password +
                "&login=Login&Check=OK&r=&t=", null);
            string s = GetUrl("http://myeve.eve-online.com/character/skilltree.asp", null);

            Regex re = new Regex(@"<a href=""/character/skilltree.asp\?characterID=(\d+)"".*?<br>([^<>]+?)<\/td><\/table>", RegexOptions.IgnoreCase);
            MatchCollection mcol = re.Matches(s);
            if (mcol.Count == 0)
            {
                if (OnNeedLogin())
                    goto RESTART;
                return false;
            }
            m_characters.Clear();
            foreach (Match m in mcol)
            {
                string charId = m.Groups[1].Value;
                string charName = m.Groups[2].Value;
                m_characters[charName] = Convert.ToInt32(charId);
            }
            return true;
        }

        public event NeedLoginDelegate NeedLogin;
        public event EventHandler ImageChanged;
    }

    public class SkillGroup
    {
        private string m_name;
        private int m_id;
        private List<Skill> m_skills = new List<Skill>();

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public List<Skill> Skills
        {
            get { return m_skills; }
            set { m_skills = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(m_name);
            sb.Append(" - ");
            sb.Append(m_skills.Count);
            sb.Append(" Skill");
            if (m_skills.Count > 1)
                sb.Append("s");
            sb.Append(", ");
            int points = 0;
            foreach (Skill s in m_skills)
            {
                points += s.SkillPoints;
            }
            sb.Append(points.ToString("#,##0"));
            sb.Append(" points");
            return sb.ToString();
        }
    }

    public class Skill
    {
        private string m_name;
        private int m_id;
        private int m_rank;
        private int m_skillPoints;
        private int m_level;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public int Rank
        {
            get { return m_rank; }
            set { m_rank = value; }
        }

        public int SkillPoints
        {
            get { return m_skillPoints; }
            set { m_skillPoints = value; }
        }

        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }

        private static string[] s_levels = new string[6] { "(none)", "I", "II", "III", "IV", "V" };

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t");
            sb.Append(m_name);
            sb.Append(" ");
            sb.Append(s_levels[m_level]);
            sb.Append(" (Rank ");
            sb.Append(m_rank.ToString());
            sb.Append(") ");
            sb.Append(m_skillPoints.ToString("#,##0"));
            sb.Append(" points");
            return sb.ToString();
        }
    }

    public class SkillInTraining
    {
        private string m_skillName;

        public string SkillName
        {
            get { return m_skillName; }
            set { m_skillName = value; }
        }

        private int m_trainingToLevel;

        public int TrainingToLevel
        {
            get { return m_trainingToLevel; }
            set { m_trainingToLevel = value; }
        }

        private DateTime m_estimatedCompletion;

        public DateTime EstimatedCompletion
        {
            get { return m_estimatedCompletion; }
            set { m_estimatedCompletion = value; }
        }

        private int m_currentPoints;

        public int CurrentPoints
        {
            get { return m_currentPoints; }
            set { m_currentPoints = value; }
        }

        private int m_neededPoints;

        public int NeededPoints
        {
            get { return m_neededPoints; }
            set { m_neededPoints = value; }
        }
    }

    public delegate void NeedLoginDelegate(EveCharacterReader sender, NeedLoginEventArgs e);

    public class NeedLoginEventArgs : EventArgs
    {
        private string m_username;
        private string m_password;

        internal NeedLoginEventArgs()
        {
        }

        public string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }
    }
}
