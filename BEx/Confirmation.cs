using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class Confirmation : APIResult
    {
        public Confirmation(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        { }

        public bool IsConfirmed
        {
            get;
            internal set;
        }
    }
}