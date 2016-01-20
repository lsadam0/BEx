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
        private readonly IRequestDispatcher dispatcher;

        private readonly ResultTranslation translator;

        internal ExecutionEngine(Uri baseUri, ExchangeType sourceExchange)
        {
            dispatcher = new RequestDispatcher(baseUri);
            translator = new ResultTranslation(sourceExchange);

        }

        internal ExecutionEngine(Uri baseUri, IExchangeAuthenticator authenticator, ExchangeType sourceExchange)
        {
            dispatcher = new RequestDispatcher(baseUri, authenticator);

            translator = new ResultTranslation(sourceExchange);
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
            var request = RequestFactory.GetRequest(toExecute, pair, paramCollection);

            var result = dispatcher.Dispatch(request, toExecute);

            if (result.ErrorException == null 
                && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return translator.Translate(
                                         result.Content,
                                         toExecute,
                                         pair);
            }
            else if (result.ErrorException != null)
            {
                throw result.ErrorException;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}