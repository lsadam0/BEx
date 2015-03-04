using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx
{
    public class Transactions : APIResult
    {
        internal Transactions(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        {
        }

        internal Transactions(List<Transaction> transactions, Currency baseCurrency, Currency counterCurrency)
            : base(DateTime.Now)
        {
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            TransactionsCollection = transactions;
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

        public List<Transaction> TransactionsCollection
        {
            get;
            set;
        }

        public override string ToString()
        {
            string output = "{0}/{1} - Transactions: {2} - Oldest: {3} - Newest: {4}";

            if (TransactionsCollection.Count > 0)
            {
                DateTime oldest = TransactionsCollection.Min(x => x.CompletedTime);
                DateTime newest = TransactionsCollection.Max(x => x.CompletedTime);

                return string.Format(output, BaseCurrency, CounterCurrency, TransactionsCollection.Count, oldest.ToString(), newest.ToString());
            }
            else
                return "No Transactions in Collection";
        }
    }
}