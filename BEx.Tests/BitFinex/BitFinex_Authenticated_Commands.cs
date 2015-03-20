// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx;
using NUnit.Framework;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("Bitfinex.AuthenticatedCommands")]
    public class Bitfinex_Authenticated_Commands : ExchangeVerificationBase
    {
        private ExchangeCommandVerification commandVerification;
        public Bitfinex_Authenticated_Commands()
            : base()
        {
            testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Bitfinex) as Exchange;
            commandVerification = new ExchangeCommandVerification(testCandidate);
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
        public void Bitfinex_GetDepositAddress_LTC_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.LTC);
        }

        [Test]
        public void Bitfinex_GetDepositAddress_DRK_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.DRK);
        }

        [Test]
        public void Bitfinex_GetUserTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyUserTransactions(testCandidate.DefaultPair);
        }

        [Test]
        public void Bitfinex_GetUserTransactions_DRKBTC_Success()
        {
            commandVerification.VerifyUserTransactions(new CurrencyTradingPair(Currency.DRK, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetUserTransactions_DRKUSD_Success()
        {
            commandVerification.VerifyUserTransactions(new CurrencyTradingPair(Currency.DRK, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetUserTransactions_LTCBTC_Success()
        {
            commandVerification.VerifyUserTransactions(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetUserTransactions_LTCUSD_Success()
        {
            commandVerification.VerifyUserTransactions(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetOpenOrders_Success()
        {
            commandVerification.VerifyOpenOrders();
        }
    }
}