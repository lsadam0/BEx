// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    ///     Represents a Buy or Sell transaction previously executed for your user account.
    /// </summary>
    public struct UserTransaction : IEquatable<UserTransaction>, IExchangeResult
    {
        private OrderType _transactionType;

        internal UserTransaction(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : this()
        {
            SourceExchange = sourceExchange;
            ExchangeTimeStampUTC = exchangeTimeStamp;
            LocalTimeStampUTC = DateTime.UtcNow;
        }

        /// <summary>
        ///     Signifies whether this transaction is a Buy or Sell Order
        /// </summary>
        public OrderType TransactionType
        {
            get { return _transactionType; }
            internal set
            {
                if (value == OrderType.Sell)
                {
                    BaseCurrencyAmount = Math.Abs(BaseCurrencyAmount)*-1;
                    CounterCurrencyAmount = Math.Abs(CounterCurrencyAmount);
                }
                else
                {
                    BaseCurrencyAmount = Math.Abs(BaseCurrencyAmount);
                    CounterCurrencyAmount = Math.Abs(CounterCurrencyAmount)*-1;
                }

                _transactionType = value;
            }
        }

        /// <summary>
        ///     Trading Pair for this Transaction
        /// </summary>
        public TradingPair Pair { get; internal set; }

        /// <summary>
        ///     Base Currency Amount.  Positive for Buy transactions; Negative for Sell transactions.
        /// </summary>
        public decimal BaseCurrencyAmount { get; internal set; }

        /// <summary>
        ///     Transaction completion timestamp.
        /// </summary>
        public DateTime CompletedTime { get; internal set; }

        /// <summary>
        ///     Counter Currency Amount.  Negative for Buy transactions; Positive for Sell transactions.
        /// </summary>
        public decimal CounterCurrencyAmount { get; internal set; }

        /// <summary>
        ///     Currency Pair Exchange Rate used to execute transaction.
        /// </summary>
        public decimal ExchangeRate { get; internal set; }

        /// <summary>
        ///     Exchange Order Id
        /// </summary>
        public int OrderId { get; internal set; }

        /// <summary>
        ///     Total Fee paid for transaction
        /// </summary>
        public decimal TradeFee { get; internal set; }

        /// <summary>
        ///     Currency in which trading fee was paid
        /// </summary>
        public Currency TradeFeeCurrency { get; internal set; }

        public override string ToString() => string.Format("{0} {1} - Order Id: {2}", SourceExchange, Pair, OrderId);

        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        public ExchangeType SourceExchange { get; }

        public static bool operator !=(UserTransaction a, UserTransaction b)
        {
            return !(a == b);
        }

        public static bool operator ==(UserTransaction a, UserTransaction b)
        {
            if ((object) a == null
                || (object) b == null)
            {
                return Equals(a, b);
            }

            return
                a.BaseCurrencyAmount == b.BaseCurrencyAmount
                && a.CompletedTime == b.CompletedTime
                && a.CounterCurrencyAmount == b.CounterCurrencyAmount
                && a.ExchangeRate == b.ExchangeRate
                && a.OrderId == b.OrderId
                && a.Pair == b.Pair
                && a.SourceExchange == b.SourceExchange
                && a.TransactionType == b.TransactionType;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is UserTransaction))
            {
                return false;
            }

            return this == (UserTransaction) obj;
        }

        public bool Equals(UserTransaction b)
        {
            return this == b;
        }

        public override int GetHashCode()
        {
            return
                TransactionType.GetHashCode()
                ^ OrderId.GetHashCode()
                ^ BaseCurrencyAmount.GetHashCode()
                ^ CompletedTime.GetHashCode()
                ^ CounterCurrencyAmount.GetHashCode()
                ^ ExchangeRate.GetHashCode()
                ^ Pair.GetHashCode()
                ^ SourceExchange.GetHashCode()
                ^ TransactionType.GetHashCode()
                ^ ExchangeTimeStampUTC.GetHashCode()
                ^ LocalTimeStampUTC.GetHashCode()
                ^ TradeFee.GetHashCode()
                ^ TradeFeeCurrency.GetHashCode();
        }
    }
}