// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BEx
{
    /// <summary>
    /// Contains the 50 most recent Order Transactions that have been completed for your user
    /// </summary>
    public sealed class UserTransactions : BExResult
    {
        internal UserTransactions(IEnumerable<IExchangeResponse<UserTransaction>> transactions, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
            Pair = pair;

            TransactionsCollection =
                new ReadOnlyCollection<UserTransaction>(
                    transactions.Select(x => x.Convert(pair))
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