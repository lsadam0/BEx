// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx.ExchangeEngine.Bitfinex.JSON
{
    internal class Bid
    {
        [JsonProperty("price", Required = Required.Always)]
        public string Price { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public string Timestamp { get; set; }
    }

    internal class Ask
    {
        [JsonProperty("price", Required = Required.Always)]
        public string Price { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public string Timestamp { get; set; }
    }

    internal class OrderBookIntermediate : IExchangeResponse<OrderBook>
    {
        [JsonProperty("bids", Required = Required.Always)]
        public Bid[] Bids { get; set; }

        [JsonProperty("asks", Required = Required.Always)]
        public Ask[] Asks { get; set; }


        public OrderBook Convert(TradingPair pair)
        {
            IList<OrderBookEntry> convertedBids = Bids.Select(
           x => new OrderBookEntry(Conversion.ToDecimalInvariant(x.Amount), Conversion.ToDecimalInvariant(x.Price))).ToList();

            IList<OrderBookEntry> convertedAsks = Asks.Select(
                x => new OrderBookEntry(Conversion.ToDecimalInvariant(x.Amount), Conversion.ToDecimalInvariant(x.Price))).ToList();

            return new OrderBook(convertedBids, convertedAsks, DateTime.UtcNow, ExchangeType.Bitfinex)
            {
                Pair = pair
            };
        }
    }
}