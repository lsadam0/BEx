using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class PendingDeposits : APIResult
    {
        public List<PendingDeposit> Deposits
        {
            get;
            set;
        }

        internal PendingDeposits() : base()
        {
            Deposits = new List<PendingDeposit>();
        }

        internal PendingDeposits(List<PendingDeposit> deposits)
            : base()
        {
            Deposits = deposits;

        }


    }
}
