using OrderedJson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Parser
{
    public class ParseDefiner
    {
        public static void ParseClass(Type type, OJData data)
        {
            //Type type = typeof(CommondDefiner);
            var methodInfos = type.GetMethods();

            foreach (var methodInfo in methodInfos)
            {
                IOJMethod method = new OJMethodImpl(methodInfo);
                data[method.Name] = method;
            }
        }

    }
}
