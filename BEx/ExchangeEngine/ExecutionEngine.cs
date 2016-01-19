// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
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

        internal ExecutionEngine(IExchangeAuthenticator authenticator, IRequestDispatcher dispatcher, ExchangeType sourceExchange)
        {
            _dispatcher = dispatcher;
            _translator = new ResultTranslation(sourceExchange);
        }

        internal ExecutionEngine(Uri baseUri, ExchangeType sourceExchange)
        {
            _dispatcher = new RequestDispatcher(baseUri);
            _translator = new ResultTranslation(sourceExchange);

        }

        internal ExecutionEngine(Uri baseUri, IExchangeAuthenticator authenticator, ExchangeType sourceExchange)
        {
            _dispatcher = new RequestDispatcher(baseUri, authenticator);

            _translator = new ResultTranslation(sourceExchange);
        }

        public T Execute<T>(IExchangeCommand<T> toExecute) where T : IExchangeResult
        {
            return ExecutionPipeline(toExecute, default(TradingPair));
        }

        public T Execute<T>(IExchangeCommand<T> toExecute, IDictionary<StandardParameter, string> parameters) where T : IExchangeResult
        {
            return ExecutionPipeline(toExecute, default(TradingPair), parameters);
        }

        public T Execute<T>(IExchangeCommand<T> toExecute, TradingPair pair) where T : IExchangeResult
        {
            return ExecutionPipeline(toExecute, pair);
        }

        public T Execute<T>(IExchangeCommand<T> toExecute, TradingPair pair, IDictionary<StandardParameter, string> parameters) where T : IExchangeResult
        {
            return ExecutionPipeline(toExecute, pair, parameters);
        }

        private T ExecutionPipeline<T>(
                                        IExchangeCommand<T> toExecute,
                                        TradingPair pair,
                                        IDictionary<StandardParameter, string> paramCollection = null) where T : IExchangeResult
        {
            IRestRequest request = RequestFactory.GetRequest(toExecute, pair, paramCollection);

            IRestResponse result = _dispatcher.Dispatch(request, toExecute);

            if (result.ErrorException == null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return _translator.Translate(
                                         result.Content,
                                         toExecute,
                                         pair);
            }
            else
            {

                throw new NotImplementedException();
                //   throw _errorHandler.HandleErrorResponse(toExecute, result, request, pair);
            }
        }
    }
}