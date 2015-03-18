namespace BEx.ExchangeSupport
{
    internal interface IExchangeResponse
    {
        APIResult ConvertToStandard(CurrencyTradingPair pair);
    }
}