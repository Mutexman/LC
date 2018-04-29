namespace LC
{
    partial class FormEditMFU
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
            this.labelMFUName = new System.Windows.Forms.Label();
            this.textBoxMFUName = new System.Windows.Forms.TextBox();
            this.labelMFUIP = new System.Windows.Forms.Label();
            this.textBoxMFUIP = new System.Windows.Forms.TextBox();
            this.labelMFUDescription = new System.Windows.Forms.Label();
            this.textBoxMFUDescription = new System.Windows.Forms.TextBox();
            this.buttonMFUSave = new System.Windows.Forms.Button();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelMFUName
            // 
            this.labelMFUName.AutoSize = true;
            this.labelMFUName.Location = new System.Drawing.Point(12, 9);
            this.labelMFUName.Name = "labelMFUName";
            this.labelMFUName.Size = new System.Drawing.Size(29, 13);
            this.labelMFUName.TabIndex = 0;
            this.labelMFUName.Text = "Имя";
            // 
            // textBoxMFUName
            // 
            this.textBoxMFUName.Location = new System.Drawing.Point(75, 6);
            this.textBoxMFUName.Name = "textBoxMFUName";
            this.textBoxMFUName.Size = new System.Drawing.Size(368, 20);
            this.textBoxMFUName.TabIndex = 1;
            // 
            // labelMFUIP
            // 
            this.labelMFUIP.AutoSize = true;
            this.labelMFUIP.Location = new System.Drawing.Point(12, 34);
            this.labelMFUIP.Name = "labelMFUIP";
            this.labelMFUIP.Size = new System.Drawing.Size(17, 13);
            this.labelMFUIP.TabIndex = 2;
            this.labelMFUIP.Text = "IP";
            // 
            // textBoxMFUIP
            // 
            this.textBoxMFUIP.Location = new System.Drawing.Point(75, 33);
            this.textBoxMFUIP.Name = "textBoxMFUIP";
            this.textBoxMFUIP.ReadOnly = true;
            this.textBoxMFUIP.Size = new System.Drawing.Size(368, 20);
            this.textBoxMFUIP.TabIndex = 3;
            // 
            // labelMFUDescription
            // 
            this.labelMFUDescription.AutoSize = true;
            this.labelMFUDescription.Location = new System.Drawing.Point(12, 62);
            this.labelMFUDescription.Name = "labelMFUDescription";
            this.labelMFUDescription.Size = new System.Drawing.Size(57, 13);
            this.labelMFUDescription.TabIndex = 4;
            this.labelMFUDescription.Text = "Описание";
            // 
            // textBoxMFUDescription
            // 
            this.textBoxMFUDescription.Location = new System.Drawing.Point(75, 59);
            this.textBoxMFUDescription.Multiline = true;
            this.textBoxMFUDescription.Name = "textBoxMFUDescription";
            this.textBoxMFUDescription.Size = new System.Drawing.Size(368, 148);
            this.textBoxMFUDescription.TabIndex = 5;
            // 
            // buttonMFUSave
            // 
            this.buttonMFUSave.Location = new System.Drawing.Point(209, 264);
            this.buttonMFUSave.Name = "buttonMFUSave";
            this.buttonMFUSave.Size = new System.Drawing.Size(75, 23);
            this.buttonMFUSave.TabIndex = 6;
            this.buttonMFUSave.Text = "Сохранить";
            this.buttonMFUSave.UseVisualStyleBackColor = true;
            this.buttonMFUSave.Click += new System.EventHandler(this.buttonMFUSave_Click);
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(72, 210);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(0, 13);
            this.labelErrorMessage.TabIndex = 7;
            // 
            // FormEditMFU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 308);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.buttonMFUSave);
            this.Controls.Add(this.textBoxMFUDescription);
            this.Controls.Add(this.labelMFUDescription);
            this.Controls.Add(this.textBoxMFUIP);
            this.Controls.Add(this.labelMFUIP);
            this.Controls.Add(this.textBoxMFUName);
            this.Controls.Add(this.labelMFUName);
            this.Name = "FormEditMFU";
            this.Text = "МФУ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMFUName;
        private System.Windows.Forms.TextBox textBoxMFUName;
        private System.Windows.Forms.Label labelMFUIP;
        private System.Windows.Forms.TextBox textBoxMFUIP;
        private System.Windows.Forms.Label labelMFUDescription;
        private System.Windows.Forms.TextBox textBoxMFUDescription;
        private System.Windows.Forms.Button buttonMFUSave;
        private System.Windows.Forms.Label labelErrorMessage;
    }
}