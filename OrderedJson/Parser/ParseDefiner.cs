using OrderedJson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Parser
{
    /// <summary>
    /// 解析定义的api, 变为IOJMethod
    /// </summary>
    public class ParseDefiner
    {
        /// <summary>
        /// 解析定义api的类，将api存储到OJData
        /// </summary>
        public static void ParseClass(Type type, OJData data)
        {
            //Type type = typeof(CommondDefiner);
            var methodInfos = type.GetMethods();

            foreach (var methodInfo in methodInfos)
            {
                if (!methodInfo.IsStatic) continue;
                IOJMethod method = new OJMethodImpl(methodInfo);
                data[method.Name] = method;
            }
        }

    }
}
