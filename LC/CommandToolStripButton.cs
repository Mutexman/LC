using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace LC
{
    class CommandToolStripButton : ToolStripButton
    {
        // Поле в котором будет храниться ссылка на список
        public ListView listItems = null;
        // Поле на ToolStrip в котором находиться данная кнопка
        public static ToolStrip toolStrip = null;
        // Поле в котором храниться ссылка на ListBox главной формы, куда будут выводиться сообщения
        public static ListBox listBoxMessage = null;
        // Поле в котором храниться ссылака на Label панели статуса
        public static ToolStripLabel StatusLabel= null;
        // Поле в котором храниться путь к файлу хранения настроек кнопок
        public string fileName = "";
        // поле хранящее выполняемую команду
        private string command = "";
        /// <summary>
        /// Свойство возвращающее выполняемую программу или команду
        /// </summary>
        public string Command
        {
            get
            {
                return this.command;
            }
            set
            {
                this.command = value;
            }
        }
        // Поле хранящее параметры команды
        private string parameters = "";
        /// <summary>
        /// Парметры выполняемой программы или команды
        /// </summary>
        public string Parameters
        {
            get
            {
                return this.parameters;
            }
            set
            {
                this.parameters = value;
            }
        }
        // ссылка на TabControl, чтобы получать  IP адреса
        static public TabControl tabControl = null;
        /// <summary>
        /// Метод выполянющий команду для которой предназначенна данная кнопка
        /// </summary>
        public void Execute()
        {
            string ip;
            try
            {
                ip = listItems.SelectedItems[0].SubItems[0].Text;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                this.WriteListBox(e.Message);
                this.WriteListBox("Не выделен компьютер для подключения !");
                return;
            }

            string execStr = this.parameters;
            execStr = execStr.Replace("@[IP]", ip);
            execStr = execStr.Replace("@[User]", FormMain.User);
            string displayStr = execStr;
            execStr = execStr.Replace("@[Password]", FormMain.Password);
            displayStr = displayStr.Replace("@[Password]", "********");
            this.WriteListBox(" Выполняем: " + this.command + " " + displayStr);
            try
            {
                System.Diagnostics.Process.Start(this.command, execStr);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                this.WriteListBox(e.Message);
            }
            catch (InvalidOperationException e)
            {
                this.WriteListBox(e.Message);
            }

        }
        private void WriteListBox(string message)
        {
            if (listBoxMessage != null)
            {
                listBoxMessage.Items.Add("[" + DateTime.Now.ToString() + "] " + message);
                listBoxMessage.SelectedIndex = listBoxMessage.Items.Count - 1;
            }
            if (StatusLabel != null)
            {
                StatusLabel.Text = message;
            }
        }
        public void Set(string text, string command, string parameters, string toolTipText)
        {
            this.Text = text;
            this.command = command;
            this.parameters = parameters;
            this.ToolTipText = toolTipText;
        }
        /// <summary>
        /// Конструктор программируемой кнопки
        /// </summary>
        /// <param name="text">Текс который будет отображаться на кнопке</param>
        /// <param name="command">Выполняемая кнопкой команда</param>
        /// <param name="parameters">Параметры выполняемой кнопкой команды</param>
        /// <param name="click">Определять ли событие Click</param>
        public CommandToolStripButton(string text, string command, string parameters,string toolTipText,bool click)
            : base()
        {
            this.Text = text;
            this.command = command;
            this.parameters = parameters;
            this.ToolTipText = toolTipText;
            if (click)
            {
                this.Click += new EventHandler(this.CommandToolStripButton_Click);
            }
        }
        public void CommandToolStripButton_Click(object sender, EventArgs e)
        {
            this.Execute();
        }
        
        public void ReloadCommandButtons()
        {
            while (toolStrip.Items.Count > 4)
            {
                toolStrip.Items.RemoveAt(toolStrip.Items.Count - 1);
            }
            //CreateCommandButtons();
        }
        /// <summary>
        /// Метод сохранения коммандных кнопок
        /// </summary>
        private void SaveCommandButtons()
        {
            // Открытие нового XML-файла с помощью объекта XmlTextWriter
            XmlTextWriter xw = new XmlTextWriter(this.fileName, System.Text.Encoding.UTF8);
            // Запись декларации документа
            xw.WriteStartDocument();
            // Запись первого элемента
            xw.WriteStartElement("Buttons");
            for (int i = 4; i < toolStrip.Items.Count; i++)
            {
                CommandToolStripButton t = (CommandToolStripButton)toolStrip.Items[i];
                xw.WriteStartElement("Button");
                xw.WriteElementString("Text", t.Text);
                xw.WriteElementString("Command", t.Command);
                xw.WriteElementString("Parameters", t.Parameters);
                xw.WriteElementString("ToolTipText", t.ToolTipText);
                xw.WriteEndElement();
            }
            xw.WriteEndElement();
            xw.WriteEndDocument();
            xw.Flush();
            xw.Close();
        }
    }
}
