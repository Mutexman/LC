using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LC
{
    enum TypeModeTabPage
    {
        ReadOnly,   // Режим просмотра
        Edit        // Режим редактирования
    }
    class LCTabPage : TabPage
    {
        private System.Windows.Forms.Button buttonCloseTabPage;
        public LCTabPage() : base()
        {
            this.buttonCloseTabPage = new Button();
            this.Size = new System.Drawing.Size(680, 300);
            this.buttonCloseTabPage.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.buttonCloseTabPage.Location = new System.Drawing.Point(658, -4);
            this.buttonCloseTabPage.Name = "buttonCloseTabPage";
            this.buttonCloseTabPage.Size = new System.Drawing.Size(26, 26);
            this.buttonCloseTabPage.Text = "X";
            this.buttonCloseTabPage.Click += new System.EventHandler(this.buttonCloseTabPage_Click);
            this.Controls.Add(this.buttonCloseTabPage);
            this.Padding = new System.Windows.Forms.Padding(3);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        // Ссылка на панель инструментов с кнопками комманд
        //static public ToolStrip ToolStripActions;
        // Ссылка на главный ListBox для вывода сообщений
        static public ListBox ListBoxMessage;
        // Ссылка на label в панелис статуса
        static public ToolStripLabel StatusLabel;
        // Режим в котором находиться данный TabPage
        protected TypeModeTabPage mode = TypeModeTabPage.ReadOnly;
        public virtual TypeModeTabPage Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }
        /// <summary>
        /// Ссылка на отображаемый объект
        /// </summary>
        protected LCTreeNode lcTreeNode = null;
        /// <summary>
        /// Узел дерева с объектом, свойства которого отображаются в данном TabPage
        /// </summary>
        public LCTreeNode LCTreeNode
        {
            get
            {
                return this.lcTreeNode;
            }
            set
            {
                this.lcTreeNode = value;
            }
        }
        /// <summary>
        /// Метод сохранения данных
        /// </summary>
        virtual public bool Save()
        {
            return true;
        }
        /// <summary>
        /// Метод вывода сообщений в главный ListBox
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        protected void WriteListBox(string message)
        {
            if(ListBoxMessage != null)
            {
                ListBoxMessage.Items.Add("[" + DateTime.Now.ToString() + "] " + message);
                ListBoxMessage.SelectedIndex = ListBoxMessage.Items.Count - 1;
            }
            if (StatusLabel != null)
            {
                StatusLabel.Text = message;
            }
        }
        /// <summary>
        /// Событие кнопки закрытия вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCloseTabPage_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                TabControl tc = (TabControl)this.Parent;
                tc.TabPages.Remove(this);
            }
        }
    }
}
