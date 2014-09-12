using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BEx
{
    public class CancelOrderRejectedException : Exception, ISerializable
    {
        public CancelOrderRejectedException()
        {
        }

        public CancelOrderRejectedException(string message)
            : base(message)
        {
        }

        public CancelOrderRejectedException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected CancelOrderRejectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
