using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LC
{
    class LCTabPageSubnet : LCTabPage
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
                    this.textBoxNameSubnet.ReadOnly = false;
                    this.textBoxIPSubnet.ReadOnly = false;
                    this.textBoxMaskSubnet.ReadOnly = false;
                    this.textBoxDescription.ReadOnly = false;
                    this.toolStripButtonEdit.Enabled = false;
                    this.toolStripButtonSave.Enabled = true;
                    this.mode = value;
                }
                if ((value == TypeModeTabPage.ReadOnly) && (this.mode == TypeModeTabPage.Edit))
                {
                    this.textBoxNameSubnet.ReadOnly = true;
                    this.textBoxIPSubnet.ReadOnly = true;
                    this.textBoxMaskSubnet.ReadOnly = true;
                    this.textBoxDescription.ReadOnly = true;
                    this.toolStripButtonEdit.Enabled = true;
                    this.toolStripButtonSave.Enabled = false;
                    this.mode = value;
                }
            }
        }

        private LCTreeNodeSubnet lcTreeNodeSubnet = null;
       
        private System.Windows.Forms.Label labelNameParentGroup;
        public System.Windows.Forms.TextBox textBoxNameParentGroup;
        private System.Windows.Forms.Label labelNameSubnet;
        public System.Windows.Forms.TextBox textBoxNameSubnet;
        private System.Windows.Forms.Label labelIPSunet;
        private System.Windows.Forms.TextBox textBoxIPSubnet;
        private System.Windows.Forms.Label labelMaskSubnet;
        private System.Windows.Forms.TextBox textBoxMaskSubnet;
        public System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStrip toolStripActions;

        #region Создание компонентов
        private void InitializeComponent()
        {
            this.labelNameParentGroup = new System.Windows.Forms.Label();
            this.textBoxNameParentGroup = new System.Windows.Forms.TextBox();
            this.labelNameSubnet = new System.Windows.Forms.Label();
            this.textBoxNameSubnet = new System.Windows.Forms.TextBox();
            this.labelIPSunet = new System.Windows.Forms.Label();
            this.textBoxIPSubnet = new System.Windows.Forms.TextBox();
            this.labelMaskSubnet = new System.Windows.Forms.Label();
            this.textBoxMaskSubnet = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripActions = new System.Windows.Forms.ToolStrip();
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
            // labelNameSubnet
            // 
            this.labelNameSubnet.BackColor = System.Drawing.Color.White;
            this.labelNameSubnet.Location = new System.Drawing.Point(10, 35);
            this.labelNameSubnet.Name = "labelNameGroup";
            this.labelNameSubnet.Size = new System.Drawing.Size(140, 20);
            this.labelNameSubnet.TabIndex = 2;
            this.labelNameSubnet.Text = "Имя сети";
            // 
            // textBoxNameSubnet
            // 
            this.textBoxNameSubnet.BackColor = System.Drawing.Color.White;
            this.textBoxNameSubnet.Location = new System.Drawing.Point(153, 35);
            this.textBoxNameSubnet.Name = "textBoxNameSubnet";
            this.textBoxNameSubnet.ReadOnly = true;
            this.textBoxNameSubnet.Size = new System.Drawing.Size(300, 20);
            this.textBoxNameSubnet.TabIndex = 3;
            //
            // labelIPSubnet
            //
            this.labelIPSunet.BackColor = System.Drawing.Color.White;
            this.labelIPSunet.Location = new System.Drawing.Point(10, 60);
            this.labelIPSunet.Name = "labelIPSubnet";
            this.labelIPSunet.Size = new System.Drawing.Size(140, 20);
            this.labelIPSunet.TabIndex = 4;
            this.labelIPSunet.Text = "IP сети";
            //
            // textBoxIPSubnet
            //
            this.textBoxIPSubnet.BackColor = System.Drawing.Color.White;
            this.textBoxIPSubnet.Location = new System.Drawing.Point(153, 60);
            this.textBoxIPSubnet.Name = "textBoxIPSubnt";
            this.textBoxIPSubnet.ReadOnly = true;
            this.textBoxIPSubnet.Size = new System.Drawing.Size(300, 20);
            this.textBoxIPSubnet.TabIndex = 5;
            //
            // labelMaskSubnet
            //
            this.labelMaskSubnet.BackColor = System.Drawing.Color.White;
            this.labelMaskSubnet.Location = new System.Drawing.Point(10, 85);
            this.labelMaskSubnet.Name = "labelMaskSubnet";
            this.labelMaskSubnet.Size = new System.Drawing.Size(140, 20);
            this.labelMaskSubnet.TabIndex = 6;
            this.labelMaskSubnet.Text = "Маска сети";
            //
            // textBoxMaskSubnet
            //
            this.textBoxMaskSubnet.BackColor = System.Drawing.Color.White;
            this.textBoxMaskSubnet.Location = new System.Drawing.Point(153, 85);
            this.textBoxMaskSubnet.Name = "textBoxMaskSubnet";
            this.textBoxMaskSubnet.ReadOnly = true;
            this.textBoxMaskSubnet.Size = new System.Drawing.Size(300, 20);
            this.textBoxMaskSubnet.TabIndex = 6;

            //
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.BackColor = System.Drawing.Color.White;
            this.textBoxDescription.Location = new System.Drawing.Point(5, 110);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(660, 130);
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
            this.toolStripSeparator});
            this.toolStripActions.Location = new System.Drawing.Point(3, 242);
            this.toolStripActions.Name = "toolStripActions";
            this.toolStripActions.Size = new System.Drawing.Size(494, 55);
            this.toolStripActions.TabIndex = 2;
            this.toolStripActions.Text = "toolStripActions";
            // 
            // TabPageSubnet
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelNameParentGroup);
            this.Controls.Add(this.textBoxNameParentGroup);
            this.Controls.Add(this.labelNameSubnet);
            this.Controls.Add(this.textBoxNameSubnet);
            this.Controls.Add(this.labelIPSunet);
            this.Controls.Add(this.textBoxIPSubnet);
            this.Controls.Add(this.labelMaskSubnet);
            this.Controls.Add(this.textBoxMaskSubnet);
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
        /// <param name="treeNode">Узел дерева который связан с данным узлом XML</param>
        public LCTabPageSubnet(TreeNode treeNode):base()
        {
            this.InitializeComponent();
            // записываем ссылку на узел дерева
            this.lcTreeNode = (LCTreeNode) treeNode;
            this.lcTreeNodeSubnet = (LCTreeNodeSubnet)treeNode;
            // Заполняем поля на форме
            this.textBoxNameSubnet.Text = this.lcTreeNodeSubnet.Text;
            this.textBoxIPSubnet.Text = this.lcTreeNodeSubnet.IPSubnet;
            this.textBoxMaskSubnet.Text = this.lcTreeNodeSubnet.MaskSubnet;
            this.textBoxDescription.Text = this.lcTreeNodeSubnet.Description;
            // Пишем текст в заголовке
            this.Text = this.textBoxNameSubnet.Text;
            this.toolStripButtonSave.Enabled = false;
            // Определяем в какой группе находиться данная группа
            this.textBoxNameParentGroup.Text = this.lcTreeNode.ParentGroup;
            this.ToolTipText = this.textBoxNameParentGroup.Text + "\\" + this.textBoxNameSubnet.Text;
        }
        /// <summary>
        /// Метод сохранения данных данного TabPage
        /// </summary>
        public override bool Save()
        {
            if (this.textBoxNameSubnet.Text == "")
            {
                this.WriteListBox("Сохранение невозможно, не задано имя сети!");
                return false;
            }
            this.lcTreeNodeSubnet.Text = this.textBoxNameSubnet.Text;
            this.lcTreeNodeSubnet.IPSubnet = this.textBoxIPSubnet.Text;
            this.lcTreeNodeSubnet.MaskSubnet = this.textBoxMaskSubnet.Text;
            this.lcTreeNodeSubnet.Description = this.textBoxDescription.Text;
            this.lcTreeNodeSubnet.ToolTipText = this.textBoxNameSubnet.Text + "\n" + this.textBoxDescription.Text;
            this.Text = this.textBoxNameSubnet.Text;
            this.ToolTipText = this.textBoxNameParentGroup.Text + "\\" + this.textBoxNameSubnet.Text;
            this.mode = TypeModeTabPage.ReadOnly;
            this.WriteListBox("Данные сети " + this.Text + " сохранены.");
            return true;
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
    }
}
