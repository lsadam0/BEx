using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    interface IAuthenticatedExchange
    {
        Confirmation CancelOrder(Order toCancel);

        Confirmation CancelOrder(int id);

        Order CreateBuyOrder(decimal amount, decimal price);

        Order CreateBuyOrder(CurrencyTradingPair pair, decimal amount, decimal price);

        Order CreateSellOrder(decimal amount, decimal price);

        Order CreateSellOrder(CurrencyTradingPair pair, decimal amount, decimal price);

        /// <summary>
        /// Get complete Balance information for your Exchange account
        /// </summary>
        /// <returns>AccountBalance</returns>
        AccountBalance GetAccountBalance();

        /// <summary>
        /// Get your BTC Deposit Address for the Exchange
        /// </summary>
        /// <returns>DepositAddress</returns>
        DepositAddress GetDepositAddress();

        /// <summary>
        /// Get the Deposit Address for the requested CryptoCurrency
        /// </summary>
        /// <param name="toDeposit">CryptoCurrency to deposit</param>
        /// <returns></returns>
        DepositAddress GetDepositAddress(Currency toDeposit);

        OpenOrders GetOpenOrders();

        OpenOrders GetOpenOrders(CurrencyTradingPair pair);

        UserTransactions GetUserTransactions();

        UserTransactions GetUserTransactions(CurrencyTradingPair pair);

    }
}
