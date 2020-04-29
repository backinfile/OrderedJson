using OrderedJson.Code;
using OrderedJson.Core;
using OrderedJson.Tokenize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Definer
{
    /// <summary>
    /// 定义双目运算符
    /// </summary>
    internal class BinaryOperatorDefiner : IOJMethod
    {
        readonly Block left;
        readonly Block right;
        readonly Token token;

        public BinaryOperatorDefiner(Block left, Block right, Token token)
        {
            this.left = left;
            this.right = right;
            this.token = token;
        }

        public string Name => "Or";

        public Type ReturnType => throw new NotImplementedException();

        public object Invoke(OJContext context, params object[] args)
        {
            switch (token.type)
            {
                case TokenType.Add:
                    {
                        var leftValue = left.OJToInt(context);
                        var rightValue = right.OJToInt(context);
                        return leftValue + rightValue;
                    }
                case TokenType.Sub:
                    {
                        var leftValue = left.OJToInt(context);
                        var rightValue = right.OJToInt(context);
                        return leftValue - rightValue;
                    }
                case TokenType.Mul:
                    {
                        var leftValue = left.OJToInt(context);
                        var rightValue = right.OJToInt(context);
                        return leftValue * rightValue;
                    }
                case TokenType.Div:
                    {
                        var leftValue = left.OJToInt(context);
                        var rightValue = right.OJToInt(context);
                        if (rightValue == 0)
                        {
                            throw new RuntimeException("0不能当作除数");
                        }
                        return leftValue / rightValue;
                    }
                case TokenType.And:
                    {
                        var leftValue = left.OJToBool(context);
                        if (!leftValue)
                        {
                            return false;
                        }
                        return right.OJToBool(context);
                    }
                case TokenType.Or:
                    {
                        var leftValue = left.OJToBool(context);
                        if (leftValue)
                        {
                            return true;
                        }
                        return right.OJToBool(context);
                    }
                case TokenType.Cmp:
                    {
                        int cmp = left.OJCompareTo(context, right);
                        if (cmp == 0)
                        {
                            if (token.value == "==" || token.value == "<=" || token.value == ">=")
                            {
                                return true;
                            }
                            return false;
                        }
                        else if (cmp > 0)
                        {
                            if (token.value == ">" || token.value == ">=" || token.value == "!=")
                            {
                                return true;
                            }
                            return false;
                        }
                        else // if (cmp < 0)
                        {
                            if (token.value == "<" || token.value == "<=" || token.value == "!=")
                            {
                                return true;
                            }
                            return false;
                        }
                    }
            }

            return null;
        }


    }
}
