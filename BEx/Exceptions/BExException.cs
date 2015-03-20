// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.Serialization;

namespace BEx.Exceptions
{
    [Serializable]
    public class BExException : Exception
    {
        public BExException(string message)
            : base(message)
        { }

        public BExException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected BExException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}