// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx;
using NUnit.Framework;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("Bitfinex.UnauthenticatedCommands")]
    public class BitFinex_Unauthenticated_Commands : ExchangeVerificationBase
    {
        private ExchangeCommandVerification commandVerification;
        public BitFinex_Unauthenticated_Commands()
            : base()
        {
            testCandidate = ExchangeFactory.GetUnauthenticatedExchange(ExchangeType.Bitfinex) as Exchange;
            commandVerification = new ExchangeCommandVerification(testCandidate);
        }

        [Test]
        public void Bitfinex_GetTick_BTCUSD_Success()
        {
            commandVerification.VerifyTick(new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTick_LTCUSD_Success()
        {
            commandVerification.VerifyTick(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTick_LTCBTC_Success()
        {
            commandVerification.VerifyTick(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetTick_DRKUSD_Success()
        {
            commandVerification.VerifyTick(new CurrencyTradingPair(Currency.DRK, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTick_DRKBTC_Success()
        {
            commandVerification.VerifyTick(new CurrencyTradingPair(Currency.DRK, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetOrderBook_BTCUSD_Success()
        {
            commandVerification.VerifyOrderBook(testCandidate.DefaultPair);
        }

        [Test]
        public void Bitfinex_GetOrderBook_LTCUSD_Success()
        {
            commandVerification.VerifyOrderBook(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetOrderBook_DRKUSD_Success()
        {
            commandVerification.VerifyOrderBook(new CurrencyTradingPair(Currency.DRK, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetOrderBook_DRKBTC_Success()
        {
            commandVerification.VerifyOrderBook(new CurrencyTradingPair(Currency.DRK, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetOrderBook_LTCBTC_Success()
        {
            commandVerification.VerifyOrderBook(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTransactions_DRKBTC_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.DRK, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetTransactions_DRKUSD_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.DRK, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTransactions_LTCBTC_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetTransactions_LTCUSD_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }
    }
}