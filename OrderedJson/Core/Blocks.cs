using OrderedJson.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Core
{
    public class Blocks : IOJMethod, IList<Block>
    {
        private List<Block> blocks;

        public Blocks(List<Block> blocks)
        {
            this.blocks = blocks;
        }
        public Blocks(params Block[] blocks)
        {
            this.blocks = new List<Block>(blocks);
        }


        #region method
        public string Name => blocks.Last().Name;

        public Type ReturnType => blocks.Last().ReturnType;

        public List<(string, Type)> ArgTypes => blocks.Last().ArgTypes;
        public object Invoke(OJContext context, params object[] args)
        {
            object returnValue = null;
            foreach (var item in this)
            {
                returnValue = item.Invoke(context, args);
            }
            return returnValue;
        }
        #endregion

        #region list

        public int Count => ((IList<Block>)blocks).Count;

        public bool IsReadOnly => ((IList<Block>)blocks).IsReadOnly;

        public Block this[int index] {
            get => ((IList<Block>)blocks)[index];
            set => ((IList<Block>)blocks)[index] = value;
        }
        public void Add(Block item)
        {
            ((IList<Block>)blocks).Add(item);
        }

        public void Clear()
        {
            ((IList<Block>)blocks).Clear();
        }

        public bool Contains(Block item)
        {
            return ((IList<Block>)blocks).Contains(item);
        }

        public void CopyTo(Block[] array, int arrayIndex)
        {
            ((IList<Block>)blocks).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return ((IList<Block>)blocks).GetEnumerator();
        }

        public int IndexOf(Block item)
        {
            return ((IList<Block>)blocks).IndexOf(item);
        }

        public void Insert(int index, Block item)
        {
            ((IList<Block>)blocks).Insert(index, item);
        }


        public bool Remove(Block item)
        {
            return ((IList<Block>)blocks).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Block>)blocks).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<Block>)blocks).GetEnumerator();
        }
        #endregion
    }
}
