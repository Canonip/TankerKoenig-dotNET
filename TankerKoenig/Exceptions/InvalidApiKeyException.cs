using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankerKoenig.Exceptions
{
    public class InvalidApiKeyException : Exception
    {
        public InvalidApiKeyException(string message) : base(message) { }
    }
}
