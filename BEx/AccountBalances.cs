using System;
using System.Collections.Generic;

namespace BEx
{
    /// <summary>
    /// A Complete list of all Available balances.  Values do not include amounts reserved in open orders.
    /// </summary>
    public class AccountBalances : APIResult
    {
        // public List<AccountBalance> Balances;


        public Dictionary<Currency, Decimal> Balances = new Dictionary<Currency, decimal>();

        internal AccountBalances(List<AccountBalance> balances, Currency baseCurrency, Currency counterCurrency)
            : base(DateTime.Now)
        {
            //Balances = balances;

            foreach (AccountBalance balance in balances)
            {
                foreach (KeyValuePair<Currency, Decimal> b in balance.Available)
                {
                    Balances.Add(b.Key, b.Value);
                }
            }
        }
    }
}