// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RestSharp;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Consumes an IRestRequest, Authenticates the Request, and
    /// sends it to the targeted Exchange
    /// </summary>
    internal class RequestDispatcher : IRequestDispatcher
    {
        private readonly Exchange _sourceExchange;

        /// <summary>
        /// Don't allow burst requests
        /// </summary>
        private readonly RateLimiter _throttler;

        internal RequestDispatcher(Exchange sourceExchange)
        {
            _sourceExchange = sourceExchange;
            _throttler = new RateLimiter(_sourceExchange.ExchangeSourceType);
        }

        /// <summary>
        /// Send the IRestRequest to the targeted exchange
        /// </summary>
        /// <param name="request">To Dispatch</param>
        /// <param name="commandReference">Reference Command</param>
        /// <returns>IRestResponse</returns>
        public IRestResponse Dispatch(IRestRequest request, IExchangeCommand commandReference)
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