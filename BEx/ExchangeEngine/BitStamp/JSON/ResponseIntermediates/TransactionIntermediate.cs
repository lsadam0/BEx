// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStamp.JSON.ResponseIntermediates
{
    internal class TransactionIntermediate : IExchangeResponse<Transaction>
    {
        [JsonProperty("date", Required = Required.Always)]
        public long date { get; set; }

        [JsonProperty("tid", Required = Required.Always)]
        public int tid { get; set; }

        [JsonProperty("price", Required = Required.Always)]
        public string price { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public string amount { get; set; }

        public Transaction Convert(TradingPair pair)
        {
            return new Transaction(
                amount,
                pair,
                date,
                tid,
                price,
                ExchangeType.BitStamp);
        }
    }
}