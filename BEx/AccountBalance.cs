using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class AccountBalance : APIResult
    {

        public Dictionary<Currency, decimal> Available
        {
            get;
            set;
        }

        public Dictionary<Currency, decimal> Reserved
        {
            get;
            set;
        }

        public Dictionary<Currency, decimal> Balance
        {
            get;
            set;
        }

        internal AccountBalance() : base()
        {
            Balance = new Dictionary<Currency, decimal>();
            Available = new Dictionary<Currency, decimal>();
            Reserved = new Dictionary<Currency, decimal>();

        }
    }
}
