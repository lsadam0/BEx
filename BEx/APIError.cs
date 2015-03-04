using System;

namespace BEx
{
    [Serializable]
    public class APIError : APIResult
    {
        public APIError(ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
        }

        public APIError(string message, BExErrorCode code, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Message = message;
            ErrorCode = code;
        }

        public BExErrorCode ErrorCode
        {
            get;
            set;
        }

        public HttpResponseCode HttpStatus
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
}