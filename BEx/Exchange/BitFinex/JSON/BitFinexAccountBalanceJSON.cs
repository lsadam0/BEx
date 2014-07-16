using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BEx.BitFinexSupport
{
    public class BitFinexAccountBalanceJSON
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        public static AccountBalance ConvertToStandard(List<BitFinexAccountBalanceJSON> balances, Currency baseCurrency, Currency counterCurrency)
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
        }
    }
}
