using System;
using System.Linq;
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

        public override string ToString()
        {
            string output = "{0}/{1} - Transactions: {2} - Oldest: {3} - Newest: {4}";

            if (UserTrans.Count > 0)
            {
                DateTime oldest = UserTrans.Min(x => x.CompletedTime);
                DateTime newest = UserTrans.Max(x => x.CompletedTime);

                return string.Format(output, BaseCurrency, CounterCurrency, UserTrans.Count, oldest.ToString(), newest.ToString());
            }
            else
                return "No Transactions in Collection";
        }



        internal UserTransactions(List<UserTransaction> transactions, Currency baseCurrency, Currency counterCurrency)
            : base(DateTime.Now)
        {
            UserTrans = transactions;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
        }
    }
}