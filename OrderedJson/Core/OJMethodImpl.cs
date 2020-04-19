using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OrderedJson.Code;

namespace OrderedJson.Core
{
    public class OJMethodImpl : IOJMethod
    {
        private readonly MethodInfo methodInfo;
        private readonly List<(string, Type)> argTypes;
        private readonly string name;


        public List<(string, Type)> ArgTypes => argTypes;

        public string Name => name;

        public Type ReturnType => methodInfo.ReturnType;

        public OJMethodImpl(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
            argTypes = methodInfo.GetParameters().Select(para => (para.Name, para.ParameterType)).Skip(1).ToList();
            name = methodInfo.Name;
        }


        public object Invoke(OJContext context, params object[] args)
        {
            //检测参数匹配
            if (args.Length != ArgTypes.Count)
                throw new RuntimeException($"{name}参数个数不匹配：需要{ArgTypes.Count}个 提供了{args.Length}个");

            var values = args.Cast<Block>().Select(b => b.GetValue()).ToList();
            for (int i = 0; i < values.Count; i++)
            {
                Type type = values[i].GetType();
                if (ArgTypes[i].Item2 != typeof(IOJMethod) && type == typeof(Block))
                {
                    values[i] = ((Block)values[i]).Invoke(context);
                }
                if (!this.ArgTypes[i].Item2.IsAssignableFrom(values[i].GetType()))
                {
                    throw new RuntimeException($"{name}的第{i + 1}个参数类型不匹配：需要{ArgTypes[i].Item2} 提供了{type}");
                }
            }

            object[] parameters = (new object[] { context }).Concat(values).ToArray();
            return methodInfo.Invoke(null, parameters);
        }
    }
}
