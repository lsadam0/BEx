using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class AccountBalances : APIResult
    {
        public List<AccountBalance> Balances;

        internal AccountBalances() : base()
        {

        }

        internal AccountBalances(List<AccountBalance> balances)
            : base()
        {
            Balances = balances;
        }
    }
}
