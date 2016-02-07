// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BEx.ExchangeEngine;

namespace BEx
{
    /// <summary>
    ///     All open Orders for your Exchange Account
    /// </summary>
    public sealed class OpenOrders : BExResult
    {
        internal OpenOrders(IEnumerable<IExchangeResponseIntermediate<Order>> orders, TradingPair pair,
            ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
            var allOrders = orders.Select(x => x.Convert(pair));

            BuyOrders =
                new ReadOnlyDictionary<string, Order>(
                    allOrders
                        .Where(x => x.IsBuyOrder)
                        .ToDictionary(x => x.Id, x => x));

            SellOrders = new ReadOnlyDictionary<string, Order>(
                allOrders
                    .Where(x => x.IsSellOrder)
                    .ToDictionary(x => x.Id, x => x));
        }

        public IReadOnlyDictionary<string, Order> SellOrders { get; }

        public IReadOnlyDictionary<string, Order> BuyOrders { get; }
    }
}