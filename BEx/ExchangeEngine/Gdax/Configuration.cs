using System;
using System.Collections.Immutable;

namespace BEx.ExchangeEngine.Gdax
{
    public class Configuration : IExchangeConfiguration
    {
        private static readonly Configuration instance = new Configuration();

        private Configuration()
        {
            BaseUri = new Uri(@"https://api.gdax.com"); //"https://api.exchange.coinbase.com");
            WebSocketUri = new Uri(@"wss://ws-feed.gdax.com"); //"wss://ws-feed.exchange.coinbase.com");
            DefaultPair = new TradingPair(Currency.BTC, Currency.USD);
            ExchangeSourceType = ExchangeType.Gdax;
            SupportedCurrencies = ImmutableHashSet.Create(Currency.BTC, Currency.USD);
            SupportedPairs = ImmutableHashSet.Create(DefaultPair);
        }

        public static IExchangeConfiguration Singleton => instance;

        public Uri WebSocketUri { get; }

        public Uri BaseUri { get; }

        public TradingPair DefaultPair { get; }

        public Type ErrorJsonType { get; }

        public ExchangeType ExchangeSourceType { get; }

        public ImmutableHashSet<Currency> SupportedCurrencies { get; }

        public ImmutableHashSet<TradingPair> SupportedPairs { get; }
    }
}