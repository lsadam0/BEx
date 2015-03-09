using RestSharp;
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

            //try
            //{
            // Get Request object from Factory
            RestRequest request = factory.GetRequest(toExecute, pair, paramCollection);

            IRestResponse result = dispatcher.Dispatch(request, toExecute, pair);

            if (result.ErrorException != null
                || result.StatusCode != System.Net.HttpStatusCode.OK)
                errorHandler.HandleErrorResponse(result, request, result.ErrorException);
            else
            {
                res = translator.Translate(result.Content,
                                                toExecute,
                                                pair);
            }
            //}
            //catch (Exception ex)
            //{
            // errorHandler.HandleException(ex);
            //}
            // Handle Error
            //errorHandler

            //translator.Translate<

            return res;
        }
    }
}