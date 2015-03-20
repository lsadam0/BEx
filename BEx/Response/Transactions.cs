// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BEx.ExchangeEngine;

namespace BEx
{
    public sealed class Transactions : ApiResult
    {
        internal Transactions(IEnumerable<IExchangeResponse> transactions, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Pair = pair;

            TransactionsCollection =
                new ReadOnlyCollection<Transaction>(
                    transactions.Select(x => x.ConvertToStandard(pair) as Transaction)
                    .OfType<Transaction>()
                    .ToList());
        }

        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        public IEnumerable<Transaction> TransactionsCollection
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1} - Transactions: {2}", SourceExchange, Pair, TransactionsCollection.Count()); }
        }
    }
}