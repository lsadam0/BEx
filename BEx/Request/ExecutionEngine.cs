using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.Request
{
    internal class ExecutionEngine
    {
        private Exchange sourceExchange;

        private RequestFactory factory;

        private RequestDispatcher dispatcher;

        private ResultTranslation translator;

        private ErrorHandler errorHandler;

        internal ExecutionEngine(Exchange targetExchange)
        {
            sourceExchange = targetExchange;

            factory = new RequestFactory();
            factory.GetSignature += sourceExchange.CreateSignature;

            dispatcher = new RequestDispatcher(sourceExchange);

            translator = new ResultTranslation(sourceExchange.ExchangeSourceType);

            errorHandler = new ErrorHandler(sourceExchange.ExchangeSourceType);

            errorHandler.IsExchangeError += sourceExchange.IsError;
            errorHandler.DetermineErrorCondition += sourceExchange.DetermineErrorCondition;
        }

        public APIResult ExecuteCommand(ExchangeCommand toExecute, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> paramCollection = null)
        {
            APIResult res = null;

            res = ExecutionPipeline(toExecute, pair, paramCollection);

            return res;
        }

        private APIResult ExecutionPipeline(ExchangeCommand toExecute,
                                                CurrencyTradingPair pair,
                                                Dictionary<StandardParameterType, string> paramCollection = null)
        {
            APIResult res = null;

            RestRequest request = factory.GetRequest(toExecute, pair, paramCollection);

            IRestResponse result = dispatcher.Dispatch(request, toExecute, pair);

            if (errorHandler.IsResponseError(result))
                errorHandler.HandleErrorResponse(result, request);
            else
            {
                try
                {
                    res = translator.Translate(result.Content,
                                                    toExecute,
                                                    pair);
                }
                catch (Exception ex)
                {
                    errorHandler.HandleErrorResponse(result, request, ex);
                }
            }

            return res;
        }
    }
}