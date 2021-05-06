namespace LC
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxDirectory = new System.Windows.Forms.GroupBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.textBoxPathCurrentUser = new System.Windows.Forms.TextBox();
            this.textBoxPathAllUsers = new System.Windows.Forms.TextBox();
            this.radioButtonCurrentUser = new System.Windows.Forms.RadioButton();
            this.radioButtonAllUsers = new System.Windows.Forms.RadioButton();
            this.groupBoxDirectory.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(262, 348);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(182, 348);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 2;
            this.buttonAccept.Text = "Применить";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(136, 348);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(41, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ок";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // groupBoxDirectory
            // 
            this.groupBoxDirectory.Controls.Add(this.labelInfo);
            this.groupBoxDirectory.Controls.Add(this.textBoxPathCurrentUser);
            this.groupBoxDirectory.Controls.Add(this.textBoxPathAllUsers);
            this.groupBoxDirectory.Controls.Add(this.radioButtonCurrentUser);
            this.groupBoxDirectory.Controls.Add(this.radioButtonAllUsers);
            this.groupBoxDirectory.Location = new System.Drawing.Point(6, 9);
            this.groupBoxDirectory.Name = "groupBoxDirectory";
            this.groupBoxDirectory.Size = new System.Drawing.Size(331, 160);
            this.groupBoxDirectory.TabIndex = 9;
            this.groupBoxDirectory.TabStop = false;
            this.groupBoxDirectory.Text = "Место хранения справочника";
            // 
            // labelInfo
            // 
            this.labelInfo.Location = new System.Drawing.Point(24, 122);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(302, 31);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = "Для применения данной настройки, необходимо перезапустить приложение!";
            // 
            // textBoxPathCurrentUser
            // 
            this.textBoxPathCurrentUser.Location = new System.Drawing.Point(27, 92);
            this.textBoxPathCurrentUser.Name = "textBoxPathCurrentUser";
            this.textBoxPathCurrentUser.ReadOnly = true;
            this.textBoxPathCurrentUser.Size = new System.Drawing.Size(298, 20);
            this.textBoxPathCurrentUser.TabIndex = 3;
            // 
            // textBoxPathAllUsers
            // 
            this.textBoxPathAllUsers.Location = new System.Drawing.Point(26, 43);
            this.textBoxPathAllUsers.Name = "textBoxPathAllUsers";
            this.textBoxPathAllUsers.ReadOnly = true;
            this.textBoxPathAllUsers.Size = new System.Drawing.Size(299, 20);
            this.textBoxPathAllUsers.TabIndex = 2;
            // 
            // radioButtonCurrentUser
            // 
            this.radioButtonCurrentUser.AutoSize = true;
            this.radioButtonCurrentUser.Location = new System.Drawing.Point(9, 69);
            this.radioButtonCurrentUser.Name = "radioButtonCurrentUser";
            this.radioButtonCurrentUser.Size = new System.Drawing.Size(144, 17);
            this.radioButtonCurrentUser.TabIndex = 1;
            this.radioButtonCurrentUser.TabStop = true;
            this.radioButtonCurrentUser.Text = "Текущий пользователь";
            this.radioButtonCurrentUser.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllUsers
            // 
            this.radioButtonAllUsers.AutoSize = true;
            this.radioButtonAllUsers.Location = new System.Drawing.Point(9, 20);
            this.radioButtonAllUsers.Name = "radioButtonAllUsers";
            this.radioButtonAllUsers.Size = new System.Drawing.Size(118, 17);
            this.radioButtonAllUsers.TabIndex = 0;
            this.radioButtonAllUsers.TabStop = true;
            this.radioButtonAllUsers.Text = "Все пользователи";
            this.radioButtonAllUsers.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(344, 391);
            this.Controls.Add(this.groupBoxDirectory);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.buttonCancel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.groupBoxDirectory.ResumeLayout(false);
            this.groupBoxDirectory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBoxDirectory;
        private System.Windows.Forms.RadioButton radioButtonCurrentUser;
        private System.Windows.Forms.RadioButton radioButtonAllUsers;
        private System.Windows.Forms.TextBox textBoxPathCurrentUser;
        private System.Windows.Forms.TextBox textBoxPathAllUsers;
        private System.Windows.Forms.Label labelInfo;
    }
}