using System;

namespace BEx
{
    public class PendingDeposit : APIResult
    {
        internal PendingDeposit(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        { }

        public string Address
        {
            get;
            set;
        }

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

        public Currency DepositedCurrency
        {
            get;
            set;
        }
    }
}