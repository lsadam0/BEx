using System;

namespace BEx
{
    public class Transaction : APIResult
    {
        internal Transaction(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
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