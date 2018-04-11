using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LC
{
    class LCTreeNodeNoList : LCTreeNode
    {
        public LCTreeNodeNoList() : base()
        {
            this.lcObjectType = LCObjectType.NoList;
        }
        public override void Save(XmlTextWriter xw)
        {
            base.Save(xw);
            xw.WriteStartElement("NoList");
            xw.WriteAttributeString("NameGroup", this.Text);
            xw.WriteAttributeString("Description", this.Description); // А почему свойство ?????
            xw.WriteAttributeString("FotoFile", this.fotoFile);
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