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
    public class Stmt: IOJMethod
    {
        internal Block parent;

        public readonly List<Block> args;
        private IOJMethod method;
        private bool isRemaped = false;

        public Type ReturnType => method.ReturnType;

        public string Name { get; set; }

        public Stmt(MethodInfo methodInfo, List<Block> args)
        {
            this.args = args;
            method = new OJMethodImpl(methodInfo);
        }
        public Stmt(IOJMethod method, List<Block> args)
        {
            this.args = args;
            this.method = method;
        }

        public void SetRemaped(string remapName)
        {
            isRemaped = true;
            Name = remapName;
        }

        /// <summary>
        /// stmt当作oj函数调用
        ///     当stmt是映射时，需要提供args参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object Invoke(OJContext context, params object[] args)
        {
            if (isRemaped)
            {
                if (args.Length != this.args.Count)
                {
                    throw new RuntimeException($"{Name}参数个数不匹配：需要{this.args.Count}个 提供了{args.Length}个");
                }

                for (int i = 0; i < args.Length; i++)
                {
                    this.args[i].SetValue(args[i]);
                }
            }
            return method.Invoke(context, this.args.ToArray());
        }


        #region localvar

        //internal int GetLocalVar(string name)
        //{
        //    return parent.localVar[name];
        //}
        //internal bool HasLocalVar(string name)
        //{
        //    return parent.localVar.ContainsKey(name);
        //}
        //internal void SetLocalVar(string name, int value)
        //{
        //    parent.localVar[name] = value;
        //}


        #endregion
    }
}
