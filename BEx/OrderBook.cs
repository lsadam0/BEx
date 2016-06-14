// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using BEx.ExchangeEngine.API;

namespace BEx
{
    /// <summary>
    ///     OpenModel Order Book for the trading Pair
    /// </summary>
    public sealed class OrderBook : BExResult
    {
        internal OrderBook(
            IList<OrderBookEntry> bids,
            IList<OrderBookEntry> asks,
            DateTime exchangeTimeStamp,
            ExchangeType sourceExchange,
            TradingPair pair)
            : base(exchangeTimeStamp, sourceExchange)
        {
            Asks = new ReadOnlyCollection<OrderBookEntry>(asks);
            Bids = new ReadOnlyCollection<OrderBookEntry>(bids);
            Pair = pair;
        }

        public IReadOnlyList<OrderBookEntry> Asks { get; }

        /// <summary>
        ///     Trading Pair
        /// </summary>
        public TradingPair Pair { get; }

        public IReadOnlyList<OrderBookEntry> Bids { get; }
    }
}