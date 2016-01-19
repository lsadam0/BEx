// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BEx.ExchangeEngine
{
    internal abstract class ExchangeCommand<T> : IExchangeCommand<T> where T : IExchangeResult
    {
        /// <summary>
        /// Initialize ExchangeCommand Instance
        /// </summary>
        /// <param name="identifier">CommandClass for this Instance</param>
        /// <param name="httpMethod">Http Execution Method</param>
        /// <param name="relativeUri">Exchange Uri, relative to the Base Uri</param>
        /// <param name="isAuthenticated">Does this Command required Authentication with the Exchange?</param>
        /// <param name="intermediateType">JSON Response -> IntermediateType -> BEx.ApiResult Sub-Type</param>
        public ExchangeCommand(
                     ExecutionEngine executor,
                    Method httpMethod,
                    Uri relativeUri,
                    bool isAuthenticated,
                    Type intermediateType,
                    Type apiResultType)
            : this(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, apiResultType, new List<ExchangeParameter>())
        {
        }

        /// <summary>
        /// Initialize ExchangeCommand Instance
        /// </summary>
        /// <param name="identifier">CommandClass for this Instance</param>
        /// <param name="httpMethod">Http Execution Method</param>
        /// <param name="relativeUri">Exchange Uri, relative to the Base Uri</param>
        /// <param name="isAuthenticated">Does this Command required Authentication with the Exchange?</param>
        /// <param name="intermediateType">JSON Response -> IntermediateType -> BEx.ApiResult Sub-Type</param>
        /// <param name="parameters">Collection of Parameters for this Command</param>
        public ExchangeCommand(
                            ExecutionEngine executor,
                            Method httpMethod,
                            Uri relativeUri,
                            bool isAuthenticated,
                            Type intermediateType,
                            Type apiResultType,
                            IList<ExchangeParameter> parameters)
        {
            if (intermediateType == null)
                throw new ArgumentNullException("intermediateType");

            Executor = executor;
            ApiResultSubType = apiResultType;
            ReturnsValueType = intermediateType.IsValueType || intermediateType == typeof(string);

            if (intermediateType.IsGenericType)
                ReturnsCollection = intermediateType.GetGenericTypeDefinition() == typeof(List<>);
            else
                ReturnsCollection = false;

            HttpMethod = httpMethod;

            IsAuthenticated = isAuthenticated;
            RelativeUri = relativeUri;

            IntermediateType = intermediateType;

            Parameters =
                new ReadOnlyDictionary<string, ExchangeParameter>(
                    parameters.ToDictionary(x => x.ExchangeParameterName, x => x)
                    );
        }

        /// <summary>
        /// BEx.ApiResult Sub-Type
        /// </summary>
        public Type ApiResultSubType
        {
            get;
            private set;
        }

        public ExecutionEngine Executor { get; private set; }

        /// <summary>
        /// Http Execution Method (GET, POST, PUT, etc.)
        /// </summary>
        public Method HttpMethod
        {
            get;
            private set;
        }

        /// <summary>
        /// JSON Response -> IntermediateType -> ApiResultSubType
        /// </summary>
        public Type IntermediateType
        {
            get;
            private set;
        }

        /// <summary>
        /// Flags if this command requires authentication with the target Exchange
        /// </summary>
        public bool IsAuthenticated
        {
            get;
            private set;
        }

        /// <summary>
        /// Command Parameters, including default values
        /// </summary>
        public IReadOnlyDictionary<string, ExchangeParameter> Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Exchange URL endpoint relative to the base address
        /// </summary>
        public Uri RelativeUri
        {
            get;
            private set;
        }

        /// <summary>
        /// Is the Intermediate Type a Collection?
        /// </summary>
        public bool ReturnsCollection
        {
            get;
            private set;
        }

        /// <summary>
        /// Is the IntermediateType a Value Type (or string)
        /// </summary>
        public bool ReturnsValueType
        {
            get;
            private set;
        }

        public T Execute(IDictionary<StandardParameter, string> parameters)
        {
            return Executor.Execute<T>(this, parameters);
        }

        public T Execute()
        {
            return Executor.Execute(this);
        }

        public T Execute(TradingPair pair)
        {
            return Executor.Execute(this, pair);
        }

        public T Execute(TradingPair pair, IDictionary<StandardParameter, string> parameters)
        {
            return Executor.Execute(this, pair, parameters);
        }
    }
}