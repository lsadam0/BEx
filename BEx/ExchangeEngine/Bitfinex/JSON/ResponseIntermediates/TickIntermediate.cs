// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON
{
    internal class TickIntermediate : IExchangeResponse<Tick>
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
        public string Timestamp { get; set; }

        public Tick Convert(TradingPair pair)
        {
            return new Tick(Timestamp.ToDateTimeUTC(), ExchangeType.Bitfinex)
            {
                Pair = pair,
                Ask = Conversion.ToDecimalInvariant(Ask),
                Bid = Conversion.ToDecimalInvariant(Bid),
                High = Conversion.ToDecimalInvariant(High),
                Last = Conversion.ToDecimalInvariant(LastPrice),
                Low = Conversion.ToDecimalInvariant(Low),
                Volume = Conversion.ToDecimalInvariant(Volume)
            };
        }
    }
}