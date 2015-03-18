using BEx.ExchangeSupport;
using System;
using System.Collections.Generic;

namespace BEx
{
    /// <summary>
    /// All open Orders for your Exchange Account
    /// </summary>
    public sealed class OpenOrders : APIResult
    {
        internal OpenOrders(IEnumerable<IExchangeResponse> orders, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Orders = new Dictionary<int, Order>();

            foreach (IExchangeResponse order in orders)
            {
                Order converted = order.ConvertToStandard(pair) as Order;
                Orders.Add(converted.ID, converted);
            }
            //orders.ForEach(x => Orders.Add(x.ID, x));
        }

        /// <summary>
        /// Orders by Exchange Order ID
        /// </summary>
        public Dictionary<int, Order> Orders
        {
            get;
            internal set;
        }
    }
}