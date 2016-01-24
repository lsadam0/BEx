using System;
using System.Runtime.Serialization;

namespace BEx.Exceptions
{
    [Serializable]
    public class InvalidAddressException : Exception
    {
        public InvalidAddressException(string message)
            : base(message)
        {
        }

        public InvalidAddressException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected InvalidAddressException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
