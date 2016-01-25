// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine.Commands;
using RestSharp;

namespace BEx.ExchangeEngine
{
    /// <summary>
    ///     Consumes an IRestRequest, Authenticates the Request, and
    ///     sends it to the targeted Exchange
    /// </summary>
    internal class RequestDispatcher : IRequestDispatcher
    {
        private readonly IExchangeAuthenticator _authenticator;
        private readonly Uri _baseUri;

        internal RequestDispatcher(Uri baseUri, IExchangeAuthenticator authenticator)
        {
            _baseUri = baseUri;
            _authenticator = authenticator;
        }

        internal RequestDispatcher(Uri baseUri)
        {
            _baseUri = baseUri;
            _authenticator = null;
        }

        /// <summary>
        ///     Send the IRestRequest to the targeted exchange
        /// </summary>
        /// <param name="request">To Dispatch</param>
        /// <param name="commandReference">Reference Command</param>
        /// <returns>IRestResponse</returns>
        public IRestResponse Dispatch<T>(IRestRequest request, IExchangeCommand commandReference)
            where T : IExchangeResult
        {
            var client = new RestClient(_baseUri);

            IRestResponse response;

            if (commandReference.IsAuthenticated)
            {

                lock (this)
                {
                    // Exchanges strictly enforce sequential nonce values
                    // this lock insures that authenticated requests always
                    // contain a correct sequential nonce value.
                    client.Authenticator = _authenticator;
                    response = client.Execute(request);
                }
            }
            else
            {
                response = client.Execute(request);
            }

            return response;
        }
    }
}