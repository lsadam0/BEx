// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        public Exception HandleErrorResponse<T>(
                                        IExchangeCommand<T> referenceCommand,
                                        IRestResponse response,
                                        IRestRequest request,
                                        TradingPair pair) where T : IExchangeResult
        {
            var errorObject = GetErrorObject(response.Content, pair);

            errorObject.HttpStatus = (HttpResponseCode)(int)response.StatusCode;

            Exception res;

            res = DetermineExceptionType(errorObject, referenceCommand, response.ErrorException);

            res.Source = _sourceExchange.ToString();
            res.Data.Add("BExErrorObject", errorObject);

            return res;
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

        private Exception DetermineExceptionType<T>(BExError errorObject, IExchangeCommand<T> referenceCommand, Exception inner) where T : IExchangeResult
        {
            Type exceptionType = _sourceExchange.ErrorInterpreter.Interpret(errorObject);

            return (Exception)Activator.CreateInstance(
                                                exceptionType,
                                                BindingFlags.Public | BindingFlags.Instance,
                                                null,
                                                new object[] { errorObject.Message, inner },
                                                null);
        }

        private BExError GetErrorObject(string json, TradingPair pair)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                throw new NotImplementedException();
                /*  var deserialized = JsonConvert.DeserializeObject(
                                          json,
                                          _sourceExchange.Configuration.ErrorJsonType
                                          ) as IExchangeResponse;

                  return deserialized.ConvertToStandard(pair, _sourceExchange) as BExError;*/
            }
            else
                return new BExError(_sourceExchange.ExchangeSourceType)
                {
                    Message = json
                };
        }
    }
}