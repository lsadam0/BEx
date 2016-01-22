// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class TransactionIntermediate : IExchangeResponse<Transaction>
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

        public Transaction Convert(TradingPair pair)
        {
            return new Transaction(DateTime.UtcNow, ExchangeType.Bitfinex)
            {
                Amount = Conversion.ToDecimalInvariant(amount),
                Price = Conversion.ToDecimalInvariant(price),
                TransactionId = tid,
                CompletedTime = System.Convert.ToDouble(timestamp).ToDateTimeUTC(),
                Pair = pair
            };
        }
    }
}