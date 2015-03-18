using RestSharp;
using System.Collections.Generic;

namespace BEx.CommandProcessing
{
    internal class ExecutionEngine
    {
        private Exchange sourceExchange;

        //private RequestFactory factory;

        private RequestDispatcher dispatcher;

        private ResultTranslation translator;

        private ErrorHandler errorHandler;

        internal ExecutionEngine(Exchange targetExchange)
        {
            sourceExchange = targetExchange;

            //factory = new RequestFactory();
            // factory.GetSignature += sourceExchange.CreateSignature;

            dispatcher = new RequestDispatcher(sourceExchange);

            translator = new ResultTranslation(sourceExchange);

            errorHandler = new ErrorHandler(sourceExchange.ExchangeSourceType);

            errorHandler.IsExchangeError += sourceExchange.IsError;
            errorHandler.DetermineErrorCondition += sourceExchange.DetermineErrorCondition;
        }

        public ApiResult ExecuteCommand(ExchangeCommand toExecute, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> paramCollection = null)
        {
            ApiResult res = null;

            res = ExecutionPipeline(toExecute, pair, paramCollection);

            return res;
        }

        private ApiResult ExecutionPipeline(ExchangeCommand toExecute,
                                                CurrencyTradingPair pair,
                                                Dictionary<StandardParameterType, string> paramCollection = null)
        {
            ApiResult res = null;

            RestRequest request = RequestFactory.GetRequest(toExecute, pair, paramCollection);

            IRestResponse result = dispatcher.Dispatch(request, toExecute);

            if (errorHandler.IsResponseError(result))
                errorHandler.HandleErrorResponse(result, request);
            else
            {
                res = translator.Translate(result.Content,
                                                toExecute,
                                                pair);
            }

            return res;
        }
    }
}