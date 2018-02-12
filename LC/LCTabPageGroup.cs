using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LC
{
    class LCTabPageGroup : LCTabPage
    {
        /// <summary>
        /// Возвращает и устанавливает режим в котором находится данная страница
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
                    this.textBoxNameGroup.ReadOnly = false;
                    this.textBoxDescription.ReadOnly = false;
                    this.toolStripButtonEdit.Enabled = false;
                    this.toolStripButtonSave.Enabled = true;
                    this.buttonSelectFotoFile.Visible = true;
                    this.buttonClearFotoFile.Visible = true;
                    this.mode = value;
                }
                if ((value == TypeModeTabPage.ReadOnly) && (this.mode == TypeModeTabPage.Edit))
                {
                    this.textBoxNameGroup.ReadOnly = true;
                    this.textBoxDescription.ReadOnly = true;
                    this.toolStripButtonEdit.Enabled = true;
                    this.toolStripButtonSave.Enabled = false;
                    this.buttonSelectFotoFile.Visible = false;
                    this.buttonClearFotoFile.Visible = false;
                    this.mode = value;
                }
            }
        }

        private LCTreeNodeGroup lcTreeNodeGroup = null;
       
        private System.Windows.Forms.Label labelNameParentGroup;
        public System.Windows.Forms.TextBox textBoxNameParentGroup;
        private System.Windows.Forms.Label labelNameGroup;
        public System.Windows.Forms.TextBox textBoxNameGroup;
        private System.Windows.Forms.Label labelFotoFile;
        public System.Windows.Forms.TextBox textBoxFotoFile;
        private System.Windows.Forms.Button buttonSelectFotoFile;
        private System.Windows.Forms.Button buttonClearFotoFile;
        public System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonFoto;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStrip toolStripActions;
        private OpenFileDialog openFileDialog;

        #region Создание компонентов
        private void InitializeComponent()
        {
            this.labelNameParentGroup = new System.Windows.Forms.Label();
            this.textBoxNameParentGroup = new System.Windows.Forms.TextBox();
            this.labelNameGroup = new System.Windows.Forms.Label();
            this.textBoxNameGroup = new System.Windows.Forms.TextBox();
            this.labelFotoFile = new System.Windows.Forms.Label();
            this.textBoxFotoFile = new System.Windows.Forms.TextBox();
            this.buttonSelectFotoFile = new System.Windows.Forms.Button();
            this.buttonClearFotoFile = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFoto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripActions = new System.Windows.Forms.ToolStrip();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // labelNameParentGroup
            // 
            this.labelNameParentGroup.BackColor = System.Drawing.Color.White;
            this.labelNameParentGroup.Location = new System.Drawing.Point(10, 10);
            this.labelNameParentGroup.Name = "labelNameParentGroup";
            this.labelNameParentGroup.Size = new System.Drawing.Size(140, 20);
            this.labelNameParentGroup.TabIndex = 0;
            this.labelNameParentGroup.Text = "Состоит в группе";
            // 
            // textBoxNameParentGroup
            // 
            this.textBoxNameParentGroup.BackColor = System.Drawing.Color.White;
            this.textBoxNameParentGroup.Location = new System.Drawing.Point(153, 10);
            this.textBoxNameParentGroup.Name = "textBoxNameParentGroup";
            this.textBoxNameParentGroup.ReadOnly = true;
            this.textBoxNameParentGroup.Size = new System.Drawing.Size(300, 20);
            this.textBoxNameParentGroup.TabIndex = 1;
            // 
            // labelNameGroup
            // 
            this.labelNameGroup.BackColor = System.Drawing.Color.White;
            this.labelNameGroup.Location = new System.Drawing.Point(10, 35);
            this.labelNameGroup.Name = "labelNameGroup";
            this.labelNameGroup.Size = new System.Drawing.Size(140, 20);
            this.labelNameGroup.TabIndex = 2;
            this.labelNameGroup.Text = "Группа";
            // 
            // textBoxNameGroup
            // 
            this.textBoxNameGroup.BackColor = System.Drawing.Color.White;
            this.textBoxNameGroup.Location = new System.Drawing.Point(153, 35);
            this.textBoxNameGroup.Name = "textBoxNameGroup";
            this.textBoxNameGroup.ReadOnly = true;
            this.textBoxNameGroup.Size = new System.Drawing.Size(300, 20);
            this.textBoxNameGroup.TabIndex = 3;
            // 
            // labelFotoFile
            // 
            this.labelFotoFile.BackColor = System.Drawing.Color.White;
            this.labelFotoFile.Location = new System.Drawing.Point(10, 60);
            this.labelFotoFile.Name = "labelFotoFile";
            this.labelFotoFile.Size = new System.Drawing.Size(140, 20);
            this.labelFotoFile.TabIndex = 4;
            this.labelFotoFile.Text = "Фотография";
            // 
            // textBoxFotoFile
            // 
            this.textBoxFotoFile.BackColor = System.Drawing.Color.White;
            this.textBoxFotoFile.Location = new System.Drawing.Point(153, 60);
            this.textBoxFotoFile.Name = "textBoxFotoFile";
            this.textBoxFotoFile.ReadOnly = true;
            this.textBoxFotoFile.Size = new System.Drawing.Size(300, 20);
            this.textBoxFotoFile.TabIndex = 5;
            // 
            // buttonSelectFotoFile
            // 
            this.buttonSelectFotoFile.Location = new System.Drawing.Point(455, 59);
            this.buttonSelectFotoFile.Name = "buttonSelectFotoFile";
            this.buttonSelectFotoFile.Size = new System.Drawing.Size(22, 22);
            this.buttonSelectFotoFile.TabIndex = 6;
            this.buttonSelectFotoFile.Text = "...";
            this.buttonSelectFotoFile.Visible = false;
            this.buttonSelectFotoFile.Click += new System.EventHandler(this.buttonSelectFotoFile_Click);
            // 
            // buttonClearFotoFile
            // 
            this.buttonClearFotoFile.Location = new System.Drawing.Point(480, 59);
            this.buttonClearFotoFile.Name = "buttonClearFotoFile";
            this.buttonClearFotoFile.Size = new System.Drawing.Size(64, 22);
            this.buttonClearFotoFile.TabIndex = 7;
            this.buttonClearFotoFile.Text = "Очистить";
            this.buttonClearFotoFile.Visible = false;
            this.buttonClearFotoFile.Click += new System.EventHandler(this.buttonClearFotoFile_Click);
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
            this.textBoxDescription.TabIndex = 4;
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEdit.Image = global::LC.Properties.Resources.Edit;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonEdit.Text = "Изменить";
            this.toolStripButtonEdit.Click += new EventHandler(this.toolStripButtonEdit_Click);
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
            this.toolStripButtonSave.Click += new EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonFoto
            // 
            this.toolStripButtonFoto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFoto.Image = global::LC.Properties.Resources.Foto;
            this.toolStripButtonFoto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFoto.Name = "toolStripButtonFoto";
            this.toolStripButtonFoto.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonFoto.Text = "Фото";
            this.toolStripButtonFoto.Click +=new EventHandler(this.toolStripButtonFoto_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(23, 52);
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
            this.toolStripActions.Size = new System.Drawing.Size(494, 55);
            this.toolStripActions.TabIndex = 2;
            this.toolStripActions.Text = "toolStripActions";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Фото (*.jpg)|*.jpg|Фото (*.jpeg)|*.jpeg";
            this.openFileDialog.Title = "Выбор фотографии";
            // 
            // TabPageGroup
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelNameParentGroup);
            this.Controls.Add(this.textBoxNameParentGroup);
            this.Controls.Add(this.labelNameGroup);
            this.Controls.Add(this.textBoxNameGroup);
            this.Controls.Add(this.labelFotoFile);
            this.Controls.Add(this.textBoxFotoFile);
            this.Controls.Add(this.buttonSelectFotoFile);
            this.Controls.Add(this.buttonClearFotoFile);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.toolStripActions);

            this.Padding = new System.Windows.Forms.Padding(3);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        /// <summary>
        /// Этот конструкто используется для отображения или редактирования данных о работнике
        /// </summary>
        /// <param name="xmlNode">Узел XML документа который необходимо отобразить</param>
        /// <param name="treeNode">Узел дерева который связан с данным узлом XML</param>
        public LCTabPageGroup(TreeNode treeNode):base()
        {
            this.InitializeComponent();
            // записываем ссылку на узел дерева
            this.lcTreeNode = (LCTreeNode) treeNode;
            this.lcTreeNodeGroup = (LCTreeNodeGroup)treeNode;
            // Заполняем поля на форме
            this.textBoxNameGroup.Text = this.lcTreeNodeGroup.Text;
            this.textBoxFotoFile.Text = this.lcTreeNodeGroup.GetFileNameFoto();
            this.textBoxDescription.Text = this.lcTreeNodeGroup.Description;
            // Пишем текст в заголовке
            this.Text = this.textBoxNameGroup.Text;
            this.toolStripButtonSave.Enabled = false;
            // Определяем в какой группе находиться данная группа
            this.textBoxNameParentGroup.Text = this.lcTreeNode.ParentGroup;
            this.ToolTipText = this.textBoxNameParentGroup.Text + "\\" + this.textBoxNameGroup.Text;
        }
        /// <summary>
        /// Метод сохранения данных данного TabPage
        /// </summary>
        public override bool Save()
        {
            if (this.textBoxNameGroup.Text == "")
            {
                this.WriteListBox("Сохранение невозможно, не задано имя группы!");
                return false;
            }
            this.lcTreeNodeGroup.Text = this.textBoxNameGroup.Text;
            if (this.textBoxFotoFile.Text != "")
            {
                this.lcTreeNodeGroup.SetFoto(this.textBoxFotoFile.Text);
            }
            this.lcTreeNodeGroup.SetFoto(this.textBoxFotoFile.Text);
            this.lcTreeNodeGroup.Description = this.textBoxDescription.Text;
            this.lcTreeNodeGroup.ToolTipText = this.textBoxNameGroup.Text + "\n" + this.textBoxDescription.Text;
            this.Text = this.textBoxNameGroup.Text;
            this.ToolTipText = this.textBoxNameParentGroup.Text + "\\" + this.textBoxNameGroup.Text;
            this.mode = TypeModeTabPage.ReadOnly;
            this.WriteListBox("Данные группы " + this.Text + " сохранены.");
            return true;
        }  
        private void buttonSelectFotoFile_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFotoFile.Text = this.openFileDialog.FileName;
            }
        }
        private void buttonClearFotoFile_Click(object sender, EventArgs e)
        {
            this.textBoxFotoFile.Text = "";
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
    }
}
