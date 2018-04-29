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
    public partial class FormLogin : Form
    {
        private string user = "";
        private string password = "";
        public FormLogin()
        {
            InitializeComponent();
        }
        public string User
        {
            get
            {
                return this.user;
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }

        }
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            this.Input();
            this.Close();
        }
        private void Input()
        {
            if (this.textBoxLogin.Text != "")
            {
                this.user = this.textBoxLogin.Text;
                Properties.Settings.Default.User = this.user;
                this.password = this.textBoxPassword.Text;
                Properties.Settings.Default.Password = this.password;
                Properties.Settings.Default.Save();
            }
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.textBoxLogin.Text = Properties.Settings.Default.User;
            this.textBoxPassword.Text = Properties.Settings.Default.Password;
            this.textBoxPassword.Focus();
        }

        private void buttonGetCurrentUser_Click(object sender, EventArgs e)
        {
            this.textBoxLogin.Text = SystemInformation.UserName;
        }
    }
}
