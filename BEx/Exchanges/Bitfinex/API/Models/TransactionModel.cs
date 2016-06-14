// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using Newtonsoft.Json;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.Bitfinex.API.Models
{
    internal class TransactionModel : IExchangeResponseIntermediate<Transaction>
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
            return new Transaction(
                amount,
                pair,
                timestamp,
                tid,
                price,
                ExchangeType.Bitfinex);
        }
    }
}