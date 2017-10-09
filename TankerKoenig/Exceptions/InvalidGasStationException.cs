using System;

namespace TankerKoenig.Exceptions
{
    public class InvalidGasStationException : Exception
    {
        public InvalidGasStationException(string message) : base(message) { }
    }
}
