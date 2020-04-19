using OrderedJson.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Core
{
    public class OJMethods : IOJMethod, IList<IOJMethod>
    {
        private List<IOJMethod> methods;

        public OJMethods(List<IOJMethod> methods)
        {
            this.methods = methods;
        }
        public OJMethods(List<Block> blocks)
        {
            this.methods = blocks.Cast<IOJMethod>().ToList();
        }
        public OJMethods(params IOJMethod[] methods)
        {
            this.methods = new List<IOJMethod>(methods);
        }

        public IOJMethod this[int index] { get => ((IList<IOJMethod>)methods)[index]; set => ((IList<IOJMethod>)methods)[index] = value; }


        #region method
        public string Name => methods.Last().Name;

        public Type ReturnType => methods.Last().ReturnType;

        public List<(string, Type)> ArgTypes => methods.Last().ArgTypes;

        public int Count => ((IList<IOJMethod>)methods).Count;

        public bool IsReadOnly => ((IList<IOJMethod>)methods).IsReadOnly;

        public void Add(IOJMethod item)
        {
            ((IList<IOJMethod>)methods).Add(item);
        }

        public void Clear()
        {
            ((IList<IOJMethod>)methods).Clear();
        }

        public bool Contains(IOJMethod item)
        {
            return ((IList<IOJMethod>)methods).Contains(item);
        }

        public void CopyTo(IOJMethod[] array, int arrayIndex)
        {
            ((IList<IOJMethod>)methods).CopyTo(array, arrayIndex);
        }

        public IEnumerator<IOJMethod> GetEnumerator()
        {
            return ((IList<IOJMethod>)methods).GetEnumerator();
        }

        public int IndexOf(IOJMethod item)
        {
            return ((IList<IOJMethod>)methods).IndexOf(item);
        }

        public void Insert(int index, IOJMethod item)
        {
            ((IList<IOJMethod>)methods).Insert(index, item);
        }

        public object Invoke(OJContext context, params object[] args)
        {
            object returnValue = null;
            foreach (var item in this)
            {
                returnValue = item.Invoke(context, args);
            }
            return returnValue;
        }

        public bool Remove(IOJMethod item)
        {
            return ((IList<IOJMethod>)methods).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<IOJMethod>)methods).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<IOJMethod>)methods).GetEnumerator();
        }
        #endregion

    }
}
