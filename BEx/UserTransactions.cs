using System.Collections.Generic;

namespace BEx
{
    public class UserTransactions : APIResult
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

        public List<UserTransaction> UserTrans
        {
            get;
            set;
        }

        internal UserTransactions()
            : base()
        {
            UserTrans = new List<UserTransaction>();
        }

        internal UserTransactions(List<UserTransaction> transactions, Currency baseCurrency, Currency counterCurrency)
            : base()
        {
            UserTrans = transactions;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
        }
    }
}