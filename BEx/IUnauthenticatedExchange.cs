// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public interface IUnauthenticatedExchange
    {
        /// <summary>
        /// Url the current BTC/USD Order Book.
        /// </summary>
        /// <returns></returns>
        OrderBook GetOrderBook();

        /// <summary>
        /// Url the current Order Book for the specified Currency pair.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        OrderBook GetOrderBook(CurrencyTradingPair pair);

        /// <summary>
        /// Url the current Tick for the Default Pair of the Exchange
        /// </summary>
        /// <returns></returns>
        Tick GetTick();

        /// <summary>
        /// Url the current Tick for the specified currency pair.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        Tick GetTick(CurrencyTradingPair pair);

        /// <summary>
        /// Return BTC/USD general Transactions for past hour.
        /// </summary>
        /// <returns></returns>
        Transactions GetTransactions();

        /// <summary>
        /// Return general Transactions from the past hour for the specified currency pair.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        Transactions GetTransactions(CurrencyTradingPair pair);

    }
}
