// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class BExError : BExResult
    {
        internal BExError(ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
        }

        internal BExError(string message, BExErrorCode code, Exchange sourceExchange)
            : base(DateTime.UtcNow, sourceExchange.ExchangeSourceType)
        {
            Message = message;
            ErrorCode = code;
        }

        public BExErrorCode ErrorCode
        {
            get;
            internal set;
        }

        public HttpResponseCode HttpStatus
        {
            get;
            internal set;
        }

        public string Message
        {
            get;
            internal set;
        }
    }
}