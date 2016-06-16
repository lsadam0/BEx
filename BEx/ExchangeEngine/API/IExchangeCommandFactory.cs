// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.API.Commands;

namespace BEx.ExchangeEngine.API
{
    /// <summary>
    ///     Provide Exchange Specific ExchangeCommand objects
    ///     that describe how each Exchange implements the
    ///     commands specified by IAuthenticatedCommands and
    ///     IUnauthenticatedCommands
    /// </summary>
    internal interface IExchangeCommandFactory
    {
        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.GetAccountBalance()
        /// </summary>
        /// <returns></returns>
        AccountBalanceCommand AccountBalance { get; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.CreateBuyOrder()
        /// </summary>
        /// <returns></returns>
        LimitOrderCommand BuyOrder { get; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.CancelOrder()
        /// </summary>
        /// <returns></returns>
        CancelOrderCommand CancelOrder { get; }

        DayRangeCommand DayRange { get; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.GetOpenOrders()
        /// </summary>
        /// <returns></returns>
        OpenOrdersCommand OpenOrders { get; }

        /// <summary>
        ///     ExchangeCommand associated with IUnauthenticatedCommands.GetOrderBook()
        /// </summary>
        /// <returns></returns>
        OrderBookCommand OrderBook { get; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.CreateSellOrder()
        /// </summary>
        /// <returns></returns>
        LimitOrderCommand SellOrder { get; }

        /// <summary>
        ///     ExchangeCommand associated with IUnauthenticatedCommands.GetTick()
        /// </summary>
        /// <returns></returns>
        TickCommand Tick { get; }

        /// <summary>
        ///     ExchangeCommand associated with IUnauthenticatedCommands.GetTransactions()
        /// </summary>
        /// <returns></returns>
        TransactionsCommand Transactions { get; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.GetUserTransactions()
        /// </summary>
        /// <returns></returns>
        UserTransactionsCommand UserTransactions { get; }
    }
}