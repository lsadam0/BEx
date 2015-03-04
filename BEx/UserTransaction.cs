using System;

namespace BEx
{
    public enum UserTransactionType
    {
        Deposit,
        Withdrawal,
        Trade
    }

    public class UserTransaction : APIResult
    {
        public UserTransaction(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        { }

        public Decimal BaseCurrencyAmount
        {
            get;
            set;
        }

        public DateTime CompletedTime
        {
            get;
            set;
        }

        public Decimal CounterCurrencyAmount
        {
            get;
            set;
        }

        public Decimal ExchangeRate
        {
            get;
            set;
        }

        public Decimal Fee
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public int? OrderID
        {
            get;
            set;
        }

        public UserTransactionType Type
        {
            get;
            set;
        }
    }
}