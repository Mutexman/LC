using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LC
{
    /// <summary>
    /// Типы хостов
    /// </summary>
    enum LCTypeHost
    {
        HOST,       //Неопрелённый тип устройства
        COMPUTER,   //Компьютер
        MFU,        //Сетевое МФУ
        ETCO        //Киоск ЭТСО
    }
    class LCTreeNodeHost : LCTreeNode
    {
        public LCTreeNodeHost()
            : base()
        {
            this.lcObjectType = LCObjectType.Host;
        }
        private LCTypeHost lcTypeHost = LCTypeHost.HOST;
        public LCTypeHost TypeHost
        {
            get
            {
                return this.lcTypeHost;
            }
            set
            {
                this.lcTypeHost = value;
            }
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
            xw.WriteStartElement("Host");
            xw.WriteAttributeString("TypeHost", this.TypeHost.ToString());
            xw.WriteAttributeString("NameHost", this.Text);
            xw.WriteAttributeString("IP", this.ip);
            xw.WriteAttributeString("Description", this.Description);
        }
        public override void RemoveLC()
        {
            ((ListViewItem)this.Tag).Remove();
            base.RemoveLC();
        }
    }
}