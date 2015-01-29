using System;
using System.Collections.Generic;

namespace BEx
{
    public class OrderBook : APIResult
    {
        public Currency BaseCurrency
        {
            get;
            set;
        }

        public Currency CounterCurrency
        {
            get;
            set;
        }

        public SortedDictionary<Decimal, Decimal> BidsByPrice
        {
            get;
            set;
        }

        public SortedDictionary<Decimal, Decimal> AsksByPrice
        {
            get;
            set;
        }

        public override string ToString()
        {
            string output = "{0}/{1} - Bids: {2} - Asks: {3}";

            return string.Format(output, BaseCurrency, CounterCurrency, BidsByPrice.Count, AsksByPrice.Count);
        }
        internal OrderBook()
            : base()
        {
            BidsByPrice = new SortedDictionary<decimal, decimal>();
            AsksByPrice = new SortedDictionary<decimal, decimal>();
        }
    }
}