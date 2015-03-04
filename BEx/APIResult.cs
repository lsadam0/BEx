using System;

namespace BEx
{
    /// <summary>
    /// Exchange Result Base Class
    /// </summary>
    public class APIResult
    {
        internal APIResult(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
        {
            ExchangeTimeStamp = exchangeTimeStamp;
            LocalTimeStamp = DateTime.Now;
            SourceExchange = sourceExchange;
        }

        /// <summary>
        /// Exchange reported TimeStamp of the action.  When the Exchange oes not provide
        /// a TimeStamp, this value will be eual to LocalTimeStamp.
        ///
        /// </summary>
        public DateTime ExchangeTimeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// Local Machine TimeStamp marking the time at which an APICommand has successfully executed.
        /// </summary>
        public DateTime LocalTimeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// Exchange from which this result was received
        /// </summary>
        public ExchangeType SourceExchange
        {
            get;
            private set;
        }
    }
}