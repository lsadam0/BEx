// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using BEx.ExchangeEngine;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Provide Exchange Specific ExchangeCommand objects
    /// that describe how each Exchange implements the 
    /// commands specified by IAuthenticatedCommands and
    /// IUnauthenticatedCommands
    /// </summary>
    public interface IExchangeCommandFactory
    {
        /// <summary>
        /// Get the ExchangeCommand associated with the CommandClass value
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns>Specific ExchangeCommand</returns>
        ExchangeCommand GetCommand(CommandClass commandType);

        /// <summary>
        /// Return the entire ExchangeCommand Collection
        /// </summary>
        /// <returns>All ExchangeCommand objects for the Exchange</returns>
        Dictionary<CommandClass, ExchangeCommand> GetCommandCollection();

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.GetAccountBalance()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildAccountBalanceCommand();

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.CreateBuyOrder()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildBuyOrderCommand();

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.CancelOrder()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildCancelOrderCommand();

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.GetDepositAddress()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildDepositAddressCommand();

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.GetOpenOrders()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildOpenOrdersCommand();

        /// <summary>
        /// ExchangeCommand associated with IUnauthenticatedCommands.GetOrderBook()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildOrderBookCommand();

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.CreateSellOrder()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildSellOrderCommand();

        /// <summary>
        /// ExchangeCommand associated with IUnauthenticatedCommands.GetTick()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildTickCommand();

        /// <summary>
        /// ExchangeCommand associated with IUnauthenticatedCommands.GetTransactions()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildTransactionsCommand();

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.GetUserTransactions()
        /// </summary>
        /// <returns></returns>
        ExchangeCommand BuildUserTransactionsCommand();
    }
}