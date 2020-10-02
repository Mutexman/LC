using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using System.Collections;

namespace LC
{
    /// <summary>
    /// Класс справочника в котором хранятся все данные.
    /// </summary>
    class LCDirectory
    {
        public LCDirectory()
        {
        }
        // поле показывающее корректно ли был загружен справочник
        private bool correctLoad = true;
        public static TreeView treeView = null;
        public static ListBox listBox = null;
        public static ToolStripStatusLabel toolStripStatusLabel = null;

        private string fileData;
        #region Загрузка справочника 
        /// <summary>
        /// Метод создания модели DOM на основе текста
        /// </summary>
        public void CreateDOMXML(string fileName)
        {
            this.fileData = fileName;
            treeView.Nodes.Clear();
            XDocument xmlDocument;
            if (File.Exists(this.fileData))
            {
                //Создаём резевную копию файла с данными о компьютерах
                //Формируем путь в файлу резервной копии
                string backupFileName = Application.LocalUserAppDataPath;
                backupFileName += "\\Backup\\LCDirectory.xml." + DateTime.Today.ToShortDateString() + ".backup";
                //Проверяем была ли уже сделана резервная копия сегодня
                if (!(File.Exists(backupFileName)))
                {
                    //формируем пусь к паке backup
                    string backupDirectory = Application.LocalUserAppDataPath + "\\Backup";
                    //Создаём папку backup
                    Directory.CreateDirectory(backupDirectory);
                    //Создаём резервную копию
                    File.Copy(this.fileData, backupFileName);
                }
                try
                {
                    // Загружаем XML файл
                    xmlDocument = XDocument.Load(this.fileData);
                }
                catch (Exception e)
                {
                    this.WriteListBox("Ошибка загрузки файла: " + e.Message);
                    LCTreeNodeGroup lcTreeRootError = new LCTreeNodeGroup
                    {
                        Text = "!",
                        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        // Здесь возможна загрузка специальной иконки обозначающей сбой
                        ImageIndex = 1
                    };
                    treeView.Nodes.Add(lcTreeRootError);
                    //lcTreeRootError.ContextMenuStrip = this.contextMenuStripLCRoot;
                    this.WriteListBox("Справочник не загружен. Закройте приложение и проверьте корректность файла!");
                    this.correctLoad = false;
                    return;
                }
            }
            else
            {
                this.WriteListBox("Файл справочника не найден!");
                xmlDocument = new XDocument(
                    new XDeclaration("1.0", "windows-1251", "yes"),
                    new XElement("Root"));
                this.WriteListBox("Создан новый, пустой справочник.");
            }
            // Получаем корневой узел элемента
            XElement xnodDE = xmlDocument.Root;
            // Здесь надо правильно настроить этот узел
            LCTreeNodeGroup lcTreeRoot = new LCTreeNodeGroup
            {
                Name = "Root",
                Text = ".",
                Description = "Корневой узел справочника.",
                ContextMenuStrip = LCTreeNode.rootContextMenuStrip,
                ImageIndex = 1
            };
            // И добавить его в дерево
            treeView.Nodes.Add(lcTreeRoot);
            TreeNode node = lcTreeRoot;
            treeView.BeginUpdate();
            //рекурсивный обход дерева
            AddChildrenDOMXML(xnodDE, node);
            // Сортируем объекты в дереве
            treeView.Sort();
            treeView.EndUpdate();
            this.WriteListBox("Справочник успешно загружен.");
            // открываем дочерние узлы узла root
            node.Expand();
        }
        /// <summary>
        /// Метод добавления дочерних узлов в дерево
        /// </summary>
        /// <param name="xnod">XML элемент</param>
        /// <param name="newNode">Компонент TreeNode в котором создаются узлы</param>
        private void AddChildrenDOMXML(XElement xnod, TreeNode newNode)
        {
            if (xnod.NodeType == System.Xml.XmlNodeType.Element)
            {
                switch (xnod.Name.ToString())
                {
                    case "Group":
                        {
                            LCTreeNodeGroup lcTreeNodeGroup = new LCTreeNodeGroup
                            {
                                Text = xnod.Attribute("NameGroup").Value,
                                Description = xnod.Attribute("Description").Value,
                                ContextMenuStrip = LCTreeNode.groupContextMemuStrip,
                                ImageIndex = 2
                            };
                            lcTreeNodeGroup.ToolTipText += lcTreeNodeGroup.Text;
                            lcTreeNodeGroup.ToolTipText += "\n" + lcTreeNodeGroup.Description;
                            newNode.Nodes.Add(lcTreeNodeGroup);
                            newNode = lcTreeNodeGroup;
                        }
                        break;
                    case "NoList":
                        {
                            LCTreeNodeNoList lcTreeNodeNoList = new LCTreeNodeNoList
                            {
                                Text = xnod.Attribute("NameGroup").Value,
                                Description = xnod.Attribute("Description").Value,
                                ContextMenuStrip = LCTreeNode.noListContextMenuStrip,
                                ImageIndex = 2
                            };
                            lcTreeNodeNoList.ToolTipText += lcTreeNodeNoList.Text;
                            lcTreeNodeNoList.ToolTipText += "\n" + lcTreeNodeNoList.Description;
                            newNode.Nodes.Add(lcTreeNodeNoList);
                            newNode = lcTreeNodeNoList;
                        }
                        break;
                    case "Host":
                        {
                            LCTreeNodeHost lcTreeNodeHost = new LCTreeNodeHost
                            {
                                Text = xnod.Attribute("NameHost").Value,
                                IP = xnod.Attribute("IP").Value,
                                Description = xnod.Attribute("Description").Value,
                                TypeHost = (LCTypeHost)Enum.Parse(typeof(LCTypeHost), xnod.Attribute("TypeHost").Value),
                                ContextMenuStrip = LCTreeNode.computerContextMenuStrip,
                                ImageIndex = 3
                            };
                            lcTreeNodeHost.ToolTipText = lcTreeNodeHost.TypeHost.ToString();
                            lcTreeNodeHost.ToolTipText += "\n" + lcTreeNodeHost.Text;
                            lcTreeNodeHost.ToolTipText += "\n" + lcTreeNodeHost.IP;
                            lcTreeNodeHost.ToolTipText += "\n" + lcTreeNodeHost.Description;
                            newNode.Nodes.Add(lcTreeNodeHost);
                            return;
                            //newNode = lcTreeNodeHost;
                        }
                    case "Subnet":
                        {
                            LCTreeNodeSubnet lcTreeNodeSubnet = new LCTreeNodeSubnet
                            {
                                Text = xnod.Attribute("NameSubnet").Value,
                                IPSubnet = xnod.Attribute("IPSubnet").Value,
                                MaskSubnet = xnod.Attribute("MaskSubnet").Value,
                                Description = xnod.Attribute("Description").Value,
                                ContextMenuStrip = LCTreeNode.subnetContextMenuStrip,
                                ImageIndex = 5
                            };
                            lcTreeNodeSubnet.ToolTipText += lcTreeNodeSubnet.Text;
                            lcTreeNodeSubnet.ToolTipText += "\n" + lcTreeNodeSubnet.IPSubnet;
                            lcTreeNodeSubnet.ToolTipText += "\n" + lcTreeNodeSubnet.MaskSubnet;
                            lcTreeNodeSubnet.ToolTipText += "\n" + lcTreeNodeSubnet.Description;
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
                foreach (XElement element in xnod.Elements())
                {
                    AddChildrenDOMXML(element, newNode);
                }
            }
        }
        #endregion

        #region Сохранение справочника с использованием DOM модели
        private XElement SaveChildren(LCTreeNode item, XElement current)
        {
            LCTreeNode xnodWorking;
            switch (item.LCObjectType)
            {
                case LCObjectType.Group:
                    {
                        if (item.Name != "Root")
                        {
                            LCTreeNodeGroup lcGroup = (LCTreeNodeGroup)item;
                            XElement xElement = new XElement("Group",
                                new XAttribute("NameGroup", item.Text),
                                new XAttribute("Description", item.Description));
                            current = xElement;
                        }
                    }
                    break;
                case LCObjectType.Host:
                    {
                        LCTreeNodeHost lcHost = (LCTreeNodeHost)item;
                        XElement xElement = new XElement("Host",
                            new XAttribute("TypeHost", lcHost.TypeHost.ToString()),
                            new XAttribute("NameHost", lcHost.Text),
                            new XAttribute("IP", lcHost.IP),
                            new XAttribute("Description", lcHost.Description));
                        current = xElement;
                    }
                    break;
                case LCObjectType.NoList:
                    {
                        LCTreeNodeNoList lcNoList = (LCTreeNodeNoList)item;
                        XElement xElement = new XElement("NoList",
                            new XAttribute("NameGroup", lcNoList.Text),
                            new XAttribute("Description", lcNoList.Description));
                        current = xElement;
                    }
                    break;
                case LCObjectType.SubNet:
                    {
                        LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet)item;
                        XElement xElement = new XElement("Subnet",
                            new XAttribute("NameSubnet", lcSubnet.Text),
                            new XAttribute("IPSubnet", lcSubnet.IPSubnet),
                            new XAttribute("MaskSubnet", lcSubnet.MaskSubnet),
                            new XAttribute("Description", lcSubnet.Description));
                        current = xElement;
                    }
                    break;
            }
            //рекурсивный перебор всех дочерних узлов
            if (item.Nodes.Count > 0)
            {
                xnodWorking = (LCTreeNode)item.FirstNode;
                while (xnodWorking != null)
                {
                    current.Add(SaveChildren(xnodWorking, current));
                    xnodWorking = (LCTreeNode)xnodWorking.NextNode;
                }
            }
            return current;
        }
        /// <summary>
        /// Метод сохранения XML-справочника
        /// </summary>
        public void SaveDomXML()
        {
            // Сохранение справочника надо выполнять только в случае если он вообще загружался
            if (this.correctLoad)
            {
                // Проверяем имя файла, возможно создаётся новый справочник
                if (this.fileData == "")
                {
                    this.fileData = Application.LocalUserAppDataPath + "\\LCDirectory.xml";
                }
                XElement root = new XElement("Root");
                LCTreeNode lcNode = (LCTreeNode)treeView.Nodes[0];
                XDocument xDoc = new XDocument(this.SaveChildren(lcNode, root));
                xDoc.Save(this.fileData);
            }
        }
        #endregion

        #region Поиск данных в справочнике
        /// <summary>
        /// Поиск сети по имени
        /// </summary>
        /// <param name="nameNet">Имя сети</param>
        /// <returns>Возвращает найденую сеть.</returns>
        public LCTreeNodeSubnet FindNet(string nameNet)
        {
            foreach (LCTreeNode node in AllLCTreeNode(treeView.Nodes))
            {
                if(node.LCObjectType == LCObjectType.SubNet)
                {
                    LCTreeNodeSubnet lcSubNet = (LCTreeNodeSubnet)node;
                    if(lcSubNet.Text == nameNet)
                    {
                        this.WriteListBox("Найдена сеть с именем: " + lcSubNet.Text + ".");
                        return lcSubNet;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Поиск группы по имени
        /// </summary>
        /// <param name="nameGroup">Имя сети</param>
        /// <returns>Возвращает найденую группу</returns>
        public LCTreeNodeGroup FindGroup(string nameGroup)
        {
            foreach (LCTreeNode node in AllLCTreeNode(treeView.Nodes))
            {
                if (node.LCObjectType == LCObjectType.Group)
                {
                    LCTreeNodeGroup lcGroup = (LCTreeNodeGroup)node;
                    if (lcGroup.Text == nameGroup)
                    {
                        this.WriteListBox("Найдена группа с именем: " + lcGroup.Text + ".");
                        return lcGroup;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Поиск хоста по ip адресу
        /// </summary>
        /// <param name="ip">IP адрес.</param>
        /// <returns>Возвращает найденный хост</returns>
        public LCTreeNodeHost FindHost(string ip)
        {
            foreach(LCTreeNode node in AllLCTreeNode(treeView.Nodes))
            {
                if(node.LCObjectType == LCObjectType.Host)
                {
                    LCTreeNodeHost lcHost = (LCTreeNodeHost)node;
                    if(lcHost.IP == ip)
                    {
                        this.WriteListBox("Найдена хост с ip : " + lcHost.IP + ".");
                        return lcHost;
                    }
                }
            }
            return null;
        }
        #endregion

        #region Экспорт данных из справочника
        /// <summary>
        /// Метод экспортирует все сети в JSON файл JS
        /// </summary>
        /// <param name="fileExport">Имя файла в которы осуществляется экспорт</param>
        public void ExportNetsToJSON(string fileExport)
        {
            StreamWriter sw = new StreamWriter(fileExport, false, System.Text.Encoding.UTF8);
            sw.WriteLine("var nets = [");
            bool firstNet = false;
            foreach(LCTreeNode node in AllLCTreeNode(treeView.Nodes))
            {
                if(node.LCObjectType == LCObjectType.SubNet)
                {
                    LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet)node;
                    if(firstNet)
                    {
                        sw.WriteLine(",");
                    }
                    else
                    {
                        firstNet = true;
                    }
                    string str = "\t{name:'" + lcSubnet.Text + "', gateway:'" + lcSubnet.IPSubnet + "',mask:'" + lcSubnet.MaskSubnet + "'}";
                    sw.Write(str);
                }
            }
            sw.WriteLine();
            sw.Write("];");
            sw.Close();
        }
        #endregion

        /// <summary>
        /// Возвращает узел дерева "не в списке", а если его не существует, то создает новый. 
        /// </summary>
        /// <returns>Возвращает узел дерева "не в списке".</returns>
        public TreeNode ReturnGroupNoList()
        {
            TreeNode treeNode = LCDirectory.treeView.Nodes[0];
            if (treeNode != null)
            {
                //перебор всех дочерних узлов
                foreach (TreeNode treeNodeWorking in treeNode.Nodes)
                {
                    if (((LCTreeNode)treeNodeWorking).LCObjectType == LCObjectType.NoList)
                    {
                        return treeNodeWorking;
                    }
                }
            }
            LCTreeNodeNoList lcTreeNodeNoList = new LCTreeNodeNoList
            {
                Text = "<Не в списке>",
                Description = "Компьютеры которые не добавлялись в группу",
                ContextMenuStrip = LCTreeNode.noListContextMenuStrip,
                ImageIndex = 2,
                ToolTipText = "<Не в списке>\nКомпьютеры которые не добавлялись в группу. Сообщение для отладки: Ранее группы не было!"
            };
            treeNode.Nodes.Add(lcTreeNodeNoList);
            return lcTreeNodeNoList;
        }

        /// <summary>
        /// Проверка с помощью регулярно выражения действительно ли это ip адрес
        /// </summary>
        /// <param name="ipStr">Строка с ip-адресом, которую нужно проверить.</param>
        /// <returns>Возвращает истину если введённая строка верна.</returns>
        public bool CorrectIP(ref string ipStr)
        {
            //string str = this.toolStripTextBoxIP.Text;
            string str = ipStr;
            str = str.Replace('-', '.');
            string pattern = @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
                      @"(25[0-5]|2[0-4]\d|[01]?\d\d?)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(str);
            if (match.Success)
            {
                //this.toolStripTextBoxIP.Text = match.Value;
                ipStr = match.Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Вывод сообщения в нижний ListBox компонент
        /// </summary>
        /// <param name="message">Текст сообщений</param>
        public void WriteListBox(string message)
        {
            listBox.Items.Add("[" + DateTime.Now.ToString() + "] " + message);
            listBox.SelectedIndex = listBox.Items.Count - 1;
            toolStripStatusLabel.Text = message;
        }
        /// <summary>
        /// Итератор для перебора всех узлов LCDirectory
        /// </summary>
        /// <param name="nodes">Узел с которого начинается перебор.</param>
        /// <returns>Возвращает итератор.</returns>
        public IEnumerable<TreeNode> AllLCTreeNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode c1 in nodes)
            {
                yield return c1;
                foreach (TreeNode c2 in AllLCTreeNode(c1.Nodes))
                {
                    yield return c2;
                }
            }
        }
    }
}