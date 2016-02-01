// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.Serialization;

namespace BEx.Exceptions
{
    [Serializable]
    public class RemoteExchangeException : Exception
    {
        public RemoteExchangeException(string message)
            : base(message)
        {
        }

        public RemoteExchangeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RemoteExchangeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}