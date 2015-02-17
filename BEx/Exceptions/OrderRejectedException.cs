using System;
using System.Runtime.Serialization;

namespace BEx
{
    //class OrderRejectedException
    //{
    //}

    [Serializable]
    public class InsufficientFundsException : Exception, ISerializable
    {
        public InsufficientFundsException()
        {
        }

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