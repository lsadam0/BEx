using System;
using System.Runtime.Serialization;

namespace BEx
{
    [Serializable]
    public class ExchangeAuthorizationException : Exception, ISerializable
    {
        public ExchangeAuthorizationException()
        {
        }

        public ExchangeAuthorizationException(string message)
            : base(message)
        {
        }

        public ExchangeAuthorizationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ExchangeAuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}