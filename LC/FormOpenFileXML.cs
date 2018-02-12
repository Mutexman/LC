using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LC
{
    public partial class FormOpenFileXML : Form
    {
        private string path = "";
        public FormOpenFileXML()
        {
            InitializeComponent();

            if (Properties.Settings.Default.AllUserAccess)
            {
                this.path = Application.CommonAppDataPath;
            }
            else
            {
                this.path = Application.LocalUserAppDataPath;
            }
            // получение списка файлов в корневом каталоге
            string[] aFiles = Directory.GetFiles(this.path, "*.xml", SearchOption.TopDirectoryOnly);
            this.countXMLFiles = 0;
            // добавляем в список
            foreach (string st in aFiles)
            {
                FileInfo file = new FileInfo(st);
                // Исключаем файл конфигурации, так как он не является справочником
                if (file.Name != "config.xml")
                {
                    this.listBoxXMLFiles.Items.Add(file.Name);
                    // считаем сколько файлов добавили в список
                    this.countXMLFiles++;
                }
            }
            if (this.countXMLFiles > 0)
            {
                this.listBoxXMLFiles.SelectedIndex = 0;
            }
        }
        // Был ли произведён выбор файла
        private bool selectOk = false;
        /// <summary>
        /// Свойство показывающее был ли произведён выбор файла
        /// </summary>
        public bool SelectOk
        {
            get
            {
                return this.selectOk;
            }
        }
        // Выбранный файл
        private string openFile = "";
        /// <summary>
        /// Свойство возвращающее выбранный файл
        /// </summary>
        public string OpenFile
        {
            get
            {
                return this.openFile;
            }
        }
        // Количество найденных файлов
        private int countXMLFiles = 0;
        /// <summary>
        /// Количество найденых XML файлов, т.е. справочников
        /// </summary>
        public int CountXMLFiles
        {
            get
            {
                return this.countXMLFiles;
            }
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (listBoxXMLFiles.SelectedItem != null)
            {
                this.openFile = path + "\\" + listBoxXMLFiles.SelectedItem.ToString();
                this.selectOk = true;
                this.Close();
            }
        }
    }
}
