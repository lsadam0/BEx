using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class Transactions : APIResult
    {
        public List<Transaction> TransactionsCollection
        {
            get;
            set;
        }

        public Transactions() : base()
        {
            TransactionsCollection = new List<Transaction>();
        }
    }
}
