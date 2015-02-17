using System.Collections.Generic;
using System;

namespace BEx
{
    public class PendingDeposits : APIResult
    {
        public List<PendingDeposit> Deposits
        {
            get;
            set;
        }


        internal PendingDeposits(List<PendingDeposit> deposits, Currency baseCurrency, Currency counterCurrency)
            : base(DateTime.Now)
        {
            Deposits = deposits;
        }
    }
}