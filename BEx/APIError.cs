using System;

namespace BEx
{
    [Serializable]
    public sealed class APIError : APIResult
    {
        internal APIError(ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
        }

        internal APIError(string message, BExErrorCode code, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
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