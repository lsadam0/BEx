// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.BitStampSupport
{
    internal class BitStampAccountBalanceJSON : IExchangeResponse
    {
        [JsonProperty("btc_reserved", Required = Required.Always)]
        public string BtcReserved { get; set; }

        [JsonProperty("fee", Required = Required.Always)]
        public string Fee { get; set; }

        [JsonProperty("btc_available", Required = Required.Always)]
        public string BtcAvailable { get; set; }

        [JsonProperty("usd_reserved", Required = Required.Always)]
        public string UsdReserved { get; set; }

        [JsonProperty("btc_balance", Required = Required.Always)]
        public string BtcBalance { get; set; }

        [JsonProperty("usd_balance", Required = Required.Always)]
        public string UsdBalance { get; set; }

        [JsonProperty("usd_available", Required = Required.Always)]
        public string UsdAvailable { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            Balance btcBalance = new Balance(DateTime.UtcNow, sourceExchange)
            {
                BalanceCurrency = Currency.BTC,
                AvailableToTrade = Conversion.ToDecimalInvariant(BtcAvailable),
                TotalBalance = Conversion.ToDecimalInvariant(BtcBalance)
            };

            Balance usdBalance = new Balance(DateTime.UtcNow, sourceExchange)
            {
                AvailableToTrade = Conversion.ToDecimalInvariant(UsdAvailable),
                TotalBalance = Conversion.ToDecimalInvariant(UsdBalance),
                BalanceCurrency = Currency.USD
            };
            List<Balance> balances = new List<Balance>()
            {
                btcBalance,
                usdBalance
            };

            return new AccountBalance(balances, pair, sourceExchange);
        }
    }
}