using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Core
{
    public class TokenizeException : ApplicationException
    {
        public TokenizeException(string message) : base(message)
        {
        }

    }

    public class DefinerException : ApplicationException
    {
        public DefinerException(string message) : base(message)
        {
        }

    }



    public class ParseException : ApplicationException
    {
        public ParseException(string message) : base(message)
        {
        }

    }

    public class RuntimeException: ApplicationException
    {
        public RuntimeException(string message) : base(message)
        {
        }
    }

}
