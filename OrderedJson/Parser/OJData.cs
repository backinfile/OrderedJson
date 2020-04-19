using OrderedJson.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Parser
{
    /// <summary>
    /// 存储用户定义的函数和getter
    /// 不区分大小写
    /// </summary>
    public class OJData
    {
        public Dictionary<string, IOJMethod> methods = new Dictionary<string, IOJMethod>();

        public IOJMethod GetMethod(string name)
        {
            if (!ContainsKey(name))
            {
                throw new DefinerException($"函数{name}未定义!");
            }
            return this[name];
        }

        public IOJMethod GetProperty(string name)
        {
            string methodName = "get" + name;
            if (!ContainsKey(methodName))
            {
                throw new DefinerException($"属性{name}未定义!");
            }
            return this[methodName];
        }

        public IOJMethod this[string key] { get => methods[key.ToLower()]; set => methods[key.ToLower()] = value; }
        public bool ContainsKey(string key)
        {
            return methods.ContainsKey(key.ToLower());
        }


        public int Count => ((IDictionary)methods).Count;

        public void Clear()
        {
            ((IDictionary)methods).Clear();
        }

    }
}
