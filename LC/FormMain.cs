﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;

namespace LC
{
    public partial class FormMain : Form
    {
        // поле показывающее был ли корректно осуществлен ввод логина и пароля
        public static bool logged = false;
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
        //private string fileConfig = Application.LocalUserAppDataPath + "\\config.xml";
        private string fileConfigComputers = Application.LocalUserAppDataPath + "\\configComputers.xml";
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
            }
            if (Properties.Settings.Default.VisibleProtocol)
            {
                this.splitContainer1.Panel2Collapsed = true;
            }
            this.CreateCommandButtons(this.fileConfigComputers, this.toolStripComputers, this.listViewComputers);
        }
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

        #region События формы
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Выводим в заголовок номер версии программы
            this.Text += " " + Application.ProductVersion;
            // Проверяем был ли введены параметры, логин и пароль
            if(Environment.GetCommandLineArgs().Length >= 3)
            {
                // Параметр были переданы, запоминаем логин и пароль пользователя
                FormMain.User = Environment.GetCommandLineArgs()[1];
                FormMain.Password = Environment.GetCommandLineArgs()[2];
                FormMain.logged = true;
            }
            else
            {
                FormLogin formLogin = new FormLogin();
                formLogin.ShowDialog();
                if (formLogin.Accept)
                {
                    FormMain.User = formLogin.User;
                    FormMain.Password = formLogin.Password;
                    FormMain.logged = true;
                }
            }
            if (FormMain.logged)
            {
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
                FormEditComputer.treeView = this.treeViewObject;
                LCDirectory.treeView = this.treeViewObject;
                LCDirectory.listBox = this.listBoxOperation;
                LCDirectory.toolStripStatusLabel = this.toolStripStatusLabelMain;
                this.lCDirectory = new LCDirectory();
                this.lCDirectory.CreateDOM(this.fileData);
                // Восстанавливаем открытые вкладки
                this.OpenSavedPages();
                // Открываем компьютер ip адрес которого был передан в параметрах командной строки
                if (Environment.GetCommandLineArgs().Length >= 4)
                {
                    this.toolStripTextBoxIP.Text = Environment.GetCommandLineArgs()[3];
                    this.toolStripButtonFind_Click(null, null);
                }
            }
            else
            {
                this.Close();
            }
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Сохраняем справочник
            this.lCDirectory.SaveXML();
            // Сохраняем открытые вкладки
            this.SaveOpenedPages();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormMain.logged)
            {
                if (MessageBox.Show("Вы действительно хотите завершить работу приложения ? " +
                    "При следующем запуске приложения, Вам опять потребуется вводить свой логин и пароль! Рекомендуется свернуть программу на панель задач.",
                    "Линейный специалист", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Главная панель инструментов
        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            // здесь надо провести проверку корректности введенного значения IP-адреса
            if (this.CorrectIP())
            {
                this.WriteListBox("Поиск компьютера с IP " + this.toolStripTextBoxIP.Text + " запущен.");
                // проверяем на всякий пожарный, не пустое ли дерево справочника
                if (this.treeViewObject.Nodes.Count > 0)
                {
                    countFind = 0;
                    this.FindComputer_IP(this.treeViewObject.Nodes[0], this.toolStripTextBoxIP.Text);
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
                        lcSubnet.AddComputer(this.toolStripTextBoxIP.Text, this.toolStripTextBoxIP.Text, "");
                        // и сразу же выделяем этот объект
                        countFind = 0;
                        this.FindComputer_IP(this.findSubnet, this.toolStripTextBoxIP.Text);
                    }
                    else
                    {
                        LCTreeNodeNoList lcNoList = (LCTreeNodeNoList) this.ReturnGroupNoList();
                        lcNoList.AddComputer(this.toolStripTextBoxIP.Text, this.toolStripTextBoxIP.Text, "");
                        // и сразу же выделяем этот объект
                        countFind = 0;
                        this.FindComputer_IP(this.ReturnGroupNoList(), this.toolStripTextBoxIP.Text);
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
        /// Поиск компьютера по IP
        /// </summary>
        /// <param name="treeNode">Узел дерева с которого начинаем искать</param>
        /// <param name="ip">ip-адрес</param>
        private void FindComputer_IP(TreeNode treeNode, string ip)
        {
            LCTreeNode lcTreeNodeWork = (LCTreeNode)treeNode;
            if (lcTreeNodeWork.LCObjectType == LCObjectType.Computer)
            {
                // Этот узел компьютер, приводим объект к нужному классу
                LCTreeNodeComputer lcComp = (LCTreeNodeComputer)lcTreeNodeWork;
                // Проверяем по IP-адресу
                if (lcComp.IP == ip)
                {
                    // Делаем активным компьютер в дереве справочника
                    this.treeViewObject.SelectedNode = lcComp;

                    this.openLCTreeNode(lcComp);

                    countFind++;
                    this.WriteListBox("Найден компьютер с именем: " + lcComp.Text + ".");
                }
            }
            else
            {
                // рекурсивный перебор всех дочерних узлов
                foreach (TreeNode treeNodeWorking in treeNode.Nodes)
                {
                    this.FindComputer_IP(treeNodeWorking, ip);
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
        private void toolStripButtonRunCMD_Click(object sender, EventArgs e)
        {
            // запускаем интерпретатор коммандной строки cmd.exe
            System.Diagnostics.Process.Start("cmd.exe");
        }
        private void toolStripButtonPasteClipboard_Click(object sender, EventArgs e)
        {
            this.toolStripTextBoxIP.Text = Clipboard.GetText(TextDataFormat.Text);
        }
        #endregion

        #region Главное меню
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void опцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
            // проверяем настройки
            if (Properties.Settings.Default.VisibleProtocol)
            {
                this.splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = false;
            }
        }
        private void командныеКнопкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton();
            formSettingCommandButton.ShowDialog();
        }
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

        #region Действия приложения
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
            LCTreeNode lcTreeNode = (LCTreeNode)treeNode;

            switch (lcTreeNode.LCObjectType)
            {
                case LCObjectType.Computer:
                    {
                        this.tabControlObject.SelectedTab = this.tabPageComputers;
                        LCTreeNodeComputer lcPC = (LCTreeNodeComputer)lcTreeNode;
                        foreach (ListViewItem curilv in this.listViewComputers.Items)
                        {
                            if (curilv.Tag == lcPC)
                            {
                                curilv.Selected = true;
                                return;
                            }
                        }
                        // определяем родительскую группу элемента
                        LCTreeNode lcNode = (LCTreeNode)lcPC.Parent;
                        // Пока сделано так, в будущем предполагаются что ПК состоят только в сетях. Кроме группы <Не в списке>
                        if (lcNode.LCObjectType == LCObjectType.SubNet)
                        {
                            ListViewGroup lvg = null;
                            foreach (ListViewGroup curGroup in this.listViewComputers.Groups)
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
                            this.listViewComputers.Groups.Add(lvg);
                            ListViewItem lvi = new ListViewItem(new string[] { lcPC.IP, lcPC.Text, lcPC.ParentGroup, lcPC.Description }, lvg)
                            {
                                Tag = lcPC
                            };
                            lcPC.Tag = lvi;
                            this.listViewComputers.Items.Add(lvi);
                        }
                        else
                        {
                            ListViewItem lvi = new ListViewItem(new string[] { lcPC.IP, lcPC.Text, lcPC.ParentGroup, lcPC.Description })
                            {
                                Tag = lcPC
                            };
                            lcPC.Tag = lvi;
                            this.listViewComputers.Items.Add(lvi);
                        }
                        break;
                    }
                case LCObjectType.SubNet:
                    {
                        this.tabControlObject.SelectedTab = this.tabPageSubnets;
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
                            lcSubnet.MaskSubnet,lcSubnet.ParentGroup,lcSubnet.Description })
                        {
                            Tag = lcSubnet
                        };
                        lcSubnet.Tag = lvi;
                        this.listViewSubnets.Items.Add(lvi);
                        break;
                    }
                case LCObjectType.Group:
                    {
                        this.tabControlObject.SelectedTab = this.tabPageGroups;
                        LCTreeNodeGroup lcGroup = (LCTreeNodeGroup)lcTreeNode;
                        foreach (ListViewItem curilv in this.listViewGroups.Items)
                        {
                            if (curilv.Tag == lcGroup)
                            {
                                curilv.Selected = true;
                                return;
                            }
                        }
                        ListViewItem lvi = new ListViewItem(new string[] { lcGroup.Text, lcGroup.ParentGroup, lcGroup.Description })
                        {
                            Tag = lcGroup
                        };
                        lcGroup.Tag = lvi;
                        this.listViewGroups.Items.Add(lvi);
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
                case LCObjectType.Computer:
                    {
                        FormEditComputer formEditComputer = new FormEditComputer(this.treeViewObject.SelectedNode);
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
                        case LCObjectType.Computer:
                            {
                                //tempStr = "Компьютер: " + tempStr + " удалён.";
                                LCTreeNodeComputer lcComputer = (LCTreeNodeComputer)lcTreeNode;
                                ListViewItem itm = (ListViewItem)lcComputer.Tag;
                                this.listViewComputers.Items.Remove(itm);
                                lcComputer.Remove();
                                tempStr = "Компьютер: " + tempStr + " удалён.";
                                // Сообщаем об удалении
                                this.WriteListBox(tempStr);
                                break;
                            }
                        case LCObjectType.SubNet:
                            {
                                LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet)lcTreeNode;
                                ListViewItem itm = (ListViewItem)lcSubnet.Tag;
                                this.listViewSubnets.Items.Remove(itm);
                                lcSubnet.Remove();
                                tempStr = "Сеть: " + tempStr + " удалена.";
                                this.WriteListBox(tempStr);
                                break;
                            }
                        case LCObjectType.Group:
                            {
                                LCTreeNodeGroup lcGroup = (LCTreeNodeGroup)lcTreeNode;
                                ListViewItem itm = (ListViewItem)lcGroup.Tag;
                                this.listViewGroups.Items.Remove(itm);
                                lcGroup.Remove();
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
        /// <summary>
        /// Проверка с помощью регулярно выражения действительно ли это ip адрес
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private bool CorrectIP()
        {
            string str = this.toolStripTextBoxIP.Text;
            str = str.Replace('-', '.');
            string pattern = @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"(25[0-5]|2[0-4]\d|[01]?\d\d?)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(str);
            if (match.Success)
            {
                this.toolStripTextBoxIP.Text = match.Value;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Возвращает узел дерева "не в списке", а если его не существует, то создает новый. 
        /// </summary>
        /// <returns>Возвращает узел дерева "не в списке".</returns>
        private TreeNode ReturnGroupNoList()
        {
            TreeNode treeNode = this.treeViewObject.Nodes[0];
            if (treeNode != null)
            {
                // рекурсивный перебор всех дочерних узлов
                foreach (TreeNode treeNodeWorking in treeNode.Nodes)
                {
                    if (treeNodeWorking.Text == "<Не в списке>")
                    {
                        return treeNodeWorking;
                    }
                }
            }
            LCTreeNodeNoList lcTreeNodeNoList = new LCTreeNodeNoList
            {
                Text = "<Не в списке>",
                Description = "Компьютеры которые не добавлялись в группу",
                ContextMenuStrip = this.contextMenuStripNoList,
                ImageIndex = 2,
                ToolTipText = "<Не в списке>\nКомпьютеры которые не добавлялись в группу"
            };
            treeNode.Nodes.Add(lcTreeNodeNoList);
            return lcTreeNodeNoList;
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
                string[] str = Properties.Settings.Default.OpenPages.Split(';');
                foreach (string st in str)
                {
                    if (st != "")
                    {
                        this.FindComputer_IP(this.treeViewObject.Nodes[0], st);
                    }
                }
            }
        }
        /// <summary>
        /// Сохранение не закрытых вкладок
        /// </summary>
        private void SaveOpenedPages()
        {
            Properties.Settings.Default.OpenPages = "";
            foreach (ListViewItem lvi in this.listViewComputers.Items)
            {
                Properties.Settings.Default.OpenPages += lvi.SubItems[0].Text + ";";
            }
            Properties.Settings.Default.Save();
        }
        #endregion

        private void toolStripButtonGetNamePC_Click(object sender, EventArgs e)
        {
            string ipStr;
            // Пока не понятно как определить что выделена какая либо строка в listView
            try
            {
                ipStr = this.listViewComputers.SelectedItems[0].SubItems[0].Text;
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
                this.listViewComputers.SelectedItems[0].SubItems[1].Text = hostName;
                this.WriteListBox("Имя ПК с IP " + ipStr + " @ " + hostName);
            }
            catch (System.Exception myException)
            {
                this.WriteListBox("Определение имени для ПК с IP " + ipStr + ":" + myException.Message);
            }
        }

        private void listViewComputers_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewComputers.SelectedItems.Count > 0)
            {
                LCTreeNodeComputer tn = (LCTreeNodeComputer)this.listViewComputers.SelectedItems[0].Tag;
                FormEditComputer formNewComputer = new FormEditComputer(tn);
                formNewComputer.ShowDialog();
            }
        }

        private void listViewComputers_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (this.listViewComputers.SelectedItems.Count >0)
                {
                    this.listViewComputers.SelectedItems[0].Remove();
                }
            }
        }

        private void toolStripMenuItemClearPCList_Click(object sender, EventArgs e)
        {
            this.listViewComputers.Items.Clear();
        }
    }
}