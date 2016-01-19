// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Commands;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx.UnitTests.RequestFactoryTests
{
    [TestFixture]
    [Category("RequestFactory")]
    public class RequestFactory_RequestCreation
    {
        private IRestRequest toTest;
        private ExchangeCommand<Order> command;
        private TradingPair pair;

        [TestFixtureSetUp]
        public void Setup()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD"),
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair),
                new ExchangeParameter(ParameterMethod.QueryString, "price", StandardParameter.Price),
                new ExchangeParameter(ParameterMethod.Post, "exchange", StandardParameter.None, "bitfinex"),
                new ExchangeParameter(ParameterMethod.QueryString, "type", StandardParameter.None, "exchange limit"),
                new ExchangeParameter(ParameterMethod.Url, "side", StandardParameter.None, "sell")
            };

            var values = new Dictionary<StandardParameter, string>()
            {
                { StandardParameter.Price, "100.00"}
            };

            Assert.Fail();
            /*
            command = new LimitOrderCommand(
                                 new ExecutionEngine(,
                                 Method.POST,
                                 new Uri("/v1/order/new/{pair}/{side}", UriKind.Relative),
                                 true,
                                 typeof(object),
                                 param);

            pair = new TradingPair(Currency.LTC, Currency.BTC);

            toTest = BEx.ExchangeEngine.RequestFactory.GetRequest(command, pair, values);*/
        }

        [Test]
        public void Request_Configuration_Success()
        {
            Assert.That(toTest.Method == Method.POST);
            Assert.That(toTest.Resource == "/v1/order/new/{pair}/{side}");
        }

        [Test]
        public void Request_AllParameters_Populated()
        {
            Assert.That(toTest.Parameters.Count == command.Parameters.Count);

            foreach (var parameter in command.Parameters)
            {
                Assert.IsNotNull(toTest.Parameters.Find(x => x.Name == parameter.Value.ExchangeParameterName));
            }
        }

        [Test]
        public void Request_UrlParameters_Populated()
        {
            var urlParams = toTest.Parameters.Where(x => x.Type == ParameterType.UrlSegment).ToList();

            Assert.That(urlParams.Count == 2);

            Assert.That(urlParams[0].Name == "pair");
            Assert.That(urlParams[0].Value.ToString() == pair.ToString());

            Assert.That(urlParams[1].Name == "side");
            Assert.That(urlParams[1].Value.ToString() == "sell");
        }

        [Test]
        public void Request_PostParameters_Populated()
        {
            var postParams = toTest.Parameters.Where(x => x.Type == ParameterType.GetOrPost).ToList();

            Assert.That(postParams.Count == 2);

            Assert.That(postParams[0].Name == "symbol");
            Assert.That(postParams[0].Value.ToString() == pair.ToString());

            Assert.That(postParams[1].Name == "exchange");
            Assert.That(postParams[1].Value.ToString() == "bitfinex");
        }

        [Test]
        public void Request_QueryStringParameters_Populated()
        {
            var queryParams = toTest.Parameters.Where(x => x.Type == ParameterType.QueryString).ToList();

            Assert.That(queryParams.Count == 2);

            Assert.That(queryParams[0].Name == "price");
            Assert.That(queryParams[0].Value.ToString() == "100.00");

            Assert.That(queryParams[1].Name == "type");
            Assert.That(queryParams[1].Value.ToString() == "exchange limit");
        }
    }
}