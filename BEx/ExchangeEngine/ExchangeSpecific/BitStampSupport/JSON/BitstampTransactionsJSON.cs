// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;


namespace BEx.ExchangeEngine.BitStampSupport
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

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            return new Transaction(DateTime.UtcNow, sourceExchange)
            {
                Amount = Conversion.ToDecimalInvariant(amount),
                Price = Conversion.ToDecimalInvariant(price),
                TransactionId = tid,
                Pair = pair,
                CompletedTime = date.ToDateTimeUTC()
            };
        }
    }
}