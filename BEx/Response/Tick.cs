// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;

namespace BEx
{
    /// <summary>
    /// Exchange Tick
    /// </summary>
    public sealed class Tick : BExResult
    {
        internal Tick(DateTime exchangeTimeStamp, Exchange sourceExchange)
            : base(exchangeTimeStamp, sourceExchange.ExchangeSourceType)
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
        /// Highest Buy Price
        /// </summary>
        public decimal Bid
        {
            get;
            internal set;
        }

        /// <summary>
        /// Trading pair
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        /// <summary>
        /// Highest trade price of the last 24 hours
        /// </summary>
        public decimal High
        {
            get;
            internal set;
        }

        /// <summary>
        /// Price at which the last order executed
        /// </summary>
        public decimal Last
        {
            get;
            internal set;
        }

        /// <summary>
        /// Lowest trade price of the last 24 hours
        /// </summary>
        public decimal Low
        {
            get;
            internal set;
        }

        /// <summary>
        /// Trade volume of the last 24 hours
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