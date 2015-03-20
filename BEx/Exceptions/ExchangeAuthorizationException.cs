// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.Serialization;

namespace BEx.Exceptions
{
    [Serializable]
    public class ExchangeAuthorizationException : BExException
    {
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