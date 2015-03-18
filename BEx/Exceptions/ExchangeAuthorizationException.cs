using System;
using System.Runtime.Serialization;

namespace BEx
{
    [Serializable]
    public class ExchangeAuthorizationException : BExException
    {
        public ExchangeAuthorizationException()
            : base()
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

        protected ExchangeAuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}