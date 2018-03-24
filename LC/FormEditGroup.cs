using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LC
{
    public enum ModeForm
    {
        Edit,
        New
    };
    public partial class FormEditGroup : Form
    {
        public FormEditGroup()
        {
            InitializeComponent();
        }
        public FormEditGroup(TreeNode treeNode, LC.ModeForm modeForm)
        {
            InitializeComponent();
            this.modeForm = modeForm;
            this.lcTreeNodeGroup = (LCTreeNodeGroup)treeNode;
            this.Text += " (в группу: " + this.lcTreeNodeGroup.Text;
        }
        private ModeForm modeForm;
        private LCTreeNodeGroup lcTreeNodeGroup = null;
        private LCTreeNodeGroup lcTreeNode = null;
        public static ListBox ListBoxOperation = null;
        /// <summary>
        /// Возвращает созданую новую группу в данной форме
        /// </summary>
        public TreeNode TreeNode
        {
            get
            {
                return this.lcTreeNode;
            }
        }
        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            if (this.textBoxNameGroup.Text != "")
            {
                this.lcTreeNode = this.lcTreeNodeGroup.AddGroup(this.textBoxNameGroup.Text, this.textBoxDescription.Text, "");
                this.Close();
            }
            else
            {
                this.labelErrorMessage.Text = "Не задано имя группы";
            }
        }
    }
}