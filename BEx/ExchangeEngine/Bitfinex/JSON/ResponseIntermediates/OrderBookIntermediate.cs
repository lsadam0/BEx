// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class Bid
    {
        [JsonProperty("price", Required = Required.Always)]
        public string Price { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }
    }

    internal class Ask
    {
        [JsonProperty("price", Required = Required.Always)]
        public string Price { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }
    }

    internal class OrderBookIntermediate : IExchangeResponse<OrderBook>
    {
        [JsonProperty("bids", Required = Required.Always)]
        public Bid[] Bids { get; set; }

        [JsonProperty("asks", Required = Required.Always)]
        public Ask[] Asks { get; set; }

        public OrderBook Convert(TradingPair pair)
        {
            var convertedBids = Bids.Select(
                 x => new OrderBookEntry(
                     Conversion.ToDecimalInvariant(x.Amount),
                     Conversion.ToDecimalInvariant(x.Price),
                     (long)x.Timestamp,
                     ExchangeType.Bitfinex))
                 .ToList();

            var convertedAsks = Asks.Select(
                 x => new OrderBookEntry(
                     Conversion.ToDecimalInvariant(x.Amount),
                     Conversion.ToDecimalInvariant(x.Price),
                     (long)x.Timestamp,
                     ExchangeType.Bitfinex))
                 .ToList();

            return new OrderBook(
                convertedBids,
                convertedAsks,
                DateTime.UtcNow,
                ExchangeType.Bitfinex,
                pair);

        }
    }
}