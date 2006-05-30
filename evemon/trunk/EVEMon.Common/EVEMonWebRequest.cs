using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Net;
using System.IO;
using System.Xml;

namespace EVEMon.Common
{
    public static class EVEMonWebRequest
    {
        public static XmlDocument LoadXml(string url)
        {
            WebRequestState wrs = new WebRequestState();
            wrs.Accept = "text/xml,application/xml,application/xhtml+xml;q=0.8,*/*;q=0.5";
            Stream s = GetUrlStream(url, wrs);
            try
            {
                if (s != null)
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(s);
                    return xdoc;
                }
                else
                {
                    throw new EVEMonNetworkException(wrs.RequestError);
                }
            }
            finally
            {
                if (s != null)
                    s.Dispose();
            }
        }

        public static Stream GetUrlStream(string url)
        {
            HttpWebResponse junk;
            WebRequestState wrs = new WebRequestState();
            Stream result = GetUrlStream(url, wrs, out junk);
            if (result == null)
                throw new EVEMonNetworkException(wrs.RequestError);
            return result;
        }

        public static Stream GetUrlStream(string url, WebRequestState wrs)
        {
            HttpWebResponse junk;
            return GetUrlStream(url, wrs, out junk);
        }

        public static Stream GetUrlStream(string url, WebRequestState wrs, out HttpWebResponse resp)
        {
        AGAIN:
            if (wrs.RedirectsRemain-- <= 0)
            {
                wrs.RedirectsRemain = 0;
                wrs.RequestError = RequestError.MaxRedirectsExceeded;
                resp = null;
                return null;
            }

            HttpWebRequest req = GetWebRequest(url, wrs);
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                wrs.RequestError = RequestError.WebException;
                wrs.WebException = ex;
                resp = null;
                return null;
            }
            if (resp.StatusCode == HttpStatusCode.Redirect && wrs.AllowRedirects)
            {
                string loc = resp.GetResponseHeader("Location");
                Uri x = new Uri(url);
                Uri newUri = new Uri(x, loc);

                wrs.Referer = url;
                url = newUri.ToString();
                resp.Close();
                goto AGAIN;
            }

            return resp.GetResponseStream();
        }

        public static Image GetUrlImage(string url)
        {
            WebRequestState wrs = new WebRequestState();
            wrs.Accept = "image/*;q=0.8,*/*;q=0.5";
            Image result = GetUrlImage(url, wrs);
            if (result == null)
                throw new EVEMonNetworkException(wrs.RequestError);
            return result;
        }

        public static Image GetUrlImage(string url, WebRequestState wrs)
        {
            Stream s = GetUrlStream(url, wrs);
            try
            {
                if (s == null)
                    return null;
                return Image.FromStream(s, true, true);
            }
            catch (WebException ex)
            {
                wrs.RequestError = RequestError.WebException;
                wrs.WebException = ex;
                return null;
            }
            catch
            {
                if (s != null)
                    s.Dispose();
                throw;
            }
        }

        public static string GetUrlString(string url)
        {
            WebRequestState wrs = new WebRequestState();
            string result = GetUrlString(url, wrs);
            if (result == null)
                throw new EVEMonNetworkException(wrs.RequestError);
            return result;
        }

        public static string GetUrlString(string url, WebRequestState wrs)
        {
            HttpWebResponse junk;
            return GetUrlString(url, wrs, out junk);
        }

        public static string GetUrlString(string url, WebRequestState wrs, out HttpWebResponse resp)
        {
            Stream s = GetUrlStream(url, wrs, out resp);
            try
            {
                if (s == null)
                    return null;
                using (StreamReader sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (s != null)
                    s.Dispose();
                wrs.RequestError = RequestError.WebException;
                wrs.WebException = ex;
                resp = null;
                return null;
            }
            catch
            {
                if (s != null)
                    s.Dispose();
                throw;
            }
        }

        public static HttpWebRequest GetWebRequest(string url)
        {
            return GetWebRequest(url, new WebRequestState());
        }

        public static HttpWebRequest GetWebRequest(string url, WebRequestState wrs)
        {
            Settings s = Settings.GetInstance();

            HttpWebRequest wr = WebRequest.Create(url) as HttpWebRequest;
            wr.AllowAutoRedirect = false;
            wr.Referer = wrs.Referer;
            wr.UserAgent = wrs.UserAgent;
            wr.CookieContainer = wrs.CookieContainer;
            wr.Accept = wrs.Accept;
            wr.Headers[HttpRequestHeader.AcceptLanguage] = "en-us,en;q=0.5";
            wr.Headers[HttpRequestHeader.AcceptCharset] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            wr.Headers[HttpRequestHeader.Pragma] = "no-cache";
            wr.KeepAlive = true;
            if (s.UseCustomProxySettings)
            {
                WebProxy prox = new WebProxy(s.HttpProxy.Host, s.HttpProxy.Port);
                switch (s.HttpProxy.AuthType)
                {
                    case ProxyAuthType.None:
                        prox.UseDefaultCredentials = false;
                        prox.Credentials = null;
                        break;
                    case ProxyAuthType.SystemDefault:
                        prox.UseDefaultCredentials = true;
                        break;
                    case ProxyAuthType.Specified:
                        prox.UseDefaultCredentials = false;
                        prox.Credentials = new NetworkCredential(s.HttpProxy.Username, s.HttpProxy.Password);
                        break;
                }
                wr.Proxy = prox;
            }

            return wr;
        }
    }

    public class WebRequestState
    {
        static WebRequestState()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Version currentVersion = new Version("0.0.0.0");
            foreach (Attribute a in Assembly.GetExecutingAssembly().GetCustomAttributes(false))
            {
                if (a is AssemblyFileVersionAttribute)
                {
                    AssemblyFileVersionAttribute ava = a as AssemblyFileVersionAttribute;
                    currentVersion = new Version(ava.Version);
                }
            }
            sm_appVersion = "EVEMon/" + currentVersion.ToString();
            sm_userAgent = USER_AGENT_BASE + " " + sm_appVersion;
        }

        public WebRequestState()
        {
            m_userAgent = sm_userAgent;
        }

        private static string sm_appVersion;
        private static string sm_userAgent;
        private const string USER_AGENT_BASE = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1";

        private string m_userAgent;
        private string m_referer = "http://myeve.eve-online.com/news.asp";

        public string Referer
        {
            get { return m_referer; }
            set { m_referer = value; }
        }

        public string UserAgent
        {
            get { return m_userAgent; }
            set { m_userAgent = value; }
        }

        private CookieContainer m_cookieContainer = null;

        public CookieContainer CookieContainer
        {
            get { return m_cookieContainer; }
            set { m_cookieContainer = value; }
        }

        private string m_accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";

        public string Accept
        {
            get { return m_accept; }
            set { m_accept = value; }
        }
        
        private EventHandler<NetworkLogEventArgs> m_logDelegate;

        public EventHandler<NetworkLogEventArgs> LogDelegate
        {
            get { return m_logDelegate; }
            set { m_logDelegate = value; }
        }

        private bool m_allowRedirects = true;

        public bool AllowRedirects
        {
            get { return m_allowRedirects; }
            set { m_allowRedirects = value; }
        }

        private const int MAX_REDIRECTS = 6;

        private int m_redirectsRemain = MAX_REDIRECTS;

        public int RedirectsRemain
        {
            get { return m_redirectsRemain; }
            set { m_redirectsRemain = value; }
        }

        private RequestError m_requestError;

        public RequestError RequestError
        {
            get { return m_requestError; }
            set { m_requestError = value; }
        }

        private WebException m_webException;

        public WebException WebException
        {
            get { return m_webException; }
            set { m_webException = value; }
        }
    }

    public enum RequestError
    {
        None,
        MaxRedirectsExceeded,
        WebException
    }

    public class EVEMonNetworkException : ApplicationException
    {
        private RequestError m_requestError;

        public RequestError RequestError
        {
            get { return m_requestError; }
            set { m_requestError = value; }
        }

        private WebException m_webException;

        public WebException WebException
        {
            get { return m_webException; }
            set { m_webException = value; }
        }

        public EVEMonNetworkException(RequestError re)
            : base (re.ToString())
        {
            m_requestError = re;
        }

        public EVEMonNetworkException(RequestError re, WebException exc)
            : base (exc.ToString())
        {
            m_requestError = re;
            m_webException = exc;
        }
    }
}
