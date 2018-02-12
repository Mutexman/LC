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
        private bool accept = false;
        private string user = "";
        private string password = "";
        public FormLogin()
        {
            InitializeComponent();
        }
        public bool Accept
        {
            get
            {
                return this.accept;
            }
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
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Input();
            this.Close();
        }
        private void Input()
        {
            if ((this.textBoxLogin.Text != "") && (this.textBoxPassword.Text != ""))
            {
                this.accept = true;
                // Здесь нужен код который проверяет корректность учетных данных
                // И нужна ли эта проверка вообще
                this.user = this.textBoxLogin.Text;
                this.password = this.textBoxPassword.Text;
            }
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.textBoxLogin.Text = SystemInformation.UserName;
            this.textBoxPassword.Focus();
        }
    }
}
