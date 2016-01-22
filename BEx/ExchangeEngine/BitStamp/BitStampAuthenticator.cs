// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using BEx.Exceptions;
using BEx.ExchangeEngine.Utilities;
using RestSharp;

namespace BEx.ExchangeEngine.BitStamp
{
    internal class BitStampAuthenticator : IExchangeAuthenticator
    {
        public static HMACSHA256 Hasher;

        private static long _nonce = DateTime.UtcNow.Ticks;

        private readonly string _apiKey;

        private readonly string _clientId;

        public BitStampAuthenticator(string apiKey, string secretKey, string clientId)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey), ErrorMessages.MissingArgApiKey);

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(secretKey), ErrorMessages.MissingArgSecretKey);

            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId), ErrorMessages.MissingArgClientId);

            _apiKey = apiKey;
            _clientId = clientId;

            Hasher = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
        }

        /// <summary>
        ///     Consecutively increasing action counter
        /// </summary>
        /// <value>0</value>
        public long Nonce => Interlocked.Increment(ref _nonce);

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var currentNonce = Nonce;

            var message = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", currentNonce, _clientId, _apiKey);

            var dta = Encoding.ASCII.GetBytes(message);
            var signature = BitConverter.ToString(Hasher.ComputeHash(dta)).Replace("-", string.Empty).ToUpperInvariant();

            request.AddParameter("key", Uri.EscapeUriString(_apiKey));
            request.AddParameter("signature", Uri.EscapeUriString(signature));
            request.AddParameter("nonce", Uri.EscapeUriString(currentNonce.ToStringInvariant()));
        }
    }
}