using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BEx.BitStampSupport
{
    public class BitStampAccountBalanceJSON : ExchangeResponse<AccountBalances>
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

        public override AccountBalances ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            AccountBalances res;

            AccountBalance balance = new AccountBalance();

            balance.Available.Add(Currency.USD, Convert.ToDecimal(UsdAvailable));
            balance.Available.Add(Currency.BTC, Convert.ToDecimal(BtcAvailable));

            balance.Balance.Add(Currency.USD, Convert.ToDecimal(UsdBalance));
            balance.Balance.Add(Currency.BTC, Convert.ToDecimal(BtcBalance));

            res = new AccountBalances(new List<AccountBalance>() { balance }, baseCurrency, counterCurrency);

            return res;
        }

        /*
        public AccountBalance ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            AccountBalance res = new AccountBalance();

            res.Available.Add(Currency.USD, Convert.ToDecimal(UsdAvailable));
            res.Available.Add(Currency.BTC, Convert.ToDecimal(BtcAvailable));

            res.Balance.Add(Currency.USD, Convert.ToDecimal(UsdBalance));
            res.Balance.Add(Currency.BTC, Convert.ToDecimal(BtcBalance));

            return res;
        }*/
    }
}