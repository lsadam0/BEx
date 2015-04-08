// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;
using BEx.ExchangeEngine;


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

    internal class MockOrderBookJSON : IExchangeResponse
    {
        [JsonProperty("bids")]
        public Bid[] Bids { get; set; }

        [JsonProperty("asks")]
        public Ask[] Asks { get; set; }

        public BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
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