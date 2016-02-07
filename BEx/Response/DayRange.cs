using System;
using BEx.ExchangeEngine;

namespace BEx.Response
{
    public struct DayRange : IEquatable<DayRange>, IExchangeResult
    {
        internal DayRange(
            decimal high,
            decimal low,
            DateTime exchangeTimeStamp,
            TradingPair pair,
            ExchangeType source) : this()
        {
            High = high;
            Low = low;
            ExchangeTimeStampUTC = exchangeTimeStamp;
            Pair = pair;
            SourceExchange = source;
            LocalTimeStampUTC = DateTime.UtcNow;
        }

        public decimal High { get; }

        public decimal Low { get; }

        public TradingPair Pair { get; }
        public DateTime ExchangeTimeStampUTC { get; }

        public DateTime LocalTimeStampUTC { get; }

        public ExchangeType SourceExchange { get; }

        public bool Equals(DayRange other)
        {
            throw new NotImplementedException();
        }
    }
}