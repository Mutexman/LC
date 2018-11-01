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
    public partial class FormEditSubnet : Form
    {
        public FormEditSubnet()
        {
            InitializeComponent();
        }
        public static ListBox ListBoxOperation = null;
        private LCTreeNodeGroup lcTreeNodeGroup = null;
        private LCTreeNodeSubnet lcTreeNodeSubnet = null;
        private ModeForm modeForm;
        /// <summary>
        /// Возвращает созданую новую сеть в данной форме
        /// </summary>
        public TreeNode TreeNode
        {
            get
            {
                return this.lcTreeNodeSubnet;
            }
        }
        public FormEditSubnet(TreeNode treeNode, ModeForm modeForm)
        {
            InitializeComponent();
            this.modeForm = modeForm;
            switch (this.modeForm)
            {
                case ModeForm.Edit:
                    {
                        this.lcTreeNodeSubnet = (LCTreeNodeSubnet)treeNode;
                        this.Text = "Сеть : " + this.lcTreeNodeSubnet.Text;
                        this.textBoxNameSubnet.Text = this.lcTreeNodeSubnet.Text;
                        this.textBoxIPSubnet.Text = this.lcTreeNodeSubnet.IPSubnet;
                        this.textBoxMaskSubnet.Text = this.lcTreeNodeSubnet.MaskSubnet;
                        this.textBoxDescription.Text = this.lcTreeNodeSubnet.Description;
                        this.buttonAddSubnet.Text = "Сохранить";
                        break;
                    }
                case ModeForm.New:
                    {
                        this.lcTreeNodeGroup = (LCTreeNodeGroup)treeNode;
                        this.textBoxNameSubnet.Text = "Новая сеть";
                        break;
                    }
            }
        }
       
        private void buttonAddSubnet_Click(object sender, EventArgs e)
        {
            if (this.textBoxNameSubnet.Text != "")
            {
                string pattern = @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"(25[0-5]|2[0-4]\d|[01]?\d\d?)";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(this.textBoxIPSubnet.Text);
                // Проверяем корректно ли введен IP-адрес сети
                if (match.Success)
                {
                    this.textBoxIPSubnet.Text = match.Value;
                    match = regex.Match(this.textBoxMaskSubnet.Text);
                    if (match.Success)
                    {
                        this.textBoxMaskSubnet.Text = match.Value;
                        switch (this.modeForm)
                        {
                            case ModeForm.New:
                                {
                                    this.lcTreeNodeSubnet = this.lcTreeNodeGroup.AddSubnet(this.textBoxNameSubnet.Text, 
                                        this.textBoxIPSubnet.Text, this.textBoxMaskSubnet.Text, this.textBoxDescription.Text);
                                    break;
                                }
                            case ModeForm.Edit:
                                {
                                    this.lcTreeNodeSubnet.Text = this.textBoxNameSubnet.Text;
                                    this.lcTreeNodeSubnet.IPSubnet = this.textBoxIPSubnet.Text;
                                    this.lcTreeNodeSubnet.MaskSubnet = this.textBoxMaskSubnet.Text;
                                    this.lcTreeNodeSubnet.Description = this.textBoxDescription.Text;

                                    this.lcTreeNodeSubnet.ToolTipText = this.textBoxNameSubnet.Text;
                                    this.lcTreeNodeSubnet.ToolTipText += "\n" + this.textBoxIPSubnet.Text;
                                    this.lcTreeNodeSubnet.ToolTipText += "\n" + this.textBoxMaskSubnet.Text;
                                    this.lcTreeNodeSubnet.ToolTipText += "\n" + this.textBoxDescription.Text;
                                    break;
                                }
                        }
                        this.Close();
                    }
                    else
                    {
                        this.labelError.Text = "Введенное в поле маска значение не корректно";
                    }
                }
                else
                {
                    this.labelError.Text = "Введенное в поле адрес сети значение не корректно";
                }
            }
            else
            {
                this.labelError.Text = "Не задано имя сети";
            }
        }
    }
}