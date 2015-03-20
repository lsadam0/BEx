using System;
using System.Runtime.Serialization;

namespace BEx
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