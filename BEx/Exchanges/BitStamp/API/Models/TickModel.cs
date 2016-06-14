// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.BitStamp.API.Models
{
    internal class TickModel : IExchangeResponseIntermediate<Tick>
    {
        [JsonProperty("ask", Required = Required.Always)]
        public string ask { get; set; }

        [JsonProperty("bid", Required = Required.Always)]
        public string bid { get; set; }

        [JsonProperty("high", Required = Required.Always)]
        public string high { get; set; }

        [JsonProperty("last", Required = Required.Always)]
        public string last { get; set; }

        [JsonProperty("low", Required = Required.Always)]
        public string low { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public long timestamp { get; set; }

        [JsonProperty("volume", Required = Required.Always)]
        public string volume { get; set; }

        [JsonProperty("vwap", Required = Required.Always)]
        public string vwap { get; set; }

        public Tick Convert(TradingPair pair)
        {
            return new Tick(
                Conversion.ToDecimalInvariant(ask),
                Conversion.ToDecimalInvariant(bid),
                Conversion.ToDecimalInvariant(last),
                Conversion.ToDecimalInvariant(volume),
                pair,
                ExchangeType.BitStamp,
                timestamp);
        }
    }
}