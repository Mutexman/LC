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
        /// Метод добавления хоста к данной группе
        /// </summary>
        /// <param name="nameHost">Имя хоста</param>
        /// <param name="ip">IP-адрес хоста</param>
        /// <param name="description">Описание хоста</param>
        /// <returns>Возвращает созданный хост</returns>
        public LCTreeNodeHost AddHost(string nameHost, string ip, string description)
        {
            // создаем новый узел Host
            LCTreeNodeHost lcTreeNodeHost = new LCTreeNodeHost();
            lcTreeNodeHost.Text = nameHost;
            lcTreeNodeHost.IP = ip;
            lcTreeNodeHost.Description = description;
            lcTreeNodeHost.ContextMenuStrip = LCTreeNode.computerContextMenuStrip;
            lcTreeNodeHost.ImageIndex = 3;
            lcTreeNodeHost.ToolTipText += nameHost;
            lcTreeNodeHost.ToolTipText += "\n" + ip;
            lcTreeNodeHost.ToolTipText += "\n" + description;
            this.Nodes.Add(lcTreeNodeHost);
            this.WriteListBoxOperation("Добавлен хост : " + nameHost);
            return lcTreeNodeHost;
        }
    }
}