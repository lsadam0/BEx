using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using RestSharp;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampAuthenticator : IAuthenticator
    {
        public static HMACSHA256 Hasher;

        private readonly IExchangeConfiguration _configuration;

        public BitStampAuthenticator(IExchangeConfiguration configuration)
        {
            _configuration = configuration;

            Hasher = new HMACSHA256(Encoding.ASCII.GetBytes(_configuration.SecretKey));
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            long currentNonce = _configuration.Nonce;

            string message = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", currentNonce, _configuration.ClientId, _configuration.ApiKey);

            byte[] dta = Encoding.ASCII.GetBytes(message);
            string signature = BitConverter.ToString(Hasher.ComputeHash(dta)).Replace("-", string.Empty).ToUpperInvariant();

            request.AddParameter("key", Uri.EscapeUriString(_configuration.ApiKey));
            request.AddParameter("signature", Uri.EscapeUriString(signature));
            request.AddParameter("nonce", Uri.EscapeUriString(currentNonce.ToStringInvariant()));
        }
    }
}