using System;

namespace BEx
{
    public sealed class UserTransaction : ApiResult
    {
        internal UserTransaction(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        { }

        /// <summary>
        /// Trading Pair
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        /// <summary>
        /// Base currency amount for the transaction
        /// </summary>
        public decimal BaseCurrencyAmount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Timestamp of transaction completetion
        /// </summary>
        public DateTime CompletedTime
        {
            get;
            internal set;
        }

        /// <summary>
        /// Counter currency amount for the transaction
        /// </summary>
        public decimal CounterCurrencyAmount
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

        /// <summary>
        /// Exchange Transaction Id
        /// </summary>
        public int TransactionId
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