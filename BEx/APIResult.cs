using System;

namespace BEx
{
    public class APIResult
    {
        /// <summary>
        /// Exchange reported TimeStamp of the action
        /// </summary>
        public DateTime ExchangeTimeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// Local Machine TimeStamp of the Action
        /// </summary>
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