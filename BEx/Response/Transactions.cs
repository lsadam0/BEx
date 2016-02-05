// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using BEx.ExchangeEngine;

namespace BEx
{
    public sealed class Transactions : BExResult
    {
        /*
        internal Transactions(IEnumerable<Transaction> transactions, TradingPair pair,
            ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
            Pair = pair;

            TransactionsCollection =
                transactions
                    .Where(x => x != default(Transaction))
                    .OrderByDescending(x => x.UnixCompletedTimeStamp)
                    .ToList()
                    .AsReadOnly();
        }*/

        internal Transactions(IEnumerable<IExchangeResponseIntermediate<Transaction>> transactions, TradingPair pair,
            ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
            Pair = pair;

            TransactionsCollection =
                transactions
                    .Select(x => x.Convert(pair))
                    .Where(x => x != default(Transaction))
                    .OrderByDescending(x => x.UnixCompletedTimeStamp)
                    .ToList()
                    .AsReadOnly();
        }

        public TradingPair Pair { get; }

        public IReadOnlyList<Transaction> TransactionsCollection { get; }
    }
}