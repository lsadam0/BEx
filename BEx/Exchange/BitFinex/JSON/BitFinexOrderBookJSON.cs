using Newtonsoft.Json;
using System;

namespace BEx.BitFinexSupport
{
    public class Bid
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    public class Ask
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    public class BitFinexOrderBookJSON : ExchangeResponse<OrderBook>
    {
        [JsonProperty("bids")]
        public Bid[] Bids { get; set; }

        [JsonProperty("asks")]
        public Ask[] Asks { get; set; }

        public override OrderBook ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res = new OrderBook(DateTime.Now);

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;
            decimal key;
            decimal value;

            for (int x = 0; x < Bids.Length; ++x)
            {

                decimal.TryParse(Bids[x].Price, out key);
                decimal.TryParse(Bids[x].Amount, out value);

                res.BidsByPrice.Add(key, value);
            }

            for (int x = 0; x < Asks.Length; ++x)
            {
                decimal.TryParse(Asks[x].Price, out key);
                decimal.TryParse(Asks[x].Amount, out value);


                res.AsksByPrice.Add(key, value);
            }

           // res.TimeStamp = DateTime.Now;

            return res;
        }

    
    }
}