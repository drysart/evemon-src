using System;
using System.Collections.Generic;
using System.Text;

namespace EVEMon
{
    public class WeakReference<T>: WeakReference
        where T: class
    {
        public WeakReference(T target)
            : base(target)
        {
        }

        public WeakReference(T target, bool trackResurrection)
            : base(target, trackResurrection)
        {
        }

        public new T Target
        {
            get
            {
                return base.Target as T;
            }
        }
    }
}
