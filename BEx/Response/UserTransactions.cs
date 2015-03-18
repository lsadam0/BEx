using BEx.ExchangeSupport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx
{
    /// <summary>
    /// Your transactions for the previous hour
    /// </summary>
    public sealed class UserTransactions : APIResult
    {
        internal UserTransactions(IEnumerable<IExchangeResponse> transactions, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            TransactionsCollection = new List<UserTransaction>();
            this.Pair = pair;

            foreach (IExchangeResponse transaction in transactions)
            {
                UserTransaction converted = transaction.ConvertToStandard(pair) as UserTransaction;

                if (converted != null)
                    TransactionsCollection.Add(converted);
            }
        }

        /// <summary>
        /// Trading Pair
        /// </summary>
        public CurrencyTradingPair Pair
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
            string output = "{0} - Transactions: {1} - Oldest: {2} - Newest: {3}";

            if (TransactionsCollection.Count > 0)
            {
                DateTime oldest = TransactionsCollection.Min(x => x.CompletedTime);
                DateTime newest = TransactionsCollection.Max(x => x.CompletedTime);

                return string.Format(output, Pair, TransactionsCollection.Count, oldest.ToString(), newest.ToString());
            }
            else
                return "No Transactions in Collection";
        }
    }
}