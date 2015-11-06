using BEx.UnitTests.MockTests.MockObjects;
using NUnit.Framework;

namespace BEx.UnitTests.MockTests
{
    [TestFixture]
    [Category("A.MockExchange.AuthenticatedCommands")]
    public class MockExchange_AuthenticatedCommands
    {
        private MockExchange testCandidate;
        private ExchangeCommandVerification commandVerification;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Mock) as MockExchange;
            commandVerification = new ExchangeCommandVerification(testCandidate);
        }

        [Test]
        public void MockExchange_GetAccountBalance_Success()
        {
            commandVerification.VerifyAccountBalance();
        }

        [Test]
        public void MockExchange_GetDepositAddress_BTC_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.BTC);
        }

        [Test]
        public void MockExchange_GetDepositAddress_LTC_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.LTC);
        }

        [Test]
        public void MockExchange_GetUserTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyUserTransactions(testCandidate.DefaultPair);
        }

        [Test]
        public void MockExchange_GetUserTransactions_LTCBTC_Success()
        {
            commandVerification.VerifyUserTransactions(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void MockExchange_GetOpenOrders_Success()
        {
            commandVerification.VerifyOpenOrders();
        }
    }
}