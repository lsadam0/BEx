using System;
using Newtonsoft.Json;


namespace BEx.ExchangeEngine.BitfinexSupport
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

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            OrderBook res = new OrderBook(DateTime.Now, ExchangeType.Bitfinex)
            {
                Pair = pair
            };

            decimal key;
            decimal value;

            foreach (Bid bid in Bids)
            {
                if (
                decimal.TryParse(bid.Price, out key)
                    &&
                decimal.TryParse(bid.Amount, out value))
                    res.BidsByPrice.Add(key, value);
            }

            foreach (Ask ask in Asks)
            {
                if (decimal.TryParse(ask.Price, out key)
                    &&
                decimal.TryParse(ask.Amount, out value))
                    res.AsksByPrice.Add(key, value);
            }

            return res;
        }
    }
}