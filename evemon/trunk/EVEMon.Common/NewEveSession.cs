    using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    public class EveSession
    {
        #region Static Members

        private static Dictionary<string, WeakReference<EveSession>> m_sessions = new Dictionary<string, WeakReference<EveSession>>();

        public static EveSession GetSession(string username, string password)
        {
            string junk;
            return GetSession(username, password, out junk);
        }

        public static EveSession GetSession(string username, string password, out string errMessage)
        {
            lock (m_sessions)
            {
                errMessage = String.Empty;

                string hkey = username + ":" + password;
                EveSession result = null;
                if (m_sessions.ContainsKey(hkey))
                {
                    result = m_sessions[hkey].Target;
                }
                if (result == null)
                {
                    try
                    {
                        EveSession s = new EveSession(username, password);
                        m_sessions[hkey] = new WeakReference<EveSession>(s);
                        result = s;
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.LogException(ex, true);
                        errMessage = ex.Message;
                    }
                }
                return result;
            }
        }

        public static void GetCharacterImageAsync(int charId, GetImageCallback callback)
        {
            GetImageAsync("http://img.eve.is/serv.asp?s=512&c=" + charId.ToString(), false, callback);
        }

        public static string ImageCacheDirectory
        {
            get
            {
                string cacheDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                    "/EVEMon/cache/images";
                if (!Directory.Exists(cacheDir))
                    Directory.CreateDirectory(cacheDir);
                return cacheDir;
            }
        }

        public static void GetImageAsync(string url, bool useCache, GetImageCallback callback)
        {
            if (useCache)
            {
                string cacheFileName = ImageCacheDirectory + "/" + GetCacheName(url);
                if (File.Exists(cacheFileName))
                {
                    try
                    {
                        Image i = Image.FromFile(cacheFileName, true);
                        callback(null, i);
                        return;
                    }
                    catch (Exception e)
                    {
                        ExceptionHandler.LogException(e, false);
                    }
                }
                GetImageCallback origCallback = callback;
                callback = new GetImageCallback(delegate(EveSession s, Image i)
                {
                    if (i != null)
                        AddImageToCache(url, i);
                    origCallback(s, i);
                });
            }
            HttpWebRequest wr = EVEMonWebRequest.GetWebRequest(url);
            Pair<HttpWebRequest, GetImageCallback> p =
                new Pair<HttpWebRequest, GetImageCallback>(wr, callback);
            wr.BeginGetResponse(new AsyncCallback(GotImage), p);
        }

        private static void GotImage(IAsyncResult ar)
        {
            Pair<HttpWebRequest, GetImageCallback> p = ar.AsyncState as Pair<HttpWebRequest, GetImageCallback>;
            HttpWebRequest wr = p.A;
            GetImageCallback callback = p.B;
            try
            {
                HttpWebResponse resp = (HttpWebResponse)wr.EndGetResponse(ar);
                int contentLength = Convert.ToInt32(resp.ContentLength);
                int bytesToGo = contentLength;
                byte[] data = new byte[bytesToGo];
                using (Stream s = resp.GetResponseStream())
                {
                    while (bytesToGo > 0)
                    {
                        int bytesRead = s.Read(data, contentLength - bytesToGo, bytesToGo);
                        bytesToGo -= bytesRead;
                    }
                }
                MemoryStream ms = new MemoryStream(data);
                Image i = Image.FromStream(ms, true, true);
                callback(null, i);
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e, true);
                callback(null, null);
            }
        }

        private static void AddImageToCache(string url, Image i)
        {
            string cacheName = GetCacheName(url);
            using (StreamWriter sw = new StreamWriter(ImageCacheDirectory + "/file.map", true))
            {
                sw.WriteLine(String.Format("{0} {1}", cacheName, url));
            }
            string fn = ImageCacheDirectory + "/" + cacheName;
            i.Save(fn);
        }

        private static string GetCacheName(string url)
        {
            Match extensionMatch = Regex.Match(url, @"([^\.]+)$");
            string ext = String.Empty;
            if (extensionMatch.Success)
            {
                ext = "." + extensionMatch.Groups[1];
            }
            byte[] hash = System.Security.Cryptography.MD5.Create().ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(url));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(String.Format("{0:x2}", hash[i]));
            }
            sb.Append(ext);
            return sb.ToString();
        }

        #endregion

        #region Nonstatic Members

        private string m_username;
        private string m_password;

        private EveSession(string username, string password)
        {
            m_username = username;
            m_password = password;

            ReLogin();
            GetCharacterList();
        }

        private List<Pair<string, int>> m_storedCharacterList = null;

        public List<Pair<string, int>> GetCharacterList()
        {
            if (m_storedCharacterList != null)
                return m_storedCharacterList;

            string stxt = GetSessionUrlText("http://myeve.eve-online.com/character/xml.asp");
            if (String.IsNullOrEmpty(stxt))
                throw new ApplicationException("Could not retrieve character list.");

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(stxt);
            List<Pair<string, int>> nl = new List<Pair<string, int>>();
            foreach (XmlElement el in xdoc.SelectNodes("/charactersheet/characters/character"))
            {
                nl.Add(new Pair<string, int>(el.GetAttribute("name"),
                    Convert.ToInt32(el.GetAttribute("characterID"))));
            }
            m_storedCharacterList = nl;
            return m_storedCharacterList;
        }

        private string GetSessionUrlText(string url)
        {
            bool needLogin = false;
            int maxTries = 2;

            while (maxTries > 0)
            {
                maxTries--;
                if (needLogin)
                    ReLogin();
                needLogin = true;

                string myurl = url;
                if (!String.IsNullOrEmpty(m_requestSid))
                {
                    if (myurl.Contains("?"))
                        myurl = myurl + "&sid=" + m_requestSid;
                    else
                        myurl = myurl + "?sid=" + m_requestSid;
                    m_requestSid = null;
                }

                HttpWebResponse resp = null;
                WebRequestState wrs = new WebRequestState();
                wrs.CookieContainer = m_cookies;
                wrs.AllowRedirects = false;
                wrs.LogDelegate = NetworkLogEvent;
                try
                {
                    string txt = EVEMonWebRequest.GetUrlString(myurl, wrs, out resp);
                    if (wrs.RequestError == RequestError.None && resp.StatusCode == HttpStatusCode.OK)
                        return txt;
                }
                catch (Exception e)
                {
                    ExceptionHandler.LogException(e, false);
                }
            }
            return String.Empty;
        }

        public int GetCharacterId(string charName)
        {
            foreach (Pair<string, int> p in m_storedCharacterList)
            {
                if (p.A == charName)
                    return p.B;
            }
            return -1;
        }

        public void GetCharacterInfoAsync(int charId, GetCharacterInfoCallback callback)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                SerializableCharacterInfo sci = GetCharacterInfo(charId);
                callback(null, sci);
            });
        }

        private SerializableCharacterInfo GetCharacterInfo(int charId)
        {
            try
            {
                string stxt = GetSessionUrlText("http://myeve.eve-online.com/character/skilltree.asp?characterID=" + charId.ToString());
                if (String.IsNullOrEmpty(stxt) || !stxt.Contains("skills trained, for a total of"))
                    return null;
                string xtxt = GetSessionUrlText("http://myeve.eve-online.com/character/xml.asp?characterID=" + charId.ToString());
                if (String.IsNullOrEmpty(xtxt))
                    return null;
                XmlDocument xdoc = new XmlDocument();
                try
                {
                    xdoc.LoadXml(xtxt);
                }
                catch (Exception e)
                {
                    ExceptionHandler.LogException(e, true);
                    return null;
                }

                SerializableSkillInTraining sit = ProcessSkilltreeHtml(stxt);
                int timeLeftInCache = DEFAULT_RETRY_INTERVAL;
                SerializableCharacterInfo sci = ProcessCharacterXml(xdoc, charId, out timeLeftInCache);
                sci.TimeLeftInCache = timeLeftInCache;
                sci.SkillInTraining = sit;
                return sci;
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e, true);
                return null;
            }
        }

        private SerializableCharacterInfo ProcessCharacterXml(XmlDocument xdoc, int charId)
        {
            int junk;
            return ProcessCharacterXml(xdoc, charId, out junk);
        }

        private SerializableCharacterInfo ProcessCharacterXml(XmlDocument xdoc, int charId, out int cacheExpires)
        {
            XmlSerializer xs = new XmlSerializer(typeof(SerializableCharacterInfo));
            XmlElement charRoot = xdoc.DocumentElement.SelectSingleNode("/charactersheet/characters/character[@characterID='" + charId.ToString() + "']") as XmlElement;

            cacheExpires = Convert.ToInt32(charRoot.GetAttribute("timeLeftInCache"));

            using (XmlNodeReader xnr = new XmlNodeReader(charRoot))
            {
                return (SerializableCharacterInfo)xs.Deserialize(xnr);
            }
        }

        private SerializableSkillInTraining ProcessSkilltreeHtml(string htmld)
        {
            SerializableSkillInTraining sit = null;
            int cti = htmld.IndexOf("Currently training to: ");
            if (cti != -1)
            {
                sit = new SerializableSkillInTraining();
                string bsubstr = ReverseString(htmld.Substring(cti - 400, 400));
                string s1 = Regex.Match(bsubstr, @"knaR>i< / (.+?)>""xp11:ezis-tnof").Groups[1].Value;
                sit.SkillName = ReverseString(s1);
                string fsubstr = htmld.Substring(cti, 800);
                sit.TrainingToLevel = Convert.ToInt32(Regex.Match(fsubstr, @"Currently training to: <\/font><strong>level (\d) </st").Groups[1].Value);
                string timeLeft = Regex.Match(fsubstr, @"Time left: <\/font><strong>(.+?)<\/strong>").Groups[1].Value;
                sit.EstimatedCompletion = DateTime.Now + ConvertTimeStringToTimeSpan(timeLeft);
                sit.CurrentPoints = Convert.ToInt32(Regex.Match(fsubstr, @"SP done: </font><strong>(\d+) of \d+</strong>").Groups[1].Value);
                sit.NeededPoints = Convert.ToInt32(Regex.Match(fsubstr, @"SP done: </font><strong>\d+ of (\d+)</strong>").Groups[1].Value);
            }
            else
            {
                sit = null;
            }
            return sit;
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

        private static Thread m_mainThread = null;

        public static Thread MainThread
        {
            get { return m_mainThread; }
            set { m_mainThread = value; }
        }

        public static event EventHandler<NetworkLogEventArgs> NetworkLogEvent;

        private string m_requestSid = null;
        private CookieContainer m_cookies = null;

        public void ReLogin()
        {
            if (m_cookies == null)
                m_cookies = new CookieContainer();

            WebRequestState wrs = new WebRequestState();
            wrs.CookieContainer = m_cookies;
            wrs.LogDelegate = NetworkLogEvent;
            wrs.AllowRedirects = false;
            HttpWebResponse resp = null;
            string s = EVEMonWebRequest.GetUrlString(
                "https://myeve.eve-online.com/login.asp?username=" +
                System.Web.HttpUtility.UrlEncode(m_username, Encoding.GetEncoding("iso-8859-1")) +
                "&password=" +
                System.Web.HttpUtility.UrlEncode(m_password, Encoding.GetEncoding("iso-8859-1")) +
                "&login=Login&Check=OK&r=&t=/ingameboard.asp&remember=1", wrs, out resp);
            string loc = resp.Headers[HttpResponseHeader.Location];
            Match sidm = null;
            if (!String.IsNullOrEmpty(loc))
                sidm = Regex.Match(loc, @"sid=(\d+)");
            if (sidm != null && sidm.Success)
            {
                string sid = sidm.Groups[1].Value;
                m_requestSid = sid;

                HttpWebResponse resp1 = null;
                String fs = EVEMonWebRequest.GetUrlString(
                "http://myeve.eve-online.com/login.asp", wrs, out resp1); 

                return;
            }
            else if (s.Contains("Login credentials incorrect"))
            {
                throw new ApplicationException("The username/password supplied is invalid. Please ensure it is entered correctly.");
            }
            else
            {
                throw new ApplicationException("Failed to get login SID");
            }
        }

        private class UpdateGCIArgs
        {
            public GrandCharacterInfo GrandCharacterInfo;
            public Control InvokeControl;
            public UpdateGrandCharacterInfoCallback UpdateGrandCharacterInfoCallback;
        }

        public void UpdateGrandCharacterInfoAsync(GrandCharacterInfo grandCharacterInfo, Control invokeControl, UpdateGrandCharacterInfoCallback callback)
        {
            UpdateGCIArgs xx = new UpdateGCIArgs();
            xx.GrandCharacterInfo = grandCharacterInfo;
            xx.InvokeControl = invokeControl;
            xx.UpdateGrandCharacterInfoCallback = callback;
#if DEBUG_SINGLETHREAD
            UpdateGrandCharacterInfoAsyncCaller(xx);
#else
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateGrandCharacterInfoAsyncCaller), xx);
#endif
        }

        private void UpdateGrandCharacterInfoAsyncCaller(object state)
        {
            UpdateGCIArgs args = (UpdateGCIArgs)state;
            System.GC.Collect();
            int timeLeftInCache = this.UpdateGrandCharacterInfo(args.GrandCharacterInfo, args.InvokeControl);
            args.UpdateGrandCharacterInfoCallback(null, timeLeftInCache);
        }

        private const int DEFAULT_RETRY_INTERVAL = 60 * 5 * 1000;

        public int UpdateGrandCharacterInfo(GrandCharacterInfo grandCharacterInfo, Control invokeControl)
        {
            SerializableCharacterInfo sci = GetCharacterInfo(grandCharacterInfo.CharacterId);

            if (sci == null) return DEFAULT_RETRY_INTERVAL;

            invokeControl.Invoke(new MethodInvoker(delegate
            {
                grandCharacterInfo.AssignFromSerializableCharacterInfo(sci);
            }));

            return sci.TimeLeftInCache;
        }

        #endregion
    }
}
