using System;
using System.Threading;
using System.Windows.Forms;

namespace EVEMon.Common
{
    public partial class BusyDialog : EVEMonForm
    {
        public BusyDialog()
        {
            InitializeComponent();
        }

        internal void Complete()
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.Close();
            }));
        }

        private static object m_lockObj = new object();
        private static int m_displayCounter = 0;
        private static Thread m_runThread;
        private static BusyDialog m_instance;

        public static void IncrementDisplay()
        {
            lock (m_lockObj)
            {
                if (m_displayCounter == 0)
                {
                    AutoResetEvent startEvent = new AutoResetEvent(false);
                    m_runThread = new Thread(new ThreadStart(delegate
                    {
                        using (BusyDialog d = new BusyDialog())
                        {
                            m_instance = d;
                            d.Shown += new EventHandler(delegate { startEvent.Set(); });
                            d.ShowDialog();
                            m_instance = null;
                        }
                    }));
                    m_runThread.Start();
                    startEvent.WaitOne();
                }
                m_displayCounter++;
            }
        }

        public static void DecrementDisplay()
        {
            lock (m_lockObj)
            {
                m_displayCounter--;
                if (m_displayCounter <= 0)
                {
                    m_displayCounter = 0;
                    if (m_instance != null)
                    {
                        m_instance.Invoke(new MethodInvoker(delegate
                        {
                            m_instance.Close();
                        }));
                    }
                    if (m_runThread != null)
                    {
                        m_runThread.Join();
                        m_runThread = null;
                    }
                }
            }
        }

        public static IDisposable GetScope()
        {
            return new ScopeController();
        }

        public class ScopeController : IDisposable
        {
            public ScopeController()
            {
                BusyDialog.IncrementDisplay();
            }

            #region IDisposable Members

            public void Dispose()
            {
                BusyDialog.DecrementDisplay();
            }

            #endregion
        }
    }
}
