using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEx.Request
{
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