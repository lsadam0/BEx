using RestSharp;
using RestSharp.Authenticators;

namespace BEx.ExchangeEngine
{
    public interface IExchangeAuthenticator : IAuthenticator
    {
        long Nonce { get; }
    }
}