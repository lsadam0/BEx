using System;

namespace BEx
{
    /// <summary>
    /// Individual Transaction
    /// </summary>
    public sealed class Transaction : APIResult
    {
        internal Transaction(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        {
        }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        public Decimal Amount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Trading Pair
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        /// <summary>
        /// Execution Time
        /// </summary>
        public DateTime CompletedTime
        {
            get;
            internal set;
        }

        /// <summary>
        /// Execution Price
        /// </summary>
        public Decimal Price
        {
            get;
            internal set;
        }

        /// <summary>
        /// Exchange assigned identifier
        /// </summary>
        public long TransactionID
        {
            get;
            internal set;
        }
    }
}