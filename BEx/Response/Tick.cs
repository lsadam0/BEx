// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    /// Exchange Tick
    /// </summary>
    public sealed class Tick : BExResult, IEquatable<Tick>
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

        public override int GetHashCode()
        {
            return
                this.Ask.GetHashCode()
                ^ this.Bid.GetHashCode()
                ^ this.High.GetHashCode()
                ^ this.Last.GetHashCode()
                ^ this.Low.GetHashCode()
                ^ (int)this.Pair.BaseCurrency
                ^ (int)this.Pair.CounterCurrency
                ^ (int)this.SourceExchange
                ^ this.Volume.GetHashCode();
        }

        public static bool operator !=(Tick a, Tick b)
        {
            return !(a == b);
        }

        public static bool operator ==(Tick a, Tick b)
        {
            if (a == null || b == null)
            {
                return false;
            }

            return
                (a.Ask == b.Ask)
                && (a.Bid == b.Bid)
                && (a.High == b.High)
                && (a.Last == b.Last)
                && (a.Low == b.Low)
                && (a.Pair == b.Pair)
                && (a.SourceExchange == b.SourceExchange)
                && (a.Volume == b.Volume);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Tick))
            {
                return false;
            }

            return this == (obj as Tick);
        }

        public bool Equals(Tick b)
        {
            if (b == null)
            {
                return false;
            }

            return (this == b);
        }
    }
}