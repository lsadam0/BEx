using System;

namespace BEx
{
    public sealed class Confirmation : ApiResult
    {
        public Confirmation(DateTime exchangeTimestamp, ExchangeType sourceExchange)
            : base(exchangeTimestamp, sourceExchange)
        { }

        public bool IsConfirmed
        {
            get;
            internal set;
        }
    }
}