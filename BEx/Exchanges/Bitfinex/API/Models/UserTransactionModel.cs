// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.Bitfinex.API.Models
{
    internal class UserTransactionModel : IExchangeResponseIntermediate<UserTransaction>
    {
        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("fee_amount", Required = Required.Always)]
        public string FeeAmount { get; set; }

        [JsonProperty("fee_currency", Required = Required.Always)]
        public string FeeCurrency { get; set; }

        [JsonProperty("price", Required = Required.Always)]
        public string Price { get; set; }

        [JsonProperty("tid", Required = Required.Always)]
        public int Tid { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty("order_id", Required = Required.Always)]
        public int order_id { get; set; }

        public UserTransaction Convert(TradingPair pair)
        {
            var feeCurrency = Type == "Buy" ? pair.BaseCurrency : pair.CounterCurrency;

            if (order_id <= 0)
            {
                return default(UserTransaction);
            }
            else
            {
                return new UserTransaction(
                    Conversion.ToDecimalInvariant(Amount),
                    Conversion.ToDecimalInvariant(Price)
                    *Conversion.ToDecimalInvariant(Amount),
                    (long) Timestamp,
                    Conversion.ToDecimalInvariant(Price),
                    order_id.ToString(),
                    Conversion.ToDecimalInvariant(FeeAmount),
                    feeCurrency,
                    pair,
                    ExchangeType.Bitfinex,
                    (OrderType) Enum.Parse(typeof(OrderType), Type),
                    Tid
                    );
            }
        }
    }
}




