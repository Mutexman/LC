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
    public partial class FormFoto : Form
    {
        public FormFoto()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод загрузки фотографии в форму
        /// </summary>
        /// <param name="filename">Имя файла фотографии</param>
        public void LoadFoto(string filename)
        {
            if (filename != "")
            {
                if (File.Exists(filename))
                {
                    this.label1.Text = filename;
                    this.pictureBox.Load(filename);
                    this.Height = this.pictureBox.Image.Height;
                    this.Width = this.pictureBox.Image.Width;
                }
                else
                {
                    this.label1.Text = "Такого файла не сужествует !";
                }
                this.Show();
            }
        }
    }
}
