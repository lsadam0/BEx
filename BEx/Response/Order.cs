// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    ///     A Buy or Sell Limit Order for an Exchange
    /// </summary>
    public struct Order : IEquatable<Order>, IExchangeResult
    {
        internal Order(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            :this()
        {
            this.ExchangeTimeStampUTC = exchangeTimeStamp;
            this.SourceExchange = sourceExchange;
            this.LocalTimeStampUTC = DateTime.UtcNow;
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
        public bool IsBuyOrder
        {
            get { return TradeType == OrderType.Buy; }
        }

        /// <summary>
        ///     True if this Order has a TransactionType == Sell
        /// </summary>
        public bool IsSellOrder
        {
            get { return TradeType == OrderType.Sell; }
        }

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
            if ((object)a == null
                || (object)b == null)
            {
                return Equals(a, b);
            }

            return
                a.Id == b.Id
                && a.Amount == b.Amount
                && a.Pair == b.Pair
                && a.Price == b.Price
                && a.SourceExchange == b.SourceExchange
                && a.TradeType == b.TradeType;


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

            return this == (Order)obj;
        }

        public bool Equals(Order b)
        {
            return this == b;
        }

        public override int GetHashCode()
        {
            return
                this.Id.GetHashCode()
                ^ this.Amount.GetHashCode()
                ^ this.Pair.GetHashCode()
                ^ this.Price.GetHashCode()
                ^ this.SourceExchange.GetHashCode()
                ^ this.TradeType.GetHashCode();


        }

        public override string ToString()
        {
            return string.Format("{0} {1} - ID: {2} - Amount: {3} - Type: {4}", SourceExchange, Pair, Id, Amount,
                TradeType);
        }
    }
}