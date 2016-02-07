// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Xml.Linq;

namespace BEx.Tests
{
    internal static class ExchangeFactory
    {
        private static Dictionary<ExchangeType, AuthToken> _tokens;

        private static void LoadApiKeys()
        {
            var keys = XElement.Load(@"E:\_Work\To Save\BEx\TestingKeys.xml");

            _tokens = new Dictionary<ExchangeType, AuthToken>();

            var exchangeElement = keys.Element("BitStamp");

            _tokens.Add(ExchangeType.BitStamp, new AuthToken
            {
                ApiKey = exchangeElement.Element("Key").Value,
                ClientId = exchangeElement.Element("ClientID").Value,
                Secret = exchangeElement.Element("Secret").Value
            });

            exchangeElement = keys.Element("BitFinex");

            _tokens.Add(ExchangeType.Bitfinex, new AuthToken
            {
                ApiKey = exchangeElement.Element("Key").Value,
                Secret = exchangeElement.Element("Secret").Value
            });

            exchangeElement = keys.Element("Coinbase");

            _tokens.Add(ExchangeType.Coinbase, new AuthToken
            {
                ApiKey = exchangeElement.Element("Key").Value,
                Secret = exchangeElement.Element("Secret").Value,
                ClientId = exchangeElement.Element("Passphrase").Value
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

                case ExchangeType.Coinbase:
                    return new Coinbase();

                default:
                    return null;
            }
        }

        public static Exchange GetAuthenticatedExchange(ExchangeType toGet)
        {
            if (_tokens == null)
                LoadApiKeys();

            AuthToken token;

            _tokens.TryGetValue(toGet, out token);

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
                case ExchangeType.Coinbase:
                    return new Coinbase(
                        token.ApiKey,
                        token.Secret,
                        token.ClientId
                        );

                default:
                    return null;
            }
        }

        /*
        public static AuthToken GetBitfinexAuthToken()
        {
            if (_tokens == null)
                LoadApiKeys();

            return _tokens[ExchangeType.Bitfinex];
        }

        public static AuthToken GetBitstampAuthToken()
        {
            if (_tokens == null)
                LoadApiKeys();

            return _tokens[ExchangeType.BitStamp];
        }*/
    }

    internal class AuthToken
    {
        public string ApiKey { get; set; }

        public string ClientId { get; set; }

        public string Secret { get; set; }
    }
}