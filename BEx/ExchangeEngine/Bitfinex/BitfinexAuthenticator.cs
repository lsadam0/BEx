// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using RestSharp;
using BEx.Exceptions;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex
{
    internal class BitfinexAuthenticator : IExchangeAuthenticator
    {
        public static HMACSHA384 Hasher;

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

        private readonly string ApiKey;

        public BitfinexAuthenticator(string secretKey, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException("apiKey", ErrorMessages.MissingArgApiKey);

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException("secretKey", ErrorMessages.MissingArgSecretKey);

            ApiKey = apiKey;

            Hasher = new HMACSHA384(Encoding.UTF8.GetBytes(secretKey));

        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            /*POST https://api.bitfinex.com/v1/order/new
               With a payload of
               {
               "request": "/v1/order/new",
               "nonce": "1234",
               "option1": ...
               }
               The nonce provided must be strictly increasing.

               To authenticate a request, use the following:

               payload = parameters-dictionary -> JSON encode -> base64
               signature = HMAC-SHA384(payload, api-secret) as hexadecimal
               send (api-key, payload, signature)
               These are encoded as HTTP headers named:
               X-BFX-APIKEY
               X-BFX-PAYLOAD
               X-BFX-SIGNATURE*/

            long currentNonce = Nonce;

            request.AddHeader("X-BFX-APIKEY", ApiKey);

            StringBuilder payload = new StringBuilder();



            payload.Append("{");
            payload.Append("\"request\": \"" + request.Resource + "\",");
            payload.Append("\"nonce\": \"" + currentNonce + "\"");

            if (request.Parameters.Count > 0)
            {
                foreach (Parameter p in request.Parameters)
                {
                    if (p.Type != ParameterType.UrlSegment)
                    {
                        string test = JsonConvert.SerializeObject(p);
                        payload.Append(",");
                        payload.Append("\"" + p.Name + "\": \"" + p.Value + "\"");
                    }
                }
            }

            
            payload.Append("}");

            string payload64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload.ToString()));

            request.AddHeader("X-BFX-PAYLOAD", payload64);

            byte[] hashBytes = Hasher.ComputeHash(Encoding.UTF8.GetBytes(payload64));
            request.AddHeader("X-BFX-SIGNATURE", BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLowerInvariant());
        }
    }
}