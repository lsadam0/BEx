using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitfinexTickJSON : IExchangeResponse
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

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            Tick res = new Tick(UnixTime.UnixTimeStampToDateTime(Timestamp), ExchangeType.Bitfinex);

            res.Pair = pair;
            res.Ask = Convert.ToDecimal(Ask, CultureInfo.InvariantCulture);
            res.Bid = Convert.ToDecimal(Bid, CultureInfo.InvariantCulture);
            res.High = Convert.ToDecimal(High, CultureInfo.InvariantCulture);
            res.Last = Convert.ToDecimal(LastPrice, CultureInfo.InvariantCulture);
            res.Low = Convert.ToDecimal(Low, CultureInfo.InvariantCulture);
            res.Volume = Convert.ToDecimal(Volume, CultureInfo.InvariantCulture);

            return res;
        }
    }
}