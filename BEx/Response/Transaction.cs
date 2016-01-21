// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    ///     Individual Transaction
    /// </summary>
    public struct Transaction : IEquatable<Transaction>, IExchangeResult
    {
        internal Transaction(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : this()
        {
            this.ExchangeTimeStampUTC = exchangeTimeStamp;
            this.SourceExchange = sourceExchange;
            this.LocalTimeStampUTC = DateTime.UtcNow;
        }

        /// <summary>
        ///     Transaction Amount
        /// </summary>
        public decimal Amount { get; internal set; }

        /// <summary>
        ///     Trading Pair
        /// </summary>
        public TradingPair Pair { get; internal set; }

        /// <summary>
        ///     Execution Time
        /// </summary>
        public DateTime CompletedTime { get; internal set; }

        /// <summary>
        ///     Execution Price
        /// </summary>
        public decimal Price { get; internal set; }

        /// <summary>
        ///     Exchange assigned identifier
        /// </summary>
        public long TransactionId { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0} {1} - Price: {2} - Amount: {3}", SourceExchange, Pair, Price, Amount);
        }

        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        public ExchangeType SourceExchange { get; }

        public static bool operator !=(Transaction a, Transaction b)
        {
            return !(a == b);
        }

        public static bool operator ==(Transaction a, Transaction b)
        {
            if ((object)a == null
                || (object)b == null)
            {
                return Equals(a, b);
            }

            return
                a.TransactionId == b.TransactionId
                && a.Amount == b.Amount
                && a.SourceExchange == b.SourceExchange
                && a.Pair == b.Pair
                && a.Price == b.Price;


        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Transaction))
            {
                return false;
            }

            return this == (Transaction)obj;
        }

        public bool Equals(Transaction b)
        {
            return this == b;
        }

        public override int GetHashCode()
        {
            return
                this.SourceExchange.GetHashCode()
                ^ this.Amount.GetHashCode()
                ^ this.Pair.GetHashCode()
                ^ this.Price.GetHashCode()
                ^ this.TransactionId.GetHashCode();

        }


    }
}