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

        public List<(string, Type)> ArgTypes => method.ArgTypes;

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

        public object Invoke(OJContext context, params object[] args)
        {
            return method.Invoke(context, this.args.ToArray());
        }


        #region localvar

        internal int GetLocalVar(string name)
        {
            return parent.localVar[name];
        }
        internal bool HasLocalVar(string name)
        {
            return parent.localVar.ContainsKey(name);
        }
        internal void SetLocalVar(string name, int value)
        {
            parent.localVar[name] = value;
        }


        #endregion
    }
}
