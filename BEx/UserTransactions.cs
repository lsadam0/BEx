using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class UserTransactions : APIResult
    {
        public List<UserTransaction> UserTrans
        {
            get;
            set;
        }

        public UserTransactions() : base()
        {
            UserTrans = new List<UserTransaction>();
        }
    }
}
