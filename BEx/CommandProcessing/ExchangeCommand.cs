using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace BEx.CommandProcessing
{
    public class ExchangeCommand
    {
        public ExchangeCommand(CommandClass identifier,
                                Method httpMethod,
                                string relativeUrl,
                                bool isAuthenticated,
                                Type intermediateType)
            : this(identifier, httpMethod, relativeUrl, isAuthenticated, intermediateType, null)
        {
        }

        public ExchangeCommand(CommandClass identifier,
                                Method httpMethod,
                                string relativeUrl,
                                bool isAuthenticated,
                                Type intermediateType,
                                IList<ExchangeParameter> parameters)
        {
            if (intermediateType == null)
                throw new ArgumentNullException("intermediateType");

            DefaultParameters = new Dictionary<string, ExchangeParameter>();
            DependentParameters = new Dictionary<StandardParameterType, ExchangeParameter>();

            ReturnsValueType = intermediateType.IsValueType || intermediateType == typeof(string);

            if (intermediateType.IsGenericType)
                ReturnsCollection = intermediateType.GetGenericTypeDefinition() == typeof(List<>);
            else
                ReturnsCollection = false;

            HttpMethod = httpMethod;
            Identifier = identifier;
            IsAuthenticated = isAuthenticated;
            RelativeUri = relativeUrl;

            IntermediateType = intermediateType;
            LowercaseUrlParameters = false;

            if (parameters != null)
            {
                foreach (ExchangeParameter param in parameters)
                {
                    if (param.StandardParameterIdentifier == StandardParameterType.None)
                        DefaultParameters.Add(param.ExchangeParameterName, param);
                    else
                        DependentParameters.Add(param.StandardParameterIdentifier, param);
                }
            }

            SetReturnType();
        }

        public bool ReturnsCollection
        {
            get;
            private set;
        }

        public bool HasDependentParameters
        {
            get
            {
                return DependentParameters.Count > 0;
            }
        }

        public bool LowercaseUrlParameters
        {
            get;
            set;
        }

        private void SetReturnType()
        {
            switch (Identifier)
            {
                case CommandClass.AccountBalance:
                    ReturnType = typeof(AccountBalance);
                    break;

                case CommandClass.BuyOrder:
                    ReturnType = typeof(Order);
                    break;

                case CommandClass.CancelOrder:
                    ReturnType = typeof(bool);
                    break;

                case CommandClass.DepositAddress:
                    ReturnType = typeof(DepositAddress);
                    break;

                case CommandClass.OpenOrders:
                    ReturnType = typeof(OpenOrders);
                    break;

                case CommandClass.OrderBook:
                    ReturnType = typeof(OrderBook);
                    break;

                case CommandClass.SellOrder:
                    ReturnType = typeof(Order);
                    break;

                case CommandClass.Tick:
                    ReturnType = typeof(Tick);
                    break;

                case CommandClass.Transactions:
                    ReturnType = typeof(Transactions);
                    break;

                case CommandClass.UserTransactions:
                    ReturnType = typeof(UserTransactions);
                    break;
            }
        }

        public Type ReturnType
        {
            get;
            set;
        }

        public Type IntermediateType
        {
            get;
            set;
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

        public Dictionary<StandardParameterType, ExchangeParameter> DependentParameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Command Parameters, including default values
        /// </summary>
        public Dictionary<string, ExchangeParameter> DefaultParameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Exchange URL endpoint relative to the base address
        /// </summary>
        public string RelativeUri
        {
            get;
            private set;
        }

        public string GetResolvedRelativeUri(CurrencyTradingPair pair)
        {
            if (LowercaseUrlParameters)
                return string.Format(RelativeUri, pair.BaseCurrency.ToString().ToLower(), pair.CounterCurrency.ToString().ToLower());
            else
                return string.Format(RelativeUri, pair.BaseCurrency.ToString(), pair.CounterCurrency.ToString());
        }

        public bool ReturnsValueType
        {
            get;
            private set;
        }
    }
}