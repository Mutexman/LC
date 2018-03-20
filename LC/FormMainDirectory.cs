using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LC
{
    public partial class FormMain : Form
    {
        #region Загрузка справочника
        /// <summary>
        /// Метод добавления дочерних узлов в дерево
        /// </summary>
        /// <param name="xnod">DOM модель XML</param>
        /// <param name="newNode">Компонент TreeNode в котором создаются узлы</param>
        private void AddChildren(XmlNode xnod, TreeNode newNode)
        {
            XmlNode xnodWorking;
            switch (xnod.NodeType)
            {
                case XmlNodeType.Element:
                    {
                        //если это элемент, извлечь его атрибут
                        if (xnod.NodeType == XmlNodeType.Element)
                        {
                            switch (xnod.Name)
                            {
                                case "Group":
                                    {
                                        LCTreeNodeGroup lcTreeNodeGroup = new LCTreeNodeGroup();
                                        lcTreeNodeGroup.Text = xnod.Attributes["NameGroup"].Value;
                                        lcTreeNodeGroup.Description = xnod.Attributes["Description"].Value;
                                        // Эта проверка нужна на тот случай если этого атрибута не в файле
                                        if (xnod.Attributes["FotoFile"] != null)
                                        {
                                            lcTreeNodeGroup.SetFoto(xnod.Attributes["FotoFile"].Value);
                                        }
                                        lcTreeNodeGroup.ToolTipText += lcTreeNodeGroup.Text;
                                        lcTreeNodeGroup.ToolTipText += "\n" + lcTreeNodeGroup.Description;
                                        lcTreeNodeGroup.ContextMenuStrip = this.contextMenuStripLCGroup;
                                        lcTreeNodeGroup.ImageIndex = 2;
                                        newNode.Nodes.Add(lcTreeNodeGroup);
                                        newNode = lcTreeNodeGroup;
                                    }
                                    break;
                                case "Computer":
                                    {
                                        LCTreeNodeComputer lcTreeNodeComputer = new LCTreeNodeComputer();
                                        lcTreeNodeComputer.Text = xnod.Attributes["NameComputer"].Value;
                                        lcTreeNodeComputer.IP = xnod.Attributes["IP"].Value;
                                        lcTreeNodeComputer.Description = xnod.Attributes["Description"].Value;
                                        lcTreeNodeComputer.ToolTipText += lcTreeNodeComputer.Text;
                                        lcTreeNodeComputer.ToolTipText += "\n" + lcTreeNodeComputer.IP;
                                        lcTreeNodeComputer.ToolTipText += "\n" + lcTreeNodeComputer.Description;
                                        lcTreeNodeComputer.ContextMenuStrip = this.contextMenuStripLCComputer;
                                        lcTreeNodeComputer.ImageIndex = 3;
                                        newNode.Nodes.Add(lcTreeNodeComputer);
                                        newNode = lcTreeNodeComputer;
                                    }
                                    break;
                                case "Subnet":
                                    {
                                        LCTreeNodeSubnet lcTreeNodeSubnet = new LCTreeNodeSubnet();
                                        lcTreeNodeSubnet.Text = xnod.Attributes["NameSubnet"].Value;
                                        lcTreeNodeSubnet.IPSubnet = xnod.Attributes["IPSubnet"].Value;
                                        lcTreeNodeSubnet.MaskSubnet = xnod.Attributes["MaskSubnet"].Value;
                                        lcTreeNodeSubnet.Description = xnod.Attributes["Description"].Value;
                                        lcTreeNodeSubnet.ToolTipText += lcTreeNodeSubnet.Text;
                                        lcTreeNodeSubnet.ToolTipText += "\n" + lcTreeNodeSubnet.IPSubnet;
                                        lcTreeNodeSubnet.ToolTipText += "\n" + lcTreeNodeSubnet.MaskSubnet;
                                        lcTreeNodeSubnet.ContextMenuStrip = this.contextMenuStripLCSubnet;
                                        lcTreeNodeSubnet.ImageIndex = 5;
                                        newNode.Nodes.Add(lcTreeNodeSubnet);
                                        newNode = lcTreeNodeSubnet;
                                    }
                                    break;
                                case "Root":
                                    {
                                        //MessageBox.Show("yes");
                                        //newNode.ImageIndex = 1;
                                        //newNode.Text = mapAttributes.Item(1).Value;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case XmlNodeType.Text:
                    {
                        //регулярное выражение отбрасывает символы форматирования
                        /*
                        Match mat = Regex.Match(xnod.Value, @"[^\t\v\f\r\n]+");
                        newNode = newNode.Nodes.Add(mat.Value);
                        newNode.ImageIndex = 5;
                         */
                    }
                    break;
                case XmlNodeType.CDATA:
                    {
                        newNode = newNode.Nodes.Add(xnod.Value);
                        newNode.ImageIndex = 2;
                    }
                    break;
            }
            //рекурсивный перебор всех дочерних узлов
            if (xnod.HasChildNodes)
            {
                xnodWorking = xnod.FirstChild;
                while (xnodWorking != null)
                {
                    AddChildren(xnodWorking, newNode);
                    xnodWorking = xnodWorking.NextSibling;
                }
            }
        }
        /// <summary>
        /// Метод создания модели DOM на основе текста
        /// </summary>
        private void CreateDOM()
        {
            this.treeViewObject.Nodes.Clear();
            XmlDocument xmlDocument = new XmlDocument();
            if (File.Exists(this.fileData))
            {
                //Создаём резевную копию файла с данными о компьютерах
                //Формируем путь в файлу резервной копии
                string backupFileName = Application.LocalUserAppDataPath;
                backupFileName += "\\Backup\\Computers.xml." + DateTime.Today.ToShortDateString() + ".backup";
                //Проверяем была ли уже сделана резервная копия сегодня
                if(!(File.Exists(backupFileName)))
                {
                    //формируем пусь к паке backup
                    string backupDirectory = Application.LocalUserAppDataPath + "\\Backup";
                    //Проверяем существует ли папка backup
                    if (!(Directory.Exists(backupDirectory)))
                    {
                        //Создаём папку backup
                        Directory.CreateDirectory(backupDirectory);
                    }
                    //Создаём резервную копию
                    File.Copy(this.fileData, backupFileName);
                }
                //Получается если работать под нужной учётной записью, то и с файлом можно работать без разшифровки
                //File.Decrypt(this.fileData);
                try
                {
                    // Загружаем XML файл
                    xmlDocument.Load(this.fileData);
                }
                catch (XmlException e)
                {
                    this.WriteListBox("Ошибка загрузки файла: " + e.Message);
                    LCTreeNodeGroup lcTreeRootError = new LCTreeNodeGroup();
                    lcTreeRootError.Text = "Компьютеры";
                    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    // Здесь возможна загрузка специальной иконки обозначающей сбой
                    lcTreeRootError.ImageIndex = 1;
                    this.treeViewObject.Nodes.Add(lcTreeRootError);
                    lcTreeRootError.ContextMenuStrip = this.contextMenuStripLCRoot;
                    this.WriteListBox("Справочник не загружен. Закройте приложение и проверьте корректность файла!");
                    this.correctLoad = false;
                    return;
                }
            }
            else
            {
                string st = Application.LocalUserAppDataPath + "\\Computers.cxml";
                // Проверяем нет ли в папке зашифрованного файла, алгоритмом TripleDES
                if (File.Exists(st))
                {
                    this.correctLoad = false;
                    // Если такой файл имеется, то расшифруем его.
                    // Правда для его работы придеться перезапустить приложение
                    this.Decrypt(st, this.fileData);
                    this.WriteListBox("Расшифровка завершена. Перезапустите приложение !!!!");
                    // и надо тут же завершить приложение
                    return;
                }
                else
                {
                    this.WriteListBox("Файл справочника не найден!");
                    string xmlStr = "<?xml version=\"1.0\" encoding=\"windows-1251\"?><root></root>";
                    xmlDocument.LoadXml(xmlStr);
                    this.WriteListBox("Создан новый, пустой справочник.");
                }
            }
            // Получаем корневой узел элемента
            XmlNode xnodDE = xmlDocument.DocumentElement;
            // Получение корневого узла дерева
            // Здесь надо правильно настроить этот узел
            LCTreeNodeGroup lcTreeRoot = new LCTreeNodeGroup();
            lcTreeRoot.Name = "Root";
            lcTreeRoot.Text = "Компьютеры";
            lcTreeRoot.Description = "Корневой узел справочника.";
            lcTreeRoot.ContextMenuStrip = this.contextMenuStripLCRoot;
            lcTreeRoot.ImageIndex = 1;
            // И добавить его в дерево
            this.treeViewObject.Nodes.Add(lcTreeRoot);
            TreeNode node = lcTreeRoot;
            this.treeViewObject.BeginUpdate();
            //рекурсивный обход дерева
            AddChildren(xnodDE, node);
            // Сортируем объекты в дереве
            this.treeViewObject.Sort();
            this.treeViewObject.EndUpdate();
            this.WriteListBox("Справочник успешно загружен.");
            // открываем дочерние узлы узла root
            node.Expand();
        }
        #endregion

        #region Сохранение справочника
        /// <summary>
        /// Метод сохранения дочернего узла справочника (рекурсивный)
        /// </summary>
        /// <param name="workxw">XML-считыватель</param>
        /// <param name="treeNode">Текущий узел справочника</param>
        private void SaveChildren(XmlTextWriter workxw, TreeNode treeNode)
        {
            LCTreeNode lcTreeNodeWork = (LCTreeNode)treeNode;
            if (lcTreeNodeWork.Name != "Root")
            {
                lcTreeNodeWork.Save(workxw);
            }
            else
            {
                workxw.WriteStartElement("Root");
            }
            //рекурсивный перебор всех дочерних узлов
            foreach (TreeNode treeNodeWorking in treeNode.Nodes)
            {
                this.SaveChildren(workxw, treeNodeWorking);
            }
            workxw.WriteEndElement();
        }
        /// <summary>
        /// Метод сохранения XML-справочника
        /// </summary>
        private void SaveXML()
        {
            // Сохранение справочника надо выполнять только в случае если он вообще загружался
            if (this.logged && this.correctLoad)
            {
                // Проверяем имя файла, возможно создаётся новый справочник
                if (this.fileData == "")
                {
                    this.fileData = Application.LocalUserAppDataPath + "\\Computers.xml";
                }
                XmlTextWriter xw = new XmlTextWriter(this.fileData, System.Text.Encoding.UTF8);
                // Настраиваем форматирование
                xw.Formatting = Formatting.Indented;
                xw.Indentation = 2;
                xw.IndentChar = ' ';
                // Запиись декларации документа
                xw.WriteStartDocument();
                // запускаем рекурсивное сохранение
                this.SaveChildren(xw, this.treeViewObject.Nodes[0]);
                // завершение элемента root
                //xw.WriteEndElement();
                // завершение документа
                xw.WriteEndDocument();
                // сброс буфера на диск и закрытие файла
                xw.Flush();
                xw.Close();
                if (!Properties.Settings.Default.AllUserAccess)
                {
                    try
                    {
                        File.Encrypt(this.fileData);
                    }
                    catch (IOException e)
                    {
                        this.WriteListBox("Ошибка при шифровании! ");
                        this.WriteListBox(e.Message);
                        MessageBox.Show(e.Message, "Линейный специалист", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        #endregion

        // С использованием модели DOM сохранить справочник все таки было бы проще
        #region Сохранение справочника с использованием DOM модели
        private void SaveChildren()
        {
        }
        private void SaveDomXML()
        {
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
            LCTreeNode lcTreeNode = (LCTreeNode)this.treeViewObject.SelectedNode;

            if (lcTreeNode.LCObjectType == LCObjectType.Computer)
            {
                LCTreeNodeComputer lcPC = (LCTreeNodeComputer)lcTreeNode;
                ListViewItem lvi = new ListViewItem(new string[] { lcPC.IP, lcPC.Text, lcPC.ParentGroup, lcPC.Description });
                this.listViewComputers.Items.Add(lvi);
            }
            else
            {
                // проверяем открыто ли 
                if (lcTreeNode.TabPage == null)
                {
                    // Открываем
                    lcTreeNode.CreateTabPage(this.tabControlObject);
                    // Как сделать по другому я не знаю ????
                    this.tabControlObject.SelectedIndex = this.tabControlObject.Controls.Count - 1;
                }
                else
                {
                    // выделяем открытый ранее
                    this.tabControlObject.SelectedTab = lcTreeNode.TabPage;
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
        /// Событие создания нового компьютера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewComputer(object sender, EventArgs e)
        {
            FormNewComputer formNewComputer = new FormNewComputer(this.treeViewObject.SelectedNode);
            formNewComputer.ShowDialog();
            this.treeViewObject.Sort();
            if (formNewComputer.TreeNode != null)
            {
                // Выделяем только что созданный компьютер в дереве справочника
                this.treeViewObject.SelectedNode = formNewComputer.TreeNode;
            }
        }
        /// <summary>
        /// Событие создания новой группы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewGroup(object sender, EventArgs e)
        {
            FormNewGroup formNewGroup = new FormNewGroup(this.treeViewObject.SelectedNode);
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
            FormNewSubnet formNewSubnet = new FormNewSubnet(this.treeViewObject.SelectedNode);
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
            LCTreeNode lcTreeNode = (LCTreeNode)this.treeViewObject.SelectedNode;
            LCTabPage ltp = null;
            // проверяем открыто ли 
            if (lcTreeNode.TabPage == null)
            {
                // Открываем
                lcTreeNode.CreateTabPage(this.tabControlObject);
                ltp = (LCTabPage)lcTreeNode.TabPage;
                ltp.Mode = TypeModeTabPage.Edit;
                // Как сделать по другому я не знаю ????
                this.tabControlObject.SelectedIndex = this.tabControlObject.Controls.Count - 1;
            }
            else
            {
                // выделяем открытый ранее
                this.tabControlObject.SelectedTab = lcTreeNode.TabPage;
                ltp = (LCTabPage)lcTreeNode.TabPage;
                ltp.Mode = TypeModeTabPage.Edit;
            }
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
                    // Удаляем дочерние
                    this.deleteChilds(lcTreeNode);
                    // Удаляем текущий
                    lcTreeNode.Remove();
                    // Сообщаем об удалении
                    this.WriteListBox("Группа " + tempStr + " и её дочерние объекты удалены.");
                }
            }
            else
            {
                if (MessageBox.Show("Вы дейстительно хотите удалить этот объект ?", "Удаление", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (lcTreeNode.TabPage != null)
                    {
                        this.tabControlObject.TabPages.Remove(lcTreeNode.TabPage);
                    }
                    if (lcTreeNode.LCObjectType == LCObjectType.Computer)
                    {
                        tempStr = "Компьютер: " + tempStr + " удалён.";
                    }
                    if (lcTreeNode.LCObjectType == LCObjectType.Group)
                    {
                        tempStr = "Группа: " + tempStr + " удалёна.";
                    }
                    lcTreeNode.Remove();
                    // Сообщаем об удалении
                    this.WriteListBox(tempStr);
                }
            }
        }
        /// <summary>
        /// Метод закрытия вкладок дочерних узлов
        /// </summary>
        /// <param name="lcTreenode">Удаляемый узел дерева</param>
        private void deleteChilds(LCTreeNode lcTreenode)
        {
            if (lcTreenode.TabPage != null)
            {
                this.tabControlObject.TabPages.Remove(lcTreenode.TabPage);
            }
            foreach (LCTreeNode lcTN in lcTreenode.Nodes)
            {
                this.deleteChilds(lcTN);
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
        /// Экспорт дочерних узлов выделенного узла в XML файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportsNodes(object sender, EventArgs e)
        {
            LCTreeNode selectLCTreeNode = (LCTreeNode)this.treeViewObject.SelectedNode;
            if (selectLCTreeNode != null)
            {
                if (this.saveFileDialogExport.ShowDialog() == DialogResult.OK)
                {
                    XmlTextWriter xw = new XmlTextWriter(this.saveFileDialogExport.FileName, System.Text.Encoding.UTF8);
                    // Запиись декларации документа
                    xw.WriteStartDocument();
                    // Запись первого элемента
                    xw.WriteStartElement("root");
                    // запускаем рекурсивное сохранение
                    this.SaveChildren(xw, selectLCTreeNode);
                    // завершение документа
                    xw.WriteEndDocument();
                    // сброс буфера на диск и закрытие файла
                    xw.Flush();
                    xw.Close();
                    this.WriteListBox("Экспорт группы " + selectLCTreeNode.Text + " в файл " + this.saveFileDialogExport.FileName + " успешно выполнен.");
                }
            }
        }
        /// <summary>
        /// Импорт узлов из файла xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportNodes(object sender, EventArgs e)
        {
            LCTreeNode selectLCTreeNode = (LCTreeNode)this.treeViewObject.SelectedNode;
            if (selectLCTreeNode != null)
            {
                if (this.openFileDialogImport.ShowDialog() == DialogResult.OK)
                {
                    // Надо предусмотреть ситуацию когда импортируемый xml файл некорректный
                    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    XmlDocument xmlDocument = new XmlDocument();
                    try
                    {
                        // Загружаем XML файл
                        xmlDocument.Load(this.openFileDialogImport.FileName);
                    }
                    catch (XmlException ex)
                    {
                        this.WriteListBox("Ошибка импорта: " + ex.Message);
                        this.WriteListBox("Структура импортируемого файла " + this.openFileDialogImport.FileName + " некорректна. Импорт невозможен!");
                        return;
                    }
                    // Получаем корневой узел элемента
                    XmlNode xnodDE = xmlDocument.DocumentElement;
                    //рекурсивный обход дерева
                    AddChildren(xnodDE, selectLCTreeNode);
                    // Выводим информацию об успешном импорте
                    this.WriteListBox("Файл " + this.openFileDialogImport.FileName + " успешно импортирован в группу " + selectLCTreeNode.Text + ".");
                }
            }
        }
        #endregion

        #region Прочие методы
        /// <summary>
        /// Вывод сообщения в нижний ListBox компонент
        /// </summary>
        /// <param name="message">Текст сообщений</param>
        private void WriteListBox(string message)
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
        /// Метод загрузки плагинов находящихся в папки Plugins
        /// </summary>
        private void LoadPlugins()
        {
            // записываем путь к папке с плагинами
            string sPath = Application.StartupPath + "\\Plugins";
            // Проверяем существует ли папка Plugins
            if (Directory.Exists(sPath))
            {
                List<ILCPlugin> lPlugin = new List<ILCPlugin>();
                foreach (string f in System.IO.Directory.GetFiles(sPath, "*.dll"))
                {
                    System.Reflection.Assembly a = System.Reflection.Assembly.LoadFile(f);
                    try
                    {
                        foreach (Type t in a.GetTypes())
                        {
                            foreach (Type i in t.GetInterfaces())
                            {
                                if (i.Equals(Type.GetType("LC.ILCPlugin")))
                                {
                                    ILCPlugin p = (ILCPlugin)Activator.CreateInstance(t);
                                    lPlugin.Add(p);
                                    p.Initialize(this);
                                    break;
                                }
                            }
                        }
                    }
                    catch (System.Reflection.ReflectionTypeLoadException e)
                    {
                        this.WriteListBox("Ошибка загрузки плагина: " + e.Message);
                    }
                }
            }
            else
            {
                // Создаём папку, если её по каким либо причинам нету
                Directory.CreateDirectory(sPath);
            }
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
            LCTreeNodeGroup lcTreeNodeGroup = new LCTreeNodeGroup();
            lcTreeNodeGroup.Text = "<Не в списке>";
            lcTreeNodeGroup.Description = "Компьютеры которые не добавлялись в группу";
            lcTreeNodeGroup.ContextMenuStrip = this.contextMenuStripLCGroup;
            lcTreeNodeGroup.ImageIndex = 2;
            lcTreeNodeGroup.ToolTipText += "<Не в списке>";
            lcTreeNodeGroup.ToolTipText += "\n" + "Компьютеры которые не добавлялись в группу";
            treeNode.Nodes.Add(lcTreeNodeGroup);
            return lcTreeNodeGroup;
        }
        /// <summary>
        /// Метод создания нового файла config.xml
        /// </summary>
        private void CreateDefaultFileConfig()
        {
            // открытие нового XML-файла c помощью объекта XmlTextWriter
            XmlTextWriter xw = new XmlTextWriter(Application.LocalUserAppDataPath + "\\config.xml", System.Text.Encoding.UTF8);
            // Настраиваем форматирование для более удобного чтения файла
            xw.Formatting = Formatting.Indented;
            xw.Indentation = 2;
            // запись декларации документа
            xw.WriteStartDocument();
            // запись корневого элемента
            xw.WriteStartElement("Buttons");

            // Запись о первой кнопке (VNC+proxy)
            xw.WriteStartElement("Button");
            xw.WriteElementString("Text", "VNC+proxy");
            xw.WriteElementString("Command", "C:\\Program Files\\VNCViewer\\vncviewer.exe");
            xw.WriteElementString("Parameters", "@[IP]:5047 /proxy 10.35.38.184:5901 /user @[User] /password @[Password]");
            xw.WriteElementString("ToolTipText", "Запуск VNC с использованием proxy. Для подключения к ПК в других регионах");
            xw.WriteEndElement();
            // Запись о второй кнопке
            xw.WriteStartElement("Button");
            xw.WriteElementString("Text", "VNC");
            xw.WriteElementString("Command", "C:\\Program Files\\VNCViewer\\vncviewer.exe");
            xw.WriteElementString("Parameters", "@[IP]:5047 /user @[User] /password @[Password]");
            xw.WriteElementString("ToolTipText", "Запуск VNC без использования proxy. Для подключения к ПК в своём регионе");
            xw.WriteEndElement();
            // Запись о третьей кнопке (RDP)
            xw.WriteStartElement("Button");
            xw.WriteElementString("Text", "RDP");
            xw.WriteElementString("Command", "C:\\WINDOWS\\system32\\mstsc.exe");
            xw.WriteElementString("Parameters", "/v:@[IP]");
            xw.WriteElementString("ToolTipText", "Удалённый рабочий стол");
            xw.WriteEndElement();
            // Запись о четвёртой кнопке (SMS)
            xw.WriteStartElement("Button");
            xw.WriteElementString("Text", "CMRC");
            xw.WriteElementString("Command", "rc.exe");
            xw.WriteElementString("Parameters", "1 @[IP]");
            xw.WriteElementString("ToolTipText", "Подключение через Configuration Manager Remote Control");
            xw.WriteEndElement();
            // Запись о пятой кнопке
            xw.WriteStartElement("Button");
            xw.WriteElementString("Text", "Ping");
            xw.WriteElementString("Command", "ping");
            xw.WriteElementString("Parameters", "-a @[IP]");
            xw.WriteElementString("ToolTipText", "Команда ping с параметром -a");
            xw.WriteEndElement();
            // Запись о шестой кнопке
            xw.WriteStartElement("Button");
            xw.WriteElementString("Text", "Ping -t");
            xw.WriteElementString("Command", "ping");
            xw.WriteElementString("Parameters", "-a -t @[IP]");
            xw.WriteElementString("ToolTipText", "Команда ping с параметрами -a и -t");
            xw.WriteEndElement();
            // Запись о седьмой кнопке
            xw.WriteStartElement("Button");
            xw.WriteElementString("Text", "Tracert");
            xw.WriteElementString("Command", "tracert");
            xw.WriteElementString("Parameters", "@[IP]");
            xw.WriteElementString("ToolTipText", "Трассировка по заданному IP");
            xw.WriteEndElement();

            // Закрытие корневого элемента
            xw.WriteEndElement();
            // Завершение документа
            xw.WriteEndDocument();
            // сброс буфера на диск и закрытие файла
            xw.Flush();
            xw.Close();
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
            this.tabControlObject.TabPages.Remove(this.tabPageComputers);
            this.tabControlObject.TabPages.Remove(this.tabPageSubnets);
            this.tabControlObject.TabPages.Remove(this.tabPageGroups);

            Properties.Settings.Default.OpenPages = "";
            foreach (LCTabPage lcTabPage in this.tabControlObject.TabPages)
            {
                if (lcTabPage.LCTreeNode.LCObjectType == LCObjectType.Computer)
                {
                    LCTreeNodeComputer lcComp = (LCTreeNodeComputer)lcTabPage.LCTreeNode;
                    Properties.Settings.Default.OpenPages += lcComp.IP + ";";
                }
            }
            Properties.Settings.Default.Save();
        }
        #endregion
    }
}