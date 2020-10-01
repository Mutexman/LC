using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LC
{
    class LCEnumerator : IEnumerator
    {
        public LCTreeNode treeNode;
        public LCTreeNode currentTreeNote;
        public int position = -1;
        Stack<IEnumerator<LCTreeNode>> stack = new Stack<IEnumerator<LCTreeNode>>();
        public LCEnumerator(IEnumerator iterator)
        {
            this.stack.Push((IEnumerator<LCTreeNode>)iterator);
        }
        public bool MoveNext()
        {
            if (stack.Count <= 0)
            {
                return false;
            }
            else
            {
                IEnumerator<LCTreeNode> iterator = stack.Peek();
                if (!iterator.MoveNext())
                {
                    stack.Pop();
                    return this.MoveNext();
                }
                else
                {
                    return true;
                }
            }
        }
        public void Reset()
        {

        }
        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }
        public LCTreeNode Current
        {
            get
            {
                if(this.MoveNext())
                {
                    IEnumerator<LCTreeNode> iterator = stack.Peek();
                    LCTreeNode lCTreeNode = iterator.Current;
                    stack.Push(lCTreeNode); //!!!!!!!
                    return lCTreeNode;
                }
                else
                {
                    return null; //!!!!!!!!!!!!!
                }
            }
        }
    }
}
