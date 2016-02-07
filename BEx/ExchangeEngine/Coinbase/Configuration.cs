using System;
using System.Collections.Immutable;

namespace BEx.ExchangeEngine.Coinbase
{
    public class Configuration : IExchangeConfiguration
    {
        private static readonly Configuration instance = new Configuration();

        internal Configuration()
        {
            BaseUri = new Uri(@"https://api.exchange.coinbase.com");
            DefaultPair = new TradingPair(Currency.BTC, Currency.USD);
            ExchangeSourceType = ExchangeType.Coinbase;
            SupportedCurrencies = ImmutableHashSet.Create(Currency.BTC, Currency.USD);
            SupportedPairs = ImmutableHashSet.Create(DefaultPair);
        }

        public static IExchangeConfiguration Singleton => instance;

        public Uri BaseUri { get; }

        public TradingPair DefaultPair { get; }

        public Type ErrorJsonType { get; }

        public ExchangeType ExchangeSourceType { get; }

        public ImmutableHashSet<Currency> SupportedCurrencies { get; }

        public ImmutableHashSet<TradingPair> SupportedPairs { get; }
    }
}