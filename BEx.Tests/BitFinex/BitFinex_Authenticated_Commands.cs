// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.UnitTests.BitfinexTests
{
    [TestFixture]
    [Category("Bitfinex.AuthenticatedCommands")]
    public class Bitfinex_Authenticated_Commands
    {
        private ExchangeCommandVerification commandVerification;
        private Bitfinex TestCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            TestCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Bitfinex) as Bitfinex;

            commandVerification = new ExchangeCommandVerification(TestCandidate);
        }

        [Test]
        public void Bitfinex_GetAccountBalance_Success()
        {
            commandVerification.VerifyAccountBalance();
        }

        [Test]
        public void Bitfinex_GetDepositAddress_BTC_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.BTC);
        }

        [Test]
        public void Bitfinex_GetDepositAddress_DRK_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.DRK);
        }

        [Test]
        public void Bitfinex_GetDepositAddress_LTC_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.LTC);
        }

        [Test]
        public void Bitfinex_GetOpenOrders_Success()
        {
            commandVerification.VerifyOpenOrders();
        }

        [Test]
        public void Bitfinex_GetUserTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyUserTransactions(TestCandidate.DefaultPair);
        }

        [Test]
        public void Bitfinex_GetUserTransactions_LTCBTC_Success()
        {
            commandVerification.VerifyUserTransactions(new TradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetUserTransactions_LTCUSD_Success()
        {
            commandVerification.VerifyUserTransactions(new TradingPair(Currency.LTC, Currency.USD));
        }
    }
}