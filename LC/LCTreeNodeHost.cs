using System.Windows.Forms;

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
        public override void RemoveLC()
        {
            if (this.Tag != null)
            {
                ((ListViewItem)this.Tag).Remove();
            }
            base.RemoveLC();
        }
    }
}