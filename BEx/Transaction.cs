using System;

namespace BEx
{
    public class Transaction : APIResult
    {
        internal Transaction(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        { }

        public Decimal Amount
        {
            get;
            set;
        }

        public DateTime CompletedTime
        {
            get;
            set;
        }

        public Decimal Price
        {
            get;
            set;
        }

        public long TransactionID
        {
            get;
            set;
        }
    }
}