using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BEx.BitStampSupport;
using BEx.BitFinexSupport;
using BEx.Common;

namespace BEx
{
    public class Transaction : APIResult
    {

        public DateTime TimeStamp
        {
            get;
            set;
        }

        public long TransactionID
        {
            get;
            set;
        }

        public Decimal Price
        {
            get;
            set;
        }

        public Decimal Amount
        {
            get;
            set;
        }

        internal Transaction() : base()
        {

        }


    }
}
