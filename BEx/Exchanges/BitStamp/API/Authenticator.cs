// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using BEx.Exceptions;

using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using RestSharp;

namespace BEx.Exchanges.BitStamp.API
{
    internal class Authenticator : IExchangeAuthenticator
    {
        private const string _mask = "{0}{1}{2}";
        private static HMACSHA256 _hasher;

        private static long _nonce = DateTime.UtcNow.Ticks;

        //private readonly string _apiKey;
        private readonly string _apiKey;

        //private readonly string _clientId;
        private readonly string _clientId;

        public Authenticator(string apiKey, string secretKey, string clientId)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey), ErrorMessages.MissingArgApiKey);

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException(nameof(secretKey), ErrorMessages.MissingArgSecretKey);

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException(nameof(clientId), ErrorMessages.MissingArgClientId);

            _apiKey = apiKey; // Uri.EscapeUriString(apiKey);
            _clientId = clientId; //Uri.EscapeUriString(clientId);

            _hasher = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
        }

        public long Nonce => Interlocked.Increment(ref _nonce);

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var currentNonce = Nonce;

            var message =
                Encoding.ASCII.GetBytes(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        _mask,
                        currentNonce,
                        _clientId,
                        _apiKey));

            var signature =
                BitConverter
                    .ToString(
                        _hasher.ComputeHash(message))
                    .Replace("-", string.Empty)
                    .ToUpperInvariant();

            request.AddParameter("key", _apiKey);
            request.AddParameter("signature", signature);
            request.AddParameter("nonce", currentNonce.ToStringInvariant());
        }
    }
}