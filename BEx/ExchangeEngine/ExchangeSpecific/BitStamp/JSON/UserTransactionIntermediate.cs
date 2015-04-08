// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.BitStamp.JSON
{
    internal class UserTransactionIntermediate : IExchangeResponse
    {
        [JsonProperty("usd", Required = Required.Always)]
        public string Usd { get; set; }

        [JsonProperty("btc", Required = Required.Always)]
        public string Btc { get; set; }

        [JsonProperty("btc_usd", Required = Required.Always)]
        public string BtcUsd { get; set; }

        [JsonProperty("order_id", Required = Required.Default)]
        public int? OrderId { get; set; }

        [JsonProperty("fee", Required = Required.Always)]
        public string Fee { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public int Type { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("datetime", Required = Required.Always)]
        public string Datetime { get; set; }

        public BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            if (OrderId != null && Type == 2)
            {
                return new UserTransaction(Conversion.ToDateTimeInvariant(Datetime), sourceExchange)
                {
                    ExchangeRate =  Conversion.ToDecimalInvariant(BtcUsd),
                    BaseCurrencyAmount = Conversion.ToDecimalInvariant(Btc),
                    CounterCurrencyAmount = Conversion.ToDecimalInvariant(Usd),
                    Pair = pair,
                    OrderId = (int)OrderId,
                    TradeFee = Conversion.ToDecimalInvariant(Fee),
                    TradeFeeCurrency = Currency.USD,
                    CompletedTime = Conversion.ToDateTimeInvariant(Datetime),
                    TransactionType = (Conversion.ToDecimalInvariant(Btc) < 0) ? OrderType.Sell : OrderType.Buy
                    
                };
            }
            else
                return null;
        }
    }
}