using RestSharp;
using BEx.ExchangeEngine.Commands;

namespace BEx.ExchangeEngine
{
    internal interface IRequestDispatcher
    {
        IRestResponse Dispatch<T>(IRestRequest request, IExchangeCommand referenceCommand) where T : IExchangeResult;
    }
}