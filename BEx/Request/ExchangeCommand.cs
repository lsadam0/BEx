using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.Request
{
    public class ExchangeCommand
    {
        public ExchangeCommand()
        {
            Parameters = new Dictionary<string, ExchangeParameter>();
            ReturnsValueType = false;
        }

        public CurrencyTradingPair TradingPair
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
            set;
        }

        /// <summary>
        /// Standard Command Identifier
        /// </summary>
        public CommandClass Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// Flags if this command requires authentication with the target Exchange
        /// </summary>
        public bool IsAuthenticated
        {
            get;
            set;
        }

        /// <summary>
        /// Command Parameters, including default values
        /// </summary>
        public Dictionary<string, ExchangeParameter> Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// Exchange URL endpoint relative to the base address
        /// </summary>
        public string RelativeURI
        {
            get;
            set;
        }

        /// <summary>
        /// Fully formatted URI cotaining currency pair information
        /// </summary>
        public string ResolvedRelativeURI
        {
            get
            {
                return string.Format(RelativeURI, TradingPair.BaseCurrency.ToString(), TradingPair.CounterCurrency.ToString());
            }
        }

        public bool ReturnsValueType
        {
            get;
            set;
        }
    }
}