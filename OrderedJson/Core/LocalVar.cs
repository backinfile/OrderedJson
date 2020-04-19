using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Core
{
    public class LocalVar: IDictionary<string, int>
    {
        public Dictionary<string, int> values = new Dictionary<string, int>();

        public int this[string key] { get => ((IDictionary<string, int>)values)[key]; set => ((IDictionary<string, int>)values)[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, int>)values).Keys;

        public ICollection<int> Values => ((IDictionary<string, int>)values).Values;

        public int Count => ((IDictionary<string, int>)values).Count;

        public bool IsReadOnly => ((IDictionary<string, int>)values).IsReadOnly;

        public void Add(string key, int value)
        {
            ((IDictionary<string, int>)values).Add(key, value);
        }

        public void Add(KeyValuePair<string, int> item)
        {
            ((IDictionary<string, int>)values).Add(item);
        }

        public void Clear()
        {
            ((IDictionary<string, int>)values).Clear();
        }

        public bool Contains(KeyValuePair<string, int> item)
        {
            return ((IDictionary<string, int>)values).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return ((IDictionary<string, int>)values).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, int>[] array, int arrayIndex)
        {
            ((IDictionary<string, int>)values).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
        {
            return ((IDictionary<string, int>)values).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return ((IDictionary<string, int>)values).Remove(key);
        }

        public bool Remove(KeyValuePair<string, int> item)
        {
            return ((IDictionary<string, int>)values).Remove(item);
        }

        public bool TryGetValue(string key, out int value)
        {
            return ((IDictionary<string, int>)values).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, int>)values).GetEnumerator();
        }
    }
}
