using BEx.ExchangeEngine.API;
using BEx.Exchanges.Gdax;
using BEx.Exchanges.Gdax.API;
using NUnit.Framework;

namespace BEx.Tests.GdaxTests
{
    [TestFixture]
    [Category("Gdax.ModelTranslation")]
    internal class ModelTranslation : ModelTranslationBase
    {
        public ModelTranslation() : base(
            Configuration.Singleton,
            new TradingPair(Currency.BTC, Currency.USD),
            CommandFactory.Singleton)
        {

        }


        [Test]
        public void AccountBalanceModel()
        {
            base.AccountBalanceModel(
                "[\r\n    {\r\n        \"id\": \"a1b2c3d4\",\r\n        \"balance\": \"1.100\",\r\n        \"hold\": \"0.100\",\r\n        \"available\": \"1.00\",\r\n        \"currency\": \"USD\"\r\n    },\r\n        {\r\n        \"id\": \"a1b2c3d5\",\r\n        \"balance\": \"1.100\",\r\n        \"hold\": \"0.100\",\r\n        \"available\": \"1.00\",\r\n        \"currency\": \"BTC\"\r\n    }\r\n]");

        }

        [Test]
        public void OrderConfirmationModel()
        {
            Assert.Fail();
        }


        [Test]
        public void OpenOrdersModel()
        {
            base.OpenOrdersModel(
                "[\r\n    {\r\n        \"id\": \"d50ec984-77a8-460a-b958-66f114b0de9b\",\r\n        \"size\": \"3.0\",\r\n        \"price\": \"100.23\",\r\n        \"product_id\": \"BTC-USD\",\r\n        \"status\": \"open\",\r\n        \"filled_size\": \"1.23\",\r\n        \"executed_value\": \"3.69\",\r\n        \"fill_fees\": \"0.001\",\r\n        \"settled\": false,\r\n        \"side\": \"buy\",\r\n        \"created_at\": \"2014-11-14T06:39:55.189376Z\"\r\n    }\r\n]");
        }

        [Test]
        public void OrderBookModel()
        {
            // Not paged
            base.OrderBookModel(
                "{\r\n    \"sequence\": \"3\",\r\n    \"bids\": [\r\n        [ \"295.96\", \"4.39088265\", 2 ],\r\n\t\t        [ \"294.96\", \"3.39088265\", 2 ]\r\n    ],\r\n    \"asks\": [[ \"295.97\", \"25.23542881\", 12 ],\r\n\t\t        [ \"296.97\", \"24.23542881\", 12 ]\r\n    ]\r\n}");

        }


        [Test]
        public void TickModel()
        {
            base.TickModel(
                  "{\r\n  \"trade_id\": 4729088,\r\n  \"price\": \"333.99\",\r\n  \"size\": \"0.193\",\r\n  \"bid\": \"333.98\",\r\n  \"ask\": \"333.99\",\r\n  \"volume\": \"5957.11914015\",\r\n  \"time\": \"2015-11-14T20:46:03.511254Z\"\r\n}");


        }

        [Test]
        public void TransactionsModel()
        {
            base.TransactionsModel(
                "[{\r\n    \"time\": \"2014-11-07T22:19:28.578544Z\",\r\n    \"trade_id\": 74,\r\n    \"price\": \"10.00000000\",\r\n    \"size\": \"0.01000000\",\r\n    \"side\": \"buy\"\r\n}, {\r\n    \"time\": \"2014-11-07T01:08:43.642366Z\",\r\n    \"trade_id\": 73,\r\n    \"price\": \"100.00000000\",\r\n    \"size\": \"0.01000000\",\r\n    \"side\": \"sell\"\r\n}]");
        }

        [Test]
        public void UserTransactionsModel()
        {
            base.UserTransactionsModel(
                "[\r\n    {\r\n        \"trade_id\": 74,\r\n        \"product_id\": \"BTC-USD\",\r\n        \"price\": \"10.00\",\r\n        \"size\": \"0.01\",\r\n        \"order_id\": \"d50ec984-77a8-460a-b958-66f114b0de9b\",\r\n        \"created_at\": \"2014-11-07T22:19:28.578544Z\",\r\n        \"liquidity\": \"T\",\r\n        \"fee\": \"0.00025\",\r\n        \"settled\": true,\r\n        \"side\": \"buy\"\r\n    }\r\n]");
        }
    }
}