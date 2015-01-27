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

        public DateTime TimeStamp
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

        internal OrderBook()
            : base()
        {
            BidsByPrice = new SortedDictionary<decimal, decimal>();
            AsksByPrice = new SortedDictionary<decimal, decimal>();
        }
    }
}