using System;
using System.Windows.Forms;
using System.Net;

namespace LC
{
    /// <summary>
    /// Класс компьютерной сети.
    /// </summary>
    class LCTreeNodeSubnet : LCTreeNodeGroup
    {
        public LCTreeNodeSubnet(): base()
        {
            this.lcObjectType = LCObjectType.SubNet;
        }
        /// <summary>
        /// IP адрес сети или её шлюз.
        /// </summary>
        public string IPSubnet { get; set; }
        /// <summary>
        /// Маска сети.
        /// </summary>
        public string MaskSubnet { get; set; }
        /// <summary>
        /// Метод проверки принадлежности IP адреса сети
        /// </summary>
        /// <param name="IPaddr">Проверяемый IP-адрес</param>
        /// <returns>Возвращает истину если IP адрес пренадлежит сети, и ложь если нет</returns>
        public bool CompareIPtoSubnet(String IPaddr)
        {
            IPAddress ipAdress = IPAddress.Parse(IPaddr);
            IPAddress subnet = IPAddress.Parse(this.IPSubnet);
            IPAddress mask = IPAddress.Parse(this.MaskSubnet);

            UInt32 n = BitConverter.ToUInt32(subnet.GetAddressBytes(), 0);
            UInt32 m = BitConverter.ToUInt32(mask.GetAddressBytes(), 0);
            UInt32 i = BitConverter.ToUInt32(ipAdress.GetAddressBytes(), 0);

            Console.WriteLine(n.ToString());
            Console.WriteLine(m.ToString());
            Console.WriteLine(i.ToString());

            n &= m;
            i &= m;

            if (n == i)
            {
                return true;
            }

            return false;
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
            this.ToolTipText = this.Text;
            this.ToolTipText += "\n" + this.IPSubnet;
            this.ToolTipText += "\n" + this.MaskSubnet;
            this.ToolTipText += "\n" + this.Description;

            ListViewItem lvi = (ListViewItem)this.Tag;
            if (lvi != null)
            {
                lvi.SubItems[0].Text = this.Text;
                lvi.SubItems[1].Text = this.IPSubnet;
                lvi.SubItems[2].Text = this.MaskSubnet;
                lvi.SubItems[4].Text = this.DescriptionStr;
            }
        }
    }
}