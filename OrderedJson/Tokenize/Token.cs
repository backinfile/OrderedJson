using OrderedJson.Tokenize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Tokenize
{
    public class Token
    {
        public readonly TokenType type;
        public readonly string value;
        public readonly string str;
        internal readonly CodeMap codeMap;

        public Token(TokenType type, string value, string str, string filename, int line, int column)
        {
            this.type = type;
            this.value = value;
            this.str = str;
            this.codeMap = new CodeMap(filename, line, column);
        }

        public override string ToString()
        {
            return $"[{type}] {value}";
        }
        public string GetExceptionString()
        {
            return codeMap.ToString();
        }
    }


    public enum TokenType
    {
        Int,
        Str,
        /// <summary>
        /// ;
        /// </summary>
        Semicolon,
        /// <summary>
        /// :
        /// </summary>
        Colon,
        Comma,
        /// <summary>
        /// .
        /// </summary>
        Period,
        /// <summary>
        /// {
        /// </summary>
        LBrace,
        /// <summary>
        /// }
        /// </summary>
        RBrace,
        /// <summary>
        /// (
        /// </summary>
        LParenthes,
        /// <summary>
        /// )
        /// </summary>
        RParenthes,
        /// <summary>
        /// [
        /// </summary>
        LBracket,
        /// <summary>
        /// ]
        /// </summary>
        RBracket,
        If,
        Else,
        For,
        While,
        Break,
        Continue,
        True,
        False,
        Add,
        Sub,
        Mul,
        Div,
        And,
        Or,
        Not,
        Cmp,
        Name
    }
}
