using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx.ExchangeEngine.Coinbase
{
    public class Configuration : IExchangeConfiguration
    {
        private static readonly Configuration instance = new Configuration();

        public static IExchangeConfiguration Singleton => instance;

        internal Configuration()
        {
            this.BaseUri = new Uri(@"https://api.exchange.coinbase.com");
            this.DefaultPair = new TradingPair(Currency.BTC, Currency.USD);
            this.ExchangeSourceType = ExchangeType.Coinbase;
            this.SupportedCurrencies = ImmutableHashSet.Create(Currency.BTC, Currency.USD);
            this.SupportedPairs = ImmutableHashSet.Create(this.DefaultPair);

        }

        public Uri BaseUri { get; }

        public TradingPair DefaultPair { get; }

        public Type ErrorJsonType { get; }

        public ExchangeType ExchangeSourceType { get; }

        public ImmutableHashSet<Currency> SupportedCurrencies { get; }

        public ImmutableHashSet<TradingPair> SupportedPairs { get; }
    }
}
