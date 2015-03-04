using System;

namespace BEx
{
    [Serializable]
    public class APIError : APIResult
    {
        public APIError()
            : base(DateTime.Now)
        {
        }

        public APIError(string message, BExErrorCode code)
            : base(DateTime.Now)
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