using BEx.UnitTests.MockTests.MockObjects;
using NUnit.Framework;

namespace BEx.UnitTests.MockTests
{
    [TestFixture]
    [Category("A.MockExchange.UnauthenticatedCommands")]
    public class MockExchange_UnauthenticatedCommands
    {
        private MockExchange testCandidate;
        private ExchangeCommandVerification commandVerification;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            testCandidate = ExchangeFactory.GetUnauthenticatedExchange(ExchangeType.Mock) as MockExchange;
            commandVerification = new ExchangeCommandVerification(testCandidate);
        }

        [Test]
        public void MockExchange_GetTick_BTCUSD_Success()
        {
            commandVerification.VerifyTick(new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void MockExchange_GetTick_LTCUSD_Success()
        {
            commandVerification.VerifyTick(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void MockExchange_GetTick_LTCBTC_Success()
        {
            commandVerification.VerifyTick(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void MockExchange_GetOrderBook_BTCUSD_Success()
        {
            commandVerification.VerifyOrderBook(testCandidate.DefaultPair);
        }

        [Test]
        public void MockExchange_GetOrderBook_LTCUSD_Success()
        {
            commandVerification.VerifyOrderBook(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void MockExchange_GetOrderBook_LTCBTC_Success()
        {
            commandVerification.VerifyOrderBook(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void MockExchange_GetTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void MockExchange_GetTransactions_LTCBTC_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void MockExchange_GetTransactions_LTCUSD_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }
    }
}