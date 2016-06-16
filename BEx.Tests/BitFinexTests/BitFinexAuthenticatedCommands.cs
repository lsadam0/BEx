// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.Tests.BitfinexTests
{
    [TestFixture]
    [Category("Bitfinex.AuthenticatedCommands")]
    public class BitfinexAuthenticatedCommands : ExchangeVerificationBase
    {
        public BitfinexAuthenticatedCommands()
            : base(ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Bitfinex))
        {
        }

        [Test]
        public void Bitfinex_GetAccountBalance_Success()
        {
            CommandVerification.RetrieveAndVerifyAccountBalance();
        }

        [Test]
        public void Bitfinex_GetOpenOrders_Success()
        {
            CommandVerification.RetrieveAndVerifyOpenOrders();
        }

        [Test]
        public void Bitfinex_GetUserTransactions_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyUserTransactions(TestCandidate.DefaultPair);
        }

        [Test]
        public void Bitfinex_GetUserTransactions_LTCBTC_Success()
        {
            CommandVerification.RetrieveAndVerifyUserTransactions(new TradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetUserTransactions_LTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyUserTransactions(new TradingPair(Currency.LTC, Currency.USD));
        }
    }
}