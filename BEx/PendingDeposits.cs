using System.Collections.Generic;

namespace BEx
{
    public class PendingDeposits : APIResult
    {
        public List<PendingDeposit> Deposits
        {
            get;
            set;
        }

        internal PendingDeposits()
            : base()
        {
            Deposits = new List<PendingDeposit>();
        }

        internal PendingDeposits(List<PendingDeposit> deposits, Currency baseCurrency, Currency counterCurrency)
            : base()
        {
            Deposits = deposits;
        }
    }
}