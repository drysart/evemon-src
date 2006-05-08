using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace EVEMon
{
    public class NoFlickerListBox: System.Windows.Forms.ListBox
    {
        private enum WM
        {
            WM_NULL       = 0x0000,
            WM_ERASEBKGND = 0x0014
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)WM.WM_ERASEBKGND:
                    PaintNonItemRegion();
                    m.Msg = (int)WM.WM_NULL;
                    break;
            }
            base.WndProc(ref m);
        }

        private void PaintNonItemRegion()
        {
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            using (Region r = new Region(this.ClientRectangle))
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    Rectangle itemRect = this.GetItemRectangle(i);
                    r.Exclude(itemRect);
                }
                g.FillRegion(SystemBrushes.Window, r);
            }
        }
    }
}
