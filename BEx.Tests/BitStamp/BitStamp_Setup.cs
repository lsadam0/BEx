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
        public void BitStamp_SupportedCurrencies_Complete()
        {
            Assert.IsTrue(_testCandidate.SupportedCurrencies.Count == 2);
            Assert.IsTrue(_testCandidate.SupportedCurrencies.Contains(Currency.BTC));
            Assert.IsTrue(_testCandidate.SupportedCurrencies.Contains(Currency.USD));

        }

        [Test]
        public void BitStamp_SupportedPairs_Complete()
        {
            Assert.IsTrue(_testCandidate.SupportedTradingPairs.Count == 1);

            Assert.IsTrue(_testCandidate.IsTradingPairSupported(new CurrencyTradingPair(Currency.BTC, Currency.USD)));
            Assert.IsTrue(_testCandidate.DefaultPair == new CurrencyTradingPair(Currency.BTC, Currency.USD));
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

        [Test]
        public void BitStamp_Nonce_Sequential()
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
