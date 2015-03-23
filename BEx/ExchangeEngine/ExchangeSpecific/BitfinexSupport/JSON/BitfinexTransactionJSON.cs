// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;


namespace BEx.ExchangeEngine.BitfinexSupport
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

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourcExchange)
        {
            return new Transaction(DateTime.Now, sourcExchange)
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