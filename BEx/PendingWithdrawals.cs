using System.Collections.Generic;

namespace BEx
{
    public class PendingWithdrawals : APIResult
    {
        public List<PendingWithdrawal> Withdrawals
        {
            get;
            set;
        }

        internal PendingWithdrawals()
            : base()
        {
            Withdrawals = new List<PendingWithdrawal>();
        }

        internal PendingWithdrawals(List<PendingWithdrawal> withdrawals, Currency baseCurrency, Currency counterCurrency)
            : base()
        {
            Withdrawals = withdrawals;
        }
    }
}