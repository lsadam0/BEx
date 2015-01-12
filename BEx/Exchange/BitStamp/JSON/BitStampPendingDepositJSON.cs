using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using BEx.Common;

namespace BEx.BitStampSupport
{
    public class BitStampPendingDepositJSON : ExchangeResponse<PendingDeposit>
    {

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }


        public override PendingDeposit ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            PendingDeposit res = new PendingDeposit();

            res.Amount = Convert.ToDecimal(Amount);
            res.Confirmations = Confirmations;
            res.Address = Address;
            res.DepositedCurrency = Currency.BTC;

            return res;
        }
        /*
        public PendingDeposit ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            PendingDeposit res = new PendingDeposit();

            res.Amount = Convert.ToDecimal(Amount);
            res.Confirmations = Confirmations;
            res.Address = Address;
            res.DepositedCurrency = Currency.BTC;

            return res;
        }

        public static PendingDeposits ConvertListToStandard(List<BitStampPendingDepositJSON> deposits, Currency baseCurrency, Currency counterCurrency)
        {
            PendingDeposits res = new PendingDeposits();

            foreach (BitStampPendingDepositJSON source in deposits)
            {
                res.Deposits.Add(source.ConvertToStandard(baseCurrency, counterCurrency));
            }

            return res;
        }*/
    }

}
