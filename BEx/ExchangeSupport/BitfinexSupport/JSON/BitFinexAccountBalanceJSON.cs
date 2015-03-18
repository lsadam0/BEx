using Newtonsoft.Json;
using System;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitFinexAccountBalanceJSON : IExchangeResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        public APIResult ConvertToStandard(CurrencyTradingPair pair)
        {
            Balance res = null;

            if (Type == "exchange")
            {
                res = new Balance(DateTime.Now, ExchangeType.BitFinex);
                Currency bCurrency;

                if (Enum.TryParse<Currency>(Currency.ToUpper(), out bCurrency))
                {
                    res.BalanceCurrency = bCurrency;
                    res.AvailableToTrade = Convert.ToDecimal(Available);
                    res.TotalBalance = Convert.ToDecimal(Amount);
                }
            }

            return res;
        }
    }
}