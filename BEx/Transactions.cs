using System.Collections.Generic;
using System.Linq;
using System;

namespace BEx
{
    public class Transactions : APIResult
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


        public List<Transaction> TransactionsCollection
        {
            get;
            set;
        }

        public override string ToString()
        {
            string output = "{0}/{1} - Transactions: {2} - Oldest: {3} - Newest: {4}";

            DateTime oldest = TransactionsCollection.Min(x => x.TimeStamp);
            DateTime newest = TransactionsCollection.Max(x => x.TimeStamp);

            return string.Format(output, BaseCurrency, CounterCurrency, TransactionsCollection.Count, oldest.ToString(), newest.ToString());

        }

        internal Transactions()
            : base()
        {
            TransactionsCollection = new List<Transaction>();
           
        }

        internal Transactions(List<Transaction> transactions, Currency baseCurrency, Currency counterCurrency)
            : base()
        {
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            TransactionsCollection = transactions;
        }
    }
}