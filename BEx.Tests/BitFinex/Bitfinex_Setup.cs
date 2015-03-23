using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using BEx;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("BitFinex.Setup")]
    public class Bitfinex_Setup
    {
        private Bitfinex _testCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Bitfinex) as Bitfinex;
        }

        [Test]
        public void Bitfinex_SupportedCurrencies_Complete()
        {
            Assert.IsTrue(_testCandidate.SupportedCurrencies.Count == 4);
            Assert.IsTrue(_testCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.IsTrue(_testCandidate.SupportedCurrencies.Contains(Currency.LTC));
            Assert.IsTrue(_testCandidate.SupportedCurrencies.Contains(Currency.DRK));
            Assert.IsTrue(_testCandidate.SupportedCurrencies.Contains(Currency.USD));

        }

        [Test]
        public void Bitfinex_SupportedPairs_Complete()
        {
            Assert.IsTrue(_testCandidate.SupportedTradingPairs.Count == 5);
        

            Assert.IsTrue(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.BTC, Currency.USD)));
            Assert.IsTrue(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.LTC, Currency.USD)));
            Assert.IsTrue(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.LTC, Currency.BTC)));
            Assert.IsTrue(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.DRK, Currency.USD)));
            Assert.IsTrue(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.DRK, Currency.BTC)));
            Assert.IsTrue(_testCandidate.DefaultPair == new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void Bitfinex_Commands_Complete()
        {
            Assert.IsNotNull(_testCandidate.Commands);
            Assert.IsNotNull(_testCandidate.Commands.BuyOrder);
            Assert.IsNotNull(_testCandidate.Commands.AccountBalance);
            Assert.IsNotNull(_testCandidate.Commands.CancelOrder);
            Assert.IsNotNull(_testCandidate.Commands.DepositAddress);
            Assert.IsNotNull(_testCandidate.Commands.OpenOrders);
            Assert.IsNotNull(_testCandidate.Commands.OrderBook);
            Assert.IsNotNull(_testCandidate.Commands.SellOrder);
            Assert.IsNotNull(_testCandidate.Commands.Tick);
            Assert.IsNotNull(_testCandidate.Commands.Transactions);
            Assert.IsNotNull(_testCandidate.Commands.UserTransactions);
        }

        [Test]
        public void Bitfinex_Authenticator_Complete()
        {
            Assert.IsNotNull(_testCandidate.Authenticator);

        }

        [Test]
        public void Bitfinex_Nonce_Sequential()
        {
            long _nonce = _testCandidate.Configuration.Nonce;

            for (int i = 0; i < 10; ++i)
            {
                long _nextNonce = _testCandidate.Configuration.Nonce;

                Assert.IsTrue(_nextNonce - _nonce == 1);

                _nonce = _nextNonce;
            }

         
        }


    }
}
