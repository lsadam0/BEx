// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    /// Represents a Buy or Sell transaction previously executed for your user account.
    /// </summary>
    public sealed class UserTransaction : BExResult
    {
        internal UserTransaction(DateTime exchangeTimeStamp, Exchange sourceExchange)
            : base(exchangeTimeStamp, sourceExchange.ExchangeSourceType)
        { }

        private OrderType _transactionType;

        /// <summary>
        /// Signifies whether this transaction is a Buy or Sell Order
        /// </summary>
        public OrderType TransactionType
        {
            get { return _transactionType; }
            internal set
            {
                if (value == OrderType.Sell)
                {
                    BaseCurrencyAmount = (Math.Abs(BaseCurrencyAmount) * -1);
                    CounterCurrencyAmount = Math.Abs(CounterCurrencyAmount);
                }
                else
                {
                    BaseCurrencyAmount = Math.Abs(BaseCurrencyAmount);
                    CounterCurrencyAmount = Math.Abs(CounterCurrencyAmount) * -1;
                }

                _transactionType = value;
            }
        }

        /// <summary>
        /// Trading Pair for this Transaction
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        /// <summary>
        /// Base Currency Amount.  Positive for Buy transactions; Negative for Sell transactions.
        /// </summary>
        public decimal BaseCurrencyAmount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Transaction completion timestamp.
        /// </summary>
        public DateTime CompletedTime
        {
            get;
            internal set;
        }

        /// <summary>
        /// Counter Currency Amount.  Negative for Buy transactions; Positive for Sell transactions.
        /// </summary>
        public decimal CounterCurrencyAmount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Currency Pair Exchange Rate used to execute transaction.
        /// </summary>
        public decimal ExchangeRate
        {
            get;
            internal set;
        }

        /// <summary>
        /// Exchange Order Id
        /// </summary>
        public int OrderId
        {
            get;
            internal set;
        }

        /// <summary>
        /// Total Fee paid for transaction
        /// </summary>
        public decimal TradeFee
        {
            get;
            internal set;
        }

        /// <summary>
        /// Currency in which trading fee was paid
        /// </summary>
        public Currency TradeFeeCurrency
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1} - Order Id: {2}", SourceExchange, Pair, OrderId); }
        }
    }
}