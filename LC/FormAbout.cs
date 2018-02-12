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
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }
        private void FormAbout_Load(object sender, EventArgs e)
        {
            this.labelNameProgram.Text += " : " + this.ProductName;
            this.labelVersion.Text += this.ProductVersion;
            this.labelOrganization.Text += this.CompanyName.ToString();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
