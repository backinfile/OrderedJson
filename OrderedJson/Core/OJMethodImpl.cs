using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OrderedJson.Code;

namespace OrderedJson.Core
{

    /// <summary>
    /// IOJMethod的通用实现
    /// </summary>
    public class OJMethodImpl : IOJMethod
    {
        private readonly MethodInfo methodInfo;
        private readonly string name;

        private readonly List<(string name, Type type,bool hasDefalut, object defaultValue)> argTypes;

        public string Name => name;

        public Type ReturnType => methodInfo.ReturnType;

        public OJMethodImpl(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
            name = methodInfo.Name;
            argTypes = methodInfo.GetParameters().Select(para => (para.Name, para.ParameterType,para.HasDefaultValue, para.DefaultValue)).Skip(1).ToList();
            
        }


        public object Invoke(OJContext context, params object[] args)
        {
            
            var values = args.Cast<Block>().Select(b => b.GetValue()).ToList();
            // 检测参数匹配
            if (values.Count < argTypes.Count)
            {
                for (int i = 0 + values.Count; i < argTypes.Count; i++)
                {
                    if (argTypes[i].hasDefalut) {
                        values.Add(argTypes[i].defaultValue);
                    }
                    else
                    {
                        throw new RuntimeException($"{name}参数个数不匹配：需要{argTypes.Count}个 提供了{values.Count}个");
                    }
                }
            }
            else if (values.Count > argTypes.Count)
            {
                throw new RuntimeException($"{name}参数个数不匹配：需要{argTypes.Count}个 提供了{values.Count}个");
            }


            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == null) continue;

                Type type = values[i].GetType();
                if (argTypes[i].type != typeof(IOJMethod) && type == typeof(Block))
                {
                    values[i] = ((Block)values[i]).Invoke(context);
                }
                if (!argTypes[i].type.IsAssignableFrom(values[i].GetType()))
                {
                    throw new RuntimeException($"{name}的第{i + 1}个参数类型不匹配：需要{argTypes[i].type} 提供了{type}");
                }
            }

            object[] parameters = (new object[] { context }).Concat(values).ToArray();
            return methodInfo.Invoke(null, parameters);
        }
    }
}
