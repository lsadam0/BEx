﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BEx.BitStampSupport
{
    public class BitStampAccountBalanceJSON : ExchangeResponse<AccountBalance>
    {
        [JsonProperty("btc_reserved")]
        public string BtcReserved { get; set; }

        [JsonProperty("fee")]
        public string Fee { get; set; }

        [JsonProperty("btc_available")]
        public string BtcAvailable { get; set; }

        [JsonProperty("usd_reserved")]
        public string UsdReserved { get; set; }

        [JsonProperty("btc_balance")]
        public string BtcBalance { get; set; }

        [JsonProperty("usd_balance")]
        public string UsdBalance { get; set; }

        [JsonProperty("usd_available")]
        public string UsdAvailable { get; set; }

        public override AccountBalance ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            AccountBalance res;

            Balance btcBalance = new Balance(DateTime.Now);
            btcBalance.BalanceCurrency = Currency.BTC;

            btcBalance.AvailableToTrade = Convert.ToDecimal(BtcAvailable);
            btcBalance.TotalBalance = Convert.ToDecimal(BtcBalance);

            Balance usdBalance = new Balance(DateTime.Now);

            usdBalance.AvailableToTrade = Convert.ToDecimal(UsdAvailable);
            usdBalance.TotalBalance = Convert.ToDecimal(UsdBalance);
            usdBalance.BalanceCurrency = Currency.USD;

            res = new AccountBalance(new List<Balance>() { btcBalance, usdBalance }, baseCurrency, counterCurrency);

            return res;
        }
    }
}