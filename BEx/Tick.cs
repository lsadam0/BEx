using System;

namespace BEx
{
    /// <summary>
    /// Exchange Tick
    /// </summary>
    public class Tick : APIResult
    {
        internal Tick(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        { }

        /// <summary>
        /// Ask / Buy Price
        /// </summary>
        public Decimal Ask
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
        /// Bid / Sell Price
        /// </summary>
        public Decimal Bid
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

        public Decimal High
        {
            get;
            internal set;
        }

        public Decimal Last
        {
            get;
            internal set;
        }

        public Decimal Low
        {
            get;
            internal set;
        }

        public Decimal Volume
        {
            get;
            internal set;
        }

        public override string ToString()
        {
            string output = "{0}/{1} - Bid: {2} - Ask: {3} - High: {4} - Low: {5} - Volume: {6} - Time: {7}";

            return string.Format(output, BaseCurrency, CounterCurrency, Bid, Ask, High, Low, Volume, ExchangeTimeStamp.ToString());
        }
    }
}