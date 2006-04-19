using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Mail;

namespace EVEMon
{
    class Emailer
    {
        private Emailer()
        {
        }

        private string m_server;
        private ICredentialsByHost m_serverCredentials;
        private bool m_serverRequiresSsl = false;
        private string m_fromAddr;
        private string m_toAddr;
        private string m_subject;
        private string m_body;

        public static bool SendTestMail(Settings settings)
        {
            return SendMail(settings,
                "EVE Character Monitor Test Mail",
                "This is a test email sent by EVE Character Monitor");
        }

        public static bool SendAlertMail(Settings settings, string skillName, string charName)
        {
            return SendMail(settings,
                charName + " skill " + skillName + " complete",
                charName + " has finished training " + skillName);
        }

        private static bool SendMail(Settings settings, string subject, string body)
        {
            Emailer m = new Emailer();
            m.m_server = settings.EmailServer;
            m.m_fromAddr = settings.EmailFromAddress;
            m.m_toAddr = settings.EmailToAddress;
            m.m_subject = subject;
            m.m_body = body;
            if (settings.EmailAuthRequired)
            {
                m.m_serverCredentials = new NetworkCredential(settings.EmailAuthUsername, settings.EmailAuthPassword);
            }
            m.m_serverRequiresSsl = settings.EmailServerRequiresSsl;
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
                if (m_serverCredentials != null)
                {
                    cli.Credentials = m_serverCredentials;
                    cli.UseDefaultCredentials = false;
                }
                cli.EnableSsl = m_serverRequiresSsl;
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
