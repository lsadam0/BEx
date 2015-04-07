using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BEx.ExchangeEngine
{
    public interface IExchangeAuthenticator : IAuthenticator
    {
        long Nonce
        {
            get;
        }

       // void Authenticate(IRestClient client, IRestRequest request);
    }
}
