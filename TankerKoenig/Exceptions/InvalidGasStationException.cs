using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankerKoenig.Exceptions
{
    class InvalidGasStationException : Exception
    {
        public InvalidGasStationException(string message) : base(message) { }
    }
}
