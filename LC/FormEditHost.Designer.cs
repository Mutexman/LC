namespace LC
{
    partial class FormEditHost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditHost));
            this.labelNameHost = new System.Windows.Forms.Label();
            this.textBoxNameHost = new System.Windows.Forms.TextBox();
            this.labelIP = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.labelTypeHost = new System.Windows.Forms.Label();
            this.comboBoxTypeHost = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelNameHost
            // 
            this.labelNameHost.AutoSize = true;
            this.labelNameHost.Location = new System.Drawing.Point(12, 40);
            this.labelNameHost.Name = "labelNameHost";
            this.labelNameHost.Size = new System.Drawing.Size(60, 13);
            this.labelNameHost.TabIndex = 0;
            this.labelNameHost.Text = "Имя хоста";
            // 
            // textBoxNameHost
            // 
            this.textBoxNameHost.Location = new System.Drawing.Point(113, 37);
            this.textBoxNameHost.Name = "textBoxNameHost";
            this.textBoxNameHost.Size = new System.Drawing.Size(313, 20);
            this.textBoxNameHost.TabIndex = 1;
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(12, 66);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(50, 13);
            this.labelIP.TabIndex = 2;
            this.labelIP.Text = "IP адрес";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(113, 63);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(313, 20);
            this.textBoxIP.TabIndex = 3;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(12, 92);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(57, 13);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "Описание";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(113, 89);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(313, 86);
            this.textBoxDescription.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(188, 213);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(81, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(110, 181);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(0, 13);
            this.labelErrorMessage.TabIndex = 7;
            // 
            // labelTypeHost
            // 
            this.labelTypeHost.AutoSize = true;
            this.labelTypeHost.Location = new System.Drawing.Point(12, 13);
            this.labelTypeHost.Name = "labelTypeHost";
            this.labelTypeHost.Size = new System.Drawing.Size(57, 13);
            this.labelTypeHost.TabIndex = 8;
            this.labelTypeHost.Text = "Тип хоста";
            // 
            // comboBoxTypeHost
            // 
            this.comboBoxTypeHost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeHost.FormattingEnabled = true;
            this.comboBoxTypeHost.Location = new System.Drawing.Point(113, 10);
            this.comboBoxTypeHost.Name = "comboBoxTypeHost";
            this.comboBoxTypeHost.Size = new System.Drawing.Size(156, 21);
            this.comboBoxTypeHost.TabIndex = 9;
            // 
            // FormEditHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(438, 270);
            this.Controls.Add(this.comboBoxTypeHost);
            this.Controls.Add(this.labelTypeHost);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.textBoxNameHost);
            this.Controls.Add(this.labelNameHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormEditHost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Хост";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNameHost;
        private System.Windows.Forms.TextBox textBoxNameHost;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.Label labelTypeHost;
        private System.Windows.Forms.ComboBox comboBoxTypeHost;
    }
}