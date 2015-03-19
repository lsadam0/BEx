using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampAccountBalanceJSON : IExchangeResponse
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

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            AccountBalance res;

            Balance btcBalance = new Balance(DateTime.Now, ExchangeType.BitStamp);
            btcBalance.BalanceCurrency = Currency.BTC;

            btcBalance.AvailableToTrade = Conversion.ToDecimalInvariant(BtcAvailable);
            btcBalance.TotalBalance = Conversion.ToDecimalInvariant(BtcBalance);

            Balance usdBalance = new Balance(DateTime.Now, ExchangeType.BitStamp);

            usdBalance.AvailableToTrade = Conversion.ToDecimalInvariant(UsdAvailable);
            usdBalance.TotalBalance = Conversion.ToDecimalInvariant(UsdBalance);
            usdBalance.BalanceCurrency = Currency.USD;

            List<Balance> balances = new List<Balance>();
            balances.Add(btcBalance);
            balances.Add(usdBalance);

            res = new AccountBalance(balances, pair, ExchangeType.BitStamp);

            return res;
        }
    }
}