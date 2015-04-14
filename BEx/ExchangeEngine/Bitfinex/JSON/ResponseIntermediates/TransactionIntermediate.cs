// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;


namespace BEx.ExchangeEngine.Bitfinex.JSON
{
    internal class TransactionIntermediate : IExchangeResponse
    {
        [JsonProperty("timestamp", Required = Required.Always)]
        public long timestamp { get; set; }

        [JsonProperty("tid", Required = Required.Always)]
        public int tid { get; set; }

        [JsonProperty("price", Required = Required.Always)]
        public string price { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public string amount { get; set; }

        [JsonProperty("exchange", Required = Required.Always)]
        public string exchange { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string type { get; set; }

        public BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourcExchange)
        {

            return new Transaction(DateTime.UtcNow, sourcExchange)
            {
                Amount = Conversion.ToDecimalInvariant(amount),
                Price = Conversion.ToDecimalInvariant(price),
                TransactionId = tid,
                CompletedTime = Convert.ToDouble(timestamp).ToDateTimeUTC(),
                Pair = pair,
            };
        }
    }
}