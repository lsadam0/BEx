// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Immutable;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Provide Exchange specific Configuration information
    /// </summary>
    public interface IExchangeConfiguration
    {
        /// <summary>
        /// Base Address of the Exchange Api
        /// </summary>
        Uri BaseUri
        {
            get;
        }

        /// <summary>
        /// Default Trading Pair for the Exchange
        /// </summary>
        TradingPair DefaultPair
        {
            get;
        }

        Type ErrorJsonType
        {
            get;
        }

        ExchangeType ExchangeSourceType { get; }

        /// <summary>
        /// All Currency values supported by the Exchange
        /// </summary>
        ImmutableHashSet<Currency> SupportedCurrencies
        {
            get;
        }

        /// <summary>
        /// All Trading pairs supported by the Exchange
        /// </summary>
        ImmutableHashSet<TradingPair> SupportedPairs
        {
            get;
        }
    }
}