using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine;
using RestSharp;

namespace BEx.UnitTests.MockTests.MockObjects
{
    class MockExchangeAuthenticator : IExchangeAuthenticator
    {
        public long Nonce
        {
            get
            {
                return DateTime.UtcNow.Ticks;
            }
        }

        public MockExchangeAuthenticator(IExchangeConfiguration configuration)
        {
           
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {

        }
    }
}
