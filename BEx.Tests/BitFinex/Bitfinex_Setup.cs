using NUnit.Framework;

namespace BEx.UnitTests.BitfinexTests
{
    [TestFixture]
    [Category("BitFinex.Setup")]
    public class Bitfinex_Setup
    {
        private Bitfinex testCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Bitfinex) as Bitfinex;
        }

        


        [Test]
        public void Bitfinex_SupportedCurrencies_Complete()
        {
            Assert.That(testCandidate.SupportedCurrencies.Count == 3);
            Assert.That(testCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.That(testCandidate.SupportedCurrencies.Contains(Currency.LTC));
            Assert.That(testCandidate.SupportedCurrencies.Contains(Currency.USD));
        }

        [Test]
        public void Bitfinex_SupportedPairs_Complete()
        {
            Assert.That(testCandidate.SupportedTradingPairs.Count == 3);

            Assert.That(testCandidate.IsTradingPairSupported(new TradingPair(Currency.BTC, Currency.USD)));
            Assert.That(testCandidate.IsTradingPairSupported(new TradingPair(Currency.LTC, Currency.USD)));
            Assert.That(testCandidate.IsTradingPairSupported(new TradingPair(Currency.LTC, Currency.BTC)));
            Assert.That(testCandidate.DefaultPair == new TradingPair(Currency.BTC, Currency.USD));
        }
    }
}