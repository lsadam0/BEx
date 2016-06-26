using BEx.Exchanges.BitStamp;
using BEx.Exchanges.BitStamp.API;
using NUnit.Framework;

namespace BEx.Tests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.ModelTranslation")]
    internal class ModelTranslation : ModelTranslationBase
    {
        public ModelTranslation()
            : base(
                  Exchanges.BitStamp.Configuration.Singleton,
                  new TradingPair(Currency.BTC, Currency.USD),
                  CommandFactory.Singleton)
        { }

        [Test]
        public void AccountBalanceModel()
        {
            base.AccountBalanceModel(
                "{\"btc_reserved\": \"0.00000000\", \"fee\": \"0.2500\", \"btc_available\": \"0.03117682\", \"eur_balance\": \"0.00\", \"btc_balance\": \"0.03117682\", \"eur_available\": \"0.00\", \"usd_balance\": \"0.00\", \"usd_reserved\": \"0.00\", \"eur_reserved\": \"0.00\", \"usd_available\": \"0.00\"}");
        }

        [Test]
        public void ErrorModel()
        {
            Assert.Fail();

            var raw = "{\"error\": {\"__all__\": [\"You have only 0.03117682 BTC available. Check your account balance for details.\"]}}";

            {
            }
        }

        [Test]
        public void OpenOrdersModel()
        {
            base.OpenOrdersModel(
                   "[{\"price\": \"750.00\", \"amount\": \"0.03117682\", \"type\": 1, \"id\": 133544504, \"datetime\": \"2016-06-26 15:04:41\"}, {\"price\": \"750.00\", \"amount\": \"0.03117682\", \"type\": 1, \"id\": 133544505, \"datetime\": \"2016-06-26 15:04:41\"}]");
        }

        [Test]
        public void OrderBookModel()
        {
            base.OrderBookModel(RawJson.OrderBookResponse);
        }

        [Test]
        public void OrderConfirmationModel()
        {
            base.OrderConfirmationModel(
                "{\"price\": \"750.00\", \"amount\": \"0.03117682\", \"type\": 1, \"id\": 133544504, \"datetime\": \"2016-06-26 15:04:41.132781\"}",
                CommandFactory.Singleton.BuyOrder,
                OrderType.Buy);
        }

        [Test]
        public void TickModel()
        {
            base.TickModel(
                "{\"high\": \"718.67\", \"last\": \"712.11\", \"timestamp\": \"1466038054\", \"bid\": \"711.11\", \"vwap\": \"688.95\", \"volume\": \"7260.28762905\", \"low\": \"672.12\", \"ask\": \"714.90\", \"open\": 695.00}");
        }

        [Test]
        public void TransactionsModel()
        {
            base.TransactionsModel(RawJson.TransactionsResponse);
        }

        [Test]
        public void UserTransactionsModel()
        {
            base.UserTransactionsModel(
                "[{\"usd\": \"-13.98\", \"btc\": \"0.03117682\", \"btc_usd\": \"448.41\", \"order_id\": 125469781, \"fee\": \"0.04\", \"type\": 2, \"id\": 11188576, \"datetime\": \"2016-05-19 16:58:40\"}, {\"usd\": \"1.40\", \"btc\": \"-0.00342331\", \"btc_usd\": \"408.65\", \"order_id\": 115474006, \"fee\": \"0.01\", \"type\": 2, \"id\": 10745104, \"datetime\": \"2016-03-06 18:29:56\"}, {\"usd\": \"12.67\", \"btc\": \"-0.03100377\", \"btc_usd\": \"408.65\", \"order_id\": 115474006, \"fee\": \"0.04\", \"type\": 2, \"id\": 10745103, \"datetime\": \"2016-03-06 18:29:55\"}]");
        }
    }
}