using System;

namespace BEx.ExchangeEngine
{
    public interface IExchangeResult
    {
        ExchangeType SourceExchange { get; }

        DateTime ExchangeTimeStampUTC { get; }

        DateTime LocalTimeStampUTC { get; }
    }
}