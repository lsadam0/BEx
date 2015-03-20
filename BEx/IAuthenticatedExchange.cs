// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public interface IAuthenticatedExchange
    {
        Confirmation CancelOrder(Order toCancel);

        Confirmation CancelOrder(int id);

        Order CreateBuyOrder(decimal amount, decimal price);

        Order CreateBuyOrder(CurrencyTradingPair pair, decimal amount, decimal price);

        Order CreateSellOrder(decimal amount, decimal price);

        Order CreateSellOrder(CurrencyTradingPair pair, decimal amount, decimal price);

        /// <summary>
        /// Url complete Balance information for your Exchange account
        /// </summary>
        /// <returns>AccountBalance</returns>
        AccountBalance GetAccountBalance();

        /// <summary>
        /// Url your BTC Deposit Url for the Exchange
        /// </summary>
        /// <returns>DepositAddress</returns>
        DepositAddress GetDepositAddress();

        /// <summary>
        /// Url the Deposit Url for the requested CryptoCurrency
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
