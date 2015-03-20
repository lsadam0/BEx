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
    /// Your transactions for the previous hour
    /// </summary>
    public sealed class UserTransactions : ApiResult
    {
        internal UserTransactions(IEnumerable<IExchangeResponse> transactions, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Pair = pair;

            TransactionsCollection =
                new ReadOnlyCollection<UserTransaction>(
                    transactions.Select(x => x.ConvertToStandard(pair) as UserTransaction)
                        .OfType<UserTransaction>()
                        .ToList());

        }

        /// <summary>
        /// Trading Pair
        /// </summary>
        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        /// <summary>
        /// Your Transactions for the previous hour
        /// </summary>
        public IEnumerable<UserTransaction> TransactionsCollection
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1} - Count: {2}", SourceExchange, Pair, TransactionsCollection.Count()); }
        }
    }
}