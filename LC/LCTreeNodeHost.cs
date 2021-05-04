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
        ETCO,       //Киоск ЭТСО
        SPD         //Сетевое оборудование
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
        private string barcode = "";
        private string password = "";
        /// <summary>
        /// Свойство возвращающее и задающее IP-адрес хоста
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
        /// <summary>
        /// Свойство возвращающее и задающее штрих код хоста
        /// </summary>
        public string Barcode
        {
            get
            {
                return this.barcode;
            }
            set
            {
                this.barcode = value;
            }
        }
        /// <summary>
        /// Свойство возвращающее и задающее пароль хоста
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
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
        public override void UpdateLC()
        {
            ListViewItem lvi = (ListViewItem)this.Tag;
            if (lvi != null)
            {
                lvi.SubItems[0].Text = this.TypeHost.ToString();
                lvi.SubItems[2].Text = this.Text;
                lvi.SubItems[4].Text = this.DescriptionStr;
            }

            this.ToolTipText = this.TypeHost.ToString();
            this.ToolTipText += "\n" + this.Text;
            this.ToolTipText += "\n" + this.IP;
            this.ToolTipText += "\n" + this.Barcode;
            this.ToolTipText += "\n" + this.Password;
            this.ToolTipText += "\n" + this.Description;
        }
    }
}