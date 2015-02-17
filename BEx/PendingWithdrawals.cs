using System.Collections.Generic;
using System;

namespace BEx
{
    public class PendingWithdrawals : APIResult
    {
        public List<PendingWithdrawal> Withdrawals
        {
            get;
            set;
        }



        internal PendingWithdrawals(List<PendingWithdrawal> withdrawals, Currency baseCurrency, Currency counterCurrency, DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        {
            Withdrawals = withdrawals;
        }
    }
}