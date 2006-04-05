using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Mail;

namespace EveCharacterMonitor
{
    class Emailer
    {
        private Emailer()
        {
        }

        private string m_server;
        private string m_fromAddr;
        private string m_toAddr;
        private string m_subject;
        private string m_body;

        public static bool SendTestMail(string server, string fromaddr, string toaddr)
        {
            Emailer m = new Emailer();
            m.m_server = server;
            m.m_fromAddr = fromaddr;
            m.m_toAddr = toaddr;
            m.m_subject = "EVE Character Monitor Test Mail";
            m.m_body = "This is a test email sent by EVE Character Monitor";
            return m.Send();
        }

        public static bool SendAlertMail(Settings settings, string skillName, string charName)
        {
            Emailer m = new Emailer();
            m.m_server = settings.EmailServer;
            m.m_fromAddr = settings.EmailFromAddress;
            m.m_toAddr = settings.EmailToAddress;
            m.m_subject = charName + " skill " + skillName + " complete";
            m.m_body = charName + " has finished training " + skillName;
            return m.Send();
        }

        private bool Send()
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(m_fromAddr);
                msg.To.Add(m_toAddr);
                msg.Subject = m_subject;
                msg.Body = m_body;
                SmtpClient cli = new SmtpClient(m_server);
                cli.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
