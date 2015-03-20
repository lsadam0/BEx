using System;
using System.Runtime.Serialization;

namespace BEx.Exceptions
{
    [Serializable]
    public class InsufficientFundsException : BExException
    {
        public InsufficientFundsException(string message)
            : base(message)
        {
        }

        public InsufficientFundsException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InsufficientFundsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}