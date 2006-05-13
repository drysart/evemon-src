using System;
using System.Collections.Generic;
using System.Text;

namespace EVEMon.Sales
{
    interface IMineralParser
    {
        float this[string name]
        {
            get;
        }
    }
}
