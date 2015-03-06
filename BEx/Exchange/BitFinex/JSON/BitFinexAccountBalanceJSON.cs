using Newtonsoft.Json;
using System;

namespace BEx.BitFinexSupport
{
    internal class BitFinexAccountBalanceJSON : ExchangeResponse<Balance>
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        public override Balance ConvertToStandard(CurrencyTradingPair pair)
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