using System;

namespace BEx
{
    public class Transaction : APIResult
    {
        public DateTime CompletedTime
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

        internal Transaction(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        { }
    }
}