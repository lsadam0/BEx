using System;

namespace BEx
{
    public class Tick : APIResult
    {
        internal Tick(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        { }

        public Decimal Ask
        {
            get;
            set;
        }

        public Currency BaseCurrency
        {
            get;
            set;
        }

        public Decimal Bid
        {
            get;
            set;
        }

        public Currency CounterCurrency
        {
            get;
            set;
        }

        public Decimal High
        {
            get;
            set;
        }

        public Decimal Last
        {
            get;
            set;
        }

        public Decimal Low
        {
            get;
            set;
        }

        public Decimal Volume
        {
            get;
            set;
        }

        public override string ToString()
        {
            string output = "{0}/{1} - Bid: {2} - Ask: {3} - High: {4} - Low: {5} - Volume: {6} - Time: {7}";

            return string.Format(output, BaseCurrency, CounterCurrency, Bid, Ask, High, Low, Volume, ExchangeTimeStamp.ToString());
        }
    }
}