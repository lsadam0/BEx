using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using BEx;

namespace BEx.UnitTests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.Setup")]
    public class BitStamp_Setup
    {
        private BitStamp _testCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp) as BitStamp;
        }

        [Test]
        public void Bitstamp_ImplementsInterfaces()
        {
            Assert.IsInstanceOf<IUnauthenticatedExchange>(_testCandidate);
            Assert.IsInstanceOf<IAuthenticatedExchange>(_testCandidate);
        }

        [Test]
        public void BitStamp_SupportedCurrencies_Complete()
        {
            Assert.That(_testCandidate.SupportedCurrencies.Count == 2);
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.That(_testCandidate.SupportedCurrencies.Contains(Currency.USD));

        }

        [Test]
        public void BitStamp_SupportedPairs_Complete()
        {
            Assert.That(_testCandidate.SupportedTradingPairs.Count == 1);

            Assert.That(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.BTC, Currency.USD)));
            Assert.That(_testCandidate.DefaultPair == new CurrencyTradingPair(Currency.BTC, Currency.USD));
        }

        [Test]
        public void BitStamp_Commands_Complete()
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
        public void BitStamp_Authenticator_Complete()
        {
            Assert.IsNotNull(_testCandidate.Authenticator);

        }

       

    }
}
