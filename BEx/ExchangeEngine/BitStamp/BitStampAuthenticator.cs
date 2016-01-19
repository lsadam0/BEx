// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.Exceptions;
using BEx.ExchangeEngine.Utilities;
using RestSharp;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace BEx.ExchangeEngine.BitStamp
{
    internal class BitStampAuthenticator : IExchangeAuthenticator
    {
        public static HMACSHA256 Hasher;

        private static long _nonce = DateTime.UtcNow.Ticks;

        private readonly string ApiKey;

        private readonly string ClientId;


        public BitStampAuthenticator(string apiKey, string secretKey, string clientId)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException("apiKey", ErrorMessages.MissingArgApiKey);

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException("secretKey", ErrorMessages.MissingArgSecretKey);

            if (string.IsNullOrWhiteSpace("clientId"))
                throw new ArgumentNullException("clientId", ErrorMessages.MissingArgClientId);

            ApiKey = apiKey;
            ClientId = clientId;

            Hasher = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
        }

        /// <summary>
        /// Consecutively increasing action counter
        /// </summary>
        /// <value>0</value>
        public long Nonce
        {
            get
            {
                return Interlocked.Increment(ref _nonce);
            }
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            long currentNonce = Nonce;

            string message = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", currentNonce, ClientId, ApiKey);

            byte[] dta = Encoding.ASCII.GetBytes(message);
            string signature = BitConverter.ToString(Hasher.ComputeHash(dta)).Replace("-", string.Empty).ToUpperInvariant();

            request.AddParameter("key", Uri.EscapeUriString(ApiKey));
            request.AddParameter("signature", Uri.EscapeUriString(signature));
            request.AddParameter("nonce", Uri.EscapeUriString(currentNonce.ToStringInvariant()));
        }
    }
}