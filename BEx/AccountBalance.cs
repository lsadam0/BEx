using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class AccountBalance : APIResult
    {

        public Dictionary<string, string> Balances
        {
            get;
            set;
        }

        internal AccountBalance() : base()
        {
            

        }
    }
}
