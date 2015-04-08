// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BEx.ExchangeEngine;
using System.Diagnostics;

namespace BEx
{
    public sealed class Transactions : BExResult
    {
        internal Transactions(IEnumerable<IExchangeResponse> transactions, CurrencyTradingPair pair, Exchange sourceExchange)
            : base(DateTime.UtcNow, sourceExchange.ExchangeSourceType)
        {
            Pair = pair;
            
            TransactionsCollection =
                new ReadOnlyCollection<Transaction>(
                    transactions.Select(x => x.ConvertToStandard(pair, sourceExchange) as Transaction)
                    .OfType<Transaction>()
                    .ToList());

        }

        public CurrencyTradingPair Pair
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