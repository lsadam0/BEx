// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    ///     Exchange Tick
    /// </summary>
    public struct Tick : IEquatable<Tick>, IExchangeResult
    {
        internal Tick(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : this()
        {
            ExchangeTimeStampUTC = exchangeTimeStamp;
            SourceExchange = sourceExchange;
            LocalTimeStampUTC = DateTime.UtcNow;
        }

        /// <summary>
        ///     Lowest Sell Price
        /// </summary>
        public decimal Ask { get; internal set; }

        /// <summary>
        ///     Highest Buy Price
        /// </summary>
        public decimal Bid { get; internal set; }



        /// <summary>
        ///     Highest trade price of the last 24 hours
        /// </summary>
        public decimal High { get; internal set; }

        /// <summary>
        ///     Price at which the last order executed
        /// </summary>
        public decimal Last { get; internal set; }

        /// <summary>
        ///     Trade volume of the last 24 hours
        /// </summary>
        public decimal Volume { get; internal set; }

        /// <summary>
        ///     Trading pair
        /// </summary>
        public TradingPair Pair { get; internal set; }

        /// <summary>
        ///     Lowest trade price of the last 24 hours
        /// </summary>
        public decimal Low { get; internal set; }

        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        public ExchangeType SourceExchange { get; }

        public static bool operator !=(Tick a, Tick b)
        {
            return !(a == b);
        }

        public static bool operator ==(Tick a, Tick b)
        {
            if ((object)a == null
                || (object)b == null)
            {
                return Equals(a, b);
            }

            return
                (a.Ask == b.Ask)
                && (a.Bid == b.Bid)
                && (a.High == b.High)
                && (a.Last == b.Last)
                && (a.Low == b.Low)
                && (a.Pair == b.Pair)
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

            return this == (Tick)obj;
        }

        public bool Equals(Tick b)
        {
            return this == b;
        }

        public override int GetHashCode()
        {
            return
                Ask.GetHashCode()
                ^ Bid.GetHashCode()
                ^ High.GetHashCode()
                ^ Last.GetHashCode()
                ^ Low.GetHashCode()
                ^ (int)Pair.BaseCurrency
                ^ (int)Pair.CounterCurrency
                ^ Volume.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}/{2}", Pair, Bid, Ask);
        }
    }
}