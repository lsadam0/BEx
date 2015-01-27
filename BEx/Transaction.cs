using System;

namespace BEx
{
    public class Transaction : APIResult
    {
        public DateTime TimeStamp
        {
            get;
            set;
        }

        public long TransactionID
        {
            get;
            set;
        }

        public Decimal Price
        {
            get;
            set;
        }

        public Decimal Amount
        {
            get;
            set;
        }

        internal Transaction()
            : base()
        {
        }
    }
}