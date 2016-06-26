using NUnit.Framework;
using BEx.Exchanges.BitStamp.API;

namespace BEx.Tests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.Setup")]
    internal class Configuration : ConfigurationVerificationBase
    {


        public Configuration() : base(ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp))
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
            Assert.That(TestCandidate.SupportedCurrencies.Count == 3);
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

        [Test]
        public void BitStamp_AllCommandsPresent()
        {
            base.AllCommandsPresent(CommandFactory.Singleton);
        }

        [Test]
        public void BitStamp_ConfigurationValid()
        {
            base.VerifyConfiguration(BEx.Exchanges.BitStamp.Configuration.Singleton);
        }
    }
}