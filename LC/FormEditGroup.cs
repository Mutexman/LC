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
            switch (this.modeForm)
            {
                case ModeForm.Edit:
                    {
                        this.textBoxNameGroup.Text = this.lcTreeNodeGroup.Text;
                        this.textBoxDescription.Text = this.lcTreeNodeGroup.Description;
                        this.buttonEditGroup.Text = "Сохранить";
                        break;
                    }
                case ModeForm.New:
                    {
                        this.textBoxNameGroup.Text = "Новая группа";
                        this.buttonEditGroup.Text = "Добавить";
                        break;
                    }
            }
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
        private void ButtonSaveGroup_Click(object sender, EventArgs e)
        {
            if (this.textBoxNameGroup.Text != "")
            {
                switch (this.modeForm)
                {
                    case ModeForm.New:
                        {
                            this.lcTreeNode = this.lcTreeNodeGroup.AddGroup(this.textBoxNameGroup.Text, this.textBoxDescription.Text);
                            break;
                        }
                    case ModeForm.Edit:
                        {
                            this.lcTreeNodeGroup.Text = this.textBoxNameGroup.Text;
                            this.lcTreeNodeGroup.Description = this.textBoxDescription.Text;
                            this.lcTreeNodeGroup.ToolTipText = this.textBoxNameGroup.Text;
                            this.lcTreeNodeGroup.ToolTipText += "\n" + this.textBoxDescription.Text;

                            ListViewItem lvi = (ListViewItem)lcTreeNodeGroup.Tag;
                            lvi.SubItems[0].Text = this.lcTreeNodeGroup.Text;
                            lvi.SubItems[2].Text = this.lcTreeNodeGroup.DescriptionStr;
                            
                            break;
                        }
                }
                this.Close();
            }
            else
            {
                this.labelErrorMessage.Text = "Не задано имя группы";
            }
        }
    }
}