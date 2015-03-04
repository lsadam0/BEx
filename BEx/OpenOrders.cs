using System;
using System.Collections.Generic;

namespace BEx
{
    /// <summary>
    /// All open Orders for your Exchange Account
    /// </summary>
    public class OpenOrders : APIResult
    {
        internal OpenOrders(ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Orders = new Dictionary<int, Order>();
        }

        internal OpenOrders(List<Order> orders, Currency baseCurrency, Currency counterCurrency, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Orders = new Dictionary<int, Order>();

            orders.ForEach(x => Orders.Add(x.ID, x));
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