using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareIPtoSubnet
{
    /// <summary>
    /// Класс IP сети
    /// </summary>
    class SubNet
    {
        /// <summary>
        /// Поле для хранения IP адреса сети
        /// </summary>
        private string stripsubnet;
        /// <summary>
        /// Поле для хранения маски сети в виде строки
        /// </summary>
        private string strmasksubnet;
        /// <summary>
        /// Поле для хранения маски в виде количества единиц слева
        /// </summary>
        private int intmasksubnet;
        /// <summary>
        /// Конструктор класса SubNet
        /// </summary>
        /// <param name="stripsubnet">IP адрес сети</param>
        /// <param name="strmasksubnet">Маска сети</param>
        public SubNet(string stripsubnet, string strmasksubnet)
        {
            this.stripsubnet = stripsubnet;
            this.strmasksubnet = strmasksubnet;
            strmasksubnet = strmasksubnet.Replace(".", "");
            for (int i = 0; i < strmasksubnet.Length; i++)
            {
                if (strmasksubnet[i] == '1')
                {
                    this.intmasksubnet++;
                }
            }
        }
        /// <summary>
        /// Конструктор класса SubNet
        /// </summary>
        /// <param name="stripsubnet">Адрес сети в виде IP-адрес/Маска</param>
        public SubNet(string stripsubnet)
        {
            // Отделяем адрес сети от маски
            string[] subnet_array = stripsubnet.Split('/');
            // Записываем адрес сети
            this.stripsubnet = subnet_array[0];
            // записываем количество единиц маски в переменную
            this.intmasksubnet = (subnet_array.Length == 2) ? Convert.ToInt32(subnet_array[1]) : 0;
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
            string[] subnetnameoctets_array = this.stripsubnet.Split('.');
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

            strbinsubnetname = strbinsubnetname.Substring(0, this.intmasksubnet);
            strbinipaddr = strbinipaddr.Substring(0, this.intmasksubnet);

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
        private static string ByteToBinaryString(byte ipi)
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
