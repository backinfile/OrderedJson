
using OrderedJson.Parser;
using OrderedJson.Tokenize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Core
{
    /// <summary>
    /// 新建一个OJ解析器
    /// </summary>
    public class OJParser
    {
        private readonly OJData data;
        private TokenParser parser;
        public OJParser(Type ApiDefiner)
        {
            data = ParseDefiner.ParseClass(ApiDefiner);
        }


        /// <summary>
        /// 解析脚本，返回可执行函数IOJMethod
        /// </summary>
        /// <param name="code"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IOJMethod Parse(string code, string filename)
        {
            parser = new TokenParser(data);
            var tokens = DoTokenize.Tokenize(code, filename);
            parser.Compile(tokens);
            var blocks = parser.blocks;
            if (blocks == null || !blocks.Any())
            {
                return null;
            }
            return new OJMethods(blocks);
        }
    }
}
