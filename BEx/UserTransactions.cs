using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx
{
    /// <summary>
    /// Your transactions for the previous hour
    /// </summary>
    public class UserTransactions : APIResult
    {
        internal UserTransactions(List<UserTransaction> transactions, Currency baseCurrency, Currency counterCurrency, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            TransactionsCollection = transactions;
            BaseCurrency = baseCurrency;

            CounterCurrency = counterCurrency;
        }

        /// <summary>
        /// Base currency of transactions
        /// </summary>
        public Currency BaseCurrency
        {
            get;
            internal set;
        }

        /// <summary>
        /// Counter currency of transactions
        /// </summary>
        public Currency CounterCurrency
        {
            get;
            internal set;
        }

        /// <summary>
        /// Your Transactions for the previous hour
        /// </summary>
        public List<UserTransaction> TransactionsCollection
        {
            get;
            internal set;
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