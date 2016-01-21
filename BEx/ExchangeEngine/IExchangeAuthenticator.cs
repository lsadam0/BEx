using RestSharp;

namespace BEx.ExchangeEngine
{
    public interface IExchangeAuthenticator : IAuthenticator
    {
        long Nonce { get; }
    }
}