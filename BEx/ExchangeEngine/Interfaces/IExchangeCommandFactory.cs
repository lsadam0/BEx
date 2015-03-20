using System.Collections.Generic;
using BEx.ExchangeEngine;

namespace BEx.ExchangeEngine
{
    public interface IExchangeCommandFactory
    {
        ExchangeCommand GetCommand(CommandClass commandType);

        Dictionary<CommandClass, ExchangeCommand> GetCommandCollection();

        ExchangeCommand BuildAccountBalanceCommand();

        ExchangeCommand BuildBuyOrderCommand();

        ExchangeCommand BuildCancelOrderCommand();

        ExchangeCommand BuildDepositAddressCommand();

        ExchangeCommand BuildOpenOrdersCommand();

        ExchangeCommand BuildOrderBookCommand();

        ExchangeCommand BuildSellOrderCommand();

        ExchangeCommand BuildTickCommand();

        ExchangeCommand BuildTransactionsCommand();

        ExchangeCommand BuildUserTransactionsCommand();
    }
}