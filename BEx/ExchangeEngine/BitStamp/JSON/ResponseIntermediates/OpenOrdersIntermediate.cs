﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStamp.JSON
{
    internal class OpenOrdersIntermediate : IExchangeResponse
    {
        [JsonProperty("price", Required = Required.Always)]
        public string Price { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public int Type { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("datetime", Required = Required.Always)]
        public string Datetime { get; set; }

        public BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            return new Order(Conversion.ToDateTimeInvariant(Datetime), sourceExchange)
            {
                Amount = Conversion.ToDecimalInvariant(Amount),
                Price = Conversion.ToDecimalInvariant(Price),
                TradeType = Type == 0 ? OrderType.Buy : OrderType.Sell,
                Id = Id,
                Pair = pair
            };
        }
    }
}