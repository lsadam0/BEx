using System;
using System.Collections.Generic;
using BEx.ExchangeSupport;

namespace BEx
{
    /// <summary>
    /// All open Orders for your Exchange Account
    /// </summary>
    public sealed class OpenOrders : ApiResult
    {
        internal OpenOrders(IEnumerable<IExchangeResponse> orders, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Orders = new Dictionary<int, Order>();

            foreach (IExchangeResponse order in orders)
            {
                Order converted = order.ConvertToStandard(pair) as Order;

                if (converted != null)
                    Orders.Add(converted.Id, converted);
            }
        }

        /// <summary>
        /// Orders by Exchange Order ID
        /// </summary>
        public IDictionary<int, Order> Orders
        {
            get;
            internal set;
        }
    }
}