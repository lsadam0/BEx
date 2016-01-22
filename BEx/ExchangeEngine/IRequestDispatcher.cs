using BEx.ExchangeEngine.Commands;
using RestSharp;

namespace BEx.ExchangeEngine
{
    internal interface IRequestDispatcher
    {
        IRestResponse Dispatch<T>(IRestRequest request, IExchangeCommand referenceCommand) where T : IExchangeResult;
    }
}