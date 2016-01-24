using NUnit.Framework;

namespace BEx.Tests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.Setup")]
    public class BitStampSetup : ExchangeVerificationBase
    {
        public BitStampSetup() : base(ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp))
        {
        }

        [TestFixtureSetUp]
        public void TestSetup()
        {
            Assert.IsInstanceOf<BitStamp>(TestCandidate);
        }

        [Test]
        public void BitStamp_SupportedCurrencies_Complete()
        {
            Assert.That(TestCandidate.SupportedCurrencies.Count == 2);
            Assert.That(TestCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.That(TestCandidate.SupportedCurrencies.Contains(Currency.USD));
        }

        [Test]
        public void BitStamp_SupportedPairs_Complete()
        {
            Assert.That(TestCandidate.SupportedTradingPairs.Count == 1);

            Assert.That(TestCandidate.IsTradingPairSupported(new TradingPair(Currency.BTC, Currency.USD)));
            Assert.That(TestCandidate.DefaultPair == new TradingPair(Currency.BTC, Currency.USD));
        }
    }
}