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
        private LCTreeNodeMFU lcTreeNodeMFU = null;
        public FormEditMFU()
        {
            InitializeComponent();
        }
        public FormEditMFU(TreeNode treeNode)
        {
            InitializeComponent();
            this.Text += "МФУ : " + this.lcTreeNodeMFU.Text;
            this.lcTreeNodeMFU = (LCTreeNodeMFU)treeNode;
            this.textBoxMFUName.Text = this.lcTreeNodeMFU.Text;
            this.textBoxMFUDescription.Text = this.lcTreeNodeMFU.Text;
        }

        private void buttonMFUSave_Click(object sender, EventArgs e)
        {
            this.lcTreeNodeMFU.Text = this.textBoxMFUName.Text;
            this.lcTreeNodeMFU.Text = this.textBoxMFUDescription.Text;

            if (this.textBoxMFUName.Text != "")
            {
                this.lcTreeNodeMFU.Text = this.textBoxMFUName.Text;
                this.lcTreeNodeMFU.Description = this.textBoxMFUDescription.Text;
                ListViewItem lvi = (ListViewItem)lcTreeNodeMFU.Tag;
                lvi.SubItems[1].Text = this.lcTreeNodeMFU.Text;
                lvi.SubItems[3].Text = this.lcTreeNodeMFU.Description;
                this.Close();
            }
            else
            {
                this.labelErrorMessage.Text = "Не задано имя МФУ";
            }
        }
    }
}
