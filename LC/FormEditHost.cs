using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LC
{
    public partial class FormEditHost : Form
    {
        public FormEditHost()
        {
            InitializeComponent();
        }
        public FormEditHost(TreeNode treeNode)
        {
            InitializeComponent();
            this.lcTreeNodeHost = (LCTreeNodeHost)treeNode;

            this.comboBoxTypeHost.DataSource = Enum.GetValues(typeof(LCTypeHost)).Cast<LCTypeHost>()
            .Select(p => new { Name = Enum.GetName(typeof(LCTypeHost), p), Value = (int)p }).ToList();
            this.comboBoxTypeHost.DisplayMember = "Name";
            this.comboBoxTypeHost.ValueMember = "Name";

            this.comboBoxTypeHost.Text = lcTreeNodeHost.TypeHost.ToString();
            this.textBoxNameHost.Text = this.lcTreeNodeHost.Text;
            this.textBoxIP.Text = this.lcTreeNodeHost.IP;
            this.textBoxBarcode.Text = this.lcTreeNodeHost.Barcode;
            this.textBoxPassword.Text = this.lcTreeNodeHost.Password;
            this.textBoxDescription.Text = this.lcTreeNodeHost.Description;
        }
        private readonly LCTreeNodeHost lcTreeNodeHost = null;

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (this.textBoxNameHost.Text != "")
            {
                this.lcTreeNodeHost.TypeHost = (LCTypeHost)Enum.Parse(typeof(LCTypeHost), this.comboBoxTypeHost.Text);
                this.lcTreeNodeHost.Text = this.textBoxNameHost.Text;
                this.lcTreeNodeHost.Barcode = this.textBoxBarcode.Text;
                this.lcTreeNodeHost.Login = this.textBoxLogin.Text;
                this.lcTreeNodeHost.Password = this.textBoxPassword.Text;
                this.lcTreeNodeHost.Description = this.textBoxDescription.Text;
                this.lcTreeNodeHost.UpdateLC();
                this.Close();
            }
            else
            {
                this.labelErrorMessage.Text = "Не задано имя хоста";
            }
        }

        private void TextBoxBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}