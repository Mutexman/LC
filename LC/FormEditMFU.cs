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
    public partial class FormEditMFU : Form
    {
        private ModeForm modeForm;
        public static ListBox ListBoxOperation = null;
        private LCTreeNodeGroup lcTreeNodeGroup = null;
        private LCTreeNodeMFU lcTreeNodeMFU = null;
        public FormEditMFU()
        {
            InitializeComponent();
        }
        public FormEditMFU(TreeNode treeNode, ModeForm modeForm)
        {
            InitializeComponent();
            this.modeForm = modeForm;
            switch (this.modeForm)
            {
                case ModeForm.Edit:
                    {
                        this.Text += "МФУ : " + this.lcTreeNodeMFU.Text;
                        this.lcTreeNodeMFU = (LCTreeNodeMFU)treeNode;
                        this.textBoxMFUName.Text = this.lcTreeNodeMFU.Text;
                        this.textBoxMFUDescription.Text = this.lcTreeNodeMFU.Text;
                        break;
                    }
                case ModeForm.New:
                    {
                        this.lcTreeNodeGroup = (LCTreeNodeGroup)treeNode;
                        this.textBoxMFUName.Text = "Новое МФУ";
                        break;
                    }
            }
        }
    }
}
