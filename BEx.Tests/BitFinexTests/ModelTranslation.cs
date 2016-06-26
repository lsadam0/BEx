using BEx.Exchanges.Bitfinex;
using BEx.Exchanges.Bitfinex.API;
using NUnit.Framework;

namespace BEx.Tests.BitFinexTests
{
    [TestFixture]
    [Category("BitFinex.ModelTranslation")]
    internal class ModelTranslation : ModelTranslationBase
    {
        public ModelTranslation() :
        base(
            Configuration.Singleton,
            new TradingPair(Currency.BTC, Currency.USD),
            CommandFactory.Singleton)
        { }

        [Test]
        public void AccountBalanceModel()
        {
            base.AccountBalanceModel(
                "[{\r\n  \"type\":\"deposit\",\r\n  \"currency\":\"btc\",\r\n  \"amount\":\"0.0\",\r\n  \"available\":\"0.0\"\r\n},{\r\n  \"type\":\"deposit\",\r\n  \"currency\":\"usd\",\r\n  \"amount\":\"1.0\",\r\n  \"available\":\"1.0\"\r\n},{\r\n  \"type\":\"exchange\",\r\n  \"currency\":\"btc\",\r\n  \"amount\":\"1\",\r\n  \"available\":\"1\"\r\n},{\r\n  \"type\":\"exchange\",\r\n  \"currency\":\"usd\",\r\n  \"amount\":\"1\",\r\n  \"available\":\"1\"\r\n},{\r\n  \"type\":\"trading\",\r\n  \"currency\":\"btc\",\r\n  \"amount\":\"1\",\r\n  \"available\":\"1\"\r\n},{\r\n  \"type\":\"trading\",\r\n  \"currency\":\"usd\",\r\n  \"amount\":\"1\",\r\n  \"available\":\"1\"\r\n}]");
        }

        [Test]
        public void OpenOrdersModel()
        {
            base.OpenOrdersModel("[{\r\n  \"id\":448411365,\r\n  \"symbol\":\"btcusd\",\r\n  \"exchange\":\"bitfinex\",\r\n  \"price\":\"0.02\",\r\n  \"avg_execution_price\":\"0.0\",\r\n  \"side\":\"buy\",\r\n  \"type\":\"exchange limit\",\r\n  \"timestamp\":\"1444276597.0\",\r\n  \"is_live\":true,\r\n  \"is_cancelled\":false,\r\n  \"is_hidden\":false,\r\n  \"was_forced\":false,\r\n  \"original_amount\":\"0.02\",\r\n  \"remaining_amount\":\"0.02\",\r\n  \"executed_amount\":\"0.0\"\r\n}]");
        }

        [Test]
        public void OrderBookModel()
        {
            base.OrderBookModel("{\"bids\":[{\"price\":\"621.02\",\"amount\":\"2.90479\",\"timestamp\":\"1466957411.0\"},{\"price\":\"620.6\",\"amount\":\"7.9932\",\"timestamp\":\"1466957435.0\"}],\r\n\"asks\":[{\"price\":\"621.23\",\"amount\":\"18.5929\",\"timestamp\":\"1466957435.0\"},{\"price\":\"621.24\",\"amount\":\"6.0\",\"timestamp\":\"1466957433.0\"},{\"price\":\"621.26\",\"amount\":\"16.5804\",\"timestamp\":\"1466957433.0\"}]}");
        }

        [Test]
        public void OrderConfirmationModel()
        {
            base.OrderConfirmationModel(
                "{\r\n  \"id\":448364249,\r\n  \"symbol\":\"btcusd\",\r\n  \"exchange\":\"bitfinex\",\r\n  \"price\":\"0.01\",\r\n  \"avg_execution_price\":\"0.0\",\r\n  \"side\":\"buy\",\r\n  \"type\":\"exchange limit\",\r\n  \"timestamp\":\"1444272165.252370982\",\r\n  \"is_live\":true,\r\n  \"is_cancelled\":false,\r\n  \"is_hidden\":false,\r\n  \"was_forced\":false,\r\n  \"original_amount\":\"0.01\",\r\n  \"remaining_amount\":\"0.01\",\r\n  \"executed_amount\":\"0.0\",\r\n  \"order_id\":448364249\r\n}",
                CommandFactory.Singleton.BuyOrder,
                OrderType.Buy);
        }

        [Test]
        public void TickModel()
        {
            base.TickModel(
                "{\r\n  \"mid\":\"244.755\",\r\n  \"bid\":\"244.75\",\r\n  \"ask\":\"244.76\",\r\n  \"last_price\":\"244.82\",\r\n  \"low\":\"244.2\",\r\n  \"high\":\"248.19\",\r\n  \"volume\":\"7842.11542563\",\r\n  \"timestamp\":\"1444253422.348340958\"\r\n}");
        }

        [Test]
        public void TransactionsModel()
        {
            base.TransactionsModel("[{\r\n  \"timestamp\":1444266681,\r\n  \"tid\":11988919,\r\n  \"price\":\"244.8\",\r\n  \"amount\":\"0.03297384\",\r\n  \"exchange\":\"bitfinex\",\r\n  \"type\":\"sell\"\r\n}]");
        }

        [Test]
        public void UserTransactionsModel()
        {
            base.UserTransactionsModel("[{\r\n  \"price\":\"246.94\",\r\n  \"amount\":\"1.0\",\r\n  \"timestamp\":\"1444141857.0\",\r\n  \"exchange\":\"\",\r\n  \"type\":\"Buy\",\r\n  \"fee_currency\":\"USD\",\r\n  \"fee_amount\":\"-0.49388\",\r\n  \"tid\":11970839,\r\n  \"order_id\":446913929\r\n}]");
        }
    }
}