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
    public partial class FormNewGroup : Form
    {
        public FormNewGroup()
        {
            InitializeComponent();
        }
        private LCTreeNodeGroup lcTreeNodeGroup = null;
        public static ListBox ListBoxOperation = null;
        private LCTreeNodeGroup lcTreeNode = null;
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
        public FormNewGroup(TreeNode treeNode)
        {
            InitializeComponent();
            this.lcTreeNodeGroup = (LCTreeNodeGroup)treeNode;
            this.Text += " (в группу: " + this.lcTreeNodeGroup.Text;
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
