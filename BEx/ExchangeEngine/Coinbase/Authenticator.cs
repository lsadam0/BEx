using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BEx.ExchangeEngine.Utilities;
using RestSharp;

namespace BEx.ExchangeEngine.Coinbase
{
    internal class Authenticator : IExchangeAuthenticator
    {
        private const string parameterMask = ",\"{0}\": \"{1}\"";

        private const string _cbAccessKeyHeader = "CB-ACCESS-KEY";
        private const string _cbAccessSign = "CB-ACCESS-SIGN"; // The base64-encoded signature(see Signing a Message).
        private const string _cbAccessTimeStampHeader = "CB-ACCESS-TIMESTAMP"; // A timestamp for your request.

        private const string _cbAcccessPass = "CB-ACCESS-PASSPHRASE";
            // The passphrase you specified when creating the API key.

        private static long _none = DateTime.UtcNow.Ticks;
        private readonly HMACSHA256 _hash;
        private readonly string _key;
        //  private readonly string _secret;
        private readonly string _passphrase;

        public Authenticator(string key, string secret, string passPhrase)
        {
            _key = key;
            // this._secret = secret;
            _hash = new HMACSHA256(
                Convert.FromBase64String(secret));

            _passphrase = passPhrase;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var timestamp = Nonce.ToString();

            var body = GetBody(request);

            // CB-ACCESS-KEY The api key as a string.
            request.AddHeader(_cbAccessKeyHeader, _key);

            // CB-ACCESS-SIGN The base64-encoded signature (see Signing a Message).

            //var what = timestamp + method + requestPath + body;

            var combined = timestamp + (request.Method == Method.POST ? "POST" : "GET") + "/" + request.Resource + body;
            var hashed = _hash.ComputeHash(Encoding.UTF8.GetBytes(combined));
            var signed64 = Convert.ToBase64String(hashed);
            request.AddHeader(_cbAccessSign, signed64);

            // CB-ACCESS-TIMESTAMP A timestamp for your request.
            request.AddHeader(_cbAccessTimeStampHeader, timestamp);

            // CB-ACCESS-PASSPHRASE The passphrase you specified when creating the API key.
            request.AddHeader(_cbAcccessPass, _passphrase);

            // All request bodies should have content type application/json and be valid JSON.
        }

        //  private static long _nonce = UnixTime.UnixUTCNow;

        public long Nonce => UnixTime.UnixUTCNow;

        private string GetBody(IRestRequest request)
        {
            var body = string.Empty;

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

                body = "{ " + parameters + " }";
            }

            return body;
        }
    }
}