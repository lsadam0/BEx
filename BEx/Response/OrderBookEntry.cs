using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class OrderBookEntry
    {

        internal OrderBookEntry(decimal amount, decimal price)
        {
            Amount = amount;
            Price = price;
        }

        public decimal Amount
        {
            get; 
            private set; 
        }

        public decimal Price
        {
            get; private set; }
    }
}
