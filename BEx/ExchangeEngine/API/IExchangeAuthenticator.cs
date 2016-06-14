using RestSharp.Authenticators;

namespace BEx.ExchangeEngine.API
{
    public interface IExchangeAuthenticator : IAuthenticator
    {
        long Nonce { get; }
    }
}