// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Bitfinex;
using BEx.ExchangeEngine.BitStamp;
using BEx.UnitTests.MockTests.MockObjects;

namespace BEx.UnitTests
{
    internal static class ExchangeFactory
    {
        private static Dictionary<ExchangeType, AuthToken> tokens = null;

        private static void LoadAPIKeys()
        {
            XElement keys = XElement.Load(@"C:\_Work\BEx\TestingKeys.xml");

            XElement exchangeElement;

            tokens = new Dictionary<ExchangeType, AuthToken>();

            exchangeElement = keys.Element("BitStamp");

            tokens.Add(ExchangeType.BitStamp, new AuthToken()
                {
                    ApiKey = exchangeElement.Element("Key").Value,
                    ClientId = exchangeElement.Element("ClientID").Value,
                    Secret = exchangeElement.Element("Secret").Value
                });

            exchangeElement = keys.Element("BitFinex");

            tokens.Add(ExchangeType.Bitfinex, new AuthToken()
                {
                    ApiKey = exchangeElement.Element("Key").Value,
                    Secret = exchangeElement.Element("Secret").Value
                });
        }

        public static Exchange GetUnauthenticatedExchange(ExchangeType toGet)
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

        public static Exchange GetAuthenticatedExchange(ExchangeType toGet)
        {
            if (tokens == null)
                LoadAPIKeys();

            AuthToken token;

            tokens.TryGetValue(toGet, out token);

            switch (toGet)
            {
                case ExchangeType.BitStamp:
                    return new BitStamp(
                        token.ApiKey,
                        token.Secret,
                        token.ClientId);
                case ExchangeType.Bitfinex:
                    return new Bitfinex(
                        token.ApiKey,
                        token.Secret);
                case ExchangeType.Mock:
                    return new MockExchange(new MockRequestDispatcher());
                default:
                    return null;

            }
        }


        public static AuthToken GetBitfinexAuthToken()
        {
            if (tokens == null)
                LoadAPIKeys();

            return tokens[ExchangeType.Bitfinex];
        }

        public static AuthToken GetBitstampAuthToken()
        {
            if (tokens == null)
                LoadAPIKeys();

            return tokens[ExchangeType.BitStamp];
        }

    }

    internal class AuthToken
    {
        public string ApiKey
        {
            get;
            set;
        }

        public string ClientId
        {
            get;
            set;
        }

        public string Secret
        {
            get;
            set;
        }
    }
}
