using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.UnitTests.MockTests.MockObjects;
using NUnit.Framework;

namespace BEx.UnitTests.MockTests
{
    [TestFixture]
    [Category("A.MockExchange.UnauthenticatedCommands")]
    public class MockExchange_UnauthenticatedCommands
    {
        private MockExchange _testCandidate;
        private ExchangeCommandVerification _commandVerification;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _testCandidate = ExchangeFactory.GetUnauthenticatedExchange(ExchangeType.Mock) as MockExchange;
            _commandVerification = new ExchangeCommandVerification(_testCandidate);
        }

        [Test]
        public void MockExchange_GetTick_BTCUSD_Success()
        {
            _commandVerification.VerifyTick(new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void MockExchange_GetTick_LTCUSD_Success()
        {
            _commandVerification.VerifyTick(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void MockExchange_GetTick_LTCBTC_Success()
        {
            _commandVerification.VerifyTick(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        
        [Test]
        public void MockExchange_GetOrderBook_BTCUSD_Success()
        {
            _commandVerification.VerifyOrderBook(_testCandidate.DefaultPair);
        }

        [Test]
        public void MockExchange_GetOrderBook_LTCUSD_Success()
        {
            _commandVerification.VerifyOrderBook(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }

        [Test]
        public void MockExchange_GetOrderBook_LTCBTC_Success()
        {
            _commandVerification.VerifyOrderBook(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void MockExchange_GetTransactions_BTCUSD_Success()
        {
            _commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void MockExchange_GetTransactions_LTCBTC_Success()
        {
            _commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void MockExchange_GetTransactions_LTCUSD_Success()
        {
            _commandVerification.VerifyTransactions(new CurrencyTradingPair(Currency.LTC, Currency.USD));
        }
    }
}
