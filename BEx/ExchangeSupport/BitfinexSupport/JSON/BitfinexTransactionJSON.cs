using System;
using Newtonsoft.Json;


namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitFinexTransactionJSON : IExchangeResponse
    {
        [JsonProperty("timestamp")]
        public int timestamp { get; set; }

        [JsonProperty("tid")]
        public int tid { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("amount")]
        public string amount { get; set; }

        [JsonProperty("exchange")]
        public string exchange { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            return new Transaction(DateTime.Now, ExchangeType.Bitfinex)
            {
                Amount = Conversion.ToDecimalInvariant(amount),
                Price = Conversion.ToDecimalInvariant(price),
                TransactionId = tid,
                CompletedTime = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(timestamp)),
                Pair = pair,
            };
        }
    }
}