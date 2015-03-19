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
            btcBalance.BalanceCurrency = Currency.Btc;

            btcBalance.AvailableToTrade = Convert.ToDecimal(BtcAvailable, CultureInfo.InvariantCulture);
            btcBalance.TotalBalance = Convert.ToDecimal(BtcBalance, CultureInfo.InvariantCulture);

            Balance usdBalance = new Balance(DateTime.Now, ExchangeType.BitStamp);

            usdBalance.AvailableToTrade = Convert.ToDecimal(UsdAvailable, CultureInfo.InvariantCulture);
            usdBalance.TotalBalance = Convert.ToDecimal(UsdBalance, CultureInfo.InvariantCulture);
            usdBalance.BalanceCurrency = Currency.Usd;

            List<Balance> balances = new List<Balance>();
            balances.Add(btcBalance);
            balances.Add(usdBalance);

            res = new AccountBalance(balances, pair, ExchangeType.BitStamp);

            return res;
        }
    }
}