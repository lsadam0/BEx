using Newtonsoft.Json;
using System;

namespace BEx.BitFinexSupport
{
    public class BitFinexAccountBalanceJSON : ExchangeResponse<AccountBalance>
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        public override AccountBalance ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            AccountBalance res = new AccountBalance();

            if (Type == "exchange")
            {
                Currency bCurrency;

                if (Enum.TryParse<Currency>(Currency.ToUpper(), out bCurrency))
                {
                    res.Available.Add(bCurrency, Convert.ToDecimal(Available));
                    res.Balance.Add(bCurrency, Convert.ToDecimal(Amount));
                }
            }

            return res;
        }

        /*
        public static AccountBalance ConvertListToStandard(List<BitFinexAccountBalanceJSON> balances, Currency baseCurrency, Currency counterCurrency)
        {
            AccountBalance res = new AccountBalance();

            foreach (BitFinexAccountBalanceJSON balance in balances)
            {
                if (balance.Type == "exchange")
                {
                    Currency bCurrency;

                    if (Enum.TryParse<Currency>(balance.Currency.ToUpper(), out bCurrency))
                    {
                        res.Available.Add(bCurrency, Convert.ToDecimal(balance.Available));
                        res.Balance.Add(bCurrency, Convert.ToDecimal(balance.Amount));
                    }
                }
            }

            return res;
        }*/
    }
}