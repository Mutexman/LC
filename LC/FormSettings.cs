﻿using System;
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
            this.checkBoxfullScreen.Checked = Properties.Settings.Default.FullScreen;
            this.checkBoxVisibleProtocol.Checked = Properties.Settings.Default.VisibleProtocol;
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
            Properties.Settings.Default.FullScreen = this.checkBoxfullScreen.Checked;
            Properties.Settings.Default.VisibleProtocol = this.checkBoxVisibleProtocol.Checked;
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
