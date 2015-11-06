// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Reflection;

namespace BEx.ExchangeEngine
{
    internal class ErrorHandler
    {
        private readonly Exchange _sourceExchange;

        internal ErrorHandler(Exchange sourceExchange)
        {
            _sourceExchange = sourceExchange;
        }

        private static void ThrowException<TE>(BExError source) where TE : Exception
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

        private BExError GetErrorObject(string json, CurrencyTradingPair pair)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                var deserialized = JsonConvert.DeserializeObject(
                                        json,
                                        _sourceExchange.Configuration.ErrorJsonType
                                        ) as IExchangeResponse;

                return deserialized.ConvertToStandard(pair, _sourceExchange) as BExError;
            }
            else
                return new BExError(_sourceExchange.ExchangeSourceType)
                {
                    Message = json
                };
        }

        public Exception HandleErrorResponse(
                                        IExchangeCommand referenceCommand,
                                        IRestResponse response,
                                        IRestRequest request,
                                        CurrencyTradingPair pair)
        {
            var errorObject = GetErrorObject(response.Content, pair);

            errorObject.HttpStatus = (HttpResponseCode)(int)response.StatusCode;

            Exception res;

            res = DetermineExceptionType(errorObject, referenceCommand, response.ErrorException);

            res.Source = _sourceExchange.ToString();
            res.Data.Add("BExErrorObject", errorObject);

            return res;
        }

        private Exception DetermineExceptionType(BExError errorObject, IExchangeCommand referenceCommand, Exception inner)
        {
            Type exceptionType = _sourceExchange.ErrorInterpreter.Interpret(errorObject);

            return (Exception)Activator.CreateInstance(
                                                exceptionType,
                                                BindingFlags.Public | BindingFlags.Instance,
                                                null,
                                                new object[] { errorObject.Message, inner },
                                                null);
        }
    }
}