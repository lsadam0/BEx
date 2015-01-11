using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BEx.BitFinexSupport
{
    public class BitfinexTickJSON :ExchangeResponse
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

        public override APIResult ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = new Tick();


            res.Ask = Convert.ToDecimal(Ask);
            res.BaseCurrency = baseCurrency;
            res.Bid = Convert.ToDecimal(Bid);
            res.CounterCurrency = counterCurrency;
            res.High = Convert.ToDecimal(High);
            res.Last = Convert.ToDecimal(LastPrice);
            res.Low = Convert.ToDecimal(Low);
            res.Volume = Convert.ToDecimal(Volume);

            return res;
        }
        /*
        public Tick ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = new Tick();


            res.Ask = Convert.ToDecimal(Ask);
            res.BaseCurrency = baseCurrency;
            res.Bid = Convert.ToDecimal(Bid);
            res.CounterCurrency = counterCurrency;
            res.High = Convert.ToDecimal(High);
            res.Last = Convert.ToDecimal(LastPrice);
            res.Low = Convert.ToDecimal(Low);
            res.Volume = Convert.ToDecimal(Volume);

            return res;
        }*/
    }
}
