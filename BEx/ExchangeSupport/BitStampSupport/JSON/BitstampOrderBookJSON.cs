using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitstampOrderBookJSON : IExchangeResponse
    {
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("bids")]
        public string[][] Bids { get; set; }

        [JsonProperty("asks")]
        public string[][] Asks { get; set; }

        public APIResult ConvertToStandard(CurrencyTradingPair pair)
        {
            OrderBook res = new OrderBook(UnixTime.UnixTimeStampToDateTime(Timestamp), ExchangeType.BitStamp);

            res.Pair = pair;

            for (int x = 0; x < Bids.Length; ++x)
            {
                string[] values = Bids[x];

                Decimal price = Convert.ToDecimal(values[0]);
                Decimal amount = Convert.ToDecimal(values[1]);

                res.BidsByPrice.Add(price, amount);
            }

            for (int x = 0; x < Asks.Length; ++x)
            {
                string[] values = Asks[x];

                Decimal price = Convert.ToDecimal(values[0]);
                Decimal amount = Convert.ToDecimal(values[1]);

                res.AsksByPrice.Add(price, amount);
            }

            return res;
        }
    }
}