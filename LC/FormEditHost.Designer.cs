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
            this.textBoxBarcode = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelBarcode = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelNameHost
            // 
            resources.ApplyResources(this.labelNameHost, "labelNameHost");
            this.labelNameHost.Name = "labelNameHost";
            // 
            // textBoxNameHost
            // 
            resources.ApplyResources(this.textBoxNameHost, "textBoxNameHost");
            this.textBoxNameHost.Name = "textBoxNameHost";
            // 
            // labelIP
            // 
            resources.ApplyResources(this.labelIP, "labelIP");
            this.labelIP.Name = "labelIP";
            // 
            // textBoxIP
            // 
            resources.ApplyResources(this.textBoxIP, "textBoxIP");
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.Name = "labelDescription";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelErrorMessage
            // 
            resources.ApplyResources(this.labelErrorMessage, "labelErrorMessage");
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Name = "labelErrorMessage";
            // 
            // labelTypeHost
            // 
            resources.ApplyResources(this.labelTypeHost, "labelTypeHost");
            this.labelTypeHost.Name = "labelTypeHost";
            // 
            // comboBoxTypeHost
            // 
            this.comboBoxTypeHost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeHost.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxTypeHost, "comboBoxTypeHost");
            this.comboBoxTypeHost.Name = "comboBoxTypeHost";
            // 
            // textBoxBarcode
            // 
            resources.ApplyResources(this.textBoxBarcode, "textBoxBarcode");
            this.textBoxBarcode.Name = "textBoxBarcode";
            this.textBoxBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxBarcode_KeyPress);
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            // 
            // labelBarcode
            // 
            resources.ApplyResources(this.labelBarcode, "labelBarcode");
            this.labelBarcode.Name = "labelBarcode";
            // 
            // labelPassword
            // 
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.Name = "labelPassword";
            // 
            // FormEditHost
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelBarcode);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxBarcode);
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
            this.MaximizeBox = false;
            this.Name = "FormEditHost";
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
        private System.Windows.Forms.TextBox textBoxBarcode;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelBarcode;
        private System.Windows.Forms.Label labelPassword;
    }
}