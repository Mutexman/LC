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
    public partial class FormEditComputer : Form
    {
        public FormEditComputer()
        {
            InitializeComponent();
        }
        public FormEditComputer(TreeNode treeNode)
        {
            InitializeComponent();
            this.lcTreeNodeComputer = (LCTreeNodeComputer)treeNode;
            this.NameComputer = this.lcTreeNodeComputer.Text;
            this.IP = this.lcTreeNodeComputer.IP;
            this.Description = this.lcTreeNodeComputer.Description;
        }
        private LCTreeNodeComputer lcTreeNodeComputer = null;
        public static TreeView treeView = null;
        /// <summary>
        /// Свойство возвращающее и принимающее ip
        /// </summary>
        public string IP
        {
            get
            {
                return this.textBoxIP.Text;
            }
            set
            {
                this.textBoxIP.Text = value;
            }
        }
        /// <summary>
        /// Свойство возвращающее и принимающее имя компьютера
        /// </summary>
        public string NameComputer
        {
            get
            {
                return this.textBoxNameComputer.Text;
            }
            set
            {
                this.textBoxNameComputer.Text = value;
            }
        }
        /// <summary>
        /// Свойство возвращающее и принимающее описание компьютера
        /// </summary>
        public string Description
        {
            get
            {
                return this.textBoxDescription.Text;
            }
            set
            {
                this.textBoxDescription.Text = value;
            }
        }
        /// <summary>
        /// Свойство возвращающее новый созданный компьютер
        /// </summary>
        public TreeNode TreeNode
        {
            get
            {
                return (TreeNode)this.lcTreeNodeComputer;
            }
        }
        
        #region Поиск компьютера по IP на предмет дублирования
        private bool double_;
        /// <summary>
        /// Проверяем есть ли уже компьютер с этим IP в справочнике
        /// </summary>
        /// <param name="treeNode">Узел дерева с которого начинаем искать</param>
        /// <param name="ip">ip-адрес</param>
        private void doubleIP(TreeNode treeNode, string ip)
        {
            LCTreeNode lcTreeNodeWork = (LCTreeNode)treeNode;
            if (lcTreeNodeWork.LCObjectType == LCObjectType.Computer)
            {
                // Этот узел компьютер, приводим объект к нужному классу
                LCTreeNodeComputer lcComp = (LCTreeNodeComputer)lcTreeNodeWork;
                // Проверяем по IP-адресу
                if (lcComp.IP == ip)
                {
                    this.double_ = true;
                }
            }
            else
            {
                // рекурсивный перебор всех дочерних узлов
                foreach (TreeNode treeNodeWorking in treeNode.Nodes)
                {
                    this.doubleIP(treeNodeWorking, ip);
                }
            }
        }
        /// <summary>
        /// Проверяем есть ли уже компьютер с этим IP в справочнике
        /// </summary>
        /// <param name="treeNode">Узел дерева с которого начинаем искать</param>
        /// <param name="ip">ip-адрес</param>
        /// <returns>Есть ли компьютер с таким ip-адресом</returns>
        private bool DoubleIP(TreeNode treeNode, string ip)
        {
            if (treeNode == null)
            {
                return false;
            }
            this.double_ = false;
            this.doubleIP(treeNode, ip);
            return this.double_;
        }
        #endregion

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (this.textBoxNameComputer.Text != "")
            {
                this.lcTreeNodeComputer.Text = this.textBoxNameComputer.Text;
                this.lcTreeNodeComputer.Description = this.textBoxDescription.Text;
                ListViewItem lvi = (ListViewItem)lcTreeNodeComputer.Tag;
                lvi.SubItems[1].Text = this.lcTreeNodeComputer.Text;
                lvi.SubItems[3].Text = this.lcTreeNodeComputer.Description;
                this.Close();
            }
            else
            {
                this.labelErrorMessage.Text = "Не задано имя компьютера";
            }
        }
    }
}
