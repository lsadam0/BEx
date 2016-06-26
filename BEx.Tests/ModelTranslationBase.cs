using BEx.ExchangeEngine;
using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.API.Commands;
using NUnit.Framework;

namespace BEx.Tests
{
    internal abstract class ModelTranslationBase
    {
        private IExchangeCommandFactory _commands;
        private IExchangeConfiguration _configuration;
        private TradingPair _pair;
        private ResultTranslation _translator;

        public ModelTranslationBase(
            IExchangeConfiguration configuration,
            TradingPair pair,
            IExchangeCommandFactory commands)
        {
            _configuration = configuration;
            _translator = new ResultTranslation(configuration);
            _commands = commands;
            _pair = pair;
        }

        public void AccountBalanceModel(string raw)
        {
            var translated = _translator.Translate<AccountBalance>(raw, _commands.AccountBalance, _pair);

            ResponseVerification.VerifyAccountBalance(
                translated,
                _configuration.ExchangeSourceType,
                _configuration.SupportedCurrencies);
        }

        public void OpenOrdersModel(string raw)
        {
            var translated = _translator.Translate<OpenOrders>(
                raw,
                _commands.OpenOrders,
                _pair);

            ResponseVerification.VerifyOpenOrders(
                translated,
                _configuration.ExchangeSourceType,
                _pair);
        }

        public void OrderBookModel(string raw)
        {
            var translated = _translator.Translate<OrderBook>(raw, _commands.OrderBook, _pair);

            ResponseVerification.VerifyOrderBook(
                translated,
                _pair,
                _configuration.ExchangeSourceType);
        }

        public void OrderConfirmationModel(string raw, LimitOrderCommand command, OrderType orderType)
        {
            var translated = _translator.Translate<Order>(raw, command, _pair);

            ResponseVerification.VerifyOrder(
                translated,
                _pair,
                orderType,
                _configuration.ExchangeSourceType);
        }

        public void TickModel(string raw)
        {
            var translated = _translator.Translate<Tick>(raw, _commands.Tick, _pair);

            ResponseVerification.VerifyTick(translated, _pair, _configuration.ExchangeSourceType);
        }

        public void TransactionsModel(string raw)
        {
            var translated = _translator.Translate<Transactions>(raw, _commands.Transactions, _pair);

            ResponseVerification.VerifyTransactions(translated, _configuration.ExchangeSourceType, _pair);
        }

        public void UserTransactionsModel(string raw)
        {
            var translated = _translator.Translate<UserTransactions>(
                raw,
                _commands.UserTransactions,
                _pair);

            ResponseVerification.VerifyUserTransactions(
                translated,
                _configuration.ExchangeSourceType,
                _pair);
        }
    }
}