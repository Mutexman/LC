using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Runtime.CompilerServices;

namespace LC
{
    public partial class FormMain : Form
    {
        // ссылка на найденную подсеть
        private TreeNode findSubnet= null;
        public static string User = "";
        public static string Password = "";
        /// <summary>
        /// Переменная содержащая имя файла в котором храниться справочник
        /// </summary>
        private string fileData = Application.LocalUserAppDataPath + "\\LCDirectory.xml";
        /// <summary>
        /// Переменная содержащая имя файла конфигурации кнопок
        /// </summary>
        private string fileConfigComputers = Application.LocalUserAppDataPath + "\\configComputers.xml";
        private string fileConfigMFU = Application.LocalUserAppDataPath + "\\configMFU.xml";
        private string fileConfigETCO = Application.LocalUserAppDataPath + "\\configETCO.xml";
        private string fileConfigSPD = Application.LocalUserAppDataPath + "\\configSPD.xml";
        private BufferLCTreeNode buffer = new BufferLCTreeNode();
        // Поле для сохранения найденых компьютеров
        private static int countFind = 0;
        private LCDirectory lCDirectory = null;
        public FormMain()
        {
            InitializeComponent();
            if (Properties.Settings.Default.FullScreen)
            {
                this.WindowState = FormWindowState.Maximized;
                this.ToolStripMenuItemFullScreen.Checked = true;
            }
            if (Properties.Settings.Default.VisibleProtocol)
            {
                this.splitContainer1.Panel2Collapsed = false;
                this.ToolStripMenuItemVisibleProtocol.Checked = true;
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = true;
                this.ToolStripMenuItemVisibleProtocol.Checked = false;
            }
            this.toolStripComputers.Hide();
            this.toolStripMFU.Hide();
            this.toolStripETCO.Hide();
            this.toolStripSPD.Hide();
            this.CreateCommandButtons(this.fileConfigComputers, this.toolStripComputers, this.listViewHosts);
            this.CreateCommandButtons(this.fileConfigMFU, this.toolStripMFU, this.listViewHosts);
            this.CreateCommandButtons(this.fileConfigETCO, this.toolStripETCO, this.listViewHosts);
            this.CreateCommandButtons(this.fileConfigSPD, this.toolStripSPD, this.listViewHosts);
        }

        #region Панели инструментов
        /// <summary>
        /// Метод создания коммандных кнопок в ToolStrip
        /// </summary>
        /// <param name="fileName">Имя файла хранения настроек кнопок.</param>
        /// <param name="toolStrip">Панель в которой будут создаваться кнопки.</param>
         public void CreateCommandButtons(string fileName,ToolStrip toolStrip,ListView listView)
        {
            // Загрузка файла в объект XmlDocument
            XmlDocument xd = new XmlDocument();
            if (File.Exists(fileName))
            {
                xd.Load(fileName);
            }
            else
            {
                // Здесь надо бы создавать пустой файл командных кнопок
                string xmlStr = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                xmlStr += "<Buttons></Buttons>";
                xd.LoadXml(xmlStr);
            }
            XmlNode xn = xd.DocumentElement;
            if (xn.NodeType == XmlNodeType.Element)
            {
                if (xn.Name == "Buttons")
                {
                    CommandToolStripButton progBut = null;
                    XmlNode workxn = xn.FirstChild;
                    while (workxn != null)
                    {
                        if (workxn.Name == "Button")
                        {
                            string text = "";
                            string command = "";
                            string parameters = "";
                            string toolTipText = "";
                            if (workxn.ChildNodes[0].ChildNodes[0] != null)
                            {
                                text = workxn.ChildNodes[0].ChildNodes[0].Value;
                            }
                            if (workxn.ChildNodes[1].ChildNodes[0] != null)
                            {
                                command = workxn.ChildNodes[1].ChildNodes[0].Value;
                            }
                            if (workxn.ChildNodes[2].ChildNodes[0] != null)
                            {
                                parameters = workxn.ChildNodes[2].ChildNodes[0].Value;
                            }
                            if (workxn.ChildNodes[3].ChildNodes[0] != null)
                            {
                                toolTipText = workxn.ChildNodes[3].ChildNodes[0].Value;
                            }
                            progBut = new CommandToolStripButton(text, command, parameters, toolTipText, true);
                            progBut.ImageScaling = ToolStripItemImageScaling.None;
                            progBut.listItems = listView;
                            toolStrip.Items.Add(progBut);
                        }
                        workxn = workxn.NextSibling;
                    }
                }
            }
        }
        #endregion

        #region События формы
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Выводим в заголовок номер версии программы
            this.Text += " " + Application.ProductVersion;
            // Считываем из настроек данные о пользователе
            FormMain.User = Properties.Settings.Default.User;
            FormMain.Password = Properties.Settings.Default.Password;
            // Выводим имя пользователя в заголовок формы
            this.Text += " Пользователь: " + FormMain.User;
            LCTreeNode.SetListBoxOperation(this.listBoxOperation);
            LCTreeNode.rootContextMenuStrip = this.contextMenuStripLCRoot;
            LCTreeNode.groupContextMemuStrip = this.contextMenuStripLCGroup;
            LCTreeNode.computerContextMenuStrip = this.contextMenuStripLCComputer;
            LCTreeNode.subnetContextMenuStrip = this.contextMenuStripLCSubnet;
            LCTreeNode.noListContextMenuStrip = this.contextMenuStripNoList;
            LCTreeNode.StatusLabel = this.toolStripStatusLabelMain;
            CommandToolStripButton.StatusLabel = this.toolStripStatusLabelMain;
            CommandToolStripButton.listBoxMessage = this.listBoxOperation;
            CommandToolStripButton.tabControl = this.tabControlObject;
            FormEditHost.treeView = this.treeViewObject;
            LCDirectory.treeView = this.treeViewObject;
            LCDirectory.listBox = this.listBoxOperation;
            LCDirectory.toolStripStatusLabel = this.toolStripStatusLabelMain;
            this.lCDirectory = new LCDirectory();
            this.lCDirectory.CreateDOMXML(this.fileData);
            // Восстанавливаем открытые вкладки
            this.OpenSavedPages();
            // Открываем компьютер ip адрес которого был передан в параметрах командной строки
            if (Environment.GetCommandLineArgs().Length >= 4)
            {
                this.toolStripTextBoxIP.Text = Environment.GetCommandLineArgs()[3];
                this.toolStripButtonFind_Click(null, null);
            }
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Сохраняем справочник
            this.lCDirectory.SaveDomXML();
            // Сохраняем открытые вкладки
            this.SaveOpenedPages();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите завершить работу приложения ? ",
                    "Линейный специалист", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Главная панель инструментов
        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            // здесь надо провести проверку корректности введенного значения IP-адреса
            string st = this.toolStripTextBoxIP.Text;
            if (this.lCDirectory.CorrectIP(ref st))
            {
                this.toolStripTextBoxIP.Text = st;
                this.WriteListBox("Поиск компьютера с IP " + this.toolStripTextBoxIP.Text + " запущен.");
                // проверяем на всякий пожарный, не пустое ли дерево справочника
                if (this.treeViewObject.Nodes.Count > 0)
                {
                    countFind = 0;
                    this.FindHost_IP(this.treeViewObject.Nodes[0], this.toolStripTextBoxIP.Text,true);
                }
                else
                {
                    return;
                }
                this.WriteListBox("Поиск завершён. Найдено : " + countFind.ToString());
                // Ищем принадлежность ПК к какой либо сети
                if (countFind == 0)
                {
                    this.findSubnet = null;
                    this.FindSubnet_IP(this.treeViewObject.Nodes[0], this.toolStripTextBoxIP.Text);
                    if (this.findSubnet != null)
                    {
                        LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet) this.findSubnet;
                        lcSubnet.AddHost(this.toolStripTextBoxIP.Text, this.toolStripTextBoxIP.Text, "");
                        // и сразу же выделяем этот объект
                        countFind = 0;
                        this.FindHost_IP(this.findSubnet, this.toolStripTextBoxIP.Text, true);
                    }
                    else
                    {
                        LCTreeNodeNoList lcNoList = (LCTreeNodeNoList) this.lCDirectory.ReturnGroupNoList();
                        lcNoList.AddHost(this.toolStripTextBoxIP.Text, this.toolStripTextBoxIP.Text, "");
                        // и сразу же выделяем этот объект
                        countFind = 0;
                        this.FindHost_IP(this.lCDirectory.ReturnGroupNoList(), this.toolStripTextBoxIP.Text,true);
                    }
                }
            }
            else
            {
                this.WriteListBox("Введенное значение не является IP адресом");
            }
        }
        private void toolStripTextBoxIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.toolStripButtonFind_Click(sender, e);
            }
        }

        /// <summary>
        /// Поиск хостов по IP
        /// </summary>
        /// <param name="treeNode">Узел дерева с которого начинаем искать.</param>
        /// <param name="ip">ip-адрес.</param>
        /// <param name="openhost">Открывать хост в listview или нет.</param>
        private void FindHost_IP(TreeNode treeNode, string ip, bool openhost)
        {
            LCTreeNode lcTreeNodeWork = (LCTreeNode)treeNode;
            if (lcTreeNodeWork.LCObjectType == LCObjectType.Host)
            {
                // Этот узел компьютер, приводим объект к нужному классу
                LCTreeNodeHost lcHost = (LCTreeNodeHost)lcTreeNodeWork;
                // Проверяем по IP-адресу
                if (lcHost.IP == ip)
                {
                    // Делаем активным компьютер в дереве справочника
                    this.treeViewObject.SelectedNode = lcHost;

                    if (openhost)
                    {
                        this.openLCTreeNode(lcHost);
                    }

                    countFind++;
                    this.WriteListBox("Найден хост с именем: " + lcHost.Text + ".");
                }
            }
            else
            {
                // рекурсивный перебор всех дочерних узлов
                foreach (TreeNode treeNodeWorking in treeNode.Nodes)
                {
                    this.FindHost_IP(treeNodeWorking, ip, openhost);
                }
            }
        }
        /// <summary>
        /// Поиск сети по IP
        /// </summary>
        /// <param name="treeNode">Узел дерева с которго начинаем искать</param>
        /// <param name="ip">ip-адрес</param>
        private void FindSubnet_IP(TreeNode treeNode, string ip)
        {
            LCTreeNode lcTreeNodeWork = (LCTreeNode)treeNode;
            if (lcTreeNodeWork.LCObjectType == LCObjectType.SubNet)
            {
                // Этот узел сеть, приводим объект к нужному классу
                LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet)lcTreeNodeWork;
                // Проверяем принадлежит ли IP адрес сети
                if (lcSubnet.CompareIPtoSubnet(ip))
                {
                    this.findSubnet = lcTreeNodeWork;
                    this.WriteListBox("IP адрес " + ip + " принадлежит сети " + lcSubnet.Text);
                    return;
                }
            }
            // рекурсивный перебор всех дочерних узлов
            foreach (TreeNode treeNodeWorking in treeNode.Nodes)
            {
                this.FindSubnet_IP(treeNodeWorking, ip);
            }
        }
        private void toolStripButtonPasteClipboard_Click(object sender, EventArgs e)
        {
            this.toolStripTextBoxIP.Text = Clipboard.GetText(TextDataFormat.Text);
        }
        #endregion

        #region Главное меню
        //Файл
        private void toolStripMenuItemClearPCList_Click(object sender, EventArgs e)
        {
            this.listViewHosts.Items.Clear();
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Вид
        /// <summary>
        /// Включение/отключение отображения протокола внизу главного окна приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemVisibleProtocol_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.VisibleProtocol)
            {
                Properties.Settings.Default.VisibleProtocol = false;
                this.ToolStripMenuItemVisibleProtocol.Checked = false;
                this.splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                Properties.Settings.Default.VisibleProtocol = true;
                this.ToolStripMenuItemVisibleProtocol.Checked = true;
                this.splitContainer1.Panel2Collapsed = false;
            }
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Включение отображения главного окна приложения на весь рабочий стол
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemFullScreen_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.FullScreen = this.checkBoxfullScreen.Checked;
            if (Properties.Settings.Default.FullScreen)
            {
                Properties.Settings.Default.FullScreen = false;
                this.ToolStripMenuItemFullScreen.Checked = false;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                Properties.Settings.Default.FullScreen = true;
                this.ToolStripMenuItemFullScreen.Checked = true;
                this.WindowState = FormWindowState.Maximized;
            }
            Properties.Settings.Default.Save();
        }
        //Сервис
        private void опцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }
        private void компьютерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton(this.fileConfigComputers);
            formSettingCommandButton.Text += " : Компьютер";
            formSettingCommandButton.ShowDialog();
        }
        private void мФУToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton(this.fileConfigMFU);
            formSettingCommandButton.Text += " : МФУ";
            formSettingCommandButton.ShowDialog();
        }
        private void эТСОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton(this.fileConfigETCO);
            formSettingCommandButton.Text += " : ЭТСО";
            formSettingCommandButton.ShowDialog();
        }
        private void сПДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton(this.fileConfigSPD);
            formSettingCommandButton.Text += " : СПД";
            formSettingCommandButton.ShowDialog();
        }
        private void учётнаяЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
            FormMain.User = formLogin.User;
            FormMain.Password = formLogin.Password;
            // Выводим в заголовок номер версии программы
            this.Text = "Линейный специалист " + Application.ProductVersion;
            // Выводим имя пользователя в заголовок формы
            this.Text += " Пользователь: " + FormMain.User;
        }
        //Помощь
        private void проверкаОбновленийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пока данная функция не реализована", "Линейный специалист", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.WriteListBox("Пока данная функция не реализована!");
        }
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // открываем в браузере ссылку по которой находиться справка по работе с программой
            // System.Diagnostics.Process.Start(Properties.Settings.Default.HelpLink);
            System.Diagnostics.Process.Start(Application.StartupPath + "\\LCHelp.chm");

        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }
        #endregion

        #region Действия приложения. Дерево справочника.
        /// <summary>
        /// Событие открытие какого либо LC объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLCTreeNode(object sender, EventArgs e)
        {
            this.openLCTreeNode(this.treeViewObject.SelectedNode);
        }
        /// <summary>
        /// Метод открытия узла дерева.
        /// </summary>
        /// <param name="treeNode">Открываемый узел.</param>
        private void openLCTreeNode(TreeNode treeNode)
        {
            if (treeNode == null)
                return;

            LCTreeNode lcTreeNode = (LCTreeNode)treeNode;

            switch (lcTreeNode.LCObjectType)
            {
                case LCObjectType.Host:
                    {
                        this.tabControlObject.SelectedTab = this.tabPageHosts;
                        LCTreeNodeHost lcHost = (LCTreeNodeHost)lcTreeNode;
                        // Делаем активным список хостов
                        this.listViewHosts.Focus();
                        foreach (ListViewItem curilv in this.listViewHosts.Items)
                        {
                            if (curilv.Tag == lcHost)
                            {
                                // Выделяем нужный элемент в списке
                                curilv.Selected = true;
                                return;
                            }
                        }
                        // определяем родительскую группу элемента
                        LCTreeNode lcNode = (LCTreeNode)lcHost.Parent;
                        // Пока сделано так, в будущем предполагаются что ПК состоят только в сетях. Кроме группы <Не в списке>
                        if (lcNode.LCObjectType == LCObjectType.SubNet)
                        {
                            ListViewGroup lvg = null;
                            foreach (ListViewGroup curGroup in this.listViewHosts.Groups)
                            {
                                if (curGroup.Tag == lcNode)
                                {
                                    // Группа существует
                                    lvg = curGroup;
                                    break;
                                }
                            }
                            if (lvg == null)
                            {
                                // Такой группы еще нет, надо её создать
                                lvg = new ListViewGroup(lcNode.Text);
                            }
                            lvg.Tag = lcNode;
                            this.listViewHosts.Groups.Add(lvg);
                            ListViewItem lvi = new ListViewItem(new string[] { lcHost.TypeHost.ToString(), lcHost.IP, lcHost.Text, lcHost.ParentGroup, lcHost.DescriptionStr}, lvg)
                            {
                                Tag = lcHost
                            };
                            lcHost.Tag = lvi;
                            this.listViewHosts.Items.Add(lvi);
                            lvi.Selected = true;
                        }
                        else
                        {
                            ListViewItem lvi = new ListViewItem(new string[] { lcHost.TypeHost.ToString(), lcHost.IP, lcHost.Text, lcHost.ParentGroup, lcHost.DescriptionStr })
                            {
                                Tag = lcHost
                            };
                            lcHost.Tag = lvi;
                            this.listViewHosts.Items.Add(lvi);
                            lvi.Selected = true;
                        }
                        break;
                    }
                case LCObjectType.SubNet:
                    {
                        this.tabControlObject.SelectedTab = this.tabPageSubnets;
                        this.listViewSubnets.Focus();
                        LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet)lcTreeNode;
                        foreach (ListViewItem cutilv in this.listViewSubnets.Items)
                        {
                            if (cutilv.Tag == lcSubnet)
                            {
                                cutilv.Selected = true;
                                return;
                            }
                        }
                        ListViewItem lvi = new ListViewItem(new string[] { lcSubnet.Text, lcSubnet.IPSubnet,
                            lcSubnet.MaskSubnet,lcSubnet.ParentGroup,lcSubnet.DescriptionStr })
                        {
                            Tag = lcSubnet
                        };
                        lcSubnet.Tag = lvi;
                        this.listViewSubnets.Items.Add(lvi);
                        lvi.Selected = true;
                        break;
                    }
                case LCObjectType.Group:
                    {
                        this.tabControlObject.SelectedTab = this.tabPageGroups;
                        this.listViewGroups.Focus();
                        LCTreeNodeGroup lcGroup = (LCTreeNodeGroup)lcTreeNode;
                        foreach (ListViewItem curilv in this.listViewGroups.Items)
                        {
                            if (curilv.Tag == lcGroup)
                            {
                                curilv.Selected = true;
                                return;
                            }
                        }
                        ListViewItem lvi = new ListViewItem(new string[] { lcGroup.Text, lcGroup.ParentGroup, lcGroup.DescriptionStr })
                        {
                            Tag = lcGroup
                        };
                        lcGroup.Tag = lvi;
                        this.listViewGroups.Items.Add(lvi);
                        lvi.Selected = true;
                        break;
                    }
            }
        }
        /// <summary>
        /// Отображение всех дочерних узлов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemOpenNodes(object sender, EventArgs e)
        {
            this.treeViewObject.SelectedNode.ExpandAll();
        }
        /// <summary>
        /// Событие создания новой группы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewGroup(object sender, EventArgs e)
        {
            FormEditGroup formNewGroup = new FormEditGroup(this.treeViewObject.SelectedNode, ModeForm.New);
            formNewGroup.ShowDialog();
            this.treeViewObject.Sort();
            if (formNewGroup.TreeNode != null)
            {
                // Выделяем только что созданную группу в дереве справочника
                this.treeViewObject.SelectedNode = formNewGroup.TreeNode;
            }
        }
        /// <summary>
        /// Событие создания новой сети
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewSubnet(object sender, EventArgs e)
        {
            FormEditSubnet formNewSubnet = new FormEditSubnet(this.treeViewObject.SelectedNode, ModeForm.New);
            formNewSubnet.ShowDialog();
            this.treeViewObject.Sort();
            if (formNewSubnet.TreeNode != null)
            {
                // Выделяем только что созданную сеть в дереве справочника
                this.treeViewObject.SelectedNode = formNewSubnet.TreeNode;
            }
        }
        /// <summary>
        /// Событие редактирования объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editLCTreeNode(object sender, EventArgs e)
        {
            LCTreeNode tn = (LCTreeNode)this.treeViewObject.SelectedNode;
            switch (tn.LCObjectType)
            {
                case LCObjectType.Host:
                    {
                        FormEditHost formEditComputer = new FormEditHost(this.treeViewObject.SelectedNode);
                        formEditComputer.ShowDialog();
                        break;
                    }
                case LCObjectType.Group:
                    {
                        FormEditGroup formEditGroup = new FormEditGroup(this.treeViewObject.SelectedNode, ModeForm.Edit);
                        formEditGroup.ShowDialog();
                        break;
                    }
                case LCObjectType.SubNet:
                    {
                        FormEditSubnet formEditSubnet = new FormEditSubnet(this.treeViewObject.SelectedNode, ModeForm.Edit);
                        formEditSubnet.ShowDialog();
                        break;
                    }
            }
            this.treeViewObject.Sort();
        }
        /// <summary>
        /// Событие удаления объекта LC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteLCTreeNode(object sender, EventArgs e)
        {
            string tempStr = "";
            LCTreeNode lcTreeNode = (LCTreeNode)this.treeViewObject.SelectedNode;
            tempStr = lcTreeNode.Text;
            // Проверяем есть ли у этого узла дочерние объекты
            if (lcTreeNode.Nodes.Count > 0)
            {
                // надо перебрать все узлы
                if (MessageBox.Show("Объект имеет дочерние узлы! Все равно удалить?", "Удаление", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    // Здесь нужен код для удаления из таблиц объектов, которые являются дочерними по отношению к удаляемому объекту

                    // Удаляем текущий
                    //lcTreeNode.Remove();
                    MessageBox.Show("Пока не реализовано удаление элементов с дочерними объектами!");
                    // Сообщаем об удалении
                    this.WriteListBox("Группа " + tempStr + " и её дочерние объекты удалены.");
                }
            }
            else
            {
                if (MessageBox.Show("Вы дейстительно хотите удалить этот объект ?", "Удаление", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    switch(lcTreeNode.LCObjectType)
                    {
                        case LCObjectType.Host:
                            {
                                LCTreeNodeHost lcHost = (LCTreeNodeHost)lcTreeNode;
                                lcHost.RemoveLC();
                                tempStr = "Компьютер: " + tempStr + " удалён.";
                                // Сообщаем об удалении
                                this.WriteListBox(tempStr);
                                break;
                            }
                        case LCObjectType.SubNet:
                            {
                                LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet)lcTreeNode;
                                lcSubnet.RemoveLC();
                                tempStr = "Сеть: " + tempStr + " удалена.";
                                this.WriteListBox(tempStr);
                                break;
                            }
                        case LCObjectType.Group:
                            {
                                LCTreeNodeGroup lcGroup = (LCTreeNodeGroup)lcTreeNode;
                                lcGroup.RemoveLC();
                                tempStr = "Группа: " + tempStr + " удалена.";
                                this.WriteListBox(tempStr);
                                break;
                            }
                        case LCObjectType.NoList:
                            {
                                LCTreeNodeNoList lcNoList = (LCTreeNodeNoList)lcTreeNode;
                                lcNoList.Remove();
                                tempStr = "Группа: " + tempStr + " удалена.";
                                this.WriteListBox(tempStr);
                                break;
                            }
                    }
                }
            }
        }
        /// <summary>
        /// Событие вырезания объекта в буфер обмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutLCTreeNode(object sender, EventArgs e)
        {
            LCTreeNode lc = (LCTreeNode)this.treeViewObject.SelectedNode;
            this.buffer.InBuffer(lc);
            lc.Remove();
            this.WriteListBox("Объект " + lc.Text + " помещен в буфер");
        }
        /// <summary>
        /// Вставка объекта из буфера обмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteLCTreeNode(object sender, EventArgs e)
        {
            if (this.buffer.LCTreeNode != null)
            {
                this.treeViewObject.SelectedNode.Nodes.Add(this.buffer.OutBuffer());
                this.WriteListBox("Объект успешно перемещен");
            }
            else
            {
                this.WriteListBox("Буфер пуст");
            }
        }
        /// <summary>
        /// Метод переопределения сети для хостов из группы "Не в списке"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemFindSubnet_Click(object sender, EventArgs e)
        {
            List<String> list = new List<String>();
            TreeNode node = this.lCDirectory.ReturnGroupNoList();
            foreach (TreeNode tn in node.Nodes)
            {
                LCTreeNodeHost lc = (LCTreeNodeHost)tn;
                list.Add(lc.IP);
                if (lc.Tag != null)
                {
                    ((ListViewItem)lc.Tag).Remove();
                }
            }
            // Удаляем группу "Не в списке"
            node.Remove();
            // проверяем на всякий пожарный, не пустое ли дерево справочника
            if (this.treeViewObject.Nodes.Count > 0)
            {
                this.treeViewObject.BeginUpdate();
                foreach (string st in list)
                {
                    // Ищем принадлежность ПК к какой либо сети
                    this.findSubnet = null;
                    this.FindSubnet_IP(this.treeViewObject.Nodes[0], st);
                    if (this.findSubnet != null)
                    {
                        LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet)this.findSubnet;
                        lcSubnet.AddHost(st, st, "");
                        // и сразу же выделяем этот объект
                        countFind = 0;
                        this.FindHost_IP(this.findSubnet, st, true);
                    }
                    else
                    {
                        LCTreeNodeNoList lcNoList = (LCTreeNodeNoList)this.lCDirectory.ReturnGroupNoList();
                        lcNoList.AddHost(st, st, "");
                        // и сразу же выделяем этот объект
                        countFind = 0;
                        this.FindHost_IP(this.lCDirectory.ReturnGroupNoList(), st, true);
                    }
                }
                this.treeViewObject.EndUpdate();
            }
        }
        #endregion

        #region Прочие методы
        
        /// <summary>
        /// Вывод сообщения в нижний ListBox компонент
        /// </summary>
        /// <param name="message">Текст сообщений</param>
        public void WriteListBox(string message)
        {
            this.listBoxOperation.Items.Add("[" + DateTime.Now.ToString() + "] " + message);
            this.listBoxOperation.SelectedIndex = this.listBoxOperation.Items.Count - 1;
            this.toolStripStatusLabelMain.Text = message;
        }
        #endregion

        #region Запоминание открытых вкладок
        /// <summary>
        /// Метод открывает вкладки которые не были закрыты при прошлом запуске программы
        /// </summary>
        private void OpenSavedPages()
        {
            if (this.treeViewObject.Nodes != null)
            {
                string[] str = Properties.Settings.Default.OpenNets.Split(';');
                foreach (string st in str)
                {
                    this.openLCTreeNode(this.lCDirectory.FindNet(st));
                }
                str = Properties.Settings.Default.OpenGroups.Split(';');
                foreach (string st in str)
                {
                    this.openLCTreeNode(this.lCDirectory.FindGroup(st));
                }
                str = Properties.Settings.Default.OpenHosts.Split(';');
                foreach (string st in str)
                {
                    this.FindHost_IP(this.treeViewObject.Nodes[0], st, true);
                }
            }
        }
        /// <summary>
        /// Сохранение не закрытых вкладок
        /// </summary>
        private void SaveOpenedPages()
        {
            Properties.Settings.Default.OpenHosts = "";
            foreach (ListViewItem lvi in this.listViewHosts.Items)
            {
                Properties.Settings.Default.OpenHosts += lvi.SubItems[1].Text + ";";
            }
            Properties.Settings.Default.OpenNets = "";
            foreach (ListViewItem lvi in this.listViewSubnets.Items)
            {
                Properties.Settings.Default.OpenNets += lvi.SubItems[0].Text + ";";
            }
            Properties.Settings.Default.OpenGroups = "";
            foreach (ListViewItem lvi in this.listViewGroups.Items)
            {
                Properties.Settings.Default.OpenGroups += lvi.SubItems[0].Text + ";";
            }
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Действия приложения. Список хостов.
        private void listViewHosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                LCTreeNodeHost lcHost = (LCTreeNodeHost)this.listViewHosts.SelectedItems[0].Tag;
                LCTypeHost lcTypeHost = lcHost.TypeHost;
                // Выделяем узел в дереве.
                this.treeViewObject.SelectedNode = lcHost;
                switch (lcTypeHost)
                {
                    case LCTypeHost.COMPUTER:
                        {
                            this.toolStripComputers.Show();
                            this.toolStripETCO.Hide();
                            this.toolStripMFU.Hide();
                            this.toolStripSPD.Hide();
                        }
                        break;
                    case LCTypeHost.ETCO:
                        {
                            this.toolStripComputers.Hide();
                            this.toolStripETCO.Show();
                            this.toolStripMFU.Hide();
                            this.toolStripSPD.Hide();
                        }
                        break;
                    case LCTypeHost.HOST:
                        {
                            this.toolStripComputers.Show();
                            this.toolStripETCO.Hide();
                            this.toolStripMFU.Hide();
                            this.toolStripSPD.Hide();
                        }
                        break;
                    case LCTypeHost.MFU:
                        {
                            this.toolStripComputers.Hide();
                            this.toolStripETCO.Hide();
                            this.toolStripMFU.Show();
                            this.toolStripSPD.Hide();
                        }
                        break;
                    case LCTypeHost.SPD:
                        {
                            this.toolStripComputers.Hide();
                            this.toolStripETCO.Hide();
                            this.toolStripMFU.Hide();
                            this.toolStripSPD.Show();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Удаление списка элемента по нажатии клавиши DELETE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonGetNamePC_Click(object sender, EventArgs e)
        {
            string ipStr;
            // Пока не понятно как определить что выделена какая либо строка в listView
            try
            {
                ipStr = this.listViewHosts.SelectedItems[0].SubItems[1].Text;
            }
            catch (System.ArgumentOutOfRangeException myException)
            {
                this.WriteListBox(myException.Message);
                this.WriteListBox("Не выделен компьютер для подключения !");
                return;
            }
            try
            {
                IPAddress ip = IPAddress.Parse(ipStr);
                IPHostEntry host = Dns.GetHostEntry(ip);
                string hostName = host.HostName;
                this.listViewHosts.SelectedItems[0].SubItems[2].Text = hostName;
                ((LCTreeNodeHost)this.listViewHosts.SelectedItems[0].Tag).Text = hostName;
                this.WriteListBox("Имя ПК с IP " + ipStr + " @ " + hostName);
            }
            catch (System.Exception myException)
            {
                this.WriteListBox("Определение имени для ПК с IP " + ipStr + ":" + myException.Message);
            }
        }
   
        private void listViewComputers_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                LCTreeNodeHost tn = (LCTreeNodeHost)this.listViewHosts.SelectedItems[0].Tag;
                FormEditHost formNewComputer = new FormEditHost(tn);
                formNewComputer.ShowDialog();
            }
        }

        private void listViewHosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.listViewHosts.SelectedItems.Count > 0)
                {
                    this.listViewHosts.SelectedItems[0].Remove();
                }
            }
        }

        private void toolStripMenuItemGetHostIP_Click(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                string ip = this.listViewHosts.SelectedItems[0].SubItems[1].Text;
                Clipboard.SetText(ip);
            }
        }

        private void toolStripMenuItemGetHostName_Click(object sender, EventArgs e)
        {
            string n = this.listViewHosts.SelectedItems[0].SubItems[2].Text;
            // берем имя ПК до первой точки
            n = n.Substring(0, n.IndexOf('.'));
            Clipboard.SetText(n);
        }

        private void toolStripMenuItemGetHostFullName_Click(object sender, EventArgs e)
        {
            string n = this.listViewHosts.SelectedItems[0].SubItems[2].Text;
            Clipboard.SetText(n);
        }

        #endregion

        #region Действия приложения. Список сетей.
        private void listViewSubnets_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewSubnets.SelectedItems.Count > 0)
            {
                LCTreeNodeSubnet tn = (LCTreeNodeSubnet)this.listViewSubnets.SelectedItems[0].Tag;
                FormEditSubnet formEditSubnet = new FormEditSubnet(tn, ModeForm.Edit);
                formEditSubnet.ShowDialog();
            }
        }

        private void listViewSubnets_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.listViewSubnets.SelectedItems.Count > 0)
                {
                    this.listViewSubnets.SelectedItems[0].Remove();
                }
            }
        }
        #endregion

        #region Действия приложения. Список групп.
        private void listViewGroups_DoubleClick(object sender, EventArgs e)
        {
            if(this.listViewGroups.SelectedItems.Count > 0)
            {
                LCTreeNodeGroup tn = (LCTreeNodeGroup)this.listViewGroups.SelectedItems[0].Tag;
                FormEditGroup formEditGroup = new FormEditGroup(tn, ModeForm.Edit);
                formEditGroup.ShowDialog();
            }
        }

        private void listViewGroups_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(this.listViewGroups.SelectedItems.Count >0)
                {
                    this.listViewGroups.SelectedItems[0].Remove();
                }
            }
        }
        #endregion
    }
}