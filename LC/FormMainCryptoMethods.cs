using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace LC
{
    public partial class FormMain : Form
    {
        // Ключ шифрования для данного алгоритма
        static readonly byte[] cs_key = { 0x30, 0x46, 0xc7, 0xca, 0x5c, 0x30, 0x42, 0xbb, 0x70, 0xf2, 0xbf, 0xb6, 0xd5, 0x3c, 0xca, 0x68, 0x84, 0x01, 0xa5, 0x41, 0xa6, 0x44, 0x10, 0xff };
        // Инициализирующий вектор для данного алгоритма
        static readonly byte[] cs_iv = { 0x69, 0x4f, 0xb8, 0x22, 0x09, 0x5c, 0x5f, 0x4b };
        /// <summary>
        /// Криптографический провайдер
        /// </summary>
        private TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider()
        {
            Key = cs_key,
            IV = cs_iv
        };
        /// <summary>
        /// Расшифровка файла справочника
        /// </summary>
        /// <param name="inFile">Расшифровываемый файл</param>
        /// <param name="outFile">Выходной файл</param>
        private void Decrypt(string inFile, string outFile)
        {
            CryptoStream cs = null;
            FileStream fsIn = null;
            FileStream fsOut = null;
            try
            {
                fsIn = File.OpenRead(inFile);
                fsOut = File.OpenWrite(outFile);
                try
                {
                    cs = new CryptoStream(fsIn, tdes.CreateDecryptor(), CryptoStreamMode.Read);

                    int b;
                    // копируем все данные из потока ввода в поток вывода
                    while ((b = cs.ReadByte()) > -1)
                    {
                        fsOut.WriteByte((byte)b);
                    }
                    // освобождаем ресурсы
                    fsOut.Flush();
                    fsOut.Close();
                    fsIn.Close();
                }
                catch { }
                finally
                {
                    if (cs != null)
                    {
                        cs.Close();
                    }
                }
            }
            catch { }
            finally
            {
                if (fsIn != null)
                {
                    fsIn.Close();
                }
                if (fsOut != null)
                {
                    fsOut.Close();
                }
            }
        }
        /// <summary>
        /// Метод шифрования файла справочник
        /// </summary>
        /// <param name="inFile">Шифруемый файл</param>
        /// <param name="outFile">Выходной зашифрованный файл</param>
        private void Crypt(string inFile, string outFile)
        {
            CryptoStream cs = null;
            FileStream fsIn = null;
            FileStream fsOut = null;
            try
            {
                fsIn = File.OpenRead(inFile);
                fsOut = File.OpenWrite(outFile);
                try
                {
                    cs = new CryptoStream(fsOut, tdes.CreateEncryptor(), CryptoStreamMode.Write);

                    int b;
                    // копируем все данные из потока ввода в поток вывода
                    while ((b = fsIn.ReadByte()) > -1)
                    {
                        cs.WriteByte((byte)b);
                    }
                    // освобождаем ресурсы
                    fsOut.Flush();
                    fsOut.Close();
                    fsIn.Close();
                }
                catch { }
                finally
                {
                    if (cs != null)
                    {
                        cs.Close();
                    }
                }
            }
            catch { }
            finally
            {
                if (fsOut != null)
                {
                    fsOut.Close();
                }
                if (fsIn != null)
                {
                    fsIn.Close();
                }
            }
        }
    }
}