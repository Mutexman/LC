namespace LC
{
    partial class FormEditSubnet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditSubnet));
            this.labelIPSubnet = new System.Windows.Forms.Label();
            this.textBoxIPSubnet = new System.Windows.Forms.TextBox();
            this.labelMaskSubnet = new System.Windows.Forms.Label();
            this.textBoxMaskSubnet = new System.Windows.Forms.TextBox();
            this.buttonAddSubnet = new System.Windows.Forms.Button();
            this.labelNameSubnet = new System.Windows.Forms.Label();
            this.textBoxNameSubnet = new System.Windows.Forms.TextBox();
            this.labelError = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelIPSubnet
            // 
            this.labelIPSubnet.AutoSize = true;
            this.labelIPSubnet.Location = new System.Drawing.Point(34, 47);
            this.labelIPSubnet.Name = "labelIPSubnet";
            this.labelIPSubnet.Size = new System.Drawing.Size(64, 13);
            this.labelIPSubnet.TabIndex = 0;
            this.labelIPSubnet.Text = "Адрес сети";
            // 
            // textBoxIPSubnet
            // 
            this.textBoxIPSubnet.Location = new System.Drawing.Point(113, 44);
            this.textBoxIPSubnet.Name = "textBoxIPSubnet";
            this.textBoxIPSubnet.Size = new System.Drawing.Size(140, 20);
            this.textBoxIPSubnet.TabIndex = 2;
            // 
            // labelMaskSubnet
            // 
            this.labelMaskSubnet.AutoSize = true;
            this.labelMaskSubnet.Location = new System.Drawing.Point(34, 77);
            this.labelMaskSubnet.Name = "labelMaskSubnet";
            this.labelMaskSubnet.Size = new System.Drawing.Size(66, 13);
            this.labelMaskSubnet.TabIndex = 2;
            this.labelMaskSubnet.Text = "Маска сети";
            // 
            // textBoxMaskSubnet
            // 
            this.textBoxMaskSubnet.Location = new System.Drawing.Point(113, 74);
            this.textBoxMaskSubnet.Name = "textBoxMaskSubnet";
            this.textBoxMaskSubnet.Size = new System.Drawing.Size(140, 20);
            this.textBoxMaskSubnet.TabIndex = 3;
            // 
            // buttonAddSubnet
            // 
            this.buttonAddSubnet.Location = new System.Drawing.Point(190, 237);
            this.buttonAddSubnet.Name = "buttonAddSubnet";
            this.buttonAddSubnet.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSubnet.TabIndex = 4;
            this.buttonAddSubnet.Text = "Добавить";
            this.buttonAddSubnet.UseVisualStyleBackColor = true;
            this.buttonAddSubnet.Click += new System.EventHandler(this.ButtonAddSubnet_Click);
            // 
            // labelNameSubnet
            // 
            this.labelNameSubnet.AutoSize = true;
            this.labelNameSubnet.Location = new System.Drawing.Point(34, 19);
            this.labelNameSubnet.Name = "labelNameSubnet";
            this.labelNameSubnet.Size = new System.Drawing.Size(55, 13);
            this.labelNameSubnet.TabIndex = 5;
            this.labelNameSubnet.Text = "Имя сети";
            // 
            // textBoxNameSubnet
            // 
            this.textBoxNameSubnet.Location = new System.Drawing.Point(113, 16);
            this.textBoxNameSubnet.Name = "textBoxNameSubnet";
            this.textBoxNameSubnet.Size = new System.Drawing.Size(303, 20);
            this.textBoxNameSubnet.TabIndex = 1;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(120, 205);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(0, 13);
            this.labelError.TabIndex = 7;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(113, 99);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(303, 103);
            this.textBoxDescription.TabIndex = 8;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(34, 106);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(57, 13);
            this.labelDescription.TabIndex = 9;
            this.labelDescription.Text = "Описание";
            // 
            // FormEditSubnet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(455, 271);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.textBoxNameSubnet);
            this.Controls.Add(this.labelNameSubnet);
            this.Controls.Add(this.buttonAddSubnet);
            this.Controls.Add(this.textBoxMaskSubnet);
            this.Controls.Add(this.labelMaskSubnet);
            this.Controls.Add(this.textBoxIPSubnet);
            this.Controls.Add(this.labelIPSubnet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEditSubnet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сеть";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIPSubnet;
        private System.Windows.Forms.TextBox textBoxIPSubnet;
        private System.Windows.Forms.Label labelMaskSubnet;
        private System.Windows.Forms.TextBox textBoxMaskSubnet;
        private System.Windows.Forms.Button buttonAddSubnet;
        private System.Windows.Forms.Label labelNameSubnet;
        private System.Windows.Forms.TextBox textBoxNameSubnet;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescription;
    }
}