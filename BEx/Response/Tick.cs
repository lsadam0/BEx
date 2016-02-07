// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    public struct Tick : IEquatable<Tick>, IExchangeResult
    {
        internal Tick(
            decimal ask,
            decimal bid,
            decimal last,
            decimal volume,
            TradingPair pair,
            ExchangeType sourceExchange,
            long timestamp)
            : this()
        {
            Ask = ask;
            Bid = bid;

            Last = last;
            Volume = volume;
            Pair = pair;

            SourceExchange = sourceExchange;
            UnixTimeStamp = timestamp;
            ExchangeTimeStampUTC = timestamp.ToDateTimeUTC();
            LocalTimeStampUTC = DateTime.UtcNow;
        }

        public decimal Ask { get; }

        public decimal Bid { get; }

        public DateTime ExchangeTimeStampUTC { get; }


        public decimal Last { get; }

        public DateTime LocalTimeStampUTC { get; }


        public TradingPair Pair { get; }

        public ExchangeType SourceExchange { get; }

        public long UnixTimeStamp { get; }

        public decimal Volume { get; }

        public static bool operator !=(Tick a, Tick b) => !(a == b);

        public static bool operator ==(Tick a, Tick b)
        {
            return
                (a.Ask == b.Ask)
                && (a.Bid == b.Bid)
                && (a.Last == b.Last)
                && (a.Pair == b.Pair)
                && (a.Volume == b.Volume)
                && a.UnixTimeStamp == b.UnixTimeStamp
                && a.SourceExchange == b.SourceExchange;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Tick))
            {
                return false;
            }

            return this == (Tick) obj;
        }

        public bool Equals(Tick b) => this == b;

        public override int GetHashCode()
        {
            return
                Ask.GetHashCode()
                ^ Bid.GetHashCode()
                ^ Last.GetHashCode()
                ^ Pair.GetHashCode()
                ^ Volume.GetHashCode()
                ^ UnixTimeStamp.GetHashCode()
                ^ SourceExchange.GetHashCode();
        }

        public override string ToString() => $"{Pair}: {Bid}/{Ask}";
    }
}