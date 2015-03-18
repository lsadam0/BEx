using BEx.ExchangeSupport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx
{
    public sealed class Transactions : APIResult
    {
        internal Transactions(IEnumerable<IExchangeResponse> transactions, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Pair = pair;
            TransactionsCollection = new List<Transaction>();

            foreach (IExchangeResponse transaction in transactions)
                TransactionsCollection.Add(transaction.ConvertToStandard(pair) as Transaction);
        }

        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        public List<Transaction> TransactionsCollection
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