using System.Collections.Generic;

namespace BEx.Request
{
    public interface IExchangeCommandFactory
    {
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