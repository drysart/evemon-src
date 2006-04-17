using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EveCharacterMonitor
{
    public class InstanceManager
    {
        private static InstanceManager m_instanceManager;

        public static InstanceManager GetInstance()
        {
            if (m_instanceManager == null)
                m_instanceManager = new InstanceManager();
            return m_instanceManager;
        }

        private RegisteredWaitHandle m_waitHandle;
        private Semaphore m_semaphore;
        private bool m_createdNew;

        private InstanceManager()
        {
            m_semaphore = new Semaphore(0, 1, "EVEMonInstance", out m_createdNew);
            m_waitHandle = ThreadPool.RegisterWaitForSingleObject(m_semaphore,
                                new WaitOrTimerCallback(SemaphoreReleased), null, -1, false);
        }

        public bool CreatedNew
        {
            get { return m_createdNew; }
        }

        private void SemaphoreReleased(object o, bool b)
        {
            if (Signaled != null)
                Signaled(this, new EventArgs());
        }

        public event EventHandler<EventArgs> Signaled;

        public void Signal()
        {
            try
            {
                m_semaphore.Release();
            }
            catch (SemaphoreFullException) { }
        }
    }
}
