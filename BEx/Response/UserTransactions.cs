// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BEx.ExchangeEngine;

namespace BEx
{
    /// <summary>
    ///     Contains the 50 most recent Order Transactions that have been completed for your user
    /// </summary>
    public sealed class UserTransactions : BExResult
    {
        internal UserTransactions(IEnumerable<IExchangeResponse<UserTransaction>> transactions, TradingPair pair,
            ExchangeType sourceExchange)
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
        ///     Trading Pair for all contained Transactions
        /// </summary>
        public TradingPair Pair { get; }

        /// <summary>
        ///     Your 50 most recent Order Transactions, Sorted by CompletedTime in Descending Order
        /// </summary>
        public IReadOnlyList<UserTransaction> TransactionsCollection { get; }

        public override string ToString() => string.Format("{0} {1} - Count: {2}", SourceExchange, Pair, TransactionsCollection.Count());
    }
}