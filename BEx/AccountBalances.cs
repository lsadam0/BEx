using System.Collections.Generic;

namespace BEx
{
    public class AccountBalances : APIResult
    {
        public List<AccountBalance> Balances;

        internal AccountBalances()
            : base()
        {
        }

        internal AccountBalances(List<AccountBalance> balances)
            : base()
        {
            Balances = balances;
        }
    }
}