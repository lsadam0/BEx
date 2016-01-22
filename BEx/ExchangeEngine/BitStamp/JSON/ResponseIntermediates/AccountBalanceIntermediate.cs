﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStamp.JSON.ResponseIntermediates
{
    internal class AccountBalanceIntermediate : IExchangeResponse<AccountBalance>
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

        public AccountBalance Convert(TradingPair pair)
        {
            var btcBalance = new Balance(DateTime.UtcNow, ExchangeType.BitStamp)
            {
                BalanceCurrency = Currency.BTC,
                AvailableToTrade = Conversion.ToDecimalInvariant(BtcAvailable),
                TotalBalance = Conversion.ToDecimalInvariant(BtcBalance)
            };

            var usdBalance = new Balance(DateTime.UtcNow, ExchangeType.BitStamp)
            {
                AvailableToTrade = Conversion.ToDecimalInvariant(UsdAvailable),
                TotalBalance = Conversion.ToDecimalInvariant(UsdBalance),
                BalanceCurrency = Currency.USD
            };
            var balances = new List<Balance>
            {
                btcBalance,
                usdBalance
            };

            return new AccountBalance(balances, pair, ExchangeType.BitStamp);
        }
    }
}