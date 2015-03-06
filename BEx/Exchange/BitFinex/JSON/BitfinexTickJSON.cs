using BEx.Common;
using Newtonsoft.Json;
using System;

namespace BEx.BitFinexSupport
{
    public class BitfinexTickJSON : ExchangeResponse<Tick>
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

        public override Tick ConvertToStandard(CurrencyTradingPair pair)
        {
            Tick res = new Tick(UnixTime.UnixTimeStampToDateTime(Timestamp), ExchangeType.BitFinex);

            res.Pair = pair;
            res.Ask = Convert.ToDecimal(Ask);
            res.Bid = Convert.ToDecimal(Bid);
            res.High = Convert.ToDecimal(High);
            res.Last = Convert.ToDecimal(LastPrice);
            res.Low = Convert.ToDecimal(Low);
            res.Volume = Convert.ToDecimal(Volume);

            return res;
        }
    }
}