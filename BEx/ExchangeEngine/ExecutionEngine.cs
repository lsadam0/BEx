// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System.Collections.Generic;
using RestSharp;
using BEx;

namespace BEx.ExchangeEngine
{
    internal class ExecutionEngine
    {
        private readonly RequestDispatcher dispatcher;

        private readonly ResultTranslation translator;

        // private ErrorHandler errorHandler;

        internal ExecutionEngine(Exchange targetExchange)
        {
            dispatcher = new RequestDispatcher(targetExchange);

            translator = new ResultTranslation(targetExchange);

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

            IRestResponse result = dispatcher.Dispatch(request, toExecute);

            ApiResult res = translator.Translate(
                                    result.Content,
                                    toExecute,
                                    pair);

            // Error Handling!

            return res;
        }
    }
}