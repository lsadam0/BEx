using System;
using System.Collections.Generic;

namespace BEx
{
    /// <summary>
    /// Open Order Book for the trading Pair
    /// </summary>
    public class OrderBook : APIResult
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
        /// Base Currency
        /// </summary>
        public Currency BaseCurrency
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

        /// <summary>
        /// Counter Currency
        /// </summary>
        public Currency CounterCurrency
        {
            get;
            internal set;
        }

        public override string ToString()
        {
            string output = "{0}/{1} - Bids: {2} - Asks: {3}";

            return string.Format(output, BaseCurrency, CounterCurrency, BidsByPrice.Count, AsksByPrice.Count);
        }
    }
}