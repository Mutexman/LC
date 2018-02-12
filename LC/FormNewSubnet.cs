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
    public partial class FormNewSubnet : Form
    {
        public FormNewSubnet()
        {
            InitializeComponent();
        }
        private LCTreeNodeGroup lcTreeNodeGroup = null;
        public static ListBox ListBoxOperation = null;
        private LCTreeNode lcTreeNode = null;
         /// <summary>
        /// Возвращает созданую новую сеть в данной форме
        /// </summary>
        public TreeNode TreeNode
        {
            get
            {
                return this.lcTreeNode;
            }
        }
        public FormNewSubnet(TreeNode treeNode)
        {
            InitializeComponent();
            this.lcTreeNodeGroup = (LCTreeNodeGroup)treeNode;
            this.Text += " (в группу: " + this.lcTreeNodeGroup.Text;
        }
       
        private void buttonAddSubnet_Click(object sender, EventArgs e)
        {
            if (this.textBoxNameSubnet.Text != "")
            {
                if (this.textBoxIPSubnet.Text != "")
                {
                    string pattern = @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"(25[0-5]|2[0-4]\d|[01]?\d\d?)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(this.textBoxIPSubnet.Text);
                    // Проверяем корректно ли введен IP-адрес
                    if (match.Success)
                    {
                        this.textBoxIPSubnet.Text = match.Value;
                        if (this.textBoxMaskSubnet.Text != "")
                        {
                            match = regex.Match(this.textBoxMaskSubnet.Text);
                            if (match.Success)
                            {
                                this.textBoxMaskSubnet.Text = match.Value;
                                this.lcTreeNode = this.lcTreeNodeGroup.AddSubnet(this.textBoxNameSubnet.Text, this.textBoxIPSubnet.Text,this.textBoxMaskSubnet.Text);
                                this.Close();
                            }
                            else
                            {
                                this.labelError.Text = "Введенное в поле маска значение не корректно";
                            }
                        }
                        else
                        {
                            this.labelError.Text = "Не задана маска сети";
                        }
                    }
                    else
                    {
                        this.labelError.Text = "Введенное в поле адрес сети значение не корректно";
                    }
                }
                else
                {
                    this.labelError.Text = "Не задан IP сети";
                }
            }
            else
            {
                this.labelError.Text = "Не задано имя сети";
            }
        }
    }
}
