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

        public SortedDictionary<Decimal, Decimal> BidsByPrice
        {
            get;
            set;
        }

        public SortedDictionary<Decimal, Decimal> AsksByPrice
        {
            get;
            set;
        }

        internal OrderBook(BitStampSupport.BitstampOrderBookJSON source, Currency b, Currency c)
        {
            BaseCurrency = b;
            CounterCurrency = c;

            BidsByPrice = new SortedDictionary<decimal, decimal>();


            for (int x = 0; x < source.Bids.Length; ++x)
            {
                string[] values = source.Bids[x];

                Decimal price = Convert.ToDecimal(values[0]);
                Decimal amount = Convert.ToDecimal(values[1]);

                BidsByPrice.Add(price, amount);
                

            }

            AsksByPrice = new SortedDictionary<decimal, decimal>();
            

            for (int x = 0; x < source.Asks.Length; ++x)
            {
                string[] values = source.Asks[x];

                Decimal price = Convert.ToDecimal(values[0]);
                Decimal amount = Convert.ToDecimal(values[1]);

                AsksByPrice.Add(price, amount);
                

            }

        }

        internal OrderBook(BitFinexSupport.BitFinexOrderBookJSON source, Currency b, Currency c)
        {

            BaseCurrency = b;
            CounterCurrency = c;

            BidsByPrice = new SortedDictionary<decimal, decimal>();
            AsksByPrice = new SortedDictionary<decimal, decimal>();


            for (int x = 0; x < source.Bids.Length; ++x)
            {

                //source.Bids[x].
                BidsByPrice.Add(Convert.ToDecimal(source.Bids[x].Price), Convert.ToDecimal(source.Bids[x].Amount));
                

            }

            for (int x = 0; x < source.Asks.Length; ++x)
            {
                AsksByPrice.Add(Convert.ToDecimal(source.Asks[x].Price), Convert.ToDecimal(source.Asks[x].Amount));
                
            }
        }
    }
}
