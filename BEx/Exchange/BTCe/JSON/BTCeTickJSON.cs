using Newtonsoft.Json;
using System;

namespace BEx.BTCeSupport
{

    public class Ticker
    {

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("avg")]
        public double Avg { get; set; }

        [JsonProperty("vol")]
        public double Vol { get; set; }

        [JsonProperty("vol_cur")]
        public double VolCur { get; set; }

        [JsonProperty("last")]
        public double Last { get; set; }

        [JsonProperty("buy")]
        public double Buy { get; set; }

        [JsonProperty("sell")]
        public double Sell { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("server_time")]
        public long ServerTime { get; set; }
    }

    public class BTCeTickJSON : ExchangeResponse<Tick>
    {

        [JsonProperty("ticker")]
        public Ticker Ticker { get; set; }

        public BTCeTickJSON()
        {
            Ticker = new Ticker();

        }

        public override Tick ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = new Tick();

            res.Bid = Convert.ToDecimal(Ticker.Sell);
            res.Ask = Convert.ToDecimal(Ticker.Buy);
            res.High = Convert.ToDecimal(Ticker.High);
            res.Last = Convert.ToDecimal(Ticker.Last);
            res.Low = Convert.ToDecimal(Ticker.Low);
            res.Volume = Convert.ToDecimal(Ticker.Vol);

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;


            return res;
        }

        /*
        public Tick ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = new Tick();

            res.Bid = Convert.ToDecimal(Ticker.Sell);
            res.Ask = Convert.ToDecimal(Ticker.Buy);
            res.High = Convert.ToDecimal(Ticker.High);
            res.Last = Convert.ToDecimal(Ticker.Last);
            res.Low = Convert.ToDecimal(Ticker.Low);
            res.Volume = Convert.ToDecimal(Ticker.Vol);

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;

            
            return res;
        }*/
    }

}
