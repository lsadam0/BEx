using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            translator = new ResultTranslation();

            errorHandler = new ErrorHandler();
        }

        public APIResult ExecuteCommand(ExchangeCommand toExecute)
        {
            APIResult res = null;

            res = ExecutionPipeline(toExecute);

            return res;
        }

        private APIResult ExecutionPipeline(ExchangeCommand toExecute)
        {
            APIResult res = null;

            // Get Request object from Factory
            RestRequest request = factory.GetRequest(toExecute);

            //string response = null;
            // Dispatch the request and get result;
            // dispatcher.ExecuteCommand<

            // Handle Error
            //errorHandler

            //translator.Translate<

            return res;
        }
    }
}