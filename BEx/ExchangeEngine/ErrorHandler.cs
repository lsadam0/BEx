// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using RestSharp;
using BEx.Exceptions;

namespace BEx.ExchangeEngine
{
    internal delegate ApiError DetermineErrorConditionDelegate(string content);

    internal delegate bool IsErrorDelegate(string content);

    internal class ErrorHandler
    {
        public DetermineErrorConditionDelegate DetermineErrorCondition;

        public IsErrorDelegate IsExchangeError;

        private readonly ExchangeType _sourceExchangeType;

        internal ErrorHandler(ExchangeType sourceExchange)
        {
            _sourceExchangeType = sourceExchange;
        }

        public static bool IsResponseError(IRestResponse response)
        {
            return response == null
                || response.ErrorException != null
                || response.StatusCode != System.Net.HttpStatusCode.OK;
        }

        public static void ThrowException<TE>(ApiError source) where TE : Exception
        {
            TE exception = (TE)Activator.CreateInstance(
                                                typeof(TE),
                                                BindingFlags.Public | BindingFlags.Instance,
                                                null,
                                                new object[] { source.Message },
                                                null);

            exception.Data.Add("BExError", source);

            throw exception;
        }

        public ApiError HandleErrorResponse(IRestResponse response, RestRequest request, Exception ex = null)
        {
            ApiError error = null;
            if (DetermineErrorCondition != null)
            {
                error = DetermineErrorCondition(response.Content);
            }

            if (error == null)
                error = new ApiError(_sourceExchangeType)
                {
                    Message = response.Content
                };

            error.HttpStatus = (HttpResponseCode)(int)response.StatusCode;

            if (error.ErrorCode == BExErrorCode.InsufficientFunds)
                ThrowException<InsufficientFundsException>(error);
            else if (error.ErrorCode == BExErrorCode.Authorization)
                ThrowException<ExchangeAuthorizationException>(error);

            return error;
        }
    }
}