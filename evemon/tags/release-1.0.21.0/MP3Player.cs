using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EVEMon
{
    public static class MP3Player
    {
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        [DllImport("winmm.dll")]
        private static extern bool mciGetErrorString(int fdwError, StringBuilder lpszErrorText, uint cchErrorText);

        public const int MM_MCINOTIFY = 953;
        private static bool m_filterInstalled = false;
        private static bool m_isOpen = false;
        public static void Play(string fileName, bool async)
        {
            int result;
            string cmd;

            StringBuilder res = new StringBuilder(100);
            if (m_isOpen)
            {
                cmd = "close MediaFile";
                result = mciSendString(cmd, res, 100, IntPtr.Zero);
                m_isOpen = false;
            }

            cmd = "open \"" + fileName + "\" type mpegvideo alias MediaFile";
            result = mciSendString(cmd, res, 100, IntPtr.Zero);
            if (result != 0)
            {
                mciGetErrorString(result, res, 100);
                return;
                //throw new ApplicationException(res.ToString());
            }
            m_isOpen = true;
            IntPtr cbWin = IntPtr.Zero;
            cmd = "play MediaFile";
            if (!async)
            {
                cmd += " wait";
            }
            else
            {
                cmd += " notify";
                cbWin = Application.OpenForms[0].Handle;
                if (!m_filterInstalled)
                {
                    Application.AddMessageFilter(new MsgFilter());
                    m_filterInstalled = true;
                }
            }
            result = mciSendString(cmd, res, 100, cbWin);
            if (result != 0)
            {
                mciGetErrorString(result, res, 100);
                return;
                //throw new ApplicationException(res.ToString());
            }
            if (!async)
            {
                cmd = "close MediaFile";
                result = mciSendString(cmd, res, 100, IntPtr.Zero);
                m_isOpen = false;
            }
        }

        public class MsgFilter : IMessageFilter
        {
            #region IMessageFilter Members

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == MM_MCINOTIFY)
                {
                    MP3Player.Notify(ref m);
                    return true;
                }
                return false;
            }

            #endregion
        }

        private static void Notify(ref Message m)
        {
            string cmd = "close MediaFile";
            int result = mciSendString(cmd, null, 0, IntPtr.Zero);
            m_isOpen = false;
        }
    }
}
