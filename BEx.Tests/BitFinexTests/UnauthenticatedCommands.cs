// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.Tests.BitfinexTests
{
    [TestFixture]
    [Category("Bitfinex.UnauthenticatedCommands")]
    public class BitFinexUnauthenticatedCommands : ExchangeVerificationBase
    {
        public BitFinexUnauthenticatedCommands() : base(new Bitfinex())
        {
        }

        [TestFixtureSetUp]
        public void SetupTests()
        {
            Assert.IsInstanceOf<Bitfinex>(TestCandidate);
        }

        /*
        [Test]
        public void Bitfinex_GetTransactions_LTCBTC_Success()
        {
            CommandVerification.RetrieveAndVerifyTransactions(new TradingPair(Currency.LTC, Currency.BTC));
        }*/

        

        [Test]
        public void Bitfinex_GetOrderBook_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyOrderBook(TestCandidate.DefaultPair);
        }

        [Test]
        public void Bitfinex_GetOrderBook_LTCBTC_Success()
        {
            CommandVerification.RetrieveAndVerifyOrderBook(new TradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetOrderBook_LTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyOrderBook(new TradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTick_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyTick(new TradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTick_LTCBTC_Success()
        {
            CommandVerification.RetrieveAndVerifyTick(new TradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void Bitfinex_GetTick_LTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyTick(new TradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTransactions_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyTransactions(new TradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_GetTransactions_LTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyTransactions(new TradingPair(Currency.LTC, Currency.USD));
        }
    }
}