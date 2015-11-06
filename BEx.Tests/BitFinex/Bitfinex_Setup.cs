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

            Assert.That(testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.BTC, Currency.USD)));
            Assert.That(testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.LTC, Currency.USD)));
            Assert.That(testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.LTC, Currency.BTC)));
            Assert.That(testCandidate.DefaultPair == new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_Commands_Complete()
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
        public void Bitfinex_Authenticator_Complete()
        {
            Assert.IsNotNull(testCandidate.Authenticator);
        }
    }
}