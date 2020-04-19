using OrderedJson.Core;
using OrderedJson.Tokenize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Code
{
    public class Block : IOJMethod
    {
        public LocalVar localVar = new LocalVar();


        // 4选1
        public FinalReturnType FinalType { get; private set; }

        private readonly int intValue;
        private readonly string strValue;
        private readonly bool boolValue;
        public readonly List<Stmt> stmts;


        #region as value

        internal Block(int value)
        {
            FinalType = FinalReturnType.Int;
            intValue = value;
        }
        internal Block(string value)
        {
            FinalType = FinalReturnType.String;
            strValue = value;
        }
        internal Block(bool value)
        {
            FinalType = FinalReturnType.Bool;
            boolValue = value;
        }

        public dynamic GetValue()
        {
            switch (FinalType)
            {
                case FinalReturnType.Int:
                    return intValue;
                case FinalReturnType.String:
                    return strValue;
                case FinalReturnType.Bool:
                    return boolValue;
                case FinalReturnType.Stmts:
                    return this;
                default:
                    throw new RuntimeException($"Block没有值！");
            }
        }

        //public static explicit operator int(Block block)
        //{
        //    if (block.FinalType == FinalReturnType.Int)
        //    {
        //        return block.intValue;
        //    }
        //    throw new RuntimeException("");
        //}
        #endregion

        #region as callable

        internal Block(List<Stmt> stmts)
        {
            if (stmts.Any())
            {
                FinalType = FinalReturnType.Stmts;
                this.stmts = stmts;
                foreach (var stmt in stmts)
                {
                    stmt.parent = this;
                }
            }
            else
            {
                throw new RuntimeException("不能创建没有语句的block");
            }
        }
        public Type ReturnType => stmts.Last().ReturnType;

        public List<(string, Type)> ArgTypes => stmts.Last().ArgTypes;

        public string Name { get; set; }

        public object Invoke(OJContext context, params object[] args)
        {
            localVar.Clear();
            object returnValue = null;
            foreach (var stmt in stmts)
            {
                returnValue = stmt.Invoke(context, args);
            }
            return returnValue;
        }

        #endregion
    }

    public enum FinalReturnType
    {
        Int,
        String,
        Bool,
        None,
        Stmts,
    }

}
