using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BEx
{
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
