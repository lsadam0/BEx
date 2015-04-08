// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BEx.ExchangeEngine;

namespace BEx
{
    /// <summary>
    /// All open Orders for your Exchange Account
    /// </summary>
    public sealed class OpenOrders : BExResult
    {
        internal OpenOrders(IEnumerable<IExchangeResponse> orders, CurrencyTradingPair pair, Exchange sourceExchange)
            : base(DateTime.UtcNow, sourceExchange.ExchangeSourceType)
        {

            IEnumerable<Order> allOrders = orders.Select(x => x.ConvertToStandard(pair, sourceExchange) as Order);

            BuyOrders =
                new ReadOnlyDictionary<int, Order>(
                    allOrders.Where(x => x.IsBuyOrder).ToDictionary(x => x.Id, x => x));

            SellOrders = new ReadOnlyDictionary<int, Order>(
                allOrders.Where(x => x.IsSellOrder).ToDictionary(x => x.Id, x => x));

        }


        public IReadOnlyDictionary<int, Order> SellOrders
        {
            get;
            private set;
        }

        public IReadOnlyDictionary<int, Order> BuyOrders
        {
            get;
            private set;
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