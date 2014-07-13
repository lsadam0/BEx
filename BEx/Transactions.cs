using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class Transactions
    {

        public List<Transaction> TransactionList
        {
            get;
            set;
        }

        public Transactions()
        {

            TransactionList = new List<Transaction>();
        }
    }
}
