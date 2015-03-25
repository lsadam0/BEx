using RestSharp;

namespace BEx.ExchangeEngine
{
    internal interface IRequestDispatcher
    {
        IRestResponse Dispatch(IRestRequest request, IExchangeCommand referenceCommand);
    }
}