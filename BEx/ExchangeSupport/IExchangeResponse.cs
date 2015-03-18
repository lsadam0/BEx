namespace BEx.ExchangeSupport
{
    internal interface IExchangeResponse
    {
        ApiResult ConvertToStandard(CurrencyTradingPair pair);
    }
}