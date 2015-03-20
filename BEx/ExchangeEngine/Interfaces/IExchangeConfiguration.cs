// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Provide Exchange specific Configuration information
    /// </summary>
    public interface IExchangeConfiguration
    {
        /// <summary>
        /// Exchange supplied Api Key for WS Access
        /// </summary>
        string ApiKey
        {
            get;
        }

        /// <summary>
        ///  Exchange supplied Client Id for WS Access
        /// </summary>
        /// <remarks>Not Required</remarks>
        string ClientId
        {
            get;
        }

        /// <summary>
        /// Default Trading Pair for the Exchange
        /// </summary>
        CurrencyTradingPair DefaultPair
        {
            get;
        }

        /// <summary>
        ///  Exchange supplied Secret key for WS Access
        /// </summary>
        string SecretKey
        {
            get;
        }

        /// <summary>
        /// All Trading pairs supported by the Exchange
        /// </summary>
        IList<CurrencyTradingPair> SupportedPairs
        {
            get;
        }

        /// <summary>
        /// All Currency values supported by the Exchange
        /// </summary>
        HashSet<Currency> SupportedCurrencies
        {
            get;
        }

        /// <summary>
        /// Sequential Nonce for Authenticated Commands
        /// </summary>
        long Nonce
        {
            get;
        }

        /// <summary>
        /// Base Address of the Exchange Api
        /// </summary>
        Uri BaseUri
        {
            get;
        }
    }
}