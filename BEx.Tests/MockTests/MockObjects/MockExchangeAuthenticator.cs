using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine;
using RestSharp;

namespace BEx.UnitTests.MockTests.MockObjects
{
    class MockExchangeAuthenticator : IAuthenticator
    {
        public MockExchangeAuthenticator(IExchangeConfiguration configuration)
        {
            configuration.SecretKey = null;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {

        }
    }
}
