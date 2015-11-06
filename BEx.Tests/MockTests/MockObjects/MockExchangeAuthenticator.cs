using BEx.ExchangeEngine;
using RestSharp;
using System;

namespace BEx.UnitTests.MockTests.MockObjects
{
    internal class MockExchangeAuthenticator : IExchangeAuthenticator
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