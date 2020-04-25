using OrderedJson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderedJson.Tokenize
{
    public class DoTokenize
    {
        private const string Blanks = " \r\n\t";
        private const char NewLine = '\n';

        public static List<Token> Tokenize(string buffer, string filename)
        {
            List<Token> tokens = new List<Token>();
            if (buffer == null) return tokens;

            int cursor = 0;
            int length = buffer.Length;
            int lineno = 1;
            while (cursor < length) 
            {
                StringBuilder prestr = new StringBuilder();
                while (cursor < length && Blanks.Contains(buffer[cursor]))
                {
                    if (buffer[cursor] == NewLine) lineno++;
                    prestr.Append(buffer[cursor++]);
                }
                if (cursor >= length) break;

                bool isMatched = false;
                foreach (var pattern in Pattern.Patterns)
                {
                    var type = pattern.Item1;
                    var reg = pattern.Item2;
                    var match = Regex.Match(buffer.Substring(cursor), "^"+reg);
                    if (match.Success)
                    {
                        prestr.Append(match.Value);
                        Token token = new Token(type, match.Groups[1].Value, prestr.ToString(), filename, lineno, cursor);
                        tokens.Add(token);

                        cursor += match.Value.Length;
                        isMatched = true;
                        break;
                    }
                }
                if (!isMatched)
                {
                    throw new TokenizeException($"{filename} 第{lineno}行 第{cursor}列: 语法错误");
                }

            }

            return tokens;
        }


        public static void PrintTokens(List<Token> tokens)
        {
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}
