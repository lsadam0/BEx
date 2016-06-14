// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.BitStamp.API.Models
{
    internal class UserTransactionModel : IExchangeResponseIntermediate<UserTransaction>
    {
        [JsonProperty("btc", Required = Required.Always)]
        public string Btc { get; set; }

        [JsonProperty("btc_usd", Required = Required.Always)]
        public string BtcUsd { get; set; }

        [JsonProperty("datetime", Required = Required.Always)]
        public DateTime Datetime { get; set; }

        [JsonProperty("fee", Required = Required.Always)]
        public string Fee { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("order_id", Required = Required.Default)]
        public int? OrderId { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public int Type { get; set; }

        [JsonProperty("usd", Required = Required.Always)]
        public string Usd { get; set; }

        public UserTransaction Convert(TradingPair pair)
        {
            if (OrderId != null && Type == 2)
            {
                // Datetime is already UTC
                Datetime = new DateTime(Datetime.Ticks, DateTimeKind.Utc);
                return new UserTransaction(
                    Conversion.ToDecimalInvariant(Btc),
                    Conversion.ToDecimalInvariant(Usd),
                    (long) Datetime.ToUnixTime(),
                    Conversion.ToDecimalInvariant(BtcUsd),
                    (int) OrderId,
                    Conversion.ToDecimalInvariant(Fee),
                    Currency.USD,
                    pair,
                    ExchangeType.BitStamp,
                    Conversion.ToDecimalInvariant(Btc) < 0 ? OrderType.Sell : OrderType.Buy);
            }
            return default(UserTransaction);
        }
    }
}