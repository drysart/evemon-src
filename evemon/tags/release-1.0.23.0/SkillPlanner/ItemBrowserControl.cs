using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ItemBrowserControl : UserControl
    {
        public ItemBrowserControl()
        {
            InitializeComponent();
        }

        private Plan m_plan = null;

        public Plan Plan
        {
            get { return m_plan; }
            set { m_plan = value; }
        }

        private void itemSelectControl1_SelectedItemChanged(object sender, EventArgs e)
        {
            Item i = itemSelectControl1.SelectedItem;
            foreach (Control c in splitContainer1.Panel2.Controls)
            {
                c.Visible = (i != null);
            }
            if (i != null)
            {
                Bitmap b = new Bitmap(pbItemIcon.ClientSize.Width, pbItemIcon.ClientSize.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, b.Width, b.Height));
                }
                pbItemIcon.Image = b;

                StringBuilder sb = new StringBuilder();
                ItemCategory cat = i.ParentCategory;
                while (cat != null)
                {
                    sb.Insert(0, cat.Name);
                    cat = cat.ParentCategory;
                    if (cat != null)
                        sb.Insert(0, " > ");
                }
                lblItemCategory.Text = sb.ToString();
                lblItemName.Text = i.Name;
                lblItemDescription.Text = i.Description;
                lbItemProperties.BeginUpdate();
                try
                {
                    lbItemProperties.Items.Clear();
                    foreach (ItemProperty ip in i.Properties)
                    {
                        lbItemProperties.Items.Add(ip);
                    }
                }
                finally
                {
                    lbItemProperties.EndUpdate();
                }

                bool allKnown = true;
                allKnown = SetSkillLabel(0, lblItemSkill1, i) && allKnown;
                allKnown = SetSkillLabel(1, lblItemSkill2, i) && allKnown;
                allKnown = SetSkillLabel(2, lblItemSkill3, i) && allKnown;

                if (!allKnown)
                {
                    List<Pair<GrandSkill, int>> reqSkills = new List<Pair<GrandSkill, int>>();
                    foreach (ItemRequiredSkill irs in i.RequiredSkills)
                    {
                        Pair<GrandSkill, int> p = new Pair<GrandSkill, int>();
                        p.A = m_plan.GrandCharacterInfo.GetSkill(irs.Name);
                        p.B = irs.Level;
                        reqSkills.Add(p);
                    }
                    TimeSpan trainTime = m_plan.GrandCharacterInfo.GetTrainingTimeToMultipleSkills(reqSkills);
                    lblItemTimeRequired.Text = "Training Time: " +
                        GrandSkill.TimeSpanToDescriptiveText(trainTime, DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.SpaceText);
                    btnItemSkillsAdd.Enabled = true;
                }
                else
                {
                    lblItemTimeRequired.Text = String.Empty;
                    btnItemSkillsAdd.Enabled = false;
                }

                if (!String.IsNullOrEmpty(i.Icon))
                {
                    EveSession.GetImageAsync("http://www.eve-online.com" + i.Icon, true,
                        delegate(EveSession sess, Image img)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                if (itemSelectControl1.SelectedItem == i)
                                    GotItemImage(sess, img);
                            }));
                        });
                }
            }
        }

        private bool SetSkillLabel(int skillNum, Label lblSkill, Item i)
        {
            bool isKnown = true;
            if (i.RequiredSkills.Count <= skillNum)
            {
                lblSkill.Text = String.Empty;
                return isKnown;
            }

            ItemRequiredSkill rs = i.RequiredSkills[skillNum];
            StringBuilder sb = new StringBuilder();
            sb.Append(rs.Name);
            sb.Append(' ');
            sb.Append(GrandSkill.GetRomanSkillNumber(rs.Level));
            if (m_plan != null)
            {
                GrandCharacterInfo gci = m_plan.GrandCharacterInfo;
                GrandSkill gs = gci.GetSkill(rs.Name);
                if (gs.Level >= rs.Level)
                {
                    sb.Append(" (Known)");
                    isKnown = true;
                }
                else
                {
                    if (m_plan.IsPlanned(gs, rs.Level))
                    {
                        sb.Append(" (Planned)");
                        isKnown = true;
                    }
                    else
                    {
                        isKnown = false;
                    }
                }
            }
            lblSkill.Text = sb.ToString();
            return isKnown;
        }

        private void GotItemImage(EveSession sess, Image i)
        {
            if (i != null)
                pbItemIcon.Image = i;
        }

        private void ItemBrowserControl_Load(object sender, EventArgs e)
        {
            itemSelectControl1_SelectedItemChanged(null, null);
        }

        private void btnItemSkillsAdd_Click(object sender, EventArgs e)
        {
            Item i = itemSelectControl1.SelectedItem;
            if (i == null)
                return;

            List<Pair<string, int>> skillsToAdd = new List<Pair<string, int>>();
            foreach (ItemRequiredSkill irs in i.RequiredSkills)
            {
                skillsToAdd.Add(new Pair<string, int>(irs.Name, irs.Level));
            }
            AddPlanConfirmWindow.AddSkillsWithConfirm(m_plan, skillsToAdd);
        }
    }
}
