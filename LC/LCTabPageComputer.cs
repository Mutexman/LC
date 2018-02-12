using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace LC
{
    class LCTabPageComputer : LCTabPage
    {
        /// <summary>
        /// Свойство возвращает и устанавливает режим в котором находится данная страница
        /// </summary>
        public override TypeModeTabPage Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                if ((value == TypeModeTabPage.Edit) && (this.mode == TypeModeTabPage.ReadOnly))
                {
                    this.textBoxNameComputer.ReadOnly = false;
                    this.textBoxIP.ReadOnly = false;
                    this.textBoxDescription.ReadOnly = false;
                    this.toolStripButtonEdit.Enabled = false;
                    this.toolStripButtonSave.Enabled = true;
                    this.mode = value;
                }
                if ((value == TypeModeTabPage.ReadOnly) && (this.mode == TypeModeTabPage.Edit))
                {
                    this.textBoxNameComputer.ReadOnly = true;
                    this.textBoxIP.ReadOnly = true;
                    this.textBoxDescription.ReadOnly = true;
                    this.toolStripButtonEdit.Enabled = true;
                    this.toolStripButtonSave.Enabled = false;
                    this.mode = value;
                }
            }
        }

        private LCTreeNodeComputer lcTreeNodeComputer = null;

        // Компоненты
        private System.Windows.Forms.Label labelNameGroup;
        public System.Windows.Forms.TextBox textBoxNameGroup;
        private System.Windows.Forms.Label labelNameComputer;
        public System.Windows.Forms.TextBox textBoxNameComputer;
        private System.Windows.Forms.Button buttonGetNameComputer;
        private System.Windows.Forms.Label labelIP;
        public System.Windows.Forms.TextBox textBoxIP;
        public System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonFoto;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStrip toolStripActions;

        #region Создание компонентов на TabPageComputer
        /// <summary>
        /// Метод инициализации компонентов
        /// </summary>
        private void InitializeComponent()
        {
            this.labelNameGroup = new System.Windows.Forms.Label();
            this.textBoxNameGroup = new System.Windows.Forms.TextBox();
            this.labelNameComputer = new System.Windows.Forms.Label();
            this.textBoxNameComputer = new System.Windows.Forms.TextBox();
            this.buttonGetNameComputer = new System.Windows.Forms.Button();
            this.labelIP = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFoto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripActions = new System.Windows.Forms.ToolStrip();
            this.toolStripActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelNameGroup
            // 
            this.labelNameGroup.BackColor = System.Drawing.Color.White;
            this.labelNameGroup.Location = new System.Drawing.Point(10, 10);
            this.labelNameGroup.Name = "labelNameGroup";
            this.labelNameGroup.Size = new System.Drawing.Size(140, 20);
            this.labelNameGroup.TabIndex = 0;
            this.labelNameGroup.Text = "Название группы";
            // 
            // textBoxNameGroup
            // 
            this.textBoxNameGroup.BackColor = System.Drawing.Color.White;
            this.textBoxNameGroup.Location = new System.Drawing.Point(155, 10);
            this.textBoxNameGroup.Name = "textBoxNameGroup";
            this.textBoxNameGroup.ReadOnly = true;
            this.textBoxNameGroup.Size = new System.Drawing.Size(400, 20);
            this.textBoxNameGroup.TabIndex = 1;
            // 
            // labelNameComputer
            // 
            this.labelNameComputer.BackColor = System.Drawing.Color.White;
            this.labelNameComputer.Location = new System.Drawing.Point(10, 35);
            this.labelNameComputer.Name = "labelNameComputer";
            this.labelNameComputer.Size = new System.Drawing.Size(140, 20);
            this.labelNameComputer.TabIndex = 2;
            this.labelNameComputer.Text = "Имя компьютера";
            // 
            // textBoxNameComputer
            // 
            this.textBoxNameComputer.BackColor = System.Drawing.Color.White;
            this.textBoxNameComputer.Location = new System.Drawing.Point(155, 35);
            this.textBoxNameComputer.Name = "textBoxNameComputer";
            this.textBoxNameComputer.ReadOnly = true;
            this.textBoxNameComputer.Size = new System.Drawing.Size(300, 20);
            this.textBoxNameComputer.TabIndex = 3;
            // 
            // buttonGetNameComputer
            // 
            this.buttonGetNameComputer.Location = new System.Drawing.Point(456, 34);
            this.buttonGetNameComputer.Name = "buttonGetNameComputer";
            this.buttonGetNameComputer.Size = new System.Drawing.Size(22, 22);
            this.buttonGetNameComputer.TabIndex = 4;
            this.buttonGetNameComputer.Text = "...";
            this.buttonGetNameComputer.Click += new System.EventHandler(this.buttonGetNameComputer_Click);
            // 
            // labelIP
            // 
            this.labelIP.BackColor = System.Drawing.Color.White;
            this.labelIP.Location = new System.Drawing.Point(10, 60);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(140, 20);
            this.labelIP.TabIndex = 5;
            this.labelIP.Text = "IP-адрес";
            // 
            // textBoxIP
            // 
            this.textBoxIP.BackColor = System.Drawing.Color.White;
            this.textBoxIP.Location = new System.Drawing.Point(155, 60);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(300, 20);
            this.textBoxIP.TabIndex = 6;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.BackColor = System.Drawing.Color.White;
            this.textBoxDescription.Location = new System.Drawing.Point(5, 85);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(660, 150);
            this.textBoxDescription.TabIndex = 7;
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEdit.Image = global::LC.Properties.Resources.Edit;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonEdit.Text = "Изменить";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Enabled = false;
            this.toolStripButtonSave.Image = global::LC.Properties.Resources.save;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonSave.Text = "Сохранить";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonFoto
            // 
            this.toolStripButtonFoto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFoto.Image = global::LC.Properties.Resources.Foto;
            this.toolStripButtonFoto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFoto.Name = "toolStripButtonFoto";
            this.toolStripButtonFoto.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonFoto.Text = "Фото";
            this.toolStripButtonFoto.Click += new System.EventHandler(this.toolStripButtonFoto_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripActions
            // 
            this.toolStripActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripActions.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonEdit,
            this.toolStripButtonSave,
            this.toolStripButtonFoto,
            this.toolStripSeparator});
            this.toolStripActions.Location = new System.Drawing.Point(3, 242);
            this.toolStripActions.Name = "toolStripActions";
            this.toolStripActions.Size = new System.Drawing.Size(674, 55);
            this.toolStripActions.TabIndex = 2;
            this.toolStripActions.Text = "toolStripActions";
            // 
            // LCTabPageComputer
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelNameGroup);
            this.Controls.Add(this.textBoxNameGroup);
            this.Controls.Add(this.labelNameComputer);
            this.Controls.Add(this.textBoxNameComputer);
            this.Controls.Add(this.buttonGetNameComputer);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.toolStripActions);
            this.GotFocus += new System.EventHandler(this.LCTabPageComputer_GotFocus);
            this.toolStripActions.ResumeLayout(false);
            this.toolStripActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        /// <summary>
        /// Этот конструкто используется для отображения или редактирования данных о работнике
        /// </summary>
        /// <param name="xmlNode">Узел XML документа который необходимо отобразить</param>
        /// <param name="treeNode">Узел дерева который связан с данным узлом XML</param>
        public LCTabPageComputer(TreeNode treeNode):base()
        {
            this.InitializeComponent();
            // записываем ссылку на узел дерева
            this.lcTreeNode = (LCTreeNodeComputer) treeNode;
            this.lcTreeNodeComputer = (LCTreeNodeComputer) treeNode;
            // записываем тип объекта который храним в данном TabPage
            //this.typeTabPage = TypeTabPage.Computer;
            // Заполняем поля на форме
            this.textBoxNameComputer.Text = this.lcTreeNodeComputer.Text;
            this.textBoxIP.Text = this.lcTreeNodeComputer.IP;
            this.textBoxDescription.Text = this.lcTreeNodeComputer.Description;
            // Пишем текст в заголовке
            this.Text = this.textBoxNameComputer.Text;
            // Определяем в какой группе находиться компьютер
            this.textBoxNameGroup.Text = this.lcTreeNode.ParentGroup;
            this.ToolTipText = this.textBoxNameGroup.Text + "\\" + this.textBoxNameComputer.Text +
                "\n" + "IP : " + this.textBoxIP.Text + "\n" + this.textBoxDescription.Text;
            this.toolStripButtonSave.Enabled = false;
            CommandToolStripButton.toolStrip = this.toolStripActions;
            CommandToolStripButton.CreateCommandButtons();
        }
        /// <summary>
        /// Метод сохранения данных данного TabPage
        /// </summary>
        public override bool Save()
        {
            if (this.textBoxNameComputer.Text == "")
            {
                this.WriteListBox("Сохранение невозможно, не задано имя компьютера!");
                return false;
            }
            if (this.textBoxIP.Text == "")
            {
                this.WriteListBox("Сохранение невозможно, не задано имя компьютера!");
                return false;
            }
            this.lcTreeNodeComputer.Text = this.textBoxNameComputer.Text;
            this.lcTreeNodeComputer.IP = this.textBoxIP.Text;
            this.lcTreeNodeComputer.Description = this.textBoxDescription.Text;
            this.lcTreeNodeComputer.ToolTipText = this.textBoxNameComputer.Text + "\n" + this.textBoxIP.Text + "\n" + this.textBoxDescription.Text;
            this.Text = this.textBoxNameComputer.Text;
            this.ToolTipText = this.textBoxNameGroup.Text + "\\" + this.textBoxNameComputer.Text + 
                "\n" + "IP : " + this.textBoxIP.Text + "\n" + this.textBoxDescription.Text;
            this.Mode = TypeModeTabPage.ReadOnly;
            /*
            ToolStripActions.Items["toolStripButtonEdit"].Enabled = true;
            ToolStripActions.Items["toolStripButtonSave"].Enabled = false;
             */
            this.WriteListBox("Данные компьютера " + this.Text + " сохранены.");
            return true;
        }
        private void LCTabPageComputer_GotFocus(Object sender, EventArgs e)
        {
            MessageBox.Show(this.Name);
        }
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            this.Mode = TypeModeTabPage.Edit;
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            this.Mode = TypeModeTabPage.ReadOnly;
            this.Save();
        }
        private void toolStripButtonFoto_Click(object sender, EventArgs e)
        {
            FormFoto formFoto = new FormFoto();
            formFoto.LoadFoto(this.lcTreeNode.GetFileNameFoto());
        }
        private void buttonGetNameComputer_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(this.textBoxIP.Text);
                IPHostEntry host = Dns.GetHostEntry(ip);
                string hostName = host.HostName;
                this.textBoxNameComputer.Text = hostName;
                this.WriteListBox("Имя ПК с IP " + this.textBoxIP.Text + " @ " + hostName);
            }
            catch (System.Exception myException)
            {
                this.WriteListBox("Определение имени для ПК с IP " + this.textBoxIP.Text + ":" + myException.Message);
            }
        }
    }
}
