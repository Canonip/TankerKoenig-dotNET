using System;

namespace TankerKoenig.Exceptions
{
    public class InvalidApiKeyException : Exception
    {
        public InvalidApiKeyException(string message) : base(message) { }
    }
}
