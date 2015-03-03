using System;
using System.Collections.Generic;

namespace BEx
{

    public class OpenOrders : APIResult
    {
        public Dictionary<int, Order> Orders
        {
            get;
            private set;
        }

        internal OpenOrders()
            : base(DateTime.Now)
        {
            Orders = new Dictionary<int, Order>();
        }

        internal OpenOrders(List<Order> orders, Currency baseCurrency, Currency counterCurrency)
            : base(DateTime.Now)
        {
            Orders = new Dictionary<int, Order>();

            orders.ForEach(x => Orders.Add(x.ID, x));
        }
    }
}