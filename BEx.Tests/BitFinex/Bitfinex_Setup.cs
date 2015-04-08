using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using BEx;

namespace BEx.UnitTests.BitfinexTests
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
            Assert.That(_testCandidate.SupportedCurrencies.Count == 4);
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.LTC));
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.DRK));
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.USD));

        }

        [Test]
        public void Bitfinex_SupportedPairs_Complete()
        {
            Assert.That(_testCandidate.SupportedTradingPairs.Count == 5);


            Assert.That(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.BTC, Currency.USD)));
            Assert.That(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.LTC, Currency.USD)));
            Assert.That(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.LTC, Currency.BTC)));
            Assert.That(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.DRK, Currency.USD)));
            Assert.That(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.DRK, Currency.BTC)));
            Assert.That(_testCandidate.DefaultPair == new CurrencyTradingPair(Currency.BTC, Currency.USD));
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


    }
}
