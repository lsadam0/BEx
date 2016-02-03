using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.High = high;
            this.Low = low;
            this.ExchangeTimeStampUTC = exchangeTimeStamp;
            this.Pair = pair;
            this.SourceExchange = source;
            this.LocalTimeStampUTC = DateTime.UtcNow;
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
