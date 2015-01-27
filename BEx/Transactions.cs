using System.Collections.Generic;

namespace BEx
{
    public class Transactions : APIResult
    {
        public List<Transaction> TransactionsCollection
        {
            get;
            set;
        }

        internal Transactions()
            : base()
        {
            TransactionsCollection = new List<Transaction>();
        }

        internal Transactions(List<Transaction> transactions)
            : base()
        {
            TransactionsCollection = transactions;
        }
    }
}