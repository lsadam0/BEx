using System;

namespace BEx
{
    public class UserTransaction : APIResult
    {

        public int ID
        {
            get;
            set;
        }

        public UserTransactionType Type
        {
            get;
            set;
        }

        public Decimal BaseCurrencyAmount
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

        public int? OrderID
        {
            get;
            set;
        }

        public UserTransaction() : base()
        {

        }
    }


    public enum UserTransactionType
    {
        Deposit,
        Withdrawal,
        Trade
    }
}
