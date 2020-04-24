using OrderedJson.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Core
{
    public interface IOJMethod
    {

        string Name { get; }

        Type ReturnType { get;}

        object Invoke(OJContext context,  params object[] args);
    }
}
