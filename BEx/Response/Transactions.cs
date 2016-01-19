// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BEx
{
    public sealed class Transactions : BExResult
    {
        internal Transactions(IEnumerable<IExchangeResponse<Transaction>> transactions, TradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
            Pair = pair;

            TransactionsCollection =
                new ReadOnlyCollection<Transaction>(
                    transactions.Select(x => x.Convert(pair))
                    .OfType<Transaction>()
                    .ToList());
        }

        public TradingPair Pair
        {
            get;
            private set;
        }

        public IReadOnlyList<Transaction> TransactionsCollection
        {
            get;
            private set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1} - Transactions: {2}", SourceExchange, Pair, TransactionsCollection.Count()); }
        }
    }
}