using System;



namespace BEx
{
    [Serializable]
    public class APIError : APIResult
    {

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

        public APIError()
            : base()
        {

        }

        public APIError(string message, BExErrorCode code)
            : base()
        {
            Message = message;
            ErrorCode = code;

        }
    }
}
