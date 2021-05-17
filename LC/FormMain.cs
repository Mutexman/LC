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
using LC.Properties;

namespace LC
{
    public partial class FormMain : Form
    {
        // ссылка на найденную подсеть
        public static string User = "";
        public static string Password = "";
        /// <summary>
        /// Переменная содержащая имя файла в котором храниться справочник
        /// </summary>
        private readonly string fileData = Application.LocalUserAppDataPath + "\\LCDirectory.xml";
        /// <summary>
        /// Переменная содержащая имя файла конфигурации кнопок
        /// </summary>
        private readonly string fileConfigComputers = Application.LocalUserAppDataPath + "\\configComputers.xml";
        private readonly string fileConfigMFU = Application.LocalUserAppDataPath + "\\configMFU.xml";
        private readonly string fileConfigETCO = Application.LocalUserAppDataPath + "\\configETCO.xml";
        private readonly string fileConfigSPD = Application.LocalUserAppDataPath + "\\configSPD.xml";
        private readonly BufferLCTreeNode buffer = new BufferLCTreeNode();
        // Поле для сохранения найденых компьютеров
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
                            CommandToolStripButton progBut = new CommandToolStripButton(text, command, parameters, toolTipText, true)
                            {
                                ImageScaling = ToolStripItemImageScaling.None,
                                listItems = listView
                            };
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
            // Сохраняем размеры и положение окна
            this.Size = Settings.Default.WindowSize;
            this.Location = Settings.Default.WindowLocation;
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
                this.ToolStripButtonFind_Click(null, null);
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
            else
            {
                Settings.Default.WindowLocation = this.Location;
                if (this.WindowState == FormWindowState.Normal)
                {
                    Settings.Default.WindowSize = this.Size;
                }
                else
                {
                    Settings.Default.WindowSize = this.RestoreBounds.Size;
                }
                Settings.Default.Save();
            }
        }
        #endregion

        #region Главная панель инструментов
        private void ToolStripButtonPasteClipboard_Click(object sender, EventArgs e)
        {
            this.toolStripTextBoxIP.Text = Clipboard.GetText(TextDataFormat.Text);
        }
        private void ToolStripButtonFind_Click(object sender, EventArgs e)
        {
            // здесь надо провести проверку корректности введенного значения IP-адреса
            string st = this.toolStripTextBoxIP.Text;
            if (this.lCDirectory.CorrectIP(ref st))
            {
                this.toolStripTextBoxIP.Text = st;
                this.WriteListBox("Поиск компьютера с IP " + this.toolStripTextBoxIP.Text + " запущен.");
                LCTreeNodeHost lcHost = this.lCDirectory.FindHost(st);
                if(lcHost != null)
                {
                    //Выделяем найденый хост в дереве
                    LCDirectory.treeView.SelectedNode = lcHost;
                    this.OpenLCTreeNode(lcHost);
                    this.WriteListBox("Найден хост с именем: " + lcHost.Text + ".");
                }
                else
                {
                    //Определяем принадлежность хоста сети
                    LCTreeNodeSubnet findSubnet = this.lCDirectory.FindSubnetIP(st);
                    if(findSubnet != null)
                    {
                        this.OpenLCTreeNode(findSubnet.AddHost(st, st, ""));
                        this.WriteListBox("IP адрес " + st + " принадлежит сети " + findSubnet.Text);
                    }
                    else
                    {
                        LCTreeNodeNoList lcNoList = (LCTreeNodeNoList)this.lCDirectory.ReturnGroupNoList();
                        //добавляем хост и сразу же выделяем этот объект
                        this.OpenLCTreeNode(lcNoList.AddHost(st, st, ""));
                        this.WriteListBox("IP адрес " + st + " добавлен в группу " + lcNoList.Text);
                    }
                }
            }
            else
            {
                this.WriteListBox("Введенное значение не является IP адресом");
            }
        }
        private void ToolStripTextBoxIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ToolStripButtonFind_Click(sender, e);
            }
        }
        #endregion

        #region Главное меню
        //Файл
        private void ToolStripMenuItemClearPCList_Click(object sender, EventArgs e)
        {
            this.listViewHosts.Items.Clear();
        }
        private void ToolStripMenuItemExportNetsToJSON_Click(object sender, EventArgs e)
        {
            this.saveFileDialogExport.Filter = "JSON files (*.js)|*.js";
            this.saveFileDialogExport.FileName = "nets";
            if(this.saveFileDialogExport.ShowDialog() == DialogResult.OK)
            {
                this.lCDirectory.ExportNetsToJSON(this.saveFileDialogExport.FileName);
                this.WriteListBox("Экспорт сетей в файл " + this.saveFileDialogExport.FileName + " выполнен.");
            }
        }
        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void ОпцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }
        private void КомпьютерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton(this.fileConfigComputers);
            formSettingCommandButton.Text += " : Компьютер";
            formSettingCommandButton.ShowDialog();
        }
        private void МФУToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton(this.fileConfigMFU);
            formSettingCommandButton.Text += " : МФУ";
            formSettingCommandButton.ShowDialog();
        }
        private void ЭТСОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton(this.fileConfigETCO);
            formSettingCommandButton.Text += " : ЭТСО";
            formSettingCommandButton.ShowDialog();
        }
        private void СПДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton(this.fileConfigSPD);
            formSettingCommandButton.Text += " : СПД";
            formSettingCommandButton.ShowDialog();
        }
        private void УчётнаяЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void ПроверкаОбновленийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пока данная функция не реализована", "Линейный специалист", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.WriteListBox("Пока данная функция не реализована!");
        }
        private void СправкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // открываем в браузере ссылку по которой находиться справка по работе с программой
            // System.Diagnostics.Process.Start(Properties.Settings.Default.HelpLink);
            string st = Application.StartupPath + "\\LCHelp.chm";
            if (File.Exists(st))
            {
                System.Diagnostics.Process.Start(st);
            }
            else
            {
                MessageBox.Show("Файл " + st + " не найден!");
            }
        }
        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void OpenLCTreeNode(object sender, EventArgs e)
        {
            this.OpenLCTreeNode(this.treeViewObject.SelectedNode);
        }
        /// <summary>
        /// Метод открытия узла дерева.
        /// </summary>
        /// <param name="treeNode">Открываемый узел.</param>
        private void OpenLCTreeNode(TreeNode treeNode)
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
                            lcHost.UpdateLC();
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
                            lcHost.UpdateLC();
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
        private void ToolStripMenuItemOpenNodes(object sender, EventArgs e)
        {
            this.treeViewObject.SelectedNode.ExpandAll();
        }
        /// <summary>
        /// Событие создания новой группы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewGroup(object sender, EventArgs e)
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
        private void CreateNewSubnet(object sender, EventArgs e)
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
        private void EditLCTreeNode(object sender, EventArgs e)
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
        private void DeleteLCTreeNode(object sender, EventArgs e)
        {
            LCTreeNode lcTreeNode = (LCTreeNode)this.treeViewObject.SelectedNode;
            string tempStr = lcTreeNode.Text;
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
        private void ToolStripMenuItemFindSubnet_Click(object sender, EventArgs e)
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
                    LCTreeNodeSubnet lcSubnet = this.lCDirectory.FindSubnetIP(st);
                    if (lcSubnet != null)
                    {
                        // и сразу же выделяем этот объект
                        this.OpenLCTreeNode(lcSubnet.AddHost(st, st, ""));
                    }
                    else
                    {
                        LCTreeNodeNoList lcNoList = lCDirectory.ReturnGroupNoList();
                        // и сразу же выделяем этот объект
                        this.OpenLCTreeNode(lcNoList.AddHost(st, st, ""));
                    }
                }
                this.treeViewObject.EndUpdate();
            }
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
                    this.OpenLCTreeNode(this.lCDirectory.FindSubnet(st));
                }
                str = Properties.Settings.Default.OpenGroups.Split(';');
                foreach (string st in str)
                {
                    this.OpenLCTreeNode(this.lCDirectory.FindGroup(st));
                }
                str = Properties.Settings.Default.OpenHosts.Split(';');
                foreach (string st in str)
                {
                    this.OpenLCTreeNode(this.lCDirectory.FindHost(st));
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
        private void ListViewHosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                LCTreeNodeHost lcHost = (LCTreeNodeHost)this.listViewHosts.SelectedItems[0].Tag;
                LCTypeHost lcTypeHost = lcHost.TypeHost;
                // Выделяем узел в дереве.
                this.treeViewObject.SelectedNode = lcHost;
                switch (lcTypeHost)
                {
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
        private void ToolStripButtonGetNamePC_Click(object sender, EventArgs e)
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
   
        private void ListViewComputers_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                LCTreeNodeHost tn = (LCTreeNodeHost)this.listViewHosts.SelectedItems[0].Tag;
                FormEditHost formNewComputer = new FormEditHost(tn);
                formNewComputer.ShowDialog();
            }
        }

        private void ListViewHosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.listViewHosts.SelectedItems.Count > 0)
                {
                    this.listViewHosts.SelectedItems[0].Remove();
                }
            }
        }

        private void ToolStripMenuItemGetHostIP_Click(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                string ip = this.listViewHosts.SelectedItems[0].SubItems[1].Text;
                Clipboard.SetText(ip);
            }
        }

        private void ToolStripMenuItemGetHostName_Click(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                string n = this.listViewHosts.SelectedItems[0].SubItems[2].Text;
                // берем имя ПК до первой точки
                n = n.Substring(0, n.IndexOf('.'));
                Clipboard.SetText(n);
            }
        }

        private void ToolStripMenuItemGetHostFullName_Click(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                string n = this.listViewHosts.SelectedItems[0].SubItems[2].Text;
                Clipboard.SetText(n);
            }
        }

        private void toolStripMenuItemGetHostBarcode_Click(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                LCTreeNodeHost h = (LCTreeNodeHost)this.listViewHosts.SelectedItems[0].Tag;
                if(h.Barcode.Length > 0)
                {
                    //Пустой текст в этот метод передать нельзя, поэтому пока сделано так.
                    //Надо позже сделать некативным пункт меню "Копировать ШК" если ШК пустой
                    Clipboard.SetText(h.Barcode);
                }
            }
        }

        private void toolStripMenuItemGetHostPassword_Click(object sender, EventArgs e)
        {
            if (this.listViewHosts.SelectedItems.Count > 0)
            {
                LCTreeNodeHost h = (LCTreeNodeHost)this.listViewHosts.SelectedItems[0].Tag;
                if (h.Password.Length > 0)
                {
                    //Пустой текст в этот метод передать нельзя, поэтому пока сделано так.
                    //Надо позже сделать некативным пункт меню "Копировать ШК" если ШК пустой
                    Clipboard.SetText(h.Password);
                }
            }
        }
        #endregion

        #region Действия приложения. Список сетей.
        private void ListViewSubnets_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewSubnets.SelectedItems.Count > 0)
            {
                LCTreeNodeSubnet tn = (LCTreeNodeSubnet)this.listViewSubnets.SelectedItems[0].Tag;
                FormEditSubnet formEditSubnet = new FormEditSubnet(tn, ModeForm.Edit);
                formEditSubnet.ShowDialog();
            }
        }

        private void ListViewSubnets_KeyDown(object sender, KeyEventArgs e)
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
        private void ListViewGroups_DoubleClick(object sender, EventArgs e)
        {
            if(this.listViewGroups.SelectedItems.Count > 0)
            {
                LCTreeNodeGroup tn = (LCTreeNodeGroup)this.listViewGroups.SelectedItems[0].Tag;
                FormEditGroup formEditGroup = new FormEditGroup(tn, ModeForm.Edit);
                formEditGroup.ShowDialog();
            }
        }

        private void ListViewGroups_KeyDown(object sender, KeyEventArgs e)
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
    }
}