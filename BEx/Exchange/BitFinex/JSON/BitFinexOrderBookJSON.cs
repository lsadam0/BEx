using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

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

    public class BitFinexOrderBookJSON
    {

        [JsonProperty("bids")]
        public Bid[] Bids { get; set; }

        [JsonProperty("asks")]
        public Ask[] Asks { get; set; }


        public OrderBook ToOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res = new OrderBook();

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;

            
            
            for (int x = 0; x < Bids.Length; ++x)
            {

                //source.Bids[x].
                res.BidsByPrice.Add(Convert.ToDecimal(Bids[x].Price), Convert.ToDecimal(Bids[x].Amount));


            }

            for (int x = 0; x < Asks.Length; ++x)
            {
                res.AsksByPrice.Add(Convert.ToDecimal(Asks[x].Price), Convert.ToDecimal(Asks[x].Amount));

            }

            res.TimeStamp = DateTime.Now;

            return res;
        }
    }
}
