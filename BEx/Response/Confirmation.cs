﻿using System;

namespace BEx
{
    public sealed class Confirmation : ApiResult
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