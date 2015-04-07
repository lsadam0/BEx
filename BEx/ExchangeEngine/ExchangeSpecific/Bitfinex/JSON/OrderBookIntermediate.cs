// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;


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

    internal class OrderBookIntermediate : IExchangeResponse
    {
        [JsonProperty("bids", Required = Required.Always)]
        public Bid[] Bids { get; set; }

        [JsonProperty("asks", Required = Required.Always)]
        public Ask[] Asks { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            IList<OrderBookEntry> convertedBids = Bids.Select(
                x => new OrderBookEntry(Conversion.ToDecimalInvariant(x.Amount), Conversion.ToDecimalInvariant(x.Price))).ToList();


            IList<OrderBookEntry> convertedAsks = Asks.Select(
                x => new OrderBookEntry(Conversion.ToDecimalInvariant(x.Amount), Conversion.ToDecimalInvariant(x.Price))).ToList();


            return new OrderBook(convertedBids, convertedAsks, DateTime.UtcNow, sourceExchange)
            {
                Pair = pair
            };

        }
    }
}