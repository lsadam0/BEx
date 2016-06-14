// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.BitStamp.API.Models
{
    internal class OrderBookModel : IExchangeResponseIntermediate<OrderBook>
    {
        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        [JsonProperty("bids", Required = Required.Always)]
        public string[][] Bids { get; set; }

        [JsonProperty("asks", Required = Required.Always)]
        public string[][] Asks { get; set; }

        public OrderBook Convert(TradingPair pair)
        {
            var convertedBids = Bids
                .Select(
                    x =>
                        new OrderBookEntry(
                            Conversion.ToDecimalInvariant(x[1]),
                            Conversion.ToDecimalInvariant(x[0]),
                            (long) Timestamp,
                            ExchangeType.BitStamp))
                .ToList();

            var convertedAsks = Asks
                .Select(
                    x =>
                        new OrderBookEntry(
                            Conversion.ToDecimalInvariant(x[1]),
                            Conversion.ToDecimalInvariant(x[0]),
                            (long) Timestamp,
                            ExchangeType.BitStamp))
                .ToList();

            return new OrderBook(
                convertedBids,
                convertedAsks,
                Timestamp.ToDateTimeUTC(),
                ExchangeType.BitStamp,
                pair);
        }
    }
}