// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.Serialization;

namespace BEx.Exceptions
{
    [Serializable]
    public class CancelOrderRejectedException : BExException
    {
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