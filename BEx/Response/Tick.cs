using System;
using System.Diagnostics;

namespace BEx
{
    /// <summary>
    /// Exchange Tick
    /// </summary>
    public sealed class Tick : ApiResult
    {
        internal Tick(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        { }

        /// <summary>
        /// Lowest Sell Price
        /// </summary>
        public decimal Ask
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets highest Buy Price
        /// </summary>
        public decimal Bid
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets trading pair
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets highest trade price of the last 24 hours
        /// </summary>
        public decimal High
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets price at which the last order executed
        /// </summary>
        public decimal Last
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets lowest trade price of the last 24 hours
        /// </summary>
        public decimal Low
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets trade volume of the last 24 hours
        /// </summary>
        public decimal Volume
        {
            get;
            internal set;
        }



        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1}: {2}/{3}", SourceExchange, Pair, Bid, Ask); }
        }
    }
}