using RestSharp;

namespace BEx.CommandProcessing
{
    internal class RequestDispatcher
    {
        private readonly Exchange _sourceExchange;

        private readonly RateLimiter _throttler;

        internal RequestDispatcher(Exchange sourceExchange)
        {
            _sourceExchange = sourceExchange;
            _throttler = new RateLimiter(_sourceExchange.ExchangeSourceType);
        }

        internal IRestResponse Dispatch(RestRequest request, ExchangeCommand commandReference)
        {
            var client = new RestClient(_sourceExchange.Configuration.BaseUri);

            IRestResponse response;

            if (commandReference.IsAuthenticated)
            {
                _throttler.Throttle();
                lock (this)
                {
                    // Exchanges strictly enforce sequential nonce values
                    // this lock insures that authenticated requests always
                    // contain a correct sequential nonce value.
                    client.Authenticator = _sourceExchange.Authenticator;
                    response = client.Execute(request);
                }
            }
            else
            {
                _throttler.Throttle();
                response = client.Execute(request);
            }

            return response;
        }
    }
}