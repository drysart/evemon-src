using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class SkillTreeDisplay : UserControl
    {
        public SkillTreeDisplay()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.AutoScrollMinSize = new Size(1000, 2000);
        }

        private GrandSkill m_rootSkill = null;

        public GrandSkill RootSkill
        {
            get { return m_rootSkill; }
            set
            {
                if (m_rootSkill != value)
                {
                    m_rootSkill = value;
                    BuildTree();
                }
            }
        }

        private const int SKILLBOX_WIDTH = 220;
        private const int SKILLBOX_HEIGHT = 73;

        private const int SKILLBOX_MARGIN_UD = 20;
        private const int SKILLBOX_MARGIN_LR = 10;

        private class SkillInfo
        {
            private GrandSkill m_skill;
            private SkillInfo m_parent;
            private int m_left;

            public GrandSkill Skill
            {
                get { return m_skill; }
            }

            public SkillInfo ParentSkillInfo
            {
                get { return m_parent; }
            }

            public int Left
            {
                get { return m_left; }
                set { m_left = value; }
            }

            public SkillInfo(GrandSkill gs, SkillInfo parent)
            {
                m_skill = gs;
                m_parent = parent;
            }

            internal void AddChild(SkillInfo si)
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        private List<List<SkillInfo>> m_layoutData = new List<List<SkillInfo>>();
        private Dictionary<GrandSkill, SkillInfo> m_alreadyInLayout = null;

        private void BuildTree()
        {
            SetupTree();
            LayoutTree();
            LayoutLines();
            CalculateSize();
        }

        private void SetupTree()
        {
            m_layoutData = new List<List<SkillInfo>>();
            m_alreadyInLayout = new Dictionary<GrandSkill, SkillInfo>();

            List<SkillInfo> mainLevel = new List<SkillInfo>();
            m_layoutData.Add(mainLevel);
            SkillInfo rootSi = new SkillInfo(m_rootSkill, null);
            rootSi.Left = 0;
            mainLevel.Add(rootSi);
            m_alreadyInLayout.Add(m_rootSkill, rootSi);

            BuildPrereqs(rootSi, 1);
        }

        private void BuildPrereqs(SkillInfo parentSi, int level)
        {
            GrandSkill parentSkill = parentSi.Skill;
            foreach (GrandSkill.Prereq pp in parentSkill.Prereqs)
            {
                if (!m_alreadyInLayout.ContainsKey(pp.Skill))
                {
                    if (m_layoutData.Count <= level)
                        m_layoutData.Add(new List<SkillInfo>());
                    List<SkillInfo> thisLevel = m_layoutData[level];

                    SkillInfo si = new SkillInfo(pp.Skill, parentSi);
                    if (thisLevel.Count == 0)
                        si.Left = 0;
                    else
                        si.Left = thisLevel[thisLevel.Count - 1].Left + SKILLBOX_WIDTH + SKILLBOX_MARGIN_LR;
                    thisLevel.Add(si);
                    m_alreadyInLayout.Add(pp.Skill, si);
                    parentSi.AddChild(si);

                    BuildPrereqs(si, level + 1);
                }
                else
                {
                    parentSi.AddChild(m_alreadyInLayout[pp.Skill]);
                }
            }
        }

        private void LayoutTree()
        {
            // TODO: layout using weights
            return;
        }

        private void LayoutLines()
        {
            int level = 0;
            foreach (List<SkillInfo> lsi in m_layoutData)
            {
                foreach (SkillInfo si in lsi)
                {
                    SkillInfo parentSi = si.ParentSkillInfo;

                    if (parentSi != null)
                    {
                        Point lineFrom = new Point(si.Left + (SKILLBOX_WIDTH / 2), level);
                        Point lineTo = new Point(parentSi.Left + (SKILLBOX_WIDTH / 2), level - 1);
                        PlotLine(lineFrom, lineTo);
                    }
                }
                level++;
            }
        }

        private void PlotLine(Point lineFrom, Point lineTo)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        private Rectangle m_graphBounds = new Rectangle(0, 0, 10, 10);

        private void CalculateSize()
        {
            int left = 0;
            int right = 0;
            foreach (List<SkillInfo> lsi in m_layoutData)
            {
                foreach (SkillInfo si in lsi)
                {
                    int myLeft = si.Left;
                    int myRight = si.Left + SKILLBOX_WIDTH;
                    if (myLeft < left)
                        left = myLeft;
                    if (myRight > right)
                        right = myRight;
                }
            }

            int hh = m_layoutData.Count * (SKILLBOX_HEIGHT);
            hh += (m_layoutData.Count - 1) * (SKILLBOX_MARGIN_UD);

            m_graphBounds = new Rectangle(left, 0, right - left, hh);
            this.AutoScrollMinSize = m_graphBounds.Size;
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            this.Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Brush b = new LinearGradientBrush(
                this.ClientRectangle, Color.LightBlue, Color.DarkBlue, 90.0F))
            {
                e.Graphics.FillRectangle(b, e.ClipRectangle);
            }

            int ofsLeft = 0 - m_graphBounds.Left + this.AutoScrollPosition.X;
            int ofsTop = 0 - m_graphBounds.Top + this.AutoScrollPosition.Y;
            if (this.ClientSize.Width > m_graphBounds.Width)
            {
                int twHalf = this.ClientSize.Width / 2;
                int gwHalf = m_graphBounds.Width / 2;
                ofsLeft = (twHalf - gwHalf) - m_graphBounds.Left;
            }
            if (this.ClientSize.Height > m_graphBounds.Height)
            {
                int thHalf = this.ClientSize.Height / 2;
                int ghHalf = m_graphBounds.Height / 2;
                ofsTop = (thHalf - ghHalf) - m_graphBounds.Top;
            }

            int level = 0;
            foreach (List<SkillInfo> lsi in m_layoutData)
            {
                int ttop = (level * (SKILLBOX_HEIGHT + SKILLBOX_MARGIN_UD)) + ofsTop;
                foreach (SkillInfo si in lsi)
                {
                    Rectangle rect = new Rectangle(
                        si.Left + ofsLeft, ttop, SKILLBOX_WIDTH, SKILLBOX_HEIGHT);
                    e.Graphics.FillRectangle(Brushes.White, rect);

                    using (Font boldf = new Font(this.Font, FontStyle.Bold))
                    {
                        TextRenderer.DrawText(e.Graphics, si.Skill.Name, boldf,
                            new Point(rect.Left + 5, rect.Top + 5), Color.Black);
                    }
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                }
                level++;
            }
        }
    }
}
