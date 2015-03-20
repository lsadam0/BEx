using System;
using System.Runtime.Serialization;

namespace BEx.Exceptions
{
    [Serializable]
    public class WithdrawalRejectedException : BExException
    {
        public WithdrawalRejectedException(string message)
            : base(message)
        {
        }

        public WithdrawalRejectedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected WithdrawalRejectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}