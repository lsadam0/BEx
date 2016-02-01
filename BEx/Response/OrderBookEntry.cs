using System;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    public struct OrderBookEntry : IEquatable<OrderBookEntry>, IExchangeResult
    {
        internal OrderBookEntry(
            decimal amount,
            decimal price,
            long timestamp,
            ExchangeType sourceExchange)
            : this()
        {
            Amount = amount;
            Price = price;
            UnixTimeStamp = timestamp;
            ExchangeTimeStampUTC = timestamp.ToDateTimeUTC();
            LocalTimeStampUTC = DateTime.UtcNow;
            SourceExchange = sourceExchange;
        }

        public long UnixTimeStamp { get; }

        public decimal Amount { get; }

        public decimal Price { get; }

        public ExchangeType SourceExchange { get; }

        public DateTime ExchangeTimeStampUTC { get; }

        public DateTime LocalTimeStampUTC { get; }

        public static bool operator ==(OrderBookEntry a, OrderBookEntry b)
        {
            return
                a.Amount == b.Amount
                && a.Price == b.Price
                && a.SourceExchange == b.SourceExchange
                && a.UnixTimeStamp == b.UnixTimeStamp;
        }

        public override int GetHashCode()
        {
            return
                Amount.GetHashCode()
                ^ Price.GetHashCode()
                ^ SourceExchange.GetHashCode()
                ^ UnixTimeStamp.GetHashCode();
        }

        public static bool operator !=(OrderBookEntry a, OrderBookEntry b) => !(a == b);

        public override bool Equals(object obj)
        {
            if (!(obj is OrderBookEntry))
            {
                return false;
            }

            return this == (OrderBookEntry) obj;
        }

        public bool Equals(OrderBookEntry b) => this == b;
    }
}