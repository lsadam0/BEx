using System;
using System.Collections.Generic;

namespace BEx
{
    /// <summary>
    /// Open Order Book for the trading Pair
    /// </summary>
    public sealed class OrderBook : ApiResult
    {
        internal OrderBook(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        {
            BidsByPrice = new SortedDictionary<decimal, decimal>();
            AsksByPrice = new SortedDictionary<decimal, decimal>();
        }

        /// <summary>
        /// Total Ask Orders sorted and indexed by Price
        /// </summary>
        public SortedDictionary<Decimal, Decimal> AsksByPrice
        {
            get;
            internal set;
        }

        /// <summary>
        /// Trading Pair
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        /// <summary>
        /// Total Bid Orders sorted and indexed by Price
        /// </summary>
        public SortedDictionary<Decimal, Decimal> BidsByPrice
        {
            get;
            internal set;
        }
    }
}