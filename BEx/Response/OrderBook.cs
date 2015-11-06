// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BEx
{
    /// <summary>
    /// Open Order Book for the trading Pair
    /// </summary>
    public sealed class OrderBook : BExResult
    {
        internal OrderBook(IList<OrderBookEntry> bids, IList<OrderBookEntry> asks, DateTime exchangeTimeStamp, Exchange sourceExchange)
            : base(exchangeTimeStamp, sourceExchange.ExchangeSourceType)
        {
            Asks = new ReadOnlyCollection<OrderBookEntry>(asks);
            Bids = new ReadOnlyCollection<OrderBookEntry>(bids);
        }

        public IReadOnlyList<OrderBookEntry> Asks
        {
            get;
            private set;
        }

        /// <summary>
        /// Trading Pair
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        public IReadOnlyList<OrderBookEntry> Bids
        {
            get;
            private set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} - High Bid: {1} - Low Ask: {2}", SourceExchange); }
            //BidsByPrice, AsksByPrice.FirstOrDefault()); }
        }
    }
}