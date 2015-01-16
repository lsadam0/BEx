using System.Collections.Generic;

namespace BEx
{
    public class UserTransactions : APIResult
    {
        public List<UserTransaction> UserTrans
        {
            get;
            set;
        }

        internal UserTransactions() : base()
        {
            UserTrans = new List<UserTransaction>();
        }

        internal UserTransactions(List<UserTransaction> transactions)
            : base()
        {
            UserTrans = transactions;
        }
    }
}
