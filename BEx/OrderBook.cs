using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEx
{
    public class OrderBook
    {
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

        public SortedDictionary<Decimal, Decimal> Bids
        {
            get;
            set;
        }

        public SortedDictionary<Decimal, Decimal> Asks
        {
            get;
            set;
        }

        internal OrderBook(BitstampOrderBookJSON source, Currency b, Currency c)
        {
            BaseCurrency = b;
            CounterCurrency = c;

            Bids = new SortedDictionary<decimal, decimal>();


            for (int x = 0; x < source.Bids.Length; ++x)
            {
                string[] values = source.Bids[x];

                Decimal price = Convert.ToDecimal(values[0]);
                Decimal amount = Convert.ToDecimal(values[1]);

                Bids.Add(price, amount);

            }

            Asks = new SortedDictionary<decimal, decimal>();

            for (int x = 0; x < source.Asks.Length; ++x)
            {
                string[] values = source.Asks[x];

                Decimal price = Convert.ToDecimal(values[0]);
                Decimal amount = Convert.ToDecimal(values[1]);

                Asks.Add(price, amount);

            }

        }
    }
}
