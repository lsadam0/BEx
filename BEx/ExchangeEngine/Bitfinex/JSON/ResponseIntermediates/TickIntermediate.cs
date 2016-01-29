// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class TickIntermediate : IExchangeResponse<Tick>
    {
        [JsonProperty("mid", Required = Required.Always)]
        public decimal Mid { get; set; }

        [JsonProperty("bid", Required = Required.Always)]
        public decimal Bid { get; set; }

        [JsonProperty("ask", Required = Required.Always)]
        public decimal Ask { get; set; }

        [JsonProperty("last_price", Required = Required.Always)]
        public decimal LastPrice { get; set; }

        [JsonProperty("low", Required = Required.Always)]
        public decimal Low { get; set; }

        [JsonProperty("high", Required = Required.Always)]
        public decimal High { get; set; }

        [JsonProperty("volume", Required = Required.Always)]
        public decimal Volume { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        public Tick Convert(TradingPair pair)
        {
            
            return new Tick(
                Ask,
                Bid,
                High,
                LastPrice,
                Volume,
                pair,
                Low,
                ExchangeType.Bitfinex,
                (long)Timestamp);
        }
    }
}