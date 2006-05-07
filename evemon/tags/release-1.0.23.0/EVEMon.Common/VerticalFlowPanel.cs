using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace EVEMon.Common
{
    public class VerticalFlowPanel: Panel
    {
        private VerticalFlowLayout m_layoutEngine;

        public VerticalFlowPanel()
        {
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (m_layoutEngine == null)
                {
                    m_layoutEngine = new VerticalFlowLayout();
                }

                return m_layoutEngine;
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size pSize = new Size(proposedSize.Width, proposedSize.Height);
            AGAIN:
            int internalWidth = pSize.Width - this.Padding.Left - this.Padding.Right;
            int internalHeight = pSize.Height - this.Padding.Top - this.Padding.Bottom;
            Size displaySize = new Size(internalWidth, internalHeight);
            Point nextLocation = new Point(this.Padding.Left, this.Padding.Top);
            int overallNeededWidth = 0;
            for (int i = 0; i < Controls.Count; i++)
            {
                Control c = Controls[Controls.Count - i - 1];

                if (!c.Visible)
                {
                    continue;
                }

                Size cSize;
                if (c.AutoSize)
                {
                    int thisWidth = displaySize.Width - c.Margin.Left - c.Margin.Right;
                    int thisTop = nextLocation.Y + c.Margin.Top;
                    int thisHeight = displaySize.Height - thisTop - c.Margin.Bottom;
                    Size thisProposedSize = new Size(thisWidth, thisHeight);
                    cSize = c.GetPreferredSize(thisProposedSize);
                }
                else
                {
                    cSize = c.Size;
                }
                int neededWidth = cSize.Width + c.Margin.Left + c.Margin.Right;
                if (neededWidth > displaySize.Width)
                {
                    pSize.Width = neededWidth + this.Padding.Left + this.Padding.Right;
                    goto AGAIN;
                }
                if (neededWidth > overallNeededWidth)
                {
                    overallNeededWidth = neededWidth;
                }
                nextLocation.Y += c.Margin.Top + c.Size.Height + c.Margin.Bottom;
            }

            Size prefSize = new Size(overallNeededWidth + this.Padding.Left + this.Padding.Right,
                nextLocation.Y + this.Padding.Bottom);
            return prefSize;
        }
    }

    public class VerticalFlowLayout : LayoutEngine
    {
        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            Control parent = container as Control;
            Rectangle parentDisplayRect = parent.DisplayRectangle;
            Point nextControlLocation = parentDisplayRect.Location;

            for (int i=0; i<parent.Controls.Count; i++)
            {
                Control c = parent.Controls[parent.Controls.Count - i - 1];

                if (!c.Visible)
                {
                    continue;
                }

                nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);

                c.Location = nextControlLocation;
                if (c.AutoSize)
                {
                    Size proposedSize = new Size(parentDisplayRect.Width - c.Margin.Left - c.Margin.Right,
                        parentDisplayRect.Height - nextControlLocation.Y);
                    Size preferredSize = c.GetPreferredSize(proposedSize);
                    c.Size = new Size(proposedSize.Width, preferredSize.Height);
                }

                nextControlLocation.X = parentDisplayRect.X;
                nextControlLocation.Y += c.Height + c.Margin.Bottom;
            }

            return false;
        }
    }
}
