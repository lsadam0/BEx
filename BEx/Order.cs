using System;

namespace BEx
{
    public class Order : APIResult
    {
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

        public OrderType Type
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public Currency BaseCurrency
        {
            get;
            set;
        }

        public Currency CounterCurrency
        {
            get;
            set;
        }

        public Order()
            : base()
        {
            ID = -1;
        }
    }

    public enum OrderType
    {
        Buy,
        Sell
    }
}