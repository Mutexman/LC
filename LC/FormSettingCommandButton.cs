using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace LC
{
    public partial class FormSettingCommandButton : Form
    {
        private CommandToolStripButton activeButton = null;
        public FormSettingCommandButton()
        {
            InitializeComponent();
        }
        private void FormSettingCommandButton_Load(object sender, EventArgs e)
        {
            this.CreateButtons();
            // Если есть одна и более кнопок, то делаем активной первую
            if (toolStripSet.Items.Count > 0)
            {
                this.toolStripSet.Items[0].Select();
                this.activeButton = (CommandToolStripButton) this.toolStripSet.Items[0];
                this.textBoxText.Text = this.activeButton.Text;
                this.textBoxCommand.Text = this.activeButton.Command;
                this.textBoxParameters.Text = this.activeButton.Parameters;
                this.textBoxtoolTipText.Text = this.activeButton.ToolTipText;
            }
        }

        private void CreateButtons()
        {
            // Загрузка файла в объект XmlDocument
            XmlDocument xd = new XmlDocument();
            if (File.Exists(Application.LocalUserAppDataPath + "\\config.xml"))
            {
                xd.Load(Application.LocalUserAppDataPath + "\\config.xml");
            }
            else
            {
                // Здесь надо бы создавать пустой файл командных кнопок
                string xmlStr = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                xmlStr += "<Buttons></Buttons>";
                xd.LoadXml(xmlStr);
            }
            XmlNode xn = xd.DocumentElement;
            if (xn.NodeType == XmlNodeType.Element)
            {
                if (xn.Name == "Buttons")
                {
                    CommandToolStripButton progBut = null;
                    XmlNode workxn = xn.FirstChild;
                    while (workxn != null)
                    {
                        if (workxn.Name == "Button")
                        {
                            string text = "";
                            string command = "";
                            string parameters = "";
                            string toolTipText = "";
                            if (workxn.ChildNodes[0].ChildNodes[0] != null)
                            {
                                text = workxn.ChildNodes[0].ChildNodes[0].Value;
                            }
                            if (workxn.ChildNodes[1].ChildNodes[0] != null)
                            {
                                command = workxn.ChildNodes[1].ChildNodes[0].Value;
                            }
                            if (workxn.ChildNodes[2].ChildNodes[0] != null)
                            {
                                parameters = workxn.ChildNodes[2].ChildNodes[0].Value;
                            }
                            if (workxn.ChildNodes[3].ChildNodes[0] != null)
                            {
                                toolTipText = workxn.ChildNodes[3].ChildNodes[0].Value;
                            }
                            progBut = new CommandToolStripButton(text, command, parameters, toolTipText,false);
                            progBut.Click += new EventHandler(this.CommandToolStripButton_ClickTest);
                            progBut.ImageScaling = ToolStripItemImageScaling.None;
                            this.toolStripSet.Items.Add(progBut);
                        }
                        workxn = workxn.NextSibling;
                    }
                }
            }
        }

        private void SaveButtons()
        {
            // Открытие нового XML-файла с помощью объекта XmlTextWriter
            XmlTextWriter xw = new XmlTextWriter(Application.LocalUserAppDataPath + "\\config.xml", System.Text.Encoding.UTF8);
            // Запись декларации документа
            xw.WriteStartDocument();
            // Запись первого элемента
            xw.WriteStartElement("Buttons");
            for (int i = 0; i < this.toolStripSet.Items.Count; i++)
            {
                CommandToolStripButton t = (CommandToolStripButton) this.toolStripSet.Items[i];
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

        public void CommandToolStripButton_ClickTest(object sender, EventArgs e)
        {
            this.activeButton = (CommandToolStripButton) sender;
            this.textBoxText.Text = this.activeButton.Text;
            this.textBoxCommand.Text = this.activeButton.Command;
            this.textBoxParameters.Text = this.activeButton.Parameters;
            this.textBoxtoolTipText.Text = this.activeButton.ToolTipText;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (this.activeButton != null)
            {
                if (this.textBoxText.Text != "")
                {
                    this.activeButton.Text = this.textBoxText.Text;
                    this.activeButton.Command = this.textBoxCommand.Text;
                    this.activeButton.Parameters = this.textBoxParameters.Text;
                    this.activeButton.ToolTipText = this.textBoxtoolTipText.Text;
                }
                else
                {
                    MessageBox.Show("Не задано поле текст!", "Линейный специалист", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonNewButton_Click(object sender, EventArgs e)
        {
            CommandToolStripButton ct = new CommandToolStripButton("Новая кнопка", "", "", "Новая кнопка", false);
            ct.Click += new EventHandler(this.CommandToolStripButton_ClickTest);
            this.activeButton = ct;
            this.toolStripSet.Items.Add(ct);
            this.textBoxText.Text = ct.Text;
            this.textBoxCommand.Text = ct.Command;
            this.textBoxParameters.Text = ct.Parameters;
            this.textBoxtoolTipText.Text = ct.ToolTipText;
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (this.activeButton != null)
            {
                this.textBoxText.Text = "";
                this.textBoxCommand.Text = "";
                this.textBoxParameters.Text = "";
                this.textBoxtoolTipText.Text = "";
                this.toolStripSet.Items.Remove(this.activeButton);
            }
        }
        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            this.SaveButtons();
            //ReloadCommandButtons();
            this.Close();
        }
        private void buttonPath_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            this.textBoxCommand.Text = openFileDialog.FileName;
        }
        private void buttonAddIP_Click(object sender, EventArgs e)
        {
            this.textBoxParameters.Text += "@[IP]";
        }
        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            this.textBoxParameters.Text += "@[User]";
        }
        private void buttonAddPassword_Click(object sender, EventArgs e)
        {
            this.textBoxParameters.Text += "@[Password]";
        }
    }
}
