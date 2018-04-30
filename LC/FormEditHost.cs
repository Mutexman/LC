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
            this.NameHost = this.lcTreeNodeHost.Text;
            this.IP = this.lcTreeNodeHost.IP;
            this.Description = this.lcTreeNodeHost.Description;
        }
        private LCTreeNodeHost lcTreeNodeHost = null;
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
        /// Свойство возвращающее и принимающее имя хоста
        /// </summary>
        public string NameHost
        {
            get
            {
                return this.textBoxNameHost.Text;
            }
            set
            {
                this.textBoxNameHost.Text = value;
            }
        }
        /// <summary>
        /// Свойство возвращающее и принимающее описание хоста
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
        /// Свойство возвращающее новый созданный хост
        /// </summary>
        public TreeNode TreeNode
        {
            get
            {
                return (TreeNode)this.lcTreeNodeHost;
            }
        }
        
        #region Поиск компьютера по IP на предмет дублирования
        private bool double_;
        /// <summary>
        /// Проверяем есть ли уже хост с этим IP в справочнике
        /// </summary>
        /// <param name="treeNode">Узел дерева с которого начинаем искать</param>
        /// <param name="ip">ip-адрес</param>
        private void doubleIP(TreeNode treeNode, string ip)
        {
            LCTreeNode lcTreeNodeWork = (LCTreeNode)treeNode;
            if (lcTreeNodeWork.LCObjectType == LCObjectType.Host)
            {
                // Этот узел компьютер, приводим объект к нужному классу
                LCTreeNodeHost lcHost = (LCTreeNodeHost)lcTreeNodeWork;
                // Проверяем по IP-адресу
                if (lcHost.IP == ip)
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
        /// Проверяем есть ли уже хост с этим IP в справочнике
        /// </summary>
        /// <param name="treeNode">Узел дерева с которого начинаем искать</param>
        /// <param name="ip">ip-адрес</param>
        /// <returns>Есть ли хост с таким ip-адресом</returns>
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
            if (this.textBoxNameHost.Text != "")
            {
                this.lcTreeNodeHost.TypeHost = (LCTypeHost)Enum.Parse(typeof(LCTypeHost), this.comboBoxTypeHost.Text);
                this.lcTreeNodeHost.Text = this.textBoxNameHost.Text;
                this.lcTreeNodeHost.Description = this.textBoxDescription.Text;
                ListViewItem lvi = (ListViewItem)lcTreeNodeHost.Tag;
                lvi.SubItems[0].Text = this.comboBoxTypeHost.Text;
                lvi.SubItems[2].Text = this.lcTreeNodeHost.Text;
                lvi.SubItems[4].Text = this.lcTreeNodeHost.Description;
                this.Close();
            }
            else
            {
                this.labelErrorMessage.Text = "Не задано имя хоста";
            }
        }
    }
}
