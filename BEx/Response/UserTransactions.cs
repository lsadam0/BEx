using System;
using System.Collections.Generic;
using System.Threading;
using BEx.ExchangeSupport;

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
            TransactionsCollection = new List<UserTransaction>();
            Pair = pair;

            foreach (IExchangeResponse transaction in transactions)
            {
                UserTransaction converted = transaction.ConvertToStandard(pair) as UserTransaction;

                if (converted != null)
                    TransactionsCollection.Add(converted);
            }
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
        public IList<UserTransaction> TransactionsCollection
        {
            get;
            internal set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1} - Count: {2}", SourceExchange, Pair, TransactionsCollection.Count); }
        }
    }
}