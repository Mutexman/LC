using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LC
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            this.textBoxPathAllUsers.Text = Application.CommonAppDataPath;
            this.textBoxPathCurrentUser.Text = Application.LocalUserAppDataPath;
            if (Properties.Settings.Default.AllUserAccess)
            {
                this.radioButtonAllUsers.Checked = true;
            }
            else
            {
                this.radioButtonCurrentUser.Checked = true;
            }
        }
        /// <summary>
        /// Метод сохранения настроек приложения
        /// </summary>
        private void SaveSettings()
        {
            if (this.radioButtonAllUsers.Checked)
            {
                Properties.Settings.Default.AllUserAccess = true;
            }
            else
            {
                Properties.Settings.Default.AllUserAccess = false;
            }
            // Надо обязательно сохранить настройки
            Properties.Settings.Default.Save();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.SaveSettings();
            this.Close();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            this.SaveSettings();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
