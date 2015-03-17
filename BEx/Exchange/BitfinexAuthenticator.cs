using BEx.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BEx.BitFinexSupport
{
    internal class BitfinexAuthenticator : IAuthenticator
    {
        public static HMACSHA384 Hasher;
        private IExchangeConfiguration Configuration;

        public BitfinexAuthenticator(IExchangeConfiguration configuration)
        {
            Configuration = configuration;

            Hasher = new HMACSHA384(Encoding.UTF8.GetBytes(Configuration.SecretKey));
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

            long currentNonce = Configuration.Nonce;

            request.AddHeader("X-BFX-APIKEY", Configuration.ApiKey);

            StringBuilder payload = new StringBuilder();

            payload.Append("{");

            //payload.Append("\"request\": \"" + command.GetResolvedRelativeURI(pair) + "\",");
            payload.Append("\"request\": \"" + request.Resource + "\",");

            payload.Append("\"nonce\": \"" + currentNonce + "\"");

            if (request.Parameters.Count > 0)
            {
                foreach (Parameter p in request.Parameters)
                {
                    payload.Append(",");
                    payload.Append("\"" + p.Name + "\": \"" + p.Value + "\"");
                }
            }
            /*
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> kvPair in parameters)
                {
                    payload.Append(",");
                    payload.Append("\"" + kvPair.Key + "\": \"" + kvPair.Value + "\"");
                }
            }*/

            payload.Append("}");

            string payload64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload.ToString()));

            request.AddHeader("X-BFX-PAYLOAD", payload64);

            //Any public static (Shared in Visual Basic) members of this type are thread safe

            byte[] hashBytes = Hasher.ComputeHash(Encoding.UTF8.GetBytes(payload64));
            request.AddHeader("X-BFX-SIGNATURE", BitConverter.ToString(hashBytes).Replace("-", "").ToLower());
        }
    }
}