using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data;

namespace LC
{
    /// <summary>
    ///  Типы узлов в дереве объектов
    /// </summary>
    enum LCObjectType
    {
        //Root,        // Корень дерева
        NoList,      // Узел с компьютерами не в списке
        Group,       // Группа компьютеров
        Computer,    // Компьютеров
        SubNet,      // Узел СПД
        None,        // Пустой узел
        MFU          // Сетевые МФУ или принтер
    }
    /// <summary>
    /// Класс узла дерева объектов
    /// </summary>
    class LCTreeNode : TreeNode
    {
        public LCTreeNode() : base()
        {
        }
        // поле для хранения имени файла фотографии
        protected string fotoFile = "";
        static private ListBox listBoxOperation;
        static public ToolStripLabel StatusLabel = null;
        //Контекстные меню
        public static ContextMenuStrip groupContextMemuStrip = null;
        public static ContextMenuStrip computerContextMenuStrip = null;
        public static ContextMenuStrip rootContextMenuStrip = null;
        public static ContextMenuStrip subnetContextMenuStrip = null;
        public static ContextMenuStrip noListContextMenuStrip = null;
        /// <summary>
        /// Метод настройки компонента ListBox в который будут выводиться сообщения
        /// </summary>
        /// <param name="listBox">Компонент главной формы в который будет осуществляться вывод сообщений</param>
        static public void SetListBoxOperation(ListBox listBox)
        {
            LCTreeNode.listBoxOperation = listBox;
        }
        private string description = null;
        /// <summary>
        /// Свойство описывающее данный объект
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        protected LCObjectType lcObjectType = LCObjectType.None;
        /// <summary>
        /// Возвращает тип объекта
        /// </summary>
        public LCObjectType LCObjectType
        {
            get
            {
                return this.lcObjectType;
            }
        }
        /// <summary>
        /// Метод сохранения данных объекта
        /// </summary>
        virtual public void Save(XmlTextWriter xw)
        {
        }
        /// <summary>
        /// Метод вывода сообщения в ListBoxOperation 
        /// </summary>
        /// <param name="message">Текст выводимого сообщения</param>
        protected void WriteListBoxOperation(string message)
        {
            if (listBoxOperation != null)
            {
                listBoxOperation.Items.Add("[" + DateTime.Now.ToString() + "] " + message);
                listBoxOperation.SelectedIndex = listBoxOperation.Items.Count - 1;
            }
            if (StatusLabel != null)
            {
                StatusLabel.Text = message;
            }
        }
        /// <summary>
        /// Свойство возвращающее путь к группе в которой находится данный объект
        /// </summary>
        public string ParentGroup
        {
            get
            {
                LCTreeNode lcTreeWork = this;
                string str = "";
                // Перебераем все группы двигаясь к родительской
                while (lcTreeWork.Parent != null)
                {
                    lcTreeWork = (LCTreeNode)lcTreeWork.Parent;
                    if (lcTreeWork.Name != "Root")
                    {
                        str = "\\" + lcTreeWork.Text + str;
                    }
                    else
                    {
                        // Делаем для того чтобы название всех групп и сетей отображались без имени узла Root
                        str = "." + str;
                    }
                }
                return str;
            }
        }
    }
}