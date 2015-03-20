using System;

namespace BEx
{
    /// <summary>
    /// Complete Balance Information for a specific Currency.
    /// </summary>
    public sealed class Balance : ApiResult
    {
        internal Balance(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        {
        }

        /// <summary>
        /// The Un-reserved Available Balance
        /// </summary>
        public decimal AvailableToTrade
        {
            get;
            internal set;
        }

        /// <summary>
        /// Source Currency
        /// </summary>
        public Currency BalanceCurrency
        {
            get;
            internal set;
        }

        /// <summary>
        /// Total Exchange Balance for BalanceCurrency, include amounts reserved in open trades
        /// </summary>
        public decimal TotalBalance
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get
            {
                return string.Format("{0} {1} - Available: {2} - Total: {3}", SourceExchange, BalanceCurrency,
                    AvailableToTrade, TotalBalance);
            }
        }
    }
}