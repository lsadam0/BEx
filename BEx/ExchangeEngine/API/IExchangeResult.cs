using System;

namespace BEx.ExchangeEngine.API
{
    public interface IExchangeResult
    {
        ExchangeType SourceExchange { get; }

        DateTime ExchangeTimeStampUTC { get; }

        DateTime LocalTimeStampUTC { get; }
    }
}