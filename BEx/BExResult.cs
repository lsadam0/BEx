// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;

using BEx.ExchangeEngine.API;

namespace BEx
{
    /// <summary>
    ///     Exchange Result Base Class
    /// </summary>
    public abstract class BExResult : IExchangeResult
    {
        internal BExResult(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
        {
            ExchangeTimeStampUTC = exchangeTimeStamp;
            LocalTimeStampUTC = DateTime.UtcNow;
            SourceExchange = sourceExchange;
        }

        protected virtual string DebugDisplay => $"{SourceExchange} {ExchangeTimeStampUTC}";

        /// <summary>
        ///     Exchange reported TimeStamp of the action.  When the Exchange does not provide
        ///     a TimeStamp, this value will be equal to LocalTimeStamp.
        /// </summary>
        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        /// <summary>
        ///     Exchange from which this result was received
        /// </summary>
        public ExchangeType SourceExchange { get; }
    }
}