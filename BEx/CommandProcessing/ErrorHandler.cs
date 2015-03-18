using RestSharp;
using System;
using System.Reflection;

namespace BEx.CommandProcessing
{
    internal delegate ApiError DetermineErrorConditionDelegate(string content);

    internal delegate bool IsErrorDelegate(string content);

    internal class ErrorHandler
    {
        private ExchangeType SourceExchangeType;

        public DetermineErrorConditionDelegate DetermineErrorCondition;
        public IsErrorDelegate IsExchangeError;

        public bool IsResponseError(IRestResponse response)
        {
            return (response == null
                || response.ErrorException != null
                || response.StatusCode != System.Net.HttpStatusCode.OK
                || IsExchangeError(response.Content));
        }

        internal ErrorHandler(ExchangeType sourceExchange)
        {
            SourceExchangeType = sourceExchange;
        }

        public ApiError HandleErrorResponse(IRestResponse response, RestRequest request, Exception ex = null)
        {
            ApiError error = null;
            if (DetermineErrorCondition != null)
            {
                error = DetermineErrorCondition(response.Content);
            }

            if (error == null)
            {
                error = new ApiError(SourceExchangeType);
                error.Message = response.Content;
            }

            error.HttpStatus = (HttpResponseCode)(int)response.StatusCode;

            if (error.ErrorCode == BExErrorCode.InsufficientFunds)
                ThrowException<InsufficientFundsException>(error);
            else if (error.ErrorCode == BExErrorCode.Authorization)
                ThrowException<ExchangeAuthorizationException>(error);

            return error;
        }

        public void ThrowException<E>(ApiError source) where E : Exception
        {
            E exception = (E)Activator.CreateInstance(typeof(E),
                                                    BindingFlags.Public | BindingFlags.Instance,
                                                    null,
                                                    new object[] { source.Message },
                                                    null);

            ((Exception)exception).Data.Add("BExError", source);

            throw exception;
        }
    }
}