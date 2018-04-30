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
        MFU         //Сетевое МФУ
    }
    class LCTreeNodeHost : LCTreeNode
    {
        public LCTreeNodeHost()
            : base()
        {
            this.lcObjectType = LCObjectType.Host;
        }
        static public LCTypeHost StringToTypeHost(string typeHost)
        {
            switch (typeHost)
            {
                case "ПК":
                    {
                        return LCTypeHost.COMPUTER;
                    }
                default:
                    {
                        return LCTypeHost.HOST;
                    }
            }
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
            xw.WriteAttributeString("TypeHost", "ПК");
            xw.WriteAttributeString("NameHost", this.Text);
            xw.WriteAttributeString("IP", this.ip);
            xw.WriteAttributeString("Description", this.Description); //??? почему доступно здесь только свойство
        }
    }
}