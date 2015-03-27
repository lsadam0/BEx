// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;
using BEx.ExchangeEngine;

namespace BEx.UnitTests.MockTests.MockObjects.MockJSONIntermediates
{
    internal class MockTickJSON : IExchangeResponse
    {
        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("bid")]
        public string Bid { get; set; }

        [JsonProperty("ask")]
        public string Ask { get; set; }

        [JsonProperty("last_price")]
        public string LastPrice { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            return new Tick(Timestamp.ToDateTimeUTC(), sourceExchange)
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