using System;
using System.Collections.Generic;
using System.Linq;

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
        public SortedDictionary<decimal, decimal> AsksByPrice
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
        public SortedDictionary<decimal, decimal> BidsByPrice
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} - High Bid: {1} - Low Ask: {2}", SourceExchange, BidsByPrice.FirstOrDefault(), AsksByPrice.FirstOrDefault()); }
        }
    }
}