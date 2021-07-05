using System;
using System.Windows.Forms;

namespace LC
{
    /// <summary>
    ///  Типы узлов в дереве объектов
    /// </summary>
    enum LCObjectType
    {
        NoList,      // Узел с компьютерами не в списке
        Group,       // Группа компьютеров
        SubNet,      // Узел СПД
        None,        // Пустой узел
        Host         // Какое либо устройство с IP адресом
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
        //private string description = null;
        /// <summary>
        /// Свойство описывающее данный объект
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Свойство возвращающее описание в виде строки. Заменяет символ перевода строки на пробел.
        /// </summary>
        public string DescriptionStr
        {
            get
            {
                return this.Description.Replace("\n", " ");
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
        /// <summary>
        /// Метод удаления не только самого узла, но и строки в ListView
        /// </summary>
        virtual public void RemoveLC()
        {
            this.Remove();
        }
        virtual public void UpdateLC()
        {
            this.ToolTipText = this.Text;
            this.ToolTipText += "\n" + this.Description;
        }
    }
}