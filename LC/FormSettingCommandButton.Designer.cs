namespace LC
{
    partial class FormSettingCommandButton
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettingCommandButton));
            this.toolStripSet = new System.Windows.Forms.ToolStrip();
            this.labelText = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.labelCommand = new System.Windows.Forms.Label();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.labelParameters = new System.Windows.Forms.Label();
            this.textBoxParameters = new System.Windows.Forms.TextBox();
            this.labelToolTipText = new System.Windows.Forms.Label();
            this.textBoxtoolTipText = new System.Windows.Forms.TextBox();
            this.buttonNewButton = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonPath = new System.Windows.Forms.Button();
            this.buttonAddIP = new System.Windows.Forms.Button();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.buttonAddPassword = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // toolStripSet
            // 
            this.toolStripSet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripSet.Location = new System.Drawing.Point(0, 296);
            this.toolStripSet.Name = "toolStripSet";
            this.toolStripSet.Size = new System.Drawing.Size(554, 25);
            this.toolStripSet.TabIndex = 0;
            this.toolStripSet.Text = "toolStrip1";
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(13, 13);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(37, 13);
            this.labelText.TabIndex = 1;
            this.labelText.Text = "Текст";
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(90, 10);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(336, 20);
            this.textBoxText.TabIndex = 2;
            // 
            // labelCommand
            // 
            this.labelCommand.AutoSize = true;
            this.labelCommand.Location = new System.Drawing.Point(13, 39);
            this.labelCommand.Name = "labelCommand";
            this.labelCommand.Size = new System.Drawing.Size(52, 13);
            this.labelCommand.TabIndex = 3;
            this.labelCommand.Text = "Команда";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(90, 36);
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(336, 20);
            this.textBoxCommand.TabIndex = 4;
            // 
            // labelParameters
            // 
            this.labelParameters.AutoSize = true;
            this.labelParameters.Location = new System.Drawing.Point(13, 65);
            this.labelParameters.Name = "labelParameters";
            this.labelParameters.Size = new System.Drawing.Size(66, 13);
            this.labelParameters.TabIndex = 5;
            this.labelParameters.Text = "Параметры";
            // 
            // textBoxParameters
            // 
            this.textBoxParameters.Location = new System.Drawing.Point(90, 62);
            this.textBoxParameters.Name = "textBoxParameters";
            this.textBoxParameters.Size = new System.Drawing.Size(336, 20);
            this.textBoxParameters.TabIndex = 6;
            // 
            // labelToolTipText
            // 
            this.labelToolTipText.AutoSize = true;
            this.labelToolTipText.Location = new System.Drawing.Point(13, 122);
            this.labelToolTipText.Name = "labelToolTipText";
            this.labelToolTipText.Size = new System.Drawing.Size(63, 13);
            this.labelToolTipText.TabIndex = 7;
            this.labelToolTipText.Text = "Подсказка";
            // 
            // textBoxtoolTipText
            // 
            this.textBoxtoolTipText.Location = new System.Drawing.Point(90, 122);
            this.textBoxtoolTipText.Multiline = true;
            this.textBoxtoolTipText.Name = "textBoxtoolTipText";
            this.textBoxtoolTipText.Size = new System.Drawing.Size(336, 58);
            this.textBoxtoolTipText.TabIndex = 8;
            // 
            // buttonNewButton
            // 
            this.buttonNewButton.Location = new System.Drawing.Point(467, 7);
            this.buttonNewButton.Name = "buttonNewButton";
            this.buttonNewButton.Size = new System.Drawing.Size(75, 23);
            this.buttonNewButton.TabIndex = 9;
            this.buttonNewButton.Text = "Новая";
            this.buttonNewButton.UseVisualStyleBackColor = true;
            this.buttonNewButton.Click += new System.EventHandler(this.buttonNewButton_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(467, 33);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 10;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(351, 183);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(419, 260);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(123, 23);
            this.buttonSaveAndClose.TabIndex = 12;
            this.buttonSaveAndClose.Text = "Закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(426, 35);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(22, 22);
            this.buttonPath.TabIndex = 13;
            this.buttonPath.Text = "...";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            // 
            // buttonAddIP
            // 
            this.buttonAddIP.Location = new System.Drawing.Point(90, 88);
            this.buttonAddIP.Name = "buttonAddIP";
            this.buttonAddIP.Size = new System.Drawing.Size(75, 23);
            this.buttonAddIP.TabIndex = 14;
            this.buttonAddIP.Text = "+@[IP]";
            this.buttonAddIP.UseVisualStyleBackColor = true;
            this.buttonAddIP.Click += new System.EventHandler(this.buttonAddIP_Click);
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Location = new System.Drawing.Point(171, 88);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(75, 23);
            this.buttonAddUser.TabIndex = 15;
            this.buttonAddUser.Text = "+@[User]";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // buttonAddPassword
            // 
            this.buttonAddPassword.Location = new System.Drawing.Point(252, 88);
            this.buttonAddPassword.Name = "buttonAddPassword";
            this.buttonAddPassword.Size = new System.Drawing.Size(89, 23);
            this.buttonAddPassword.TabIndex = 16;
            this.buttonAddPassword.Text = "+@[Password]";
            this.buttonAddPassword.UseVisualStyleBackColor = true;
            this.buttonAddPassword.Click += new System.EventHandler(this.buttonAddPassword_Click);
            // 
            // FormSettingCommandButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(554, 321);
            this.Controls.Add(this.buttonAddPassword);
            this.Controls.Add(this.buttonAddUser);
            this.Controls.Add(this.buttonAddIP);
            this.Controls.Add(this.buttonPath);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonNewButton);
            this.Controls.Add(this.textBoxtoolTipText);
            this.Controls.Add(this.labelToolTipText);
            this.Controls.Add(this.textBoxParameters);
            this.Controls.Add(this.labelParameters);
            this.Controls.Add(this.textBoxCommand);
            this.Controls.Add(this.labelCommand);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.toolStripSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettingCommandButton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка кнопок";
            this.Load += new System.EventHandler(this.FormSettingCommandButton_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripSet;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label labelCommand;
        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.Label labelParameters;
        private System.Windows.Forms.TextBox textBoxParameters;
        private System.Windows.Forms.Label labelToolTipText;
        private System.Windows.Forms.TextBox textBoxtoolTipText;
        private System.Windows.Forms.Button buttonNewButton;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.Button buttonAddIP;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.Button buttonAddPassword;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}