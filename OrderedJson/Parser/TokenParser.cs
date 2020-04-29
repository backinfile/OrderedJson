using OrderedJson.Code;
using OrderedJson.Core;
using OrderedJson.Definer;
using OrderedJson.Tokenize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Parser
{

    /// <summary>
    /// 解析token，生成Block和Stmt
    /// </summary>
    public class TokenParser
    {
        /// <summary>
        /// 以“$+数字”形式的参数为待赋值参数，函数映射后，进行赋值
        /// </summary>

        int index = 0;

        public readonly OJMethods blocks = new OJMethods();
        private readonly OJData data;
        internal readonly Dictionary<int, Block> lasyBlocks = new Dictionary<int, Block>();

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
                //var block = GetBlock();
                var block = Expr();
                if (block != null)
                {
                    blocks.Add(block);
                }
                else
                {
                    break;
                }
            }
            if (index < length)
            {
                throw new ParseException($"{Value().GetExceptionString()} 匹配失败!");
            }
        }


        private Block GetBlock()
        {
            if (Test(TokenType.True))
            {
                Next();
                return new Block().SetValue(true);
            }
            if (Test(TokenType.False))
            {
                Next();
                return new Block().SetValue(false);
            }
            if (Test(TokenType.Int))
            {
                var token = Match(TokenType.Int);
                if (int.TryParse(token.value, out int intValue))
                {
                    return new Block().SetValue(intValue);
                }
                throw new RuntimeException($"{token.GetExceptionString()} {token.value}不能转化为整数");
            }
            if (Test(TokenType.Str))
            {
                return new Block().SetValue(Match(TokenType.Str).value);
            }
            if (Test(TokenType.Semicolon))
            {
                Next();
                return null;
            }
            if (Test(TokenType.Name))
            {
                // 检测remap函数
                string value = Value().value;
                //Console.WriteLine("in this "+value);
                if (value.StartsWith("$"))
                {
                    if (int.TryParse(value.Substring(1), out int no))
                    {
                        Next();
                        Block block = new Block();
                        lasyBlocks[no] = block;
                        return block;
                    }
                }
            }

            if (Test(TokenType.LBrace))
            {
                Next();
                Block block = Expr();
                Match(TokenType.RBrace);
                return block;

            }
            else
            {
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
                if (stmts.Any())
                {
                    return new Block().SetValue(stmts);
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
                    .Select(str => new Block()
                         .SetValue(new List<Stmt>() { new Stmt(data.GetMethod(str), new List<Block>()) })
                         )
                    );
            }

            if (Test(TokenType.Colon))
            {
                Next();

                while(true)
                {
                    //Block block = GetBlock();
                    Block block = Expr();
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

        /// <summary>
        /// and or 的优先级
        /// </summary>
        /// <returns></returns>
        private Block Expr()
        {
            Block block = Expr1();
            if (block == null) return null;
            if (Test(TokenType.Or) || Test(TokenType.And))
            {
                Token token = Value();
                Next();
                Block right = Expr1();
                ConfirmBlock(right);
                block = new Block().SetValue(new BinaryOperatorDefiner(block, right, token));
            }
            return block;
        }

        /// <summary>
        /// 比较运算符的优先级
        /// </summary>
        /// <returns></returns>
        private Block Expr1()
        {
            Block block = Expr2();
            if (block == null) return null;
            if (Test(TokenType.Cmp))
            {
                Token token = Value();
                Next();
                Block right = Expr2();
                ConfirmBlock(right);
                block = new Block().SetValue(new BinaryOperatorDefiner(block, right, token));
            }
            return block;
        }
        
        /// <summary>
        /// 加减法的优先级
        /// </summary>
        /// <returns></returns>
        private Block Expr2()
        {
            Block block = Expr3();
            if (block == null) return null;
            if (Test(TokenType.Add) || Test(TokenType.Sub))
            {
                Token token = Value();
                Next();
                Block right = Expr3();
                ConfirmBlock(right);
                block = new Block().SetValue(new BinaryOperatorDefiner(block, right, token));
            }
            return block;
        }

        /// <summary>
        /// 乘除法的优先级
        /// </summary>
        /// <returns></returns>
        private Block Expr3()
        {
            Block block = Expr4();
            if (block == null) return null;
            if (Test(TokenType.Mul) || Test(TokenType.Div))
            {
                Token token = Value();
                Next();
                Block right = Expr4();
                ConfirmBlock(right);
                block = new Block().SetValue(new BinaryOperatorDefiner(block, right, token));
            }
            return block;
        }

        /// <summary>
        /// not,neg的优先级 
        /// </summary>
        /// <returns></returns>
        private Block Expr4()
        {
            List<Token> tests = new List<Token>();
            while (Test(TokenType.Not) || Test(TokenType.Sub))
            {
                tests.Add(Value());
                Next();
            }
            Block block = Expr5();

            if (tests.Any())
            {
                ConfirmBlock(block);
            }
            else if (block == null) return null;

            tests.Reverse();
            foreach (var token in tests)
            {
                block = new Block().SetValue(new UnaryOperatorDefiner(block, token));
            }
            return block;
        }

        private Block Expr5()
        {
            return GetBlock();
        }


        private bool Test(TokenType type)
        {
            if (index >= length) return false;
            var token = tokens[index];
            return token.type == type;
        }
        private Token Value()
        {
            return tokens[index];
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

        /// <summary>
        /// 确保block不为空
        /// </summary>
        /// <param name="block"></param>
        private void ConfirmBlock(Block block)
        {
            if (block == null)
            {
                var token = (index < length) ? Value() : tokens.Last();
                throw new ParseException($"{token.GetExceptionString()} 不能匹配到语句块");
            }
        }
    }
}
