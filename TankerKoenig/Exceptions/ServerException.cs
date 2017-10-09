using System;

namespace TankerKoenig.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }
}
