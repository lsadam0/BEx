using BEx.Exchanges.Gdax.API;
using NUnit.Framework;

namespace BEx.Tests.GdaxTests
{
    [TestFixture]
    [Category("Gdax.Setup")]
    internal class GdaxSetup : ConfigurationVerificationBase
    {
        public GdaxSetup() : base(ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Gdax))
        {
        }

        [Test]
        public void Gdax_SupportedCurrencies_Complete()
        {
            Assert.That(TestCandidate.SupportedCurrencies.Count == 3);
            Assert.That(TestCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.That(TestCandidate.SupportedCurrencies.Contains(Currency.LTC));
            Assert.That(TestCandidate.SupportedCurrencies.Contains(Currency.USD));
        }

        [Test]
        public void Gdax_SupportedPairs_Complete()
        {
            Assert.That(TestCandidate.SupportedTradingPairs.Count == 3);

            Assert.That(TestCandidate.IsTradingPairSupported(new TradingPair(Currency.BTC, Currency.USD)));
            Assert.That(TestCandidate.IsTradingPairSupported(new TradingPair(Currency.LTC, Currency.USD)));
            Assert.That(TestCandidate.IsTradingPairSupported(new TradingPair(Currency.LTC, Currency.BTC)));
            Assert.That(TestCandidate.DefaultPair == new TradingPair(Currency.BTC, Currency.USD));
        }


        [Test]
        public void Gdax_AllCommandsPresent()
        {
            base.AllCommandsPresent(CommandFactory.Singleton);
        }

        [Test]
        public void Gdax_ConfigurationValid()
        {
            base.VerifyConfiguration(BEx.Exchanges.Gdax.Configuration.Singleton);
        }
    }
}