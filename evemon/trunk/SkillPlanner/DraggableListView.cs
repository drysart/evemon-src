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
        public DraggableListView()
            : base()
        {
            HideableInit();
            DraggableInit();
        }

        #region Hidable Column Stuff
        private ColumnHeaderCollectionEx columnHeadersEx = null;

        public new ColumnHeaderCollectionEx Columns
        {
            get { return columnHeadersEx; }
        }

        private void HideableInit()
        {
            // Create a new collection to hold the columns
            columnHeadersEx = new ColumnHeaderCollectionEx(this);
            // Create the context menu.
            //base.ContextMenu = new ContextMenu();
            //base.ContextMenu.MenuItems.Add(columnHeadersEx.ContextMenu);
            //base.ContextMenu.Popup += new EventHandler(ContextMenuPopup);

            base.View = System.Windows.Forms.View.Details;
        }

        private void ContextMenuPopup(object sender, EventArgs e)
        {
        }
        #endregion

        #region Draggable stuff
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

        private void DraggableInit()
        {
            this.AllowRowReorder = true;
        }

        public event EventHandler<ListViewDragEventArgs> ListViewItemsDragging;
        public event EventHandler<EventArgs> ListViewItemsDragged;

        private bool m_dragging = false;

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            m_dragging = false;
            ClearDropMarker();
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

        private int m_dropMarkerOn = -1;
        private bool m_dropMarkerBelow = false;

        private void ClearDropMarker()
        {
            if (m_dropMarkerOn != -1)
            {
                //Rectangle rr = this.GetItemRect(m_dropMarkerOn);
                //this.Invalidate(rr);
                this.RestrictedPaint();
            }
            m_dropMarkerOn = -1;
        }

        private void DrawDropMarker(int index, bool below)
        {
            if (m_dropMarkerOn != -1 && m_dropMarkerOn != index)
                ClearDropMarker();
            if (m_dropMarkerOn != index)
            {
                m_dropMarkerOn = index;
                m_dropMarkerBelow = below;
                //Rectangle rr = this.GetItemRect(m_dropMarkerOn);
                //this.Invalidate(rr);
                this.RestrictedPaint();
            }
        }

        private void RestrictedPaint()
        {
            Rectangle itemRect = base.GetItemRect(m_dropMarkerOn);
            Point start;
            Point end;
            if (m_dropMarkerBelow)
            {
                start = new Point(itemRect.Left, itemRect.Bottom);
                end = new Point(itemRect.Right, itemRect.Bottom);
            }
            else
            {
                start = new Point(itemRect.Left, itemRect.Top);
                end = new Point(itemRect.Right, itemRect.Top);
            }
            start = this.PointToScreen(start);
            end = this.PointToScreen(end);
            ControlPaint.DrawReversibleLine(start, end, SystemColors.Window);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (m_dragging)
                RestrictedPaint();
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!this.AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                ClearDropMarker();
                return;
            }
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                ClearDropMarker();
                return;
            }
            Point cp = base.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
            if (hoverItem == null)
            {
                e.Effect = DragDropEffects.None;
                ClearDropMarker();
                return;
            }
            foreach (ListViewItem moveItem in base.SelectedItems)
            {
                if (moveItem.Index == hoverItem.Index)
                {
                    e.Effect = DragDropEffects.None;
                    hoverItem.EnsureVisible();
                    ClearDropMarker();
                    return;
                }
            }
            base.OnDragOver(e);
            String text = (String)e.Data.GetData(REORDER.GetType());
            if (text.CompareTo(REORDER) == 0)
            {
                e.Effect = DragDropEffects.Move;
                hoverItem.EnsureVisible();

                DrawDropMarker(hoverItem.Index, hoverItem.Index > this.SelectedIndices[0]);
            }
            else
            {
                e.Effect = DragDropEffects.None;
                ClearDropMarker();
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (!this.AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                ClearDropMarker();
                return;
            }
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                ClearDropMarker();
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
                ClearDropMarker();
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
            m_dragging = true;
        }
#endregion
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

    /// <summary>
    /// This class Represents the collection of 
    /// ColumnHeaderEx in a ListViewEx control.
    /// This class stores the column headers
    /// that are displayed in the ListViewEx control when the View 
    /// property is set to View.Details. 
    /// This class stores ColumnHeaderEx objects that define the text
    /// to display for a column as well as how the column header is 
    /// displayed in the ListViewEx control when displaying columns.
    /// When a ListViewEx displays columns, the items and their subitems 
    /// are displayed in their own columns. 
    /// </summary>
    public class ColumnHeaderCollectionEx : ListView.ColumnHeaderCollection
    {

        #region Private members
        /// <summary>
        /// This is to maintain the list of columns added
        /// to the ListViewEx control, this will contain both
        /// visible and hidden columns.
        /// </summary>
        private SortedList columnList = new SortedList();
        #endregion

        #region Properties
        /// <summary>
        /// Indexer to get columns by index
        /// </summary>
        public new ColumnHeaderEx this[int index]
        {
            get
            {
                return (ColumnHeaderEx)columnList.GetByIndex(index);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// You cannot create an instance of this class 
        /// without associating it with a ListView control.
        /// </summary>
        /// <param name="owner"></param>
        public ColumnHeaderCollectionEx(ListView owner)
            : base(owner)
        {
            // Create a menu item to add submenus for each column added
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Method adds a single column header to the collection.
        /// </summary>
        /// <param name="str">Text to display</param>
        /// <param name="width">Width of column</param>
        /// <param name="textAlign">Alignment</param>
        /// <returns>new ColumnHeaderEx added</returns>
        public override ColumnHeader Add(string str, int width, HorizontalAlignment textAlign)
        {
            ColumnHeaderEx column = new ColumnHeaderEx(str, width, textAlign);
            this.Add(column);
            return column;
        }

        /// <summary>
        /// Method adds a single column header to the collection.
        /// </summary>
        /// <param name="column"></param>
        /// <returns>The zero-based index into the collection 
        /// where the item was added.</returns>
        public override int Add(ColumnHeader column)
        {
            return this.Add(new ColumnHeaderEx(column));
        }

        /// <summary>
        /// Adds an array of column headers to the collection.
        /// </summary>
        /// <param name="values">An array of ColumnHeader 
        /// objects to add to the collection. </param>
        public override void AddRange(ColumnHeader[] values)
        {
            // Add range of column headers
            for (int index = 0; index < values.Length; index++)
            {
                this.Add(new ColumnHeaderEx(values[index]));
            }
        }

        /// <summary>
        /// Adds an existing ColumnHeader to the collection.
        /// </summary>
        /// <param name="column">The ColumnHeader to 
        /// add to the collection. </param>
        /// <returns>The zero-based index into the collection 
        /// where the item was added.</returns>
        public int Add(ColumnHeaderEx column)
        {
            // Add the column to the base
            int retValue = base.Add(column);
            // Keep a refrence in columnList
            columnList.Add(column.ColumnID, column);
            // Subscribe to the visiblity change event of the column
            column.VisibleChanged += new EventHandler(ColumnVisibleChanged);
            return retValue;
        }

        /// <summary>
        /// Removes the specified column header from the collection.
        /// </summary>
        /// <param name="column">A ColumnHeader representing the 
        /// column header to remove from the collection.</param>
        public new void Remove(ColumnHeader column)
        {
            // Remove from base
            base.Remove(column);
            // Remove the reference in columnList
            columnList.Remove(((ColumnHeaderEx)column).ColumnID);
            // remove the menu item associated with it
        }

        /// <summary>
        /// Removes the column header at the specified index within the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the 
        /// column header to remove</param>
        public new void RemoveAt(int index)
        {
            ColumnHeader column = this[index];
            this.Remove(column);
        }

        /// <summary>
        /// Removes all column headers from the collection.
        /// </summary>
        public new void Clear()
        {
            // Clear all columns in base
            base.Clear();
            // Remove all references
            columnList.Clear();
        }

        #endregion

        #region Private methods
        /// <summary>
        /// This method is used to find the first visible column
        /// which is present in front of the column specified
        /// </summary>
        /// <param name="column">refrence columns for search</param>
        /// <returns>null if no visible colums are in front of
        /// the column specified, else previous columns returned</returns>
        private ColumnHeaderEx FindPreviousVisibleColumn(ColumnHeaderEx column)
        {
            // Get the position of the search column
            int index = columnList.IndexOfKey(column.ColumnID);
            if (index > 0)
            {
                // Start a recursive search for a visible column
                ColumnHeaderEx prevColumn = (ColumnHeaderEx)columnList.GetByIndex(index - 1);
                if ((prevColumn != null) && (prevColumn.Visible == false))
                {
                    prevColumn = FindPreviousVisibleColumn(prevColumn);
                }
                return prevColumn;
            }
            // No visible columns found in font of specified column
            return null;
        }

        /// <summary>
        /// Handler to handel the visiblity change of columns
        /// </summary>
        /// <param name="sender">ColumnHeaderEx</param>
        /// <param name="e"></param>
        private void ColumnVisibleChanged(object sender, EventArgs e)
        {
            ColumnHeaderEx column = (ColumnHeaderEx)sender;

            if (column.Visible == true)
            {
                // Show the hidden column
                // Get the position where the hidden column has to be shown
                ColumnHeaderEx prevHeader = FindPreviousVisibleColumn(column);
                if (prevHeader == null)
                {
                    // This is the first column, so add it at 0 location
                    base.Insert(0, column);
                }
                else
                {
                    // Got the location, place it their.
                    base.Insert(prevHeader.Index + 1, column);
                }
            }
            else
            {
                // Hide the column.
                // Remove it from the base, dont worry we have the 
                // refrence in columnList to get it back
                base.Remove(column);
            }
        }

        #endregion
    }

    /// <summary>
    /// This class object represents a single column header in a ListViewEx control.
    /// This class is extended from ColumnHeader, inorder to support column hiding.
    /// </summary>
    public class ColumnHeaderEx : ColumnHeader
    {

        #region Private members
        private bool columnVisible = true;
        private int columnID = 0;
        private static int autoColumnID = 0;
        #endregion

        #region Events
        /// <summary>
        /// This event is raised when the visibility of column
        /// is changed.
        /// </summary>
        public event EventHandler VisibleChanged;
        #endregion

        #region Properties
        /// <summary>
        /// A unique identifier for a Column
        /// </summary>
        public int ColumnID
        {
            get { return columnID; }
        }

        /// <summary>
        /// Property to change the visibility of the column
        /// </summary>
        public bool Visible
        {
            get { return columnVisible; }
            set { ShowColumn(value); }
        }

        /// <summary>
        /// Column Text to be displayed
        /// </summary>
        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        /// Gets the ListViewEx control the 
        /// ColumnHeader is located in.
        /// </summary>
        public new DraggableListView ListView
        {
            get { return (DraggableListView)base.ListView; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ColumnHeaderEx()
        {
            Initialize("", 0, HorizontalAlignment.Left);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="str">Text to display</param>
        /// <param name="width">Width of column</param>
        /// <param name="textAlign">Alignment</param>
        public ColumnHeaderEx(string str, int width, HorizontalAlignment textAlign)
        {
            Initialize(str, width, textAlign);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="column"></param>
        public ColumnHeaderEx(ColumnHeader column)
        {
            Initialize(column.Text, column.Width, column.TextAlign);
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Column Initialization
        /// </summary>
        /// <param name="str">Text to display</param>
        /// <param name="width">Width of column</param>
        /// <param name="textAlign">Alignment</param>
        private void Initialize(string str, int width, HorizontalAlignment textAlign)
        {
            base.Text = str;
            base.Width = width;
            base.TextAlign = textAlign;
            columnID = autoColumnID++;
        }

        /// <summary>
        /// Method to show/hide column
        /// </summary>
        /// <param name="visible">visibility</param>
        private void ShowColumn(bool visible)
        {
            if (columnVisible != visible)
            {
                columnVisible = visible;
                if (VisibleChanged != null)
                {
                    VisibleChanged(this, EventArgs.Empty);
                }
            }
        }
        #endregion

    }
}
