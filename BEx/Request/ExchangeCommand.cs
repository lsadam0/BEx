using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.Request
{
    public class ExchangeCommand
    {
        public ExchangeCommand(CommandClass identifier,
                                Method httpMethod,
                                string relativeUrl,
                                bool isAuthenticated,
                                bool returnsValueType = false,
                                List<ExchangeParameter> parameters = null)
        {
            Parameters = new Dictionary<string, ExchangeParameter>();
            ReturnsValueType = false;

            HttpMethod = httpMethod;
            Identifier = identifier;
            IsAuthenticated = isAuthenticated;
            RelativeURI = relativeUrl;
            ReturnsValueType = returnsValueType;

            if (parameters != null)
            {
                parameters.ForEach(x => Parameters.Add(x.Name, x));
            }
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
        /// Command Parameters, including default values
        /// </summary>
        public Dictionary<string, ExchangeParameter> Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Exchange URL endpoint relative to the base address
        /// </summary>
        public string RelativeURI
        {
            get;
            private set;
        }

        public string GetResolvedRelativeURI(CurrencyTradingPair pair)
        {
            return string.Format(RelativeURI, pair.BaseCurrency.ToString(), pair.CounterCurrency.ToString());
        }

        public bool ReturnsValueType
        {
            get;
            private set;
        }
    }
}