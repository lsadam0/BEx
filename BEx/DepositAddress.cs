using System;

namespace BEx
{
    public class DepositAddress : APIResult
    {
        public string Address
        {
            get;
            set;
        }

        public Currency DepositCurrency
        {
            get;
            set;
        }

        internal DepositAddress(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        { }

        internal DepositAddress(string address, DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        {
            Address = address;
        }
    }
}