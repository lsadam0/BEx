using System;
using Newtonsoft.Json;


namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitstampTransactionJSON : IExchangeResponse
    {
        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("tid")]
        public int tid { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("amount")]
        public string amount { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            return new Transaction(DateTime.Now, ExchangeType.BitStamp)
            {
                Amount = Conversion.ToDecimalInvariant(amount),
                Price = Conversion.ToDecimalInvariant(price),
                TransactionId = tid,
                Pair = pair,
                CompletedTime = UnixTime.UnixTimeStampToDateTime(Conversion.ToDoubleInvariant(date))
            };
        }
    }
}