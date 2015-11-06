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

            Assert.That(testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.BTC, Currency.USD)));
            Assert.That(testCandidate.DefaultPair == new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void BitStamp_Commands_Complete()
        {
            Assert.IsNotNull(testCandidate.Commands);
            Assert.IsNotNull(testCandidate.Commands.BuyOrder);
            Assert.IsNotNull(testCandidate.Commands.AccountBalance);
            Assert.IsNotNull(testCandidate.Commands.CancelOrder);
            Assert.IsNotNull(testCandidate.Commands.DepositAddress);
            Assert.IsNotNull(testCandidate.Commands.OpenOrders);
            Assert.IsNotNull(testCandidate.Commands.OrderBook);
            Assert.IsNotNull(testCandidate.Commands.SellOrder);
            Assert.IsNotNull(testCandidate.Commands.Tick);
            Assert.IsNotNull(testCandidate.Commands.Transactions);
            Assert.IsNotNull(testCandidate.Commands.UserTransactions);
        }

        [Test]
        public void BitStamp_Authenticator_Complete()
        {
            Assert.IsNotNull(testCandidate.Authenticator);
        }
    }
}