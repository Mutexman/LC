using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LC
{
    public partial class FormEditHost : Form
    {
        public FormEditHost()
        {
            InitializeComponent();
        }
        public FormEditHost(TreeNode treeNode)
        {
            InitializeComponent();
            this.lcTreeNodeHost = (LCTreeNodeHost)treeNode;

            this.comboBoxTypeHost.DataSource = Enum.GetValues(typeof(LCTypeHost)).Cast<LCTypeHost>()
            .Select(p => new { Name = Enum.GetName(typeof(LCTypeHost), p), Value = (int)p }).ToList();
            this.comboBoxTypeHost.DisplayMember = "Name";
            this.comboBoxTypeHost.ValueMember = "Name";

            this.comboBoxTypeHost.Text = lcTreeNodeHost.TypeHost.ToString();
            this.textBoxNameHost.Text = this.lcTreeNodeHost.Text;
            this.textBoxIP.Text = this.lcTreeNodeHost.IP;
            this.textBoxDescription.Text = this.lcTreeNodeHost.Description;
        }
        private LCTreeNodeHost lcTreeNodeHost = null;

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (this.textBoxNameHost.Text != "")
            {
                this.lcTreeNodeHost.TypeHost = (LCTypeHost)Enum.Parse(typeof(LCTypeHost), this.comboBoxTypeHost.Text);
                switch (lcTreeNodeHost.TypeHost)
                {
                    case LCTypeHost.HOST:
                        {
                            lcTreeNodeHost.ImageIndex = 3;
                        }
                        break;
                    case LCTypeHost.MFU:
                        {
                            lcTreeNodeHost.ImageIndex = 6;
                        }
                        break;
                    case LCTypeHost.SPD:
                        {
                            lcTreeNodeHost.ImageIndex = 7;
                        }
                        break;
                    case LCTypeHost.ETCO:
                        {
                            lcTreeNodeHost.ImageIndex = 8;
                        }
                        break;
                }
                this.lcTreeNodeHost.Text = this.textBoxNameHost.Text;
                this.lcTreeNodeHost.Description = this.textBoxDescription.Text;

                ListViewItem lvi = (ListViewItem)lcTreeNodeHost.Tag;
                if (lvi != null)
                {
                    lvi.SubItems[0].Text = this.comboBoxTypeHost.Text;
                    lvi.SubItems[2].Text = this.lcTreeNodeHost.Text;
                    lvi.SubItems[4].Text = this.lcTreeNodeHost.DescriptionStr;
                }

                lcTreeNodeHost.ToolTipText = lcTreeNodeHost.TypeHost.ToString();
                lcTreeNodeHost.ToolTipText += "\n" + lcTreeNodeHost.Text;
                lcTreeNodeHost.ToolTipText += "\n" + lcTreeNodeHost.IP;
                lcTreeNodeHost.ToolTipText += "\n" + lcTreeNodeHost.Description;
                this.Close();
            }
            else
            {
                this.labelErrorMessage.Text = "Не задано имя хоста";
            }
        }
    }
}
