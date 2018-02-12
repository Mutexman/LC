using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    /// <summary>
    /// Класс буфера обмена для объекта LCTReeNode
    /// </summary>
    class BufferLCTreeNode
    {
        private LCTreeNode lcTreeNode = null;
        /// <summary>
        /// Конструктор
        /// </summary>
        public BufferLCTreeNode()
        {
        }
        /// <summary>
        /// Метод вставки объекта в буфер
        /// </summary>
        /// <param name="lcTreeNode">Объект помещаемый в буфер</param>
        public void InBuffer(LCTreeNode lcTreeNode)
        {
            this.lcTreeNode = (LCTreeNode)lcTreeNode;
        }
        /// <summary>
        /// Метод изъятия объекта из буфера
        /// </summary>
        /// <returns>Объект который находился в буфере</returns>
        public LCTreeNode OutBuffer()
        {
            LCTreeNode lcTreeNode = this.lcTreeNode;
            this.lcTreeNode = null;
            return lcTreeNode;
        }
        /// <summary>
        /// Свойство возвращающее объект находящийся в буфере
        /// </summary>
        public LCTreeNode LCTreeNode
        {
            get
            {
                return this.lcTreeNode;
            }
        }
    }
}
