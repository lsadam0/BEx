// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Linq;
using System.Web.UI.WebControls;
using BEx.ExchangeEngine;

namespace BEx
{
    /// <summary>
    /// Contains the 50 most recent Order Transactions that have been completed for your user
    /// </summary>
    public sealed class UserTransactions : ApiResult
    {
        internal UserTransactions(IEnumerable<IExchangeResponse> transactions, CurrencyTradingPair pair, Exchange sourceExchange)
            : base(DateTime.Now, sourceExchange.ExchangeSourceType)
        {
            Pair = pair;

            TransactionsCollection =
                new ReadOnlyCollection<UserTransaction>(
                    transactions.Select(x => x.ConvertToStandard(pair, sourceExchange) as UserTransaction)
                        .OfType<UserTransaction>()
                        .Take(50)
                        .ToList());

        }

        /// <summary>
        /// Trading Pair for all contained Transactions
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            private set;
        }

        /// <summary>
        /// Your 50 most recent Order Transactions, Sorted by CompletedTime in Descending Order
        /// </summary>
        public IReadOnlyList<UserTransaction> TransactionsCollection
        {
            get;
            private set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1} - Count: {2}", SourceExchange, Pair, TransactionsCollection.Count()); }
        }



    }
}