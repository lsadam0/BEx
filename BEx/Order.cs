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
    public class Order : APIResult
    {
        internal Order(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        {
        }

        /// <summary>
        /// Currency Amount
        /// </summary>
        public Decimal Amount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Base Currency for the Order
        /// </summary>
        public Currency BaseCurrency
        {
            get;
            internal set;
        }

        /// <summary>
        /// Counter Currency for the Order
        /// </summary>
        public Currency CounterCurrency
        {
            get;
            internal set;
        }

        /// <summary>
        /// Exchange Order ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// True if this Order has TradeType == Buy
        /// </summary>
        public bool IsBuyOrder
        {
            get
            {
                return (this.TradeType == OrderType.Buy);
            }
        }

        /// <summary>
        /// True if this Order has a TradeType == Sell
        /// </summary>
        public bool IsSellOrder
        {
            get
            {
                return (this.TradeType == OrderType.Sell);
            }
        }

        /// <summary>
        /// Limit Price
        /// </summary>
        public Decimal Price
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
    }
}