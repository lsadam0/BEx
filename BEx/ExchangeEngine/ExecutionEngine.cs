// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Net;
using BEx.ExchangeEngine.Commands;

namespace BEx.ExchangeEngine
{
    internal class ExecutionEngine
    {
        private readonly IRequestDispatcher _dispatcher;

        private readonly ResultTranslation _translator;

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

        public T Execute<T>(IExchangeCommand toExecute) where T : IExchangeResult
        {
            return ExecutionPipeline<T>(toExecute, default(TradingPair));
        }

        public T Execute<T>(IExchangeCommand toExecute, IDictionary<StandardParameter, string> parameters)
            where T : IExchangeResult
        {
            return ExecutionPipeline<T>(toExecute, default(TradingPair), parameters);
        }

        public T Execute<T>(IExchangeCommand toExecute, TradingPair pair) where T : IExchangeResult
        {
            return ExecutionPipeline<T>(toExecute, pair);
        }

        public T Execute<T>(IExchangeCommand toExecute, TradingPair pair,
            IDictionary<StandardParameter, string> parameters) where T : IExchangeResult
        {
            return ExecutionPipeline<T>(toExecute, pair, parameters);
        }

        private T ExecutionPipeline<T>(
            IExchangeCommand toExecute,
            TradingPair pair,
            IDictionary<StandardParameter, string> paramCollection = null) where T : IExchangeResult
        {
            var request = RequestFactory.GetRequest(toExecute, pair, paramCollection);

            var result = _dispatcher.Dispatch<T>(request, toExecute);

            if (result.ErrorException == null
                && result.StatusCode == HttpStatusCode.OK)
            {
                return _translator.Translate<T>(
                    result.Content,
                    toExecute,
                    pair);
            }
            if (result.ErrorException != null)
            {
                throw result.ErrorException;
            }
            throw new NotImplementedException();
        }
    }
}