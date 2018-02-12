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
    public partial class FormNewComputer : Form
    {
        public FormNewComputer()
        {
            InitializeComponent();
        }
        public FormNewComputer(TreeNode treeNode)
        {
            InitializeComponent();
            this.lcTreeNodeGroup  = (LCTreeNodeGroup) treeNode;
            this.Text += " (в группу: " + this.lcTreeNodeGroup.Text + " )";
        }
        private LCTreeNodeGroup lcTreeNodeGroup = null ;
        private TreeNode lcTreeNode = null;
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
        /// Свойство возвращающее новый созданный компьютер
        /// </summary>
        public TreeNode TreeNode
        {
            get
            {
                return this.lcTreeNode;
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.textBoxNameComputer.Text != "")
            {
                string pattern = @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"(25[0-5]|2[0-4]\d|[01]?\d\d?)";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(this.textBoxIP.Text);
                // Проверяем корректно ли введен IP-адрес
                if (match.Success)
                {
                    this.textBoxIP.Text = match.Value;
                    // Обнуляем счётчик компьютеров
                    // проверка нет ли уже компьютера с таким IP-адресом в справочнике
                    if (this.DoubleIP(FormNewComputer.treeView.Nodes[0], this.textBoxIP.Text))
                    {
                        if (MessageBox.Show("Компьютер с таким IP уже имеется в справочнике! Все равно добавить ?", "Линейный специалист",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    // создаем новый узел Computer
                    LCTreeNode temp = this.lcTreeNodeGroup.AddComputer(this.textBoxNameComputer.Text, this.textBoxIP.Text, this.textBoxDescription.Text);
                    //treeView.SelectedNode = this.lcTreeNodeGroup.AddComputer(this.textBoxNameComputer.Text, this.textBoxIP.Text, this.textBoxDescription.Text);
                    treeView.SelectedNode = temp;
                    this.lcTreeNode = temp;
                    this.Close();
                }
                else
                {
                    this.labelErrorMessage.Text = "Не верно задан IP адрес";
                }
            }
            else
            {
                this.labelErrorMessage.Text = "Не задано имя компьютера";
            }
        }
    }
}
