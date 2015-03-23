// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System.Collections.Generic;
using RestSharp;
using BEx;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Facade
    /// </summary>
    internal class ExecutionEngine
    {
        private readonly RequestDispatcher _dispatcher;

        private readonly ResultTranslation _translator;

        // private ErrorHandler errorHandler;

        internal ExecutionEngine(Exchange targetExchange)
        {
            _dispatcher = new RequestDispatcher(targetExchange);

            _translator = new ResultTranslation(targetExchange);

            // errorHandler = new ErrorHandler(_sourceExchange.ExchangeSourceType);
        }

        public ApiResult Execute(IExchangeCommand toExecute)
        {
            return ExecutionPipeline(toExecute, default(CurrencyTradingPair));
        }

        public ApiResult Execute(IExchangeCommand toExecute, IDictionary<StandardParameter, string> parameters)
        {
            return ExecutionPipeline(toExecute, default(CurrencyTradingPair), parameters);
        }

        public ApiResult Execute(IExchangeCommand toExecute, CurrencyTradingPair pair)
        {
            return ExecutionPipeline(toExecute, pair);
        }

        public ApiResult Execute(IExchangeCommand toExecute, CurrencyTradingPair pair, IDictionary<StandardParameter, string> parameters)
        {
            return ExecutionPipeline(toExecute, pair, parameters);
        }

        
        private ApiResult ExecutionPipeline(
                                        IExchangeCommand toExecute,
                                        CurrencyTradingPair pair,
                                        IDictionary<StandardParameter, string> paramCollection = null)
        {
            IRestRequest request = RequestFactory.GetRequest(toExecute, pair, paramCollection);

            IRestResponse result = _dispatcher.Dispatch(request, toExecute);

            ApiResult res = _translator.Translate(
                                    result.Content,
                                    toExecute,
                                    pair);

            // Error Handling!

            return res;
        }
    }
}