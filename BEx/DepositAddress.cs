using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class DepositAddress : APIResult
    {

        public string Address
        {
            get;
            set;
        }

        public Currency DepositCurrency
        {
            get;
            set;
        }

        internal DepositAddress() : base()
        {

        }

        internal DepositAddress(string address)
            : base()
        {
            Address = address;
        }
    }
}
