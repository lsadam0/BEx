// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;

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

        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        /// <summary>
        ///     Exchange Order Id
        /// </summary>
        public int OrderId { get; internal set; }

        /// <summary>
        ///     Trading Pair for this Transaction
        /// </summary>
        public TradingPair Pair { get; internal set; }

        public ExchangeType SourceExchange { get; }

        /// <summary>
        ///     Total Fee paid for transaction
        /// </summary>
        public decimal TradeFee { get; internal set; }

        /// <summary>
        ///     Currency in which trading fee was paid
        /// </summary>
        public Currency TradeFeeCurrency { get; internal set; }

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

        public static bool operator !=(UserTransaction a, UserTransaction b)
        {
            return !(a == b);
        }

        public static bool operator ==(UserTransaction a, UserTransaction b)
        {
           

            return
                a.BaseCurrencyAmount == b.BaseCurrencyAmount
                && a.CompletedTime == b.CompletedTime
                && a.CounterCurrencyAmount == b.CounterCurrencyAmount
                && a.ExchangeRate == b.ExchangeRate
                && a.OrderId == b.OrderId
                && a.Pair == b.Pair
                && a.SourceExchange == b.SourceExchange
                && a.TradeFee == b.TradeFee
                && a.TradeFeeCurrency == b.TradeFeeCurrency
                && a.TransactionType == b.TransactionType;
        }

        public override bool Equals(object obj)
        {
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
                BaseCurrencyAmount.GetHashCode()
                ^ CompletedTime.GetHashCode()
                ^ CounterCurrencyAmount.GetHashCode()
                ^ ExchangeRate.GetHashCode()
                ^ OrderId.GetHashCode()
                ^ Pair.GetHashCode()
                ^ SourceExchange.GetHashCode()
                ^ TradeFee.GetHashCode()
                ^ TradeFeeCurrency.GetHashCode()
                ^ TransactionType.GetHashCode()
                ^ TransactionType.GetHashCode();
        }

        public override string ToString() => $"{SourceExchange} {Pair} - Order Id: {OrderId}";
    }
}