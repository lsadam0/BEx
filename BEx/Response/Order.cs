// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;

namespace BEx
{
    /// <summary>
    ///     A Buy or Sell Limit Order for an Exchange
    /// </summary>
    public struct Order : IEquatable<Order>, IExchangeResult
    {
        internal Order(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : this()
        {
            ExchangeTimeStampUTC = exchangeTimeStamp;
            SourceExchange = sourceExchange;
            LocalTimeStampUTC = DateTime.UtcNow;
        }

        /// <summary>
        ///     Currency Amount
        /// </summary>
        public decimal Amount { get; internal set; }

        /// <summary>
        ///     Currency Pair
        /// </summary>
        public TradingPair Pair { get; internal set; }

        /// <summary>
        ///     Exchange Order ID
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        ///     True if this Order has TransactionType == Buy
        /// </summary>
        public bool IsBuyOrder => TradeType == OrderType.Buy;

        /// <summary>
        ///     True if this Order has a TransactionType == Sell
        /// </summary>
        public bool IsSellOrder => TradeType == OrderType.Sell;

        /// <summary>
        ///     Limit Price
        /// </summary>
        public decimal Price { get; internal set; }

        /// <summary>
        ///     Signifies whether this Order is a Buy or a Sell
        /// </summary>
        public OrderType TradeType { get; internal set; }

        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        public ExchangeType SourceExchange { get; }

        public static bool operator !=(Order a, Order b)
        {
            return !(a == b);
        }

        public static bool operator ==(Order a, Order b)
        {
            return
                a.Amount == b.Amount
                && a.Id == b.Id
                && a.Pair == b.Pair
                && a.Price == b.Price
                && a.SourceExchange == b.SourceExchange
                && a.TradeType == b.TradeType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Tick))
            {
                return false;
            }

            return this == (Order) obj;
        }

        public bool Equals(Order b)
        {
            return this == b;
        }

        public override int GetHashCode()
        {
            return
                Amount.GetHashCode()
                ^ Id.GetHashCode()
                ^ Pair.GetHashCode()
                ^ Price.GetHashCode()
                ^ SourceExchange.GetHashCode()
                ^ TradeType.GetHashCode();
        }

        public override string ToString()
            => $"{SourceExchange} {Pair} - ID: {Id} - Amount: {Amount} - Type: {TradeType}";

    }
}