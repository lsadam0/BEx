using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using BEx.Response;
using Newtonsoft.Json;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.BitStamp.API.Models
{
    internal class DayRangeModel : IExchangeResponseIntermediate<DayRange>
    {
        [JsonProperty("high", Required = Required.Always)]
        public string high { get; set; }

        [JsonProperty("last", Required = Required.Always)]
        public string last { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public long timestamp { get; set; }

        [JsonProperty("bid", Required = Required.Always)]
        public string bid { get; set; }

        [JsonProperty("vwap", Required = Required.Always)]
        public string vwap { get; set; }

        [JsonProperty("volume", Required = Required.Always)]
        public string volume { get; set; }

        [JsonProperty("low", Required = Required.Always)]
        public string low { get; set; }

        [JsonProperty("ask", Required = Required.Always)]
        public string ask { get; set; }

        public DayRange Convert(TradingPair pair)
        {
            return new DayRange(
                Conversion.ToDecimalInvariant(high),
                Conversion.ToDecimalInvariant(low),
                timestamp.ToDateTimeUTC(),
                pair,
                ExchangeType.BitStamp);
        }
    }
}