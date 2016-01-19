// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.UnitTests.BitfinexTests
{
    [TestFixture]
    [Category("Bitfinex.UnauthenticatedCommands")]
    public class BitFinex_Unauthenticated_Commands
    {
        private ExchangeCommandVerification commandVerification;
        private Bitfinex TestCandidate;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            TestCandidate = ExchangeFactory.GetUnauthenticatedExchange(ExchangeType.Bitfinex) as Bitfinex;
            Assert.IsInstanceOf<Bitfinex>(TestCandidate);
            commandVerification = new ExchangeCommandVerification(TestCandidate);
        }

        [Test]
        public void Bitfinex_GetTick_BTCUSD_Success()
        {
            commandVerification.VerifyTick(new TradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTick_LTCUSD_Success()
        {
            commandVerification.VerifyTick(new TradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTick_LTCBTC_Success()
        {
            commandVerification.VerifyTick(new TradingPair(Currency.LTC, Currency.BTC));
        }
        

        [Test]
        public void Bitfinex_GetOrderBook_BTCUSD_Success()
        {
            commandVerification.VerifyOrderBook(TestCandidate.DefaultPair);
        }

        [Test]
        public void Bitfinex_GetOrderBook_LTCUSD_Success()
        {
            commandVerification.VerifyOrderBook(new TradingPair(Currency.LTC, Currency.USD));
        }
        
        

        [Test]
        public void Bitfinex_GetOrderBook_LTCBTC_Success()
        {
            commandVerification.VerifyOrderBook(new TradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyTransactions(new TradingPair(Currency.BTC, Currency.USD));
        }

        /*
        [Test]
        public void Bitfinex_GetTransactions_DRKBTC_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.DRK, Currency.BTC));
        }*/

        /*
        [Test]
        public void Bitfinex_GetTransactions_DRKUSD_Success()
        {
            commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.DRK, Currency.USD));
        }
        */

        [Test]
        public void Bitfinex_GetTransactions_LTCBTC_Success()
        {
            commandVerification.VerifyTransactions(new TradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetTransactions_LTCUSD_Success()
        {
            commandVerification.VerifyTransactions(new TradingPair(Currency.LTC, Currency.USD));
        }
    }
}