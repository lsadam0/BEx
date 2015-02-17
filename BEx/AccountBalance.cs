using System.Collections.Generic;
using System;

namespace BEx
{
    public class AccountBalance : APIResult
    {
        public Dictionary<Currency, decimal> Available
        {
            get;
            set;
        }

        public Dictionary<Currency, decimal> Balance
        {
            get;
            set;
        }

        internal AccountBalance(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        {
            Balance = new Dictionary<Currency, decimal>();
            Available = new Dictionary<Currency, decimal>();
        }
    }
}