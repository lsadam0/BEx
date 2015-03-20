using System;

namespace BEx
{
    public enum OrderType
    {
        Buy,
        Sell
    }

    /// <summary>
    /// A Buy or Sell Limit Order for an Exchange
    /// </summary>
    public sealed class Order : ApiResult
    {
        internal Order(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        {
        }

        /// <summary>
        /// Currency Amount
        /// </summary>
        public decimal Amount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Currency Pair
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        /// <summary>
        /// Exchange Order ID
        /// </summary>
        public int Id
        {
            get;
            internal set;
        }

        /// <summary>
        /// True if this Order has TradeType == Buy
        /// </summary>
        public bool IsBuyOrder
        {
            get
            {
                return TradeType == OrderType.Buy;
            }
        }

        /// <summary>
        /// True if this Order has a TradeType == Sell
        /// </summary>
        public bool IsSellOrder
        {
            get
            {
                return TradeType == OrderType.Sell;
            }
        }

        /// <summary>
        /// Limit Price
        /// </summary>
        public decimal Price
        {
            get;
            internal set;
        }

        /// <summary>
        /// Signifies whether this Order is a Buy or a Sell
        /// </summary>
        public OrderType TradeType
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get
            {
                return string.Format("{0} {1} - ID: {2} - Amount: {3} - Type: {4}", SourceExchange, Pair, Id, Amount,
                    TradeType);
            }
        }
    }
}