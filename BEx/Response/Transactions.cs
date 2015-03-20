using System;
using System.Collections.Generic;
using BEx.ExchangeSupport;

namespace BEx
{
    public sealed class Transactions : ApiResult
    {
        internal Transactions(IEnumerable<IExchangeResponse> transactions, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Pair = pair;
            TransactionsCollection = new List<Transaction>();

            foreach (IExchangeResponse transaction in transactions)
                TransactionsCollection.Add(transaction.ConvertToStandard(pair) as Transaction);
        }

        public CurrencyTradingPair Pair
        {
            get;
            internal set;
        }

        public IList<Transaction> TransactionsCollection
        {
            get;
            internal set;
        }
    }
}