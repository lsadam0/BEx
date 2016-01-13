using RestSharp;

namespace BEx.ExchangeEngine
{
    internal interface IRequestDispatcher
    {
        IRestResponse Dispatch<T>(IRestRequest request, IExchangeCommand<T> referenceCommand) where T : IExchangeResult;
    }
}