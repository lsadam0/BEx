using System.Collections.Generic;
using System;

namespace BEx
{
    public class AccountBalances : APIResult
    {
        public List<AccountBalance> Balances;

   
        internal AccountBalances(List<AccountBalance> balances, Currency baseCurrency, Currency counterCurrency)
            : base(DateTime.Now)
        {
            Balances = balances;
        }
    }
}