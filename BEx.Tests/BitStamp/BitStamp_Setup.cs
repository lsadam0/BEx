using NUnit.Framework;

namespace BEx.UnitTests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.Setup")]
    public class BitStamp_Setup
    {
        private BitStamp testCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp) as BitStamp;
        }

        [Test]
        public void BitStamp_SupportedCurrencies_Complete()
        {
            Assert.That(testCandidate.SupportedCurrencies.Count == 2);
            Assert.That(testCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.That(testCandidate.SupportedCurrencies.Contains(Currency.USD));
        }

        [Test]
        public void BitStamp_SupportedPairs_Complete()
        {
            Assert.That(testCandidate.SupportedTradingPairs.Count == 1);

            Assert.That(testCandidate.IsTradingPairSupported(new TradingPair(Currency.BTC, Currency.USD)));
            Assert.That(testCandidate.DefaultPair == new TradingPair(Currency.BTC, Currency.USD));
        }
    }
}