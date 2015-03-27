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
    [Category("A.MockExchange.AuthenticatedCommands")]
    public class MockExchange_AuthenticatedCommands
    {
        private MockExchange _testCandidate;
        private ExchangeCommandVerification _commandVerification;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Mock) as MockExchange;
            _commandVerification = new ExchangeCommandVerification(_testCandidate);
        }

        [Test]
        public void MockExchange_GetAccountBalance_Success()
        {
            _commandVerification.VerifyAccountBalance();
        }

        [Test]
        public void MockExchange_GetDepositAddress_BTC_Success()
        {
            _commandVerification.VerifyDepositAddress(Currency.BTC);
        }

        [Test]
        public void MockExchange_GetDepositAddress_LTC_Success()
        {
            _commandVerification.VerifyDepositAddress(Currency.LTC);
        }


        [Test]
        public void MockExchange_GetUserTransactions_BTCUSD_Success()
        {
            _commandVerification.VerifyUserTransactions(_testCandidate.DefaultPair);
        }

        [Test]
        public void MockExchange_GetUserTransactions_LTCBTC_Success()
        {
            _commandVerification.VerifyUserTransactions(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
        }

        [Test]
        public void MockExchange_GetOpenOrders_Success()
        {
            _commandVerification.VerifyOpenOrders();
        }
    }
}