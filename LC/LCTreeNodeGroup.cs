using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LC
{
    /// <summary>
    /// Класс группы каких либо устройст или других групп.
    /// </summary>
    class LCTreeNodeGroup : LCTreeNode
    {
        public LCTreeNodeGroup(): base()
        {
            this.lcObjectType = LCObjectType.Group;
        }
        public override void Save(XmlTextWriter xw)
        {
            xw.WriteStartElement("Group");
            xw.WriteAttributeString("NameGroup", this.Text);
            xw.WriteAttributeString("Description", this.Description); // А почему свойство ?????
            xw.WriteAttributeString("FotoFile", this.fotoFile);
        }
        /// <summary>
        /// Метод добавления дочерней группы
        /// </summary>
        /// <param name="nameGroup">Имя группы</param>
        /// <param name="description">Описание группы</param>
        /// <param name="fotofile">Файл фотографии</param>
        /// <returns>Возвращает созданную группу</returns>
        public LCTreeNodeGroup AddGroup(string nameGroup, string description, string fotofile)
        {
            // создаем новый узел Group
            LCTreeNodeGroup lcTreeNodeGroup = new LCTreeNodeGroup
            {
                Text = nameGroup,
                Description = description,
                ContextMenuStrip = LCTreeNode.groupContextMemuStrip,
                ImageIndex = 2
            };
            lcTreeNodeGroup.ToolTipText += nameGroup;
            lcTreeNodeGroup.ToolTipText += "\n" + description;
            this.Nodes.Add(lcTreeNodeGroup);
            this.WriteListBoxOperation("Добавлена группа : " + nameGroup);
            return lcTreeNodeGroup;
        }
        /// <summary>
        /// Метод добавления сети
        /// </summary>
        /// <param name="nameSubnet">Имя сети.</param>
        /// <param name="ipSubnet">IP адрес сети.</param>
        /// <param name="maskSubnet">Маска сети.</param>
        /// <returns>Возвращает созданную сеть</returns>
        public LCTreeNodeSubnet AddSubnet(string nameSubnet, string ipSubnet, string maskSubnet)
        {
            // созадем новую сеть
            LCTreeNodeSubnet lcTreeNodeSubnet = new LCTreeNodeSubnet
            {
                Text = nameSubnet,
                ContextMenuStrip = LCTreeNode.subnetContextMenuStrip,
                IPSubnet = ipSubnet,
                MaskSubnet = maskSubnet,
                ToolTipText = nameSubnet
            };
            this.Nodes.Add(lcTreeNodeSubnet);
            this.WriteListBoxOperation("Добавлена группа : " + nameSubnet);
            return lcTreeNodeSubnet;
        }
        /// <summary>
        /// Метод добавления хоста к данной группе
        /// </summary>
        /// <param name="nameHost">Имя хоста</param>
        /// <param name="ip">IP-адрес хоста</param>
        /// <param name="description">Описание хоста</param>
        /// <returns>Возвращает созданный хост</returns>
        public LCTreeNodeHost AddHost(string nameHost, string ip, string description)
        {
            // создаем новый узел Host
            LCTreeNodeHost lcTreeNodeHost = new LCTreeNodeHost
            {
                Text = nameHost,
                IP = ip,
                Description = description,
                ContextMenuStrip = LCTreeNode.computerContextMenuStrip,
                ImageIndex = 3
            };
            lcTreeNodeHost.ToolTipText += nameHost;
            lcTreeNodeHost.ToolTipText += "\n" + ip;
            lcTreeNodeHost.ToolTipText += "\n" + description;
            this.Nodes.Add(lcTreeNodeHost);
            this.WriteListBoxOperation("Добавлен компьтер : " + nameHost);
            return lcTreeNodeHost;
        }
        public override void RemoveLC()
        {
            ((ListViewItem)this.Tag).Remove();
            base.RemoveLC();
        }
    }
}
