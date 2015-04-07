// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using RestSharp;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.BitStamp
{
    internal class BitStampAuthenticator : IExchangeAuthenticator
    {
        public static HMACSHA256 Hasher;

     //   private readonly IExchangeConfiguration _configuration;

        private static long _nonce = DateTime.UtcNow.Ticks;

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

        private string ApiKey
        {
            get;
            set;
        }

        private string ClientId
        {
            get;
            set;
        }

        public BitStampAuthenticator(string apiKey, string secretKey, string clientId)//IExchangeConfiguration configuration)
        {
           // _configuration = configuration;
            ApiKey = apiKey;
            ClientId = clientId;

            Hasher = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
            
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