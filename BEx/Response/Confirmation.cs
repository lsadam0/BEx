// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        protected override string DebugDisplay
        {
            get { return string.Format("{0} - Confirmed: {1}", SourceExchange, IsConfirmed); }
        }
    }
}