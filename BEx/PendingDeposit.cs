using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class PendingDeposit : APIResult
    {
        public Decimal Amount
        {
            get;
            set;
        }

        public int Confirmations
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public Currency DepositedCurrency
        {
            get;
            set;
        }

        internal PendingDeposit() : base()
        {

        }
    }


}
