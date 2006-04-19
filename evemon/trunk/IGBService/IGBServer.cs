using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

using EVEMon;
using EVEMon.Common;
using EVEMon.SkillPlanner;

namespace EVEMon.IGBService
{
    public class IGBServer
    {
        public IGBServer()
        {
            m_listener.ClientConnected += new EventHandler<ClientConnectedEventArgs>(m_listener_ClientConnected);
        }

        public void Start()
        {
            m_listener.Start();
        }

        public void Stop()
        {
            m_listener.Stop();
        }

        private IGBTcpListener m_listener = new IGBTcpListener(new IPEndPoint(IPAddress.Loopback, 80));
        private Dictionary<IGBTcpClient, byte[]> m_clients = new Dictionary<IGBTcpClient, byte[]>();

        private void m_listener_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            IGBTcpClient cli = new IGBTcpClient(e.TcpClient);
            cli.DataRead += new EventHandler<IGBClientDataReadEventArgs>(cli_DataRead);
            cli.Closed += new EventHandler<EventArgs>(cli_Closed);
            lock (m_clients)
            {
                m_clients.Add(cli, new byte[0]);
            }
            cli.Start();
        }

        void cli_DataRead(object sender, IGBClientDataReadEventArgs e)
        {
            byte[] newBuf;
            lock (m_clients)
            {
                byte[] existingBuf = m_clients[(IGBTcpClient)sender];
                newBuf = new byte[existingBuf.Length + e.Count];

                Array.Copy(existingBuf, newBuf, existingBuf.Length);
                Array.Copy(e.Buffer, 0, newBuf, existingBuf.Length, e.Count);

                m_clients[(IGBTcpClient)sender] = newBuf;
            }

            TryProcessBuffer((IGBTcpClient)sender, newBuf, Math.Min(e.Count+1, newBuf.Length));
        }

        private void TryProcessBuffer(IGBTcpClient client, byte[] newBuf, int tailLength)
        {
            bool gotOne = false;
            bool gotTwo = false;
            for (int i = 0; i < tailLength; i++)
            {
                if (newBuf[newBuf.Length - i - 1] == ((byte)'\n'))
                {
                    if (gotOne)
                    {
                        gotTwo = true;
                        break;
                    }
                    else
                    {
                        gotOne = true;
                    }
                }
                else if (newBuf[newBuf.Length - i - 1] != ((byte)'\r'))
                {
                    gotOne = false;
                }
            }
            if (!gotTwo)
                return;

            Dictionary<string, string> headers = new Dictionary<string,string>();
            string headerStr = System.Text.Encoding.UTF8.GetString(newBuf);
            if (headerStr.IndexOf('\r')!=-1)
                headerStr = headerStr.Replace("\r", "");

            bool first = true;
            string requestVerb = "ERR";
            string requestUrl = String.Empty;
            foreach (string tline in headerStr.Split('\n'))
            {
                if (first)
                {
                    Match m = Regex.Match(tline, @"^(GET|POST) (.+) HTTP/(.*)$");
                    if (m.Success)
                    {
                        requestVerb = m.Groups[1].Value;
                        requestUrl = m.Groups[2].Value;
                    }
                    first = false;
                }
                else
                {
                    Match m = Regex.Match(tline, "^(.*?): (.*)$");
                    if (m.Success)
                    {
                        headers[m.Groups[1].Value.ToLower()] = m.Groups[2].Value;
                    }
                }
            }

            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            {
                ProcessRequest(requestUrl, headers, sw);
                //sw.WriteLine("<html><head><title>here</title></head><body>hello world");
                //sw.WriteLine("verb={0}, url={1}</body></html>", requestVerb, requestUrl);

                sw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                client.Write("HTTP/1.1 200 OK\n");
                client.Write("Server: EVEMon/1.0\n");
                client.Write("Content-Type: text/html; charset=utf-8\n");
                if (headers["eve.trusted"].ToLower() == "no")
                    client.Write("eve.trustme: http://localhost/::EVEMon needs your pilot information.\n");
                client.Write("Connection: close\n");
                client.Write("Content-Length: " + ms.Length.ToString() + "\n\n");
                using (StreamReader sr = new StreamReader(ms))
                {
                    client.Write(sr.ReadToEnd());
                }
            }

            //client.Write("HTTP/1.0 200 OK\n");
            //client.Write("Content-type: text/plain\n\n");
            //client.Write("hello world\n");
            //client.Write(String.Format("verb={0}, url={1}", requestVerb, requestUrl));
            client.Close();
        }

        private void ProcessRequest(string requestUrl, Dictionary<string, string> headers, StreamWriter sw)
        {
            if (headers["eve.trusted"].ToLower() != "yes")
            {
                sw.WriteLine("not trusted");
                return;
            }
            if (requestUrl.StartsWith("/viewplan.x?p="))
            {
                string planName = System.Web.HttpUtility.UrlDecode(requestUrl.Substring(14));
                sw.WriteLine("<html><head><title>Plan</title></head><body>");
                sw.WriteLine(String.Format("<h1>Plan: {0}</h1>",
                    System.Web.HttpUtility.HtmlEncode(planName)));

                Plan p = Program.Settings.GetPlanByName(headers["eve.charname"], planName);
                if (p == null)
                {
                    sw.WriteLine("non-existant plan name");
                }
                else
                {
                    if (p.GrandCharacterInfo == null)
                        p.GrandCharacterInfo = Program.MainWindow.GetGrandCharacterInfo(headers["eve.charname"]);
                    if (p.GrandCharacterInfo == null)
                    {
                        sw.Write("Could not get character info");
                    }
                    else
                    {
                        p.SaveAsText(sw, new PlanTextOptions(), MarkupType.Html);
                    }
                    //foreach (PlanEntry pe in p.Entries)
                    //{
                    //    sw.WriteLine(pe.SkillName + " " + GrandSkill.GetRomanSkillNumber(pe.Level) + "<br>");
                    //}
                }

                sw.WriteLine("<hr><a href=\"/\">Back</a>");
                sw.WriteLine("</body></html>");
            }
            else
            {
                sw.WriteLine(
                    String.Format("<html><head><title>Hi</title></head><body><h1>Hello, {0}</h1>",
                    headers["eve.charname"]));

                sw.WriteLine("<h2>Your Plans:</h2>");
                foreach (string s in Program.Settings.GetPlansForCharacter(headers["eve.charname"]))
                {
                    sw.WriteLine(String.Format("<a href=\"/viewplan.x?p={0}\">{1}</a><br>", 
                        System.Web.HttpUtility.UrlEncode(s),
                        System.Web.HttpUtility.HtmlEncode(s)));
                }

                sw.WriteLine("</body></html>");
            }
        }

        void cli_Closed(object sender, EventArgs e)
        {
            lock (m_clients)
            {
                m_clients.Remove((IGBTcpClient)sender);
            }
        }
    }

    public class IGBTcpListener
    {
        public IGBTcpListener(IPEndPoint ep)
        {
            m_listenEndpoint = ep;
        }

        private bool m_running = false;
        private IPEndPoint m_listenEndpoint;
        private TcpListener m_listener;

        public void Start()
        {
            lock (this)
            {
                if (!m_running)
                {
                    m_running = true;
                    m_listener = new TcpListener(m_listenEndpoint);
                    try
                    {
                        m_listener.Start();
                        BeginAcceptTcpClient(false);
                    }
                    catch
                    {
                        m_listener = null;
                    }
                }
            }
        }

        private void BeginAcceptTcpClient()
        {
            BeginAcceptTcpClient(true);
        }

        private void BeginAcceptTcpClient(bool acquireLock)
        {
            if (acquireLock)
                System.Threading.Monitor.Enter(this);
            try
            {
                IAsyncResult ar;
                do
                {
                    ar = null;
                    if (m_running)
                        ar = m_listener.BeginAcceptTcpClient(new AsyncCallback(EndAcceptTcpClient), null);
                } while (ar!=null && ar.CompletedSynchronously);
            }
            finally
            {
                if (acquireLock)
                    System.Threading.Monitor.Exit(this);
            }
        }

        private void EndAcceptTcpClient(IAsyncResult ar)
        {
            try
            {
                bool inLock = ar.CompletedSynchronously;

                TcpClient newClient = m_listener.EndAcceptTcpClient(ar);
                OnClientConnected(newClient, !inLock);
                if (!ar.CompletedSynchronously)
                    BeginAcceptTcpClient(true);
            }
            catch { }
        }

        private void OnClientConnected(TcpClient newClient, bool acquireLock)
        {
            if (acquireLock)
                System.Threading.Monitor.Enter(this);
            try
            {
                if (m_running && ClientConnected != null)
                {
                    ClientConnected(this, new ClientConnectedEventArgs(newClient));
                }
                else
                {
                    try
                    {
                        newClient.Close();
                    }
                    catch (SocketException) { }
                }
            }
            finally
            {
                if (acquireLock)
                    System.Threading.Monitor.Exit(this);
            }
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public void Stop()
        {
            lock (this)
            {
                if (m_running)
                {
                    m_running = false;
                    if (m_listener != null)
                    {
                        m_listener.Stop();
                        m_listener = null;
                    }
                }
            }
        }
    }

    public class ClientConnectedEventArgs : EventArgs
    {
        private TcpClient m_client;

        public ClientConnectedEventArgs(TcpClient client)
        {
            m_client = client;
        }

        public TcpClient TcpClient
        {
            get { return m_client; }
        }
    }

    public class IGBTcpClient
    {
        public IGBTcpClient(TcpClient client)
        {
            m_client = client;
        }

        public void Start()
        {
            lock (this)
            {
                m_running = true;
                m_stream = m_client.GetStream();
                m_buffer = new byte[m_bufferSize];
                BeginRead(false);
            }
        }

        private int m_bufferSize = 4096;

        public int BufferSize
        {
            get { return m_bufferSize; }
            set { m_bufferSize = value; }
        }

        private byte[] m_buffer;

        private void BeginRead(bool acquireLock)
        {
            if (acquireLock)
                System.Threading.Monitor.Enter(this);
            try
            {
                IAsyncResult ar;
                do
                {
                    ar = null;
                    if (m_running)
                        ar = m_stream.BeginRead(m_buffer, 0, m_buffer.Length, new AsyncCallback(EndRead), null);
                } while (ar != null && ar.CompletedSynchronously);
            }
            finally
            {
                if (acquireLock)
                    System.Threading.Monitor.Exit(this);
            }
        }

        private void EndRead(IAsyncResult ar)
        {
            try
            {
                bool inLock = ar.CompletedSynchronously;

                int bytesRead = m_stream.EndRead(ar);
                if (bytesRead<=0)
                {
                    Close();
                }
                else
                {
                    OnDataRead(m_buffer, 0, bytesRead);
                    if (!ar.CompletedSynchronously)
                        BeginRead(true);
                }
            }
            catch
            {
                Close();
            }
        }

        private void OnDataRead(byte[] buffer, int offset, int count)
        {
            if (DataRead != null)
                DataRead(this, new IGBClientDataReadEventArgs(buffer, offset, count));
        }

        public void Close()
        {
            if (m_running)
            {
                m_running = false;
                try
                {
                    m_client.Close();
                }
                catch (SocketException) { }
                OnClosed();
            }
        }

        private void OnClosed()
        {
            if (Closed != null)
                Closed(this, new EventArgs());
        }

        public event EventHandler<IGBClientDataReadEventArgs> DataRead;
        public event EventHandler<EventArgs> Closed;

        private bool m_running = false;
        private TcpClient m_client;
        private NetworkStream m_stream;

        public void Write(string str)
        {
            byte[] outbuf = System.Text.Encoding.UTF8.GetBytes(str);
            m_stream.Write(outbuf, 0, outbuf.Length);
        }
    }

    public class IGBClientDataReadEventArgs : EventArgs
    {
        private byte[] m_buffer;
        private int m_offset;
        private int m_count;

        public IGBClientDataReadEventArgs(byte[] buffer, int offset, int count)
        {
            m_buffer = buffer;
            m_offset = offset;
            m_count = count;
        }

        public byte[] Buffer
        {
            get { return m_buffer; }
        }

        public int Offset
        {
            get { return m_offset; }
        }

        public int Count
        {
            get { return m_count; }
        }
    }
}
