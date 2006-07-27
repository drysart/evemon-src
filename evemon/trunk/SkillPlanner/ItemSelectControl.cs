using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ItemSelectControl : UserControl
    {
        public ItemSelectControl()
        {
            InitializeComponent();
        }

        private ItemCategory m_rootCategory;

        private void ItemSelectControl_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            try
            {
                m_rootCategory = ItemCategory.GetRootCategory();
            }
            catch (InvalidCastException err)
            {
                // This occurs when we're in the designer. DesignMode doesn't get set
                // when the control is a subcontrol of a user control, so we should handle
                // this here :(
                ExceptionHandler.LogException(err, true);
                return;
            }

            tvItems.BeginUpdate();
            try
            {
                if (m_rootCategory != null)
                    BuildSubtree(m_rootCategory, tvItems.Nodes);
            }
            finally
            {
                tvItems.EndUpdate();
            }
        }

        private void BuildSubtree(ItemCategory cat, TreeNodeCollection nodeCollection)
        {
            SortedDictionary<string, ItemCategory> sortedSubcats = new SortedDictionary<string, ItemCategory>();
            foreach (ItemCategory tcat in cat.Subcategories)
            {
                sortedSubcats.Add(tcat.Name, tcat);
            }
            foreach (ItemCategory tcat in sortedSubcats.Values)
            {
                TreeNode tn = new TreeNode();
                tn.Text = tcat.Name;
                BuildSubtree(tcat, tn.Nodes);
                nodeCollection.Add(tn);
            }

            SortedDictionary<string, Item> sortedItems = new SortedDictionary<string, Item>();
            foreach (Item titem in cat.Items)
            {
                sortedItems.Add(titem.Name, titem);
            }
            foreach (Item titem in sortedItems.Values)
            {
                TreeNode tn = new TreeNode();
                tn.Text = titem.Name;
                tn.Tag = titem;
                nodeCollection.Add(tn);
            }
        }

        private void lbSearchTextHint_Click(object sender, EventArgs e)
        {
            tbSearchText.Focus();
        }

        private void tbSearchText_Enter(object sender, EventArgs e)
        {
            lbSearchTextHint.Visible = false;
        }

        private void tbSearchText_Leave(object sender, EventArgs e)
        {
            lbSearchTextHint.Visible = String.IsNullOrEmpty(tbSearchText.Text);
        }

        private void tbSearchText_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbSearchText.Text) ||
                String.IsNullOrEmpty(tbSearchText.Text.Trim()))
            {
                tvItems.Visible = true;
                lbItemResults.Visible = false;
                lbNoMatches.Visible = false;
                return;
            }

            string searchText = tbSearchText.Text.Trim().ToLower();
            SortedList<string, Item> searchedItems = new SortedList<string, Item>();
            SearchCategory(m_rootCategory, searchText, searchedItems);

            lbItemResults.BeginUpdate();
            try
            {
                lbItemResults.Items.Clear();
                if (searchedItems.Count > 0)
                {
                    foreach (Item i in searchedItems.Values)
                    {
                        lbItemResults.Items.Add(i);
                    }
                }
                else
                {
                    lbNoMatches.Visible = true;
                }
            }
            finally
            {
                lbItemResults.EndUpdate();
            }
            lbItemResults.Location = tvItems.Location;
            lbItemResults.Size = tvItems.Size;

            lbItemResults.Visible = true;
            tvItems.Visible = false;
        }

        private void SearchCategory(ItemCategory cat, string searchText, SortedList<string, Item> searchedItems)
        {
            foreach (ItemCategory tcat in cat.Subcategories)
            {
                SearchCategory(tcat, searchText, searchedItems);
            }
            foreach (Item titem in cat.Items)
            {
                if (titem.Name.ToLower().Contains(searchText))
                {
                    searchedItems.Add(titem.Name, titem);
                }
            }
        }

        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvItems.SelectedNode != null)
            {
                SetSelectedItem(tvItems.SelectedNode.Tag as Item);
            }
            else
            {
                SetSelectedItem(null);
            }
        }

        private Item m_selectedItem = null;

        public Item SelectedItem
        {
            get { return m_selectedItem; }
            set { SetSelectedItem(value); }
        }

        public event EventHandler<EventArgs> SelectedItemChanged;

        private void SetSelectedItem(Item i)
        {
            m_selectedItem = i;
            if (SelectedItemChanged != null)
                SelectedItemChanged(this, new EventArgs());
        }

        private void lbItemResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedItem(lbItemResults.SelectedItem as Item);
        }
    }
}
