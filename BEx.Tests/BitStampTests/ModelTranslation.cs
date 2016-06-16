using System.IO;
using NUnit.Framework;
using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.API.Commands;
using BEx.Exchanges.BitStamp.API;

namespace BEx.Tests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.ModelTranslation")]
    public class BitStampModelTranslation
    {
        private ResultTranslation _translator = new ResultTranslation(ExchangeType.BitStamp);
        private IExchangeCommandFactory _commands = CommandFactory.Singleton;
        private TradingPair _btdUsd = new TradingPair(Currency.BTC, Currency.USD);

        [Test]
        public void AccountBalanceModel()
        {
            Assert.Fail();
        }

        [Test]
        public void BalanceModel()
        {
            Assert.Fail();
        }

        [Test]
        public void ConfirmationModel()
        {
            Assert.Fail();
        }

        [Test]
        public void DayRangeModel()
        {
            Assert.Fail("Invalid for BitStamp, this is just returning a Tick in disguise");
        }

        [Test]
        public void OpenOrdersModel()
        {
            Assert.Fail();
        }

        [Test]
        
        public void OrderBookModel()
        {
            var raw = RawJson.OrderBookResponse;

            var translated = _translator.Translate<OrderBook>(raw, _commands.OrderBook, _btdUsd);

            ResponseVerification.VerifyOrderBook(
                translated,
                _btdUsd,
                ExchangeType.BitStamp);
        }

        [Test]
        public void OrderModel()
        {
            Assert.Fail();
        }
        [Test]
        public void TickModel()
        {
            var raw =
                "{\"high\": \"718.67\", \"last\": \"712.11\", \"timestamp\": \"1466038054\", \"bid\": \"711.11\", \"vwap\": \"688.95\", \"volume\": \"7260.28762905\", \"low\": \"672.12\", \"ask\": \"714.90\", \"open\": 695.00}";

            var translated = _translator.Translate<Tick>(raw, _commands.Tick, _btdUsd);

            ResponseVerification.VerifyTick(translated, _btdUsd, ExchangeType.BitStamp);

            var reference = new Tick(
                714.90m,
                711.11m,
                712.11m,
                7260.28762905m,
                _btdUsd,
                ExchangeType.BitStamp,
                1466038054);

            Assert.AreEqual(translated, reference);
        }

        [Test]
        public void TransactionsModel()
        {
            var raw = RawJson.TransactionsResponse;

            var translated = _translator.Translate<Transactions>(raw, _commands.Transactions, _btdUsd);

            ResponseVerification.VerifyTransactions(translated, ExchangeType.BitStamp, _btdUsd);
        }

        [Test]
        public void UserTransactionsModel()
        {
            Assert.Fail();
            
        }
    }
}