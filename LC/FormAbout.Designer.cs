namespace LC
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.labelNameProgram = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelOrganization = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNameProgram
            // 
            this.labelNameProgram.AutoSize = true;
            this.labelNameProgram.Location = new System.Drawing.Point(12, 9);
            this.labelNameProgram.Name = "labelNameProgram";
            this.labelNameProgram.Size = new System.Drawing.Size(121, 13);
            this.labelNameProgram.TabIndex = 0;
            this.labelNameProgram.Text = "Линейный специалист";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(12, 32);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(115, 13);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "Версия программы : ";
            // 
            // labelOrganization
            // 
            this.labelOrganization.AutoSize = true;
            this.labelOrganization.Location = new System.Drawing.Point(12, 55);
            this.labelOrganization.Name = "labelOrganization";
            this.labelOrganization.Size = new System.Drawing.Size(83, 13);
            this.labelOrganization.TabIndex = 2;
            this.labelOrganization.Text = "Организация : ";
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(12, 79);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(113, 13);
            this.labelAuthor.TabIndex = 3;
            this.labelAuthor.Text = "Автор : Mutex 2012 г.";
            // 
            // labelDescription
            // 
            this.labelDescription.Location = new System.Drawing.Point(12, 104);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(180, 62);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "Все права совсем не защищены. Пользуйтесь на здоровье!";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(242, 104);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(183, 115);
            this.pictureBox.TabIndex = 6;
            this.pictureBox.TabStop = false;
            // 
            // buttonOk
            // 
            this.buttonOk.Image = global::LC.Properties.Resources.Ok;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(12, 169);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(137, 34);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Я все понял.";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(424, 215);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelOrganization);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelNameProgram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "О программе";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNameProgram;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelOrganization;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}