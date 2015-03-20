namespace BEx.ExchangeEngine
{
    internal interface IExchangeResponse
    {
        ApiResult ConvertToStandard(CurrencyTradingPair pair);
    }
}