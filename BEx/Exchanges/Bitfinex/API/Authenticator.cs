// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using BEx.Exceptions;
using BEx.ExchangeEngine;

using BEx.ExchangeEngine.API;
using RestSharp;

namespace BEx.Exchanges.Bitfinex.API

{
    internal class Authenticator : IExchangeAuthenticator
    {
        private const string apiHeaderKey = "X-BFX-APIKEY";
        private const string parameterMask = ",\"{0}\": \"{1}\"";
        private const string payloadHeaderKey = "X-BFX-PAYLOAD";
        private const string payloadMask = "\"request\": \"{0}\",\"nonce\": \"{1}\"{2}";
        private const string signatureHeaderKey = "X-BFX-SIGNATURE";
        private static HMACSHA384 _hasher;

        private static long _nonce = DateTime.UtcNow.Ticks;

        private readonly string _apiKey;

        public Authenticator(string secretKey, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey), ErrorMessages.MissingArgApiKey);

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(secretKey), ErrorMessages.MissingArgSecretKey);

            _apiKey = apiKey;

            _hasher = new HMACSHA384(Encoding.UTF8.GetBytes(secretKey));
        }

        public long Nonce => Interlocked.Increment(ref _nonce);

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var currentNonce = Nonce;

            request.AddHeader(apiHeaderKey, _apiKey);

            var paramsPayload = string.Empty;

            if (request.Parameters.Count > 0)
            {
                var parameters = new StringBuilder();

                foreach (var p in request.Parameters.Where(x => x.Type != ParameterType.UrlSegment))
                {
                    if (p.Type != ParameterType.UrlSegment)
                    {
                        parameters.Append(string.Format(parameterMask, p.Name, p.Value));
                    }
                }

                paramsPayload = parameters.ToString();
            }

            var payload64 =
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(
                        "{ " +
                        string.Format(
                            payloadMask,
                            request.Resource,
                            currentNonce,
                            paramsPayload)
                        + "}"));

            request.AddHeader(payloadHeaderKey, payload64);

            var hashBytes = _hasher.ComputeHash(Encoding.UTF8.GetBytes(payload64));

            request.AddHeader(signatureHeaderKey,
                BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLowerInvariant());
        }
    }
}