
using OrderedJson.Code;
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
    /// OJ解析器
    /// </summary>
    public class OJParser
    {
        private readonly OJData data;
        private TokenParser parser;

        /// <summary>
        /// 新建一个OJ解析器
        /// </summary>
        /// <param name="ApiDefiner">Api定义类</param>
        /// <param name="remaps">定义映射函数</param>
        public OJParser(Type ApiDefiner, Dictionary<string, string> remaps = null)
        {
            data = new OJData();
            ParseDefiner.ParseClass(ApiDefiner, data);

            if (remaps != null)
            {
                AddRemapMethod(remaps);
            }
        }

        public void AddRemapMethod(Dictionary<string, string> remaps)
        {
            foreach (var pair in remaps)
            {
                var name = pair.Key;
                var code = pair.Value;
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(code))
                {
                    data[name] = ParseRemapCode(code, name);
                }
            }
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

        /// <summary>
        /// 解析映射函数脚本
        /// </summary>
        /// <param name="code"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IOJMethod ParseRemapCode(string code, string filename)
        {
            parser = new TokenParser(data);
            var tokens = DoTokenize.Tokenize(code, filename);
            parser.Compile(tokens);
            var blocks = parser.blocks;
            if (blocks == null || blocks.Count != 1)
            {
                return null;
            }
            List<Block> args = new List<Block>();
            for (int i = 0; i < 10; i++)
            {
                if (parser.lasyBlocks.ContainsKey(i))
                {
                    args.Add(parser.lasyBlocks[i]);
                }
            }

            Stmt stmt = new Stmt(new OJMethods(blocks), args);
            stmt.SetRemaped(filename);
            return stmt;
        }
    }
}
