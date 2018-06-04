
using System;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;

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
                                        LCTreeNodeGroup lcTreeNodeGroup = new LCTreeNodeGroup
                                        {
                                            Text = xnod.Attributes["NameGroup"].Value,
                                            Description = xnod.Attributes["Description"].Value,
                                            ContextMenuStrip = LCTreeNode.groupContextMemuStrip,
                                            ImageIndex =2
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
                                            Text = xnod.Attributes["NameGroup"].Value,
                                            Description = xnod.Attributes["Description"].Value,
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
                                            Text = xnod.Attributes["NameHost"].Value,
                                            IP = xnod.Attributes["IP"].Value,
                                            Description = xnod.Attributes["Description"].Value,
                                            TypeHost = (LCTypeHost)Enum.Parse(typeof(LCTypeHost), xnod.Attributes["TypeHost"].Value),
                                            ContextMenuStrip = LCTreeNode.computerContextMenuStrip,
                                            ImageIndex = 3
                                        };
                                        lcTreeNodeHost.ToolTipText += lcTreeNodeHost.Text;
                                        lcTreeNodeHost.ToolTipText += "\n" + lcTreeNodeHost.IP;
                                        lcTreeNodeHost.ToolTipText += "\n" + lcTreeNodeHost.Description;
                                        newNode.Nodes.Add(lcTreeNodeHost);
                                        newNode = lcTreeNodeHost;
                                    }
                                    break;
                                case "Subnet":
                                    {
                                        LCTreeNodeSubnet lcTreeNodeSubnet = new LCTreeNodeSubnet
                                        {
                                            Text = xnod.Attributes["NameSubnet"].Value,
                                            IPSubnet = xnod.Attributes["IPSubnet"].Value,
                                            MaskSubnet = xnod.Attributes["MaskSubnet"].Value,
                                            Description = xnod.Attributes["Description"].Value,
                                            ContextMenuStrip = LCTreeNode.subnetContextMenuStrip,
                                            ImageIndex = 5
                                        };
                                        lcTreeNodeSubnet.ToolTipText += lcTreeNodeSubnet.Text;
                                        lcTreeNodeSubnet.ToolTipText += "\n" + lcTreeNodeSubnet.IPSubnet;
                                        lcTreeNodeSubnet.ToolTipText += "\n" + lcTreeNodeSubnet.MaskSubnet;
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
        public static TreeView treeView = null;
        public static ListBox listBox = null;
        public static ToolStripStatusLabel toolStripStatusLabel = null;

        private string fileData;
        /// <summary>
        /// Метод создания модели DOM на основе текста
        /// </summary>
        public void CreateDOM(string fileName)
        {
            this.fileData = fileName;
            treeView.Nodes.Clear();
            XmlDocument xmlDocument = new XmlDocument();
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
                    //Проверяем существует ли папка backup
                    if (!(Directory.Exists(backupDirectory)))
                    {
                        //Создаём папку backup
                        Directory.CreateDirectory(backupDirectory);
                    }
                    //Создаём резервную копию
                    File.Copy(this.fileData, backupFileName);
                }
                try
                {
                    // Загружаем XML файл
                    xmlDocument.Load(this.fileData);
                }
                catch (XmlException e)
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
                string xmlStr = "<?xml version=\"1.0\" encoding=\"windows-1251\"?><root></root>";
                xmlDocument.LoadXml(xmlStr);
                this.WriteListBox("Создан новый, пустой справочник.");
            }
            // Получаем корневой узел элемента
            XmlNode xnodDE = xmlDocument.DocumentElement;
            // Получение корневого узла дерева
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
            AddChildren(xnodDE, node);
            // Сортируем объекты в дереве
            treeView.Sort();
            treeView.EndUpdate();
            this.WriteListBox("Справочник успешно загружен.");
            // открываем дочерние узлы узла root
            node.Expand();
        }
        #endregion

        // С использованием модели DOM сохранить справочник все таки было бы проще
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
                xnodWorking = (LCTreeNode) item.FirstNode;
                while (xnodWorking != null)
                {
                    current.Add(SaveChildren(xnodWorking, current));
                    xnodWorking = (LCTreeNode) xnodWorking.NextNode;
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
    }
}
