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

        public PendingDeposits() : base()
        {
            Deposits = new List<PendingDeposit>();
        }
    }
}
