using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BEx
{
    //class OrderRejectedException
    //{
    //}

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
