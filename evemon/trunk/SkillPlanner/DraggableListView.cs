using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace EveCharacterMonitor.SkillPlanner
{
    public class DraggableListView: ListView
    {
        private const string REORDER = "Reorder";

        private bool allowRowReorder = true;
        public bool AllowRowReorder
        {
            get
            {
                return this.allowRowReorder;
            }
            set
            {
                this.allowRowReorder = value;
                base.AllowDrop = value;
            }
        }

        public new SortOrder Sorting
        {
            get
            {
                return SortOrder.None;
            }
            set
            {
                base.Sorting = SortOrder.None;
            }
        }

        public DraggableListView()
            : base()
        {
            this.AllowRowReorder = true;
        }

        public event EventHandler<ListViewDragEventArgs> ListViewItemsDragging;
        public event EventHandler<EventArgs> ListViewItemsDragged;

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            if (!this.AllowRowReorder)
            {
                return;
            }
            if (base.SelectedItems.Count == 0)
            {
                return;
            }
            Point cp = base.PointToClient(new Point(e.X, e.Y));
            ListViewItem dragToItem = base.GetItemAt(cp.X, cp.Y);
            if (dragToItem == null)
            {
                return;
            }
            int dropIndex = dragToItem.Index;
            if (dropIndex > base.SelectedItems[0].Index)
            {
                dropIndex++;
            }

            if (ListViewItemsDragging != null)
            {
                ListViewDragEventArgs args = new ListViewDragEventArgs(base.SelectedItems[0].Index,
                    base.SelectedItems.Count, dropIndex);
                ListViewItemsDragging(this, args);
                if (args.Cancel)
                    return;
            }

            ArrayList insertItems =
                new ArrayList(base.SelectedItems.Count);
            foreach (ListViewItem item in base.SelectedItems)
            {
                insertItems.Add(item.Clone());
            }
            for (int i = insertItems.Count - 1; i >= 0; i--)
            {
                ListViewItem insertItem =
                    (ListViewItem)insertItems[i];
                base.Items.Insert(dropIndex, insertItem);
            }
            foreach (ListViewItem removeItem in base.SelectedItems)
            {
                base.Items.Remove(removeItem);
            }

            if (ListViewItemsDragged != null)
            {
                ListViewItemsDragged(this, new EventArgs());
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!this.AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            Point cp = base.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
            if (hoverItem == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            foreach (ListViewItem moveItem in base.SelectedItems)
            {
                if (moveItem.Index == hoverItem.Index)
                {
                    e.Effect = DragDropEffects.None;
                    hoverItem.EnsureVisible();
                    return;
                }
            }
            base.OnDragOver(e);
            String text = (String)e.Data.GetData(REORDER.GetType());
            if (text.CompareTo(REORDER) == 0)
            {
                e.Effect = DragDropEffects.Move;
                hoverItem.EnsureVisible();
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (!this.AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            base.OnDragEnter(e);
            String text = (String)e.Data.GetData(REORDER.GetType());
            if (text.CompareTo(REORDER) == 0)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);
            if (!this.AllowRowReorder)
            {
                return;
            }
            base.DoDragDrop(REORDER, DragDropEffects.Move);
        }
    }

    public class ListViewDragEventArgs : EventArgs
    {
        private int m_movingFrom;

        public int MovingFrom
        {
            get { return m_movingFrom; }
        }

        private int m_movingCount;

        public int MovingCount
        {
            get { return m_movingCount; }
        }

        private int m_movingTo;

        public int MovingTo
        {
            get { return m_movingTo; }
        }

        private bool m_cancel = false;

        public bool Cancel
        {
            get { return m_cancel; }
            set { m_cancel = value; }
        }

        internal ListViewDragEventArgs(int from, int count, int to)
        {
            m_movingFrom = from;
            m_movingCount = count;
            m_movingTo = to;
        }
    }
}
