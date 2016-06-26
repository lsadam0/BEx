using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.API.Commands;
using BEx.ExchangeEngine;
using NUnit.Framework;

namespace BEx.Tests
{
    internal class ConfigurationVerificationBase
    {

        protected Exchange TestCandidate;

        public ConfigurationVerificationBase(Exchange testCandidate)
        {
            this.TestCandidate = testCandidate;
        }

        public void VerifyConfiguration(IExchangeConfiguration configuration)
        {
            Assert.NotNull(configuration);
            Assert.NotNull(configuration.BaseUri);
            Assert.NotNull(configuration.SupportedCurrencies);
            Assert.NotNull(configuration.SupportedPairs);
            Assert.IsTrue(TestCandidate.ExchangeSourceType == configuration.ExchangeSourceType);
        }

        public void AllCommandsPresent(IExchangeCommandFactory commands)
        {
            Assert.NotNull(commands.AccountBalance);
            Assert.NotNull(commands.BuyOrder);
            Assert.NotNull(commands.CancelOrder);
            Assert.NotNull(commands.OpenOrders);
            Assert.NotNull(commands.OrderBook);
            Assert.NotNull(commands.SellOrder);
            Assert.NotNull(commands.Tick);
            Assert.NotNull(commands.Transactions);
            Assert.NotNull(commands.UserTransactions);
            Assert.NotNull(commands.OrderStatus);

            VerifyExchangeCommand(commands.AccountBalance);
            VerifyExchangeCommand(commands.BuyOrder);
            VerifyExchangeCommand(commands.CancelOrder);
            VerifyExchangeCommand(commands.OpenOrders);
            VerifyExchangeCommand(commands.OrderBook);
            VerifyExchangeCommand(commands.SellOrder);
            VerifyExchangeCommand(commands.Tick);
            VerifyExchangeCommand(commands.Transactions);
            VerifyExchangeCommand(commands.UserTransactions);
            VerifyExchangeCommand(commands.OrderStatus);
        }

        private void VerifyExchangeCommand(ExchangeCommand command)
        {
            Assert.NotNull(command);
            Assert.NotNull(command.ApiResultSubType);
            Assert.NotNull(command.IntermediateType);
            Assert.NotNull(command.Parameters);
            Assert.NotNull(command.RelativeUri);
            Assert.IsTrue(command.ReturnsValueType || command.IntermediateType != null);
        }
    }
}