﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LC
{
    class LCTreeNodeSubnet : LCTreeNodeGroup
    {
        public LCTreeNodeSubnet(): base()
        {
            this.lcObjectType = LCObjectType.SubNet;
        }
        private string ipsubnet;
        private string masksubnet;
        public string IPSubnet
        {
            get
            {
                return this.ipsubnet;
            }
            set
            {
                this.ipsubnet = value;
            }
        }
        public string MaskSubnet
        {
            get
            {
                return this.masksubnet;
            }
            set
            {
                this.masksubnet = value;
            }
        }
        public override void Save(XmlTextWriter xw)
        {
            //base.Save(xw);
            xw.WriteStartElement("Subnet");
            xw.WriteAttributeString("NameSubnet", this.Text);
            xw.WriteAttributeString("IPSubnet", this.IPSubnet);
            xw.WriteAttributeString("MaskSubnet", this.MaskSubnet);
            xw.WriteAttributeString("Description", this.Description); // А почему свойство ?????
            xw.WriteAttributeString("FotoFile", this.fotoFile);
        }
        /// <summary>
        /// Метод проверки принадлежности IP адреса сети
        /// </summary>
        /// <param name="IPaddr">Проверяемый IP-адрес</param>
        /// <returns>Возвращает истину если IP адрес пренадлежит сети, и ложь если нет</returns>
        public bool CompareIPtoSubnet(String IPaddr)
        {
            String strbinsubnetname = "";
            String strbinipaddr = "";
            // делим ip адрес сети на элементы массива
            string[] subnetnameoctets_array = this.ipsubnet.Split('.');
            // переводим адрес сети в двоичное представление
            foreach (string ipoctet in subnetnameoctets_array)
            {
                strbinsubnetname += ByteToBinaryString(Convert.ToByte(ipoctet));
            }
            // делим ip адрес на элементы массива
            string[] ipaddroctets_array = IPaddr.Split('.');
            // переводим адрес в двоичное представление
            foreach (string ipoctet in ipaddroctets_array)
            {
                strbinipaddr += ByteToBinaryString(Convert.ToByte(ipoctet));
            }

            int intmasksubnet = 0;
            string strmasksubnet = "";
            string[] subnetmaskoctets_array = this.masksubnet.Split('.');
            // переводим маску в двоичное представление
            foreach (string ipoctet in subnetmaskoctets_array)
            {
                strmasksubnet += ByteToBinaryString(Convert.ToByte(ipoctet));
            }
            for (int i = 0; i < strmasksubnet.Length; i++)
            {
                if (strmasksubnet[i] == '1')
                {
                    intmasksubnet++;
                }
            }

            strbinsubnetname = strbinsubnetname.Substring(0, intmasksubnet);
            strbinipaddr = strbinipaddr.Substring(0, intmasksubnet);

            if (strbinsubnetname == strbinipaddr)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Преобразование байта в двоичную строку
        /// </summary>
        /// <param name="ipi">Преобразовываемый байт</param>
        /// <returns>Двоичная строка</returns>
        private string ByteToBinaryString(byte ipi)
        {
            byte[] dv = { 128, 64, 32, 16, 8, 4, 2, 1 };
            string rc = "";
            ipi = Convert.ToByte(ipi);
            for (int i = 0; i < dv.Length; i++)
            {
                if (ipi >= dv[i])
                {
                    rc += "1";
                    ipi -= dv[i];
                }
                else
                {
                    rc += "0";
                }
            }
            return rc;
        }
    }
}
