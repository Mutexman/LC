using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LC
{
    class LCTreeNodeGroup : LCTreeNode
    {
        public LCTreeNodeGroup(): base()
        {
            this.lcObjectType = LCObjectType.Group;
        }
        public override void Save(XmlTextWriter xw)
        {
            base.Save(xw);
            xw.WriteStartElement("Group");
            xw.WriteAttributeString("NameGroup", this.Text);
            xw.WriteAttributeString("Description", this.Description); // А почему свойство ?????
            xw.WriteAttributeString("FotoFile", this.fotoFile);
        }
        public override string GetFileNameFoto()
        {
            return base.GetFileNameFoto();
        }
        public void SetFoto(string fileName)
        {
            this.fotoFile = fileName;
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
            LCTreeNodeGroup lcTreeNodeGroup = new LCTreeNodeGroup();
            lcTreeNodeGroup.Text = nameGroup;
            lcTreeNodeGroup.Description = description;
            lcTreeNodeGroup.ContextMenuStrip = LCTreeNode.groupContextMemuStrip;
            lcTreeNodeGroup.ImageIndex = 2;
            lcTreeNodeGroup.ToolTipText += nameGroup;
            lcTreeNodeGroup.ToolTipText += "\n" + description;
            this.Nodes.Add(lcTreeNodeGroup);
            this.WriteListBoxOperation("Добавлена группа : " + nameGroup);
            return lcTreeNodeGroup;
        }
        public LCTreeNodeSubnet AddSubnet(string nameSubnet, string ipSubnet, string maskSubnet)
        {
            // созадем новую сеть
            LCTreeNodeSubnet lcTreeNodeSubnet = new LCTreeNodeSubnet();
            lcTreeNodeSubnet.Text = nameSubnet;
            lcTreeNodeSubnet.ContextMenuStrip = LCTreeNode.subnetContextMenuStrip;
            lcTreeNodeSubnet.IPSubnet = ipSubnet;
            lcTreeNodeSubnet.MaskSubnet = maskSubnet;
            lcTreeNodeSubnet.ToolTipText += nameSubnet;
            this.Nodes.Add(lcTreeNodeSubnet);
            this.WriteListBoxOperation("Добавлена группа : " + nameSubnet);
            return lcTreeNodeSubnet;
        }
        /// <summary>
        /// Метод добавления компьютера к данной группе
        /// </summary>
        /// <param name="nameComputer">Имя компьютера</param>
        /// <param name="ip">IP-адрес компьютера</param>
        /// <param name="description">Описание компьютера</param>
        /// <returns>Возвращает созданный компьютер</returns>
        public LCTreeNodeComputer AddComputer(string nameComputer, string ip, string description)
        {
            // создаем новый узел Computer
            LCTreeNodeComputer lcTreeNodeComputer = new LCTreeNodeComputer();
            lcTreeNodeComputer.Text = nameComputer;
            lcTreeNodeComputer.IP = ip;
            lcTreeNodeComputer.Description = description;
            lcTreeNodeComputer.ContextMenuStrip = LCTreeNode.computerContextMenuStrip;
            lcTreeNodeComputer.ImageIndex = 3;
            lcTreeNodeComputer.ToolTipText += nameComputer;
            lcTreeNodeComputer.ToolTipText += "\n" + ip;
            lcTreeNodeComputer.ToolTipText += "\n" + description;
            this.Nodes.Add(lcTreeNodeComputer);
            this.WriteListBoxOperation("Добавлен компьтер : " + nameComputer);
            return lcTreeNodeComputer;
        }
    }
}
