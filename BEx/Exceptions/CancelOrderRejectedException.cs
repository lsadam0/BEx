﻿using System;
using System.Runtime.Serialization;

namespace BEx
{
    [Serializable]
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