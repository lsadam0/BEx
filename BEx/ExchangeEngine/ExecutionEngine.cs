// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Facade
    /// </summary>
    internal class ExecutionEngine
    {
        private readonly IRequestDispatcher _dispatcher;

        private readonly ErrorHandler _errorHandler;
        private readonly ResultTranslation _translator;

        internal ExecutionEngine(Exchange targetExchange, IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _translator = new ResultTranslation(targetExchange);
            _errorHandler = new ErrorHandler(targetExchange);
        }

        internal ExecutionEngine(Exchange targetExchange)
        {
            _dispatcher = new RequestDispatcher(targetExchange);

            _translator = new ResultTranslation(targetExchange);

            _errorHandler = new ErrorHandler(targetExchange);
        }

        public T Execute<T>(IExchangeCommand<T> toExecute) where T : IExchangeResult
        {
            return ExecutionPipeline(toExecute, default(CurrencyTradingPair));
        }

        public T Execute<T>(IExchangeCommand<T> toExecute, IDictionary<StandardParameter, string> parameters) where T : IExchangeResult
        {
            return ExecutionPipeline(toExecute, default(CurrencyTradingPair), parameters);
        }

        public T Execute<T>(IExchangeCommand<T> toExecute, CurrencyTradingPair pair) where T : IExchangeResult
        {
            return ExecutionPipeline(toExecute, pair);
        }

        public T Execute<T>(IExchangeCommand<T> toExecute, CurrencyTradingPair pair, IDictionary<StandardParameter, string> parameters) where T : IExchangeResult
        {
            return ExecutionPipeline(toExecute, pair, parameters);
        }

        private T ExecutionPipeline<T>(
                                        IExchangeCommand<T> toExecute,
                                        CurrencyTradingPair pair,
                                        IDictionary<StandardParameter, string> paramCollection = null) where T : IExchangeResult
        {
            IRestRequest request = RequestFactory.GetRequest(toExecute, pair, paramCollection);

            IRestResponse result = _dispatcher.Dispatch(request, toExecute);

            if (result.ErrorException == null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    return _translator.Translate(
                                             result.Content,
                                             toExecute,
                                             pair);
                }
                catch (JsonSerializationException jsonEx)
                {
                    throw _errorHandler.HandleErrorResponse(toExecute, result, request, pair);
                }
            }
            else
            {
                throw _errorHandler.HandleErrorResponse(toExecute, result, request, pair);
            }
        }
    }
}