using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Core
{
    public class OJException : Exception
    {
        public OJException(string message) : base(message)
        {
        }
    }


    public class TokenizeException : OJException
    {
        public TokenizeException(string message) : base(message)
        {
        }

    }

    public class DefinerException : OJException
    {
        public DefinerException(string message) : base(message)
        {
        }

    }



    public class ParseException : OJException
    {
        public ParseException(string message) : base(message)
        {
        }

    }

    public class RuntimeException: OJException
    {
        public RuntimeException(string message) : base(message)
        {
        }
    }

}
