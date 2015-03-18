using RestSharp;

namespace BEx.CommandProcessing
{
    internal class RequestDispatcher
    {
        private Exchange SourceExchange;

        private RateLimiter _throttler;

        internal RequestDispatcher(Exchange sourceExchange)
        {
            SourceExchange = sourceExchange;
            _throttler = new RateLimiter(SourceExchange.ExchangeSourceType);
        }

        internal IRestResponse Dispatch(RestRequest request, ExchangeCommand commandReference, CurrencyTradingPair pair)
        {
            var client = new RestClient(SourceExchange.Configuration.Url);

            IRestResponse response;

            if (commandReference.IsAuthenticated)
            {
                _throttler.Throttle();
                lock (this)
                {
                    // Exchanges strictly enforce sequential nonce values
                    // this lock insures that authenticated requests always
                    // contain a correct sequential nonce value.
                    client.Authenticator = SourceExchange.Authenticator;
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