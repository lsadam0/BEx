using Newtonsoft.Json;
using System;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class Bid
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    internal class Ask
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    internal class BitFinexOrderBookJSON : IExchangeResponse
    {
        [JsonProperty("bids")]
        public Bid[] Bids { get; set; }

        [JsonProperty("asks")]
        public Ask[] Asks { get; set; }

        public APIResult ConvertToStandard(CurrencyTradingPair pair)
        {
            OrderBook res = new OrderBook(DateTime.Now, ExchangeType.BitFinex);

            res.Pair = pair;
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