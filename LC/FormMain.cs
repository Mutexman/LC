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
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LC
{
    public partial class FormMain : Form
    {
        // поле показывающее был ли корректно осуществлен ввод логина и пароля
        private bool logged = false;
        // ссылка на найденную подсеть
        private TreeNode findSubnet= null;
        // поле показывающее корректно ли был загружен справочник
        private bool correctLoad = true;
        public static string User = "";
        public static string Password = "";
        /// <summary>
        /// Переменная содержащая имя файла в котором храниться справочник
        /// </summary>
        private string fileData = Application.LocalUserAppDataPath + "\\Computers.xml";
        private string fileDataTripleDES = Application.LocalUserAppDataPath + "\\Computers.cxml";
        /// <summary>
        /// Возвращает путь к файлу зашифрованного справочника
        /// </summary>
        private string FileDataTripleDES
        {
            get
            {
                if (Properties.Settings.Default.AllUserAccess)
                {
                    return Application.CommonAppDataPath + "\\Computers.xml";
                }
                else
                {
                    return Application.LocalUserAppDataPath + "\\Computers.xml";
                }
            }
        }
        /// <summary>
        /// Переменная содержащая имя файла конфигурации кнопок
        /// </summary>
        private string fileConfig = Application.LocalUserAppDataPath + "\\config.xml";
        private BufferLCTreeNode buffer = new BufferLCTreeNode();
        // Поле для сохранения найденых компьютеров
        private static int countFind = 0;
        public FormMain()
        {
            InitializeComponent();
            if (Properties.Settings.Default.FullScreen)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            if (Properties.Settings.Default.VisibleProtocol)
            {
                this.splitContainer1.Panel2Collapsed = true;
            }
        }

        #region События формы
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Выводим в заголовок номер версии программы
            this.Text += " " + Application.ProductVersion;
            // Проверяем был ли введены параметры, логин и пароль
            if(Environment.GetCommandLineArgs().Length >= 3)
            {
                // Параметр были переданы, запоминаем логин и пароль пользователя
                FormMain.User = Environment.GetCommandLineArgs()[1];
                FormMain.Password = Environment.GetCommandLineArgs()[2];
                this.logged = true;
            }
            else
            {
                FormLogin formLogin = new FormLogin();
                formLogin.ShowDialog();
                if (formLogin.Accept)
                {
                    FormMain.User = formLogin.User;
                    FormMain.Password = formLogin.Password;
                    this.logged = true;
                }
            }
            if (this.logged)
            {
                // Выводим имя пользователя в заголовок формы
                this.Text += " Пользователь: " + FormMain.User;
                LCTreeNode.SetListBoxOperation(this.listBoxOperation);
                LCTreeNode.rootContextMenuStrip = this.contextMenuStripLCComputer;
                LCTreeNode.groupContextMemuStrip = this.contextMenuStripLCGroup;
                LCTreeNode.computerContextMenuStrip = this.contextMenuStripLCComputer;
                LCTreeNode.subnetContextMenuStrip = this.contextMenuStripLCSubnet;
                LCTreeNode.StatusLabel = this.toolStripStatusLabelMain;
                LCTabPage.ListBoxMessage = this.listBoxOperation;
                LCTabPage.StatusLabel = this.toolStripStatusLabelMain;
                CommandToolStripButton.StatusLabel = this.toolStripStatusLabelMain;
                CommandToolStripButton.listBoxMessage = this.listBoxOperation;
                CommandToolStripButton.tabControl = this.tabControlObject;
                FormNewComputer.treeView = this.treeViewObject;
                FormOpenFileXML formOpenFileXML = new FormOpenFileXML();
                formOpenFileXML.ShowDialog();
                this.fileData = formOpenFileXML.OpenFile;
                // Проверяем существует ли файл config.xml. Если его нет, то создаем.
                if (!(File.Exists(this.fileConfig)))
                {
                    this.CreateDefaultFileConfig();
                }
                this.CreateDOM();
                // Восстанавливаем открытые вкладки
                this.OpenSavedPages();
                // Загружаем плагины
                this.LoadPlugins();
                // Открываем компьютер ip адрес которого был передан в параметрах командной строки
                if (Environment.GetCommandLineArgs().Length >= 4)
                {
                    this.toolStripTextBoxIP.Text = Environment.GetCommandLineArgs()[3];
                    this.toolStripButtonFind_Click(null, null);
                }
            }
            else
            {
                this.Close();
            }
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Сохраняем справочник
            this.SaveXML();
            // Сохраняем открытые вкладки
            this.SaveOpenedPages();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.logged)
            {
                if (MessageBox.Show("Вы действительно хотите завершить работу приложения ? " +
                    "При следующем запуске приложения, Вам опять потребуется вводить свой логин и пароль! Рекомендуется свернуть программу на панель задач.",
                    "Линейный специалист", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Главная панель инструментов
        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            // здесь надо провести проверку корректности введенного значения IP-адреса
            if (this.CorrectIP())
            {
                this.WriteListBox("Поиск компьютера с IP " + this.toolStripTextBoxIP.Text + " запущен.");
                // проверяем на всякий пожарный, не пустое ли дерево справочника
                if (this.treeViewObject.Nodes.Count > 0)
                {
                    countFind = 0;
                    this.FindComputer_IP(this.treeViewObject.Nodes[0], this.toolStripTextBoxIP.Text);
                }
                else
                {
                    return;
                }
                this.WriteListBox("Поиск завершён. Найдено : " + countFind.ToString());
                // Ищем принадлежность ПК к какой либо сети
                if (countFind == 0)
                {
                    this.findSubnet = null;
                    this.FindSubnet_IP(this.treeViewObject.Nodes[0], this.toolStripTextBoxIP.Text);
                    if (this.findSubnet != null)
                    {
                        LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet) this.findSubnet;
                        lcSubnet.AddComputer(this.toolStripTextBoxIP.Text, this.toolStripTextBoxIP.Text, "");
                        // и сразу же выделяем этот объект
                        countFind = 0;
                        this.FindComputer_IP(this.findSubnet, this.toolStripTextBoxIP.Text);
                    }
                    else
                    {
                        LCTreeNodeGroup lcGroup = (LCTreeNodeGroup) this.ReturnGroupNoList();
                        lcGroup.AddComputer(this.toolStripTextBoxIP.Text, this.toolStripTextBoxIP.Text, "");
                        // и сразу же выделяем этот объект
                        countFind = 0;
                        this.FindComputer_IP(this.ReturnGroupNoList(), this.toolStripTextBoxIP.Text);
                    }
                }
            }
            else
            {
                this.WriteListBox("Введенное значение не является IP адресом");
            }
        }
        private void toolStripTextBoxIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.toolStripButtonFind_Click(sender, e);
            }
        }

        /// <summary>
        /// Поиск компьютера по IP
        /// </summary>
        /// <param name="treeNode">Узел дерева с которого начинаем искать</param>
        /// <param name="ip">ip-адрес</param>
        private void FindComputer_IP(TreeNode treeNode, string ip)
        {
            LCTreeNode lcTreeNodeWork = (LCTreeNode)treeNode;
            if (lcTreeNodeWork.LCObjectType == LCObjectType.Computer)
            {
                // Этот узел компьютер, приводим объект к нужному классу
                LCTreeNodeComputer lcComp = (LCTreeNodeComputer)lcTreeNodeWork;
                // Проверяем по IP-адресу
                if (lcComp.IP == ip)
                {
                    if (lcComp.TabPage == null)
                    {
                        // Делаем активным компьютер в дереве справочника
                        this.treeViewObject.SelectedNode = lcComp;
                        // Создаём вкладку
                        lcComp.CreateTabPage(this.tabControlObject);
                        // Делаем созданую вкладку активной
                        this.tabControlObject.SelectedIndex = this.tabControlObject.TabPages.Count - 1;
                    }
                    else
                    {
                        this.tabControlObject.SelectedTab = lcComp.TabPage;
                    }
                    countFind++;
                    this.WriteListBox("Найден компьютер с именем: " + lcComp.Text + ".");
                }
            }
            else
            {
                // рекурсивный перебор всех дочерних узлов
                foreach (TreeNode treeNodeWorking in treeNode.Nodes)
                {
                    this.FindComputer_IP(treeNodeWorking, ip);
                }
            }
        }
        /// <summary>
        /// Поиск сети по IP
        /// </summary>
        /// <param name="treeNode">Узел дерева с которго начинаем искать</param>
        /// <param name="ip">ip-адрес</param>
        private void FindSubnet_IP(TreeNode treeNode, string ip)
        {
            LCTreeNode lcTreeNodeWork = (LCTreeNode)treeNode;
            if (lcTreeNodeWork.LCObjectType == LCObjectType.SubNet)
            {
                // Этот узел сеть, приводим объект к нужному классу
                LCTreeNodeSubnet lcSubnet = (LCTreeNodeSubnet)lcTreeNodeWork;
                // Проверяем принадлежит ли IP адрес сети
                if (lcSubnet.CompareIPtoSubnet(ip))
                {
                    this.findSubnet = lcTreeNodeWork;
                    this.WriteListBox("IP адрес " + ip + " принадлежит сети " + lcSubnet.Text);
                    return;
                }
            }
            // рекурсивный перебор всех дочерних узлов
            foreach (TreeNode treeNodeWorking in treeNode.Nodes)
            {
                this.FindSubnet_IP(treeNodeWorking, ip);
            }
        }
        private void toolStripButtonRunCMD_Click(object sender, EventArgs e)
        {
            // запускаем интерпретатор коммандной строки cmd.exe
            System.Diagnostics.Process.Start("cmd.exe");
        }
        private void toolStripButtonMutexCMD_Click(object sender, EventArgs e)
        {
            string str = Application.StartupPath + "\\MutexCMD.exe";
            if (File.Exists(str))
            {
                // Запускаем мой вариант cmd.exe
                System.Diagnostics.Process.Start(str);
            }
        }
        private void toolStripButtonPasteClipboard_Click(object sender, EventArgs e)
        {
            this.toolStripTextBoxIP.Text = Clipboard.GetText(TextDataFormat.Text);
        }
        #endregion

        #region Главное меню
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpenFileXML formOpenFileXML = new FormOpenFileXML();
            formOpenFileXML.ShowDialog();
            if (formOpenFileXML.SelectOk)
            {
                this.tabControlObject.TabPages.Clear();
                this.treeViewObject.Nodes.Clear();
                this.CreateDOM();
            }

        }
        private void закрытьВсеВкладкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Закрываем все открытые вкладки
            this.tabControlObject.TabPages.Clear();
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void опцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
            // проверяем настройки
            if (Properties.Settings.Default.VisibleProtocol)
            {
                this.splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = false;
            }
        }
        private void командныеКнопкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettingCommandButton formSettingCommandButton = new FormSettingCommandButton();
            formSettingCommandButton.ShowDialog();
        }
        private void проверкаОбновленийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пока данная функция не реализована", "Линейный специалист", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.WriteListBox("Пока данная функция не реализована!");
        }
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // открываем в браузере ссылку по которой находиться справка по работе с программой
            // System.Diagnostics.Process.Start(Properties.Settings.Default.HelpLink);
            System.Diagnostics.Process.Start(Application.StartupPath + "\\LCHelp.chm");

        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }
        #endregion

        #region События TabControlObject
        private void tabControlObject_DoubleClick(object sender, EventArgs e)
        {
            TabPage tp = this.tabControlObject.SelectedTab;
            this.tabControlObject.TabPages.Remove(tp);
        }
        private void tabControlObject_ControlRemoved(object sender, ControlEventArgs e)
        {
            LCTabPage lcTP = (LCTabPage)e.Control;
            lcTP.LCTreeNode.TabPage = null;
        }
        private void tabControlObject_Selecting(object sender, TabControlCancelEventArgs e)
        {
            LCTabPage lcTabPage = (LCTabPage)this.tabControlObject.SelectedTab;
            // Проверяем есть ли открытые вкладки
            if (lcTabPage != null)
            {
                // Выделяем узел в дереве
                this.treeViewObject.SelectedNode = lcTabPage.LCTreeNode;
            }
            else
            {
                this.WriteListBox("Нет открытых вкладок !");
            }
        }
        #endregion
    }
}