using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Tokenize
{
    /// <summary>
    /// 存储token在代码中的位置
    /// </summary>
    internal class CodeMap
    {
        private readonly string filename;
        private readonly int line;
        private readonly int column;

        public CodeMap(string filename, int line, int column)
        {
            this.filename = filename;
            this.line = line;
            this.column = column;
        }

        public override string ToString()
        {
            return $"{filename} 第{line}行 第{column}列";
        }
    }
}
