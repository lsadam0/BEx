// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BEx.ExchangeEngine;

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
            BuyOrders = new Dictionary<int, Order>();
            SellOrders = new Dictionary<int, Order>();

            
            foreach (IExchangeResponse order in orders)
            {
                Order converted = order.ConvertToStandard(pair) as Order;

                if (converted != null)
                {
                    if (converted.IsBuyOrder)
                        BuyOrders.Add(converted.Id, converted);
                    else
                        SellOrders.Add(converted.Id, converted);
                }
            }
        }


        public IDictionary<int, Order> SellOrders
        {
            get;
            internal set;
        }

        public IDictionary<int, Order> BuyOrders
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get
            {
                return string.Format("{0} - Buy: {1} - Sell: {2}", SourceExchange, BuyOrders.Count, SellOrders.Count);
            }
        }
    }
}