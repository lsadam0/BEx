using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;
using System.Globalization;

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

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            OrderBook res = new OrderBook(UnixTime.UnixTimeStampToDateTime(Timestamp), ExchangeType.BitStamp);

            res.Pair = pair;

            for (int x = 0; x < Bids.Length; ++x)
            {
                string[] values = Bids[x];

                Decimal price = Conversion.ToDecimalInvariant(values[0]);
                Decimal amount = Conversion.ToDecimalInvariant(values[1]);

                res.BidsByPrice.Add(price, amount);
            }

            for (int x = 0; x < Asks.Length; ++x)
            {
                string[] values = Asks[x];

                Decimal price = Conversion.ToDecimalInvariant(values[0]);
                Decimal amount = Conversion.ToDecimalInvariant(values[1]);

                res.AsksByPrice.Add(price, amount);
            }

            return res;
        }
    }
}