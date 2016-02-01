// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class UserTransactionIntermediate : IExchangeResponseIntermediate<UserTransaction>
    {
        [JsonProperty("amount", Required = Required.Always)]
        public decimal Amount { get; set; }

        [JsonProperty("exchange", Required = Required.Always)]
        public string Exchange { get; set; }

        [JsonProperty("fee_amount", Required = Required.Always)]
        public decimal FeeAmount { get; set; }

        [JsonProperty("fee_currency", Required = Required.Always)]
        public string FeeCurrency { get; set; }

        [JsonProperty("price", Required = Required.Always)]
        public decimal Price { get; set; }

        [JsonProperty("tid", Required = Required.Always)]
        public int Tid { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        public UserTransaction Convert(TradingPair pair)
        {
            var feeCurrency = Type == "Buy" ? pair.BaseCurrency : pair.CounterCurrency;

            return new UserTransaction(
                Amount,
                Price*Amount,
                (long) Timestamp,
                Price,
                Tid,
                FeeAmount,
                feeCurrency,
                pair,
                ExchangeType.Bitfinex,
                (OrderType) Enum.Parse(typeof (OrderType), Type)
                );
        }
    }
}