// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.ExchangeEngine.API.Commands
{
    /// <summary>
    ///     The set of args shared between all exchange commands. E.g. CreateOrder will always require a price, independent of
    ///     Exchange
    /// </summary>
    public enum StandardParameter
    {
        /// <summary>
        ///     Unknown Type
        /// </summary>
        None,

        /// <summary>
        ///     Amount of currency
        /// </summary>
        Amount,

        /// <summary>
        ///     Identifier
        /// </summary>
        Id,

        /// <summary>
        ///     Buy/Sell Price
        /// </summary>
        Price,

        /// <summary>
        ///     Base Currency of a Trading Pair
        /// </summary>
        Base,

        /// <summary>
        ///     Counter Currency of a Trading Pair
        /// </summary>
        Counter,

        /// <summary>
        ///     Currency abbreviation (e.g. BTC)
        /// </summary>
        Currency,

        /// <summary>
        ///     The full name of a Currency, e.g. "Bitcoin"
        /// </summary>
        CurrencyFullName,

        /// <summary>
        ///     Trading Pair
        /// </summary>
        Pair,

        /// <summary>
        ///     Timestamp in the Unix format
        /// </summary>
        UnixTimestamp,

        /// <summary>
        ///     Standard Timestamp
        /// </summary>
        Timestamp,

        /// <summary>
        ///     Integer describing maximum size
        /// </summary>
        Limit
    }
}