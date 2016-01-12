// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;
using System;

namespace BEx.UnitTests.MockTests.MockObjects.MockJSONIntermediates
{
    internal class MockTransactionJSON : IExchangeResponse<Transaction>
    {
        [JsonProperty("timestamp")]
        public long timestamp { get; set; }

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

        public Transaction Convert(CurrencyTradingPair pair)
        {
            return new Transaction(DateTime.UtcNow, ExchangeType.Mock)
            {
                Amount = Conversion.ToDecimalInvariant(amount),
                Price = Conversion.ToDecimalInvariant(price),
                TransactionId = tid,
                CompletedTime = System.Convert.ToDouble(timestamp).ToDateTimeUTC(),
                Pair = pair,
            };
        }
    }
}