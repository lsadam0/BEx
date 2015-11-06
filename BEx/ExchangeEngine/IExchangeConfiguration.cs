// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Provide Exchange specific Configuration information
    /// </summary>
    public interface IExchangeConfiguration
    {
        /// <summary>
        /// Default Trading Pair for the Exchange
        /// </summary>
        CurrencyTradingPair DefaultPair
        {
            get;
        }

        /// <summary>
        /// All Trading pairs supported by the Exchange
        /// </summary>
        ImmutableHashSet<CurrencyTradingPair> SupportedPairs
        {
            get;
        }

        /// <summary>
        /// All Currency values supported by the Exchange
        /// </summary>
        ImmutableHashSet<Currency> SupportedCurrencies
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

        Type ErrorJsonType
        {
            get;
        }

        ExchangeType ExchangeSourceType { get; }
    }
}