// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    /// Individual Transaction
    /// </summary>
    public sealed class Transaction : BExResult
    {
        internal Transaction(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        {
        }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        public decimal Amount
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
        public decimal Price
        {
            get;
            internal set;
        }

        /// <summary>
        /// Exchange assigned identifier
        /// </summary>
        public long TransactionId
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1} - Price: {2} - Amount: {3}", SourceExchange, Pair, Price, Amount); }
        }
    }
}