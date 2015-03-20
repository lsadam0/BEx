// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Standard set of Command types defined by IAuthenticatedCommands & IUnauthenticatedCommands
    /// </summary>
    public enum CommandClass
    {
        AccountBalance,
        BuyOrder,
        CancelOrder,
        DepositAddress,
        OpenOrders,
        OrderBook,
        PendingDeposits,
        PendingWithdrawals,
        SellOrder,
        Tick,
        Transactions,
        UserTransactions,
        Withdraw
    }
}