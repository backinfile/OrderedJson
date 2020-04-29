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
    internal class UnaryOperatorDefiner:IOJMethod
    {

        readonly Block obj;
        readonly Token token;

        public UnaryOperatorDefiner(Block obj, Token token)
        {
            this.obj = obj;
            this.token = token;
        }

        public string Name => "";

        public Type ReturnType => throw new NotImplementedException();

        public object Invoke(OJContext context, params object[] args)
        {
            if (token.type == TokenType.Sub)
            {
                int value = obj.OJToInt(context);
                return -value;
            }
            else if (token.type == TokenType.Not)
            {
                bool boolValue = obj.OJToBool(context);
                return !boolValue;
            }

            return null;
        }
    }
}
