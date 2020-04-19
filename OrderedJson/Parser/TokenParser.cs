using OrderedJson.Code;
using OrderedJson.Core;
using OrderedJson.Tokenize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Parser
{
    public class TokenParser
    {
        int index = 0;

        public readonly List<Block> blocks = new List<Block>();
        private readonly OJData data;

        List<Token> tokens;
        int length;

        public TokenParser(OJData data)
        {
            this.data = data;
        }

        public void Compile(List<Token> tokens)
        {
            this.tokens = tokens;
            index = 0;
            length = tokens.Count;
            while(index < length)
            {
                var block = GetBlock();
                if (block != null)
                {
                    blocks.Add(block);
                }
            }
        }


        private Block GetBlock()
        {
            if (Test(TokenType.True))
            {
                Next();
                return new Block(true);
            }
            if (Test(TokenType.False))
            {
                Next();
                return new Block(false);
            }
            if (Test(TokenType.Int))
            {
                var token = Match(TokenType.Int);
                if (int.TryParse(token.value, out int intValue))
                {
                    return new Block(intValue);
                }
                throw new RuntimeException($"{token.GetExceptionString()} {token.value}不能转化为整数");
            }
            if (Test(TokenType.Str))
            {
                return new Block(Match(TokenType.Str).value);
            }
            if (Test(TokenType.Semicolon))
            {
                Next();
                return null;
            }

            if (Test(TokenType.LBrace))
            {
                Next();
                List<Stmt> stmts = new List<Stmt>();
                while (true)
                {
                    //Console.WriteLine($"cur: {index}");
                    Stmt stmt = GetStmt();
                    if (Test(TokenType.Semicolon))
                    {
                        Next();
                    }
                    if (stmt == null)
                    {
                        break;
                    }
                    stmts.Add(stmt);
                    if (Test(TokenType.RBrace))
                    {
                        break;
                    }

                }
                Match(TokenType.RBrace);

                if (stmts.Any())
                {
                    return new Block(stmts);
                }
            }
            else
            {
                Stmt stmt = GetStmt();
                if (stmt != null)
                {
                    return new Block(new List<Stmt>() { stmt });
                }
            }
            return null;
        }
        private Stmt GetStmt()
        {
            if (!Test(TokenType.Name))
            {
                return null;
            }

            List<Block> args = new List<Block>();


            List<string> names = new List<string>();
            Token firstToken = Match(TokenType.Name);
            names.Add(firstToken.value);
            while (true)
            {
                if (Test(TokenType.Period))
                {
                    Next();
                    names.Add(Match(TokenType.Name).value);
                }
                else
                {
                    break;
                }
            }
            if (names.Count > 1)
            {
                //if (names.Take(names.Count - 1).Any(t => !t.StartsWith("$")))
                //{
                //    throw new ParseException($"{firstToken.GetExceptionString()} 方法类型没有子方法");
                //}
                args.AddRange(
                    names
                    .Take(names.Count - 1)
                    .Reverse()
                    .Select(str => new Block(new List<Stmt>() { new Stmt(data.GetMethod(str), new List<Block>())}))
                    );
            }

            if (Test(TokenType.Colon))
            {
                Next();

                while(true)
                {
                    Block block = GetBlock();
                    if (block == null)
                    {
                        break;
                    }
                    args.Add(block);
                    if (Test(TokenType.Comma))
                    {
                        Next();
                    }
                    else
                    {
                        break;
                    }
                    if (Test(TokenType.Semicolon))
                    {
                        break;
                    }
                }
            }
            return new Stmt(data.GetMethod(names.Last()), args);
        }


        private bool Test(TokenType type)
        {
            if (index >= length) return false;
            var token = tokens[index];
            return token.type == type;
        }
        private Token Match(TokenType type, string error = null)
        {
            if (index >= length)
            {
                var t = tokens.Last();
                throw new ParseException($"{t.GetExceptionString()} 匹配到末尾!");
            }
            var token = tokens[index++];
            if (token.type != type)
            {
                if (error == null)
                {
                    throw new ParseException($"{token.GetExceptionString()} 未能将{token.type}匹配到{type}");
                }
                else
                {
                    throw new ParseException($"{token.GetExceptionString()} {error}");
                }
            }
            return token;
        }
        private void Next()
        {
            index++;
        }
    }
}
