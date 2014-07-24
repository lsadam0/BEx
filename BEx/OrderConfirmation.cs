using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class OrderConfirmation : APIResult
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
        
        public OrderConfirmation() : base()
        {

        }
    }


    public enum OrderType
    {
        Buy,
        Sell
    }
}
