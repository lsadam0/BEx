using BEx.UnitTests.MockTests.MockObjects;
using NUnit.Framework;

namespace BEx.UnitTests.MockTests
{
    [TestFixture]
    [Category("A.MockExchange.Setup")]
    public class MockExchange_Setup
    {
        private MockExchange _testCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Mock) as MockExchange;
        }

        [Test]
        public void MockExchange_SupportedCurrencies_Complete()
        {
            Assert.That(_testCandidate.SupportedCurrencies.Count == 3);
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.USD));
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.LTC));
        }

        [Test]
        public void MockExchange_SupportedPairs_Complete()
        {
            Assert.That(_testCandidate.SupportedTradingPairs.Count == 3);

            Assert.That(_testCandidate.IsTradingPairSupported(new TradingPair(Currency.BTC, Currency.USD)));
            Assert.That(_testCandidate.DefaultPair == new TradingPair(Currency.BTC, Currency.USD));
            Assert.That(_testCandidate.IsTradingPairSupported(new TradingPair(Currency.LTC, Currency.USD)));
            Assert.That(_testCandidate.IsTradingPairSupported(new TradingPair(Currency.LTC, Currency.BTC)));
        }

        [Test]
        public void MockExchange_Commands_Complete()
        {
            Assert.IsNotNull(_testCandidate.Commands);
            Assert.IsNotNull(_testCandidate.Commands.BuyOrder);
            Assert.IsNotNull(_testCandidate.Commands.AccountBalance);
            Assert.IsNotNull(_testCandidate.Commands.CancelOrder);
            Assert.IsNotNull(_testCandidate.Commands.DepositAddress);
            Assert.IsNotNull(_testCandidate.Commands.OpenOrders);
            Assert.IsNotNull(_testCandidate.Commands.OrderBook);
            Assert.IsNotNull(_testCandidate.Commands.SellOrder);
            Assert.IsNotNull(_testCandidate.Commands.Tick);
            Assert.IsNotNull(_testCandidate.Commands.Transactions);
            Assert.IsNotNull(_testCandidate.Commands.UserTransactions);
        }

        [Test]
        public void MockExchange_Authenticator_Complete()
        {
            Assert.IsNotNull(_testCandidate.Authenticator);
        }
    }
}