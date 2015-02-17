using System.Collections.Generic;
using System;

namespace BEx
{
    public class OpenOrders : APIResult
    {
        public List<Order> Orders
        {
            get;
            set;
        }

        internal OpenOrders()
            : base(DateTime.Now)
        {
            Orders = new List<Order>();
        }

        internal OpenOrders(List<Order> orders, Currency baseCurrency, Currency counterCurrency)
            : base(DateTime.Now)
        {
            Orders = orders;
        }
    }
}