using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LC
{
    /// <summary>
    /// Класс сетевого МФУ или принтера
    /// </summary>
    class LCTreeNodeMFU : LCTreeNode
    {
        public LCTreeNodeMFU():base()
        {
            this.lcObjectType = LCObjectType.MFU;
        }
        private string ip;
        public string IP
        {
            get
            {
                return this.ip;
            }
            set
            {
                this.ip = value;
            }
        }
        public override void Save(XmlTextWriter xw)
        {
            base.Save(xw);
            xw.WriteStartElement("MFU");
            xw.WriteAttributeString("NameMFU", this.Text);
            xw.WriteAttributeString("IP", this.ip);
            xw.WriteAttributeString("Description", this.Description);
        }
    }
}
