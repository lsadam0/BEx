using System;

namespace BEx
{
    public interface IExchangeResult
    {
        ExchangeType SourceExchange { get; }

        DateTime ExchangeTimeStampUTC { get; }

        DateTime LocalTimeStampUTC { get; }
    }
}