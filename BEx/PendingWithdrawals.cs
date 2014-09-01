using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BEx.Common;

namespace BEx
{
    public class PendingWithdrawals : APIResult
    {
        public List<PendingWithdrawal> Withdrawals
        {
            get;
            set;
        }

        public PendingWithdrawals() : base()
        {
            Withdrawals = new List<PendingWithdrawal>();
        }

    }
}
