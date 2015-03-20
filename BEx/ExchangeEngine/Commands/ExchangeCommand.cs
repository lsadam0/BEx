// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// ExchangeCommand implements an immutable object that
    /// contains the instructions for used by ExecutionEngine 
    /// to execute a particular command for an Exchange.
    /// </summary>
    /// <remarks>
    /// For Example, IUnauthenticatedCommands.GetTick() requests an ExchangeCommand
    /// with CommandClass.Tick.  A corresponding ExchangeCommand for BitStamp would 
    /// look like this:
    /// public ExchangeCommand BuildTickCommand()
    /// {
    ///        return new ExchangeCommand(
    ///                            CommandClass.Tick,                    // CommandClass for this Instance
    ///                            Method.GET,                           // Http Execution Method
    ///                            new Uri("ticker/", UriKind.Relative), // Url Endpoint Relative to baseUri
    ///                            false,                                // IsAuthenticated: True/False
    ///                            typeof(BitstampTickJSON));            // Intermediate Type
    /// }
    /// </remarks>
    public class ExchangeCommand
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
                    CommandClass identifier,
                    Method httpMethod,
                    Uri relativeUri,
                    bool isAuthenticated,
                    Type intermediateType)
            : this(identifier, httpMethod, relativeUri, isAuthenticated, intermediateType, new List<ExchangeParameter>())
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
                            CommandClass identifier,
                            Method httpMethod,
                            Uri relativeUri,
                            bool isAuthenticated,
                            Type intermediateType,
                            IList<ExchangeParameter> parameters)
        {
            if (intermediateType == null)
                throw new ArgumentNullException("intermediateType");

            ReturnsValueType = intermediateType.IsValueType || intermediateType == typeof(string);

            if (intermediateType.IsGenericType)
                ReturnsCollection = intermediateType.GetGenericTypeDefinition() == typeof(List<>);
            else
                ReturnsCollection = false;

            HttpMethod = httpMethod;
            Identifier = identifier;
            IsAuthenticated = isAuthenticated;
            RelativeUri = relativeUri;

            IntermediateType = intermediateType;

            Parameters =
                new ReadOnlyDictionary<string, ExchangeParameter>(
                    parameters.ToDictionary(x => x.ExchangeParameterName, x => x)
                    );




            SetReturnType();
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
        /// BEx.ApiResult Sub-Type
        /// </summary>
        public Type ApiResultSubType
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
        /// Http Execution Method (GET, POST, PUT, etc.)
        /// </summary>
        public Method HttpMethod
        {
            get;
            private set;
        }

        /// <summary>
        /// Standard Command Identifier
        /// </summary>
        public CommandClass Identifier
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
        /// Exchange URL endpoint relative to the base address
        /// </summary>
        public Uri RelativeUri
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

        /// <summary>
        /// Command Parameters, including default values
        /// </summary>
        public ReadOnlyDictionary<string, ExchangeParameter> Parameters
        {
            get;
            private set;
        }

        private void SetReturnType()
        {
            switch (Identifier)
            {
                case CommandClass.AccountBalance:
                    ApiResultSubType = typeof(AccountBalance);
                    break;

                case CommandClass.BuyOrder:
                    ApiResultSubType = typeof(Order);
                    break;

                case CommandClass.CancelOrder:
                    ApiResultSubType = typeof(bool);
                    break;

                case CommandClass.DepositAddress:
                    ApiResultSubType = typeof(DepositAddress);
                    break;

                case CommandClass.OpenOrders:
                    ApiResultSubType = typeof(OpenOrders);
                    break;

                case CommandClass.OrderBook:
                    ApiResultSubType = typeof(OrderBook);
                    break;

                case CommandClass.SellOrder:
                    ApiResultSubType = typeof(Order);
                    break;

                case CommandClass.Tick:
                    ApiResultSubType = typeof(Tick);
                    break;

                case CommandClass.Transactions:
                    ApiResultSubType = typeof(Transactions);
                    break;

                case CommandClass.UserTransactions:
                    ApiResultSubType = typeof(UserTransactions);
                    break;
            }
        }
    }
}