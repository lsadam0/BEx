using System;
using System.Collections.Generic;

namespace BEx
{
    public class PendingWithdrawals : APIResult
    {
        internal PendingWithdrawals(List<PendingWithdrawal> withdrawals, Currency baseCurrency, Currency counterCurrency, DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        {
            Withdrawals = withdrawals;
        }

        public List<PendingWithdrawal> Withdrawals
        {
            get;
            set;
        }
    }
}