using System;

namespace BEx
{
    public class APIResult
    {
        public DateTime ExchangeTimeStamp
        {
            get;
            set;
        }

        public DateTime LocalTimeStamp
        {
            get;
            set;
        }


        internal APIResult(DateTime exchangeTimeStamp)
        {
            ExchangeTimeStamp = exchangeTimeStamp;
            LocalTimeStamp = DateTime.Now;

        }
    }
}