using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BEx.ExchangeEngine.Coinbase
{
    internal class Authenticator : IExchangeAuthenticator
    {
        private static long _none = DateTime.UtcNow.Ticks;

        public void Authenticate(IRestClient client, IRestRequest request)
        {

        }


        public long Nonce { get; }
    }
}
