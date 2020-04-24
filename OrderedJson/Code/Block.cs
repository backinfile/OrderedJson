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
        //public LocalVar localVar = new LocalVar();


        // 4选1
        public FinalReturnType FinalType { get; private set; }

        private int intValue;
        private string strValue;
        private bool boolValue;
        public List<Stmt> stmts;

        internal Block SetValue(object obj)
        {
            if (obj is Block block)
            {
                return SetValue(block);
            }
            else if (obj is int value)
            {
                return SetValue(value);
            }
            else if (obj is string strValue)
            {
                return SetValue(strValue);
            }
            else if (obj is bool boolValue)
            {
                return SetValue(boolValue);
            }
            else if (obj is List<Stmt> stmtValue)
            {
                return SetValue(stmtValue);
            }
            throw new RuntimeException($"不能用{obj.GetType()}初始化Block");
        }

        internal Block SetValue(Block block)
        {
            this.FinalType = block.FinalType;
            switch (block.FinalType)
            {
                case FinalReturnType.Int:
                    this.intValue = block.intValue;
                    break;
                case FinalReturnType.String:
                    this.strValue = block.strValue;
                    break;
                case FinalReturnType.Bool:
                    this.boolValue = block.boolValue;
                    break;
                case FinalReturnType.Stmts:
                    SetValue(block.stmts);
                    break;
            }
            return this;
        }

        #region as value



        internal Block SetValue(int value)
        {
            FinalType = FinalReturnType.Int;
            intValue = value;
            return this;
        }
        internal Block SetValue(string value)
        {
            FinalType = FinalReturnType.String;
            strValue = value;
            return this;
        }
        internal Block SetValue(bool value)
        {
            FinalType = FinalReturnType.Bool;
            boolValue = value;
            return this;
        }

        internal Block() { }

        

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

        internal Block SetValue(List<Stmt> stmts)
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
            return this;
        }
        public Type ReturnType => stmts.Last().ReturnType;

        //public List<(string, Type)> ArgTypes => stmts.Last().ArgTypes;

        public string Name { get; set; }

        public object Invoke(OJContext context, params object[] args)
        {
            //localVar.Clear();
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
