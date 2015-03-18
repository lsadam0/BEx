using BEx.ExchangeSupport;
using System;
using System.Collections.Generic;
using System.Linq;

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
            this.Pair = pair;

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
    }
}