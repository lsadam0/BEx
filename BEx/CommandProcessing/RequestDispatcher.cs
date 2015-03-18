using BEx;
using BEx.ExchangeSupport;
using JackLeitch.RateGate;
using RestSharp;
using System.Collections;
using System.Collections.Generic;

namespace BEx.CommandProcessing
{
    internal class RequestDispatcher
    {
        private Exchange SourceExchange;

        private RequestThrottler _throttler;

        internal RequestDispatcher(Exchange sourceExchange)
        {
            SourceExchange = sourceExchange;
            _throttler = new RequestThrottler(SourceExchange.ExchangeSourceType);
        }

        internal IRestResponse Dispatch(RestRequest request, ExchangeCommand commandReference, CurrencyTradingPair pair)
        {
            var client = new RestClient(SourceExchange.Configuration.Url);

            IRestResponse response;

            if (commandReference.IsAuthenticated)
            {
                _throttler.Throttle();
                lock (this)
                {
                    // Exchanges strictly enforce sequential nonce values
                    // this lock insures that authenticated requests always
                    // contain a correct sequential nonce value.
                    client.Authenticator = SourceExchange.Authenticator;
                    response = client.Execute(request);
                }
            }
            else
            {
                _throttler.Throttle();
                response = client.Execute(request);
            }

            return response;
        }
    }

    internal class RequestThrottler
    {
        private static Dictionary<ExchangeType, RateGate> gates = new Dictionary<ExchangeType, RateGate>();
        private static object locker = new object();

        private ExchangeType _sourceExchange;

        public RequestThrottler(ExchangeType sourceExchange)
        {
            if (!gates.ContainsKey(sourceExchange))
            {
                lock (locker)
                {
                    if (!gates.ContainsKey(sourceExchange))
                    {
                        gates.Add(sourceExchange, new RateGate(600, new System.TimeSpan(0, 10, 0)));
                    }
                }
            }

            _sourceExchange = sourceExchange;
        }

        public void Throttle()
        {
            gates[_sourceExchange].WaitToProceed();
        }
    }
}