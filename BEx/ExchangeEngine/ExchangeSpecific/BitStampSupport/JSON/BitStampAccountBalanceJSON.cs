using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.BitStampSupport
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
            Balance btcBalance = new Balance(DateTime.Now, ExchangeType.BitStamp)
            {
                BalanceCurrency = Currency.BTC,
                AvailableToTrade = Conversion.ToDecimalInvariant(BtcAvailable),
                TotalBalance = Conversion.ToDecimalInvariant(BtcBalance)
            };

            Balance usdBalance = new Balance(DateTime.Now, ExchangeType.BitStamp)
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

            return new AccountBalance(balances, pair, ExchangeType.BitStamp);
        }
    }
}