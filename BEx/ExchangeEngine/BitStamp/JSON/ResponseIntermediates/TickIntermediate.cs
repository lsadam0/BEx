// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStamp.JSON.ResponseIntermediates
{
    internal class TickIntermediate : IExchangeResponseIntermediate<Tick>
    {
        [JsonProperty("high", Required = Required.Always)]
        public decimal high { get; set; }

        [JsonProperty("last", Required = Required.Always)]
        public decimal last { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public long timestamp { get; set; }

        [JsonProperty("bid", Required = Required.Always)]
        public decimal bid { get; set; }

        [JsonProperty("vwap", Required = Required.Always)]
        public string vwap { get; set; }

        [JsonProperty("volume", Required = Required.Always)]
        public decimal volume { get; set; }

        [JsonProperty("low", Required = Required.Always)]
        public decimal low { get; set; }

        [JsonProperty("ask", Required = Required.Always)]
        public decimal ask { get; set; }

        public Tick Convert(TradingPair pair)
        {
            return new Tick(
                ask,
                bid,
                high,
                last,
                volume,
                pair,
                low,
                ExchangeType.BitStamp,
                timestamp);
        }
    }
}