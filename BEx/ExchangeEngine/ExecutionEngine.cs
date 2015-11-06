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

        private readonly ResultTranslation _translator;

        private readonly ErrorHandler _errorHandler;

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

        public BExResult Execute(IExchangeCommand toExecute)
        {
            return ExecutionPipeline(toExecute, default(CurrencyTradingPair));
        }

        public BExResult Execute(IExchangeCommand toExecute, IDictionary<StandardParameter, string> parameters)
        {
            return ExecutionPipeline(toExecute, default(CurrencyTradingPair), parameters);
        }

        public BExResult Execute(IExchangeCommand toExecute, CurrencyTradingPair pair)
        {
            return ExecutionPipeline(toExecute, pair);
        }

        public BExResult Execute(IExchangeCommand toExecute, CurrencyTradingPair pair, IDictionary<StandardParameter, string> parameters)
        {
            return ExecutionPipeline(toExecute, pair, parameters);
        }

        private BExResult ExecutionPipeline(
                                        IExchangeCommand toExecute,
                                        CurrencyTradingPair pair,
                                        IDictionary<StandardParameter, string> paramCollection = null)
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