// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx.UnitTests.MockTests.MockObjects.MockJSONIntermediates
{
    internal class Bid
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    internal class Ask
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    internal class MockOrderBookJSON : IExchangeResponse<OrderBook>
    {
        [JsonProperty("bids")]
        public Bid[] Bids { get; set; }

        [JsonProperty("asks")]
        public Ask[] Asks { get; set; }

        public OrderBook Convert(TradingPair pair)
        {
            IList<OrderBookEntry> convertedBids = Bids.Select(
                x => new OrderBookEntry(Conversion.ToDecimalInvariant(x.Amount), Conversion.ToDecimalInvariant(x.Price))).ToList();

            IList<OrderBookEntry> convertedAsks = Asks.Select(
                x => new OrderBookEntry(Conversion.ToDecimalInvariant(x.Amount), Conversion.ToDecimalInvariant(x.Price))).ToList();

            return new OrderBook(convertedBids, convertedAsks, DateTime.UtcNow, ExchangeType.Mock)
            {
                Pair = pair
            };
        }
    }
}