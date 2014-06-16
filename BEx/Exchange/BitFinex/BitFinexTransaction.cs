using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BEx.BitFinexSupport;

namespace BEx
{
    public class BitFinexTransaction : Transaction
    {

        public string ExchangeSource
        {
            get;
            set;
        }

       internal BitFinexTransaction(BitFinexTransactionJSON source) : base(source)
        {
            ExchangeSource = source.exchange;
        }
    }
}
