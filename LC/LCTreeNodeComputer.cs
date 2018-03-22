using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LC
{
    class LCTreeNodeComputer : LCTreeNode
    {
        public LCTreeNodeComputer()
            : base()
        {
            this.lcObjectType = LCObjectType.Computer;
        }
        private string ip = "";
        /// <summary>
        /// Свойство возвращающее и задающее IP-адрес компьютера
        /// </summary>
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
            xw.WriteStartElement("Computer");
            xw.WriteAttributeString("NameComputer", this.Text);
            xw.WriteAttributeString("IP", this.ip);
            xw.WriteAttributeString("Description", this.Description); //??? почему доступно здесь только свойство
        }
        public override string GetFileNameFoto()
        {
            LCTreeNode lc = (LCTreeNode)this.Parent;
            return lc.GetFileNameFoto();
        }
    }
}