// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ApiError : ApiResult
    {
        internal ApiError(ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
        }

        internal ApiError(string message, BExErrorCode code, ExchangeType sourceExchange)
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