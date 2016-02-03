using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.Response;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStamp.JSON.ResponseIntermediates
{
    class DayRangeIntermediate : IExchangeResponseIntermediate<DayRange>
    {
        [JsonProperty("high", Required = Required.Always)]
        public string high { get; set; }

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
        public string low { get; set; }

        [JsonProperty("ask", Required = Required.Always)]
        public decimal ask { get; set; }

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
