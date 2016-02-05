
using Newtonsoft.Json;
using BEx.Response;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class DayRangeIntermediate : IExchangeResponseIntermediate<DayRange>
    {
        [JsonProperty("mid", Required = Required.Always)]
        public string Mid { get; set; }

        [JsonProperty("bid", Required = Required.Always)]
        public string Bid { get; set; }

        [JsonProperty("ask", Required = Required.Always)]
        public string Ask { get; set; }

        [JsonProperty("last_price", Required = Required.Always)]
        public string LastPrice { get; set; }

        [JsonProperty("low", Required = Required.Always)]
        public string Low { get; set; }

        [JsonProperty("high", Required = Required.Always)]
        public string High { get; set; }

        [JsonProperty("volume", Required = Required.Always)]
        public string Volume { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        public DayRange Convert(TradingPair pair)
        {
            return new DayRange(
                Conversion.ToDecimalInvariant(High),
                Conversion.ToDecimalInvariant(Low),
                Timestamp.ToDateTimeUTC(),
                pair,
                ExchangeType.Bitfinex);
            
        }
    }
}