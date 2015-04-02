// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.BitfinexSupport;
using BEx.ExchangeEngine.BitStampSupport;
using BEx.UnitTests.MockTests.MockObjects;

namespace BEx.UnitTests
{
    public static class ExchangeFactory
    {
        private static Dictionary<ExchangeType, IExchangeConfiguration> configurations;

        private static void LoadAPIKeys()
        {
            XElement keys = XElement.Load(@"C:\_Work\BEx\TestingKeys.xml");

            XElement exchangeElement;

            configurations = new Dictionary<ExchangeType, IExchangeConfiguration>();

            exchangeElement = keys.Element("BitStamp");

            configurations.Add(ExchangeType.BitStamp, new BitStampConfiguration(
                exchangeElement.Element("Key").Value,
                exchangeElement.Element("ClientID").Value,
                exchangeElement.Element("Secret").Value
                ));


            exchangeElement = keys.Element("BitFinex");

            configurations.Add(ExchangeType.Bitfinex, new BitfinexConfiguration(
                exchangeElement.Element("Key").Value,
                exchangeElement.Element("Secret").Value
                ));
        }

        public static IUnauthenticatedExchange GetUnauthenticatedExchange(ExchangeType toGet)
        {
            switch (toGet)
            {
                case ExchangeType.BitStamp:
                    return new BitStamp();
                case ExchangeType.Bitfinex:
                    return new Bitfinex();
                case ExchangeType.Mock:
                    return new MockExchange(new MockRequestDispatcher());
                default:
                    return null;
            }
        }

        public static IAuthenticatedExchange GetAuthenticatedExchange(ExchangeType toGet)
        {
            if (configurations == null)
                LoadAPIKeys();

            IExchangeConfiguration configuration;

            configurations.TryGetValue(toGet, out configuration);

            switch (toGet)
            {
                case ExchangeType.BitStamp:
                    return new BitStamp(
                        configuration.ApiKey,
                        configuration.SecretKey,
                        configuration.ClientId);
                case ExchangeType.Bitfinex:
                    return new Bitfinex(
                        configuration.ApiKey,
                        configuration.SecretKey);
                case ExchangeType.Mock:
                    return new MockExchange(new MockRequestDispatcher());
                default:
                    return null;

            }
        }
    }
}
