using System;
using System.Collections.Generic;

namespace BEx
{
    /// <summary>
    /// Complete Balance information for the source Account
    /// </summary>
    public class AccountBalance : APIResult
    {
        internal AccountBalance(List<Balance> balances, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            BalanceByCurrency = new Dictionary<Currency, Balance>();

            foreach (Balance balance in balances)
            {
                BalanceByCurrency.Add(balance.BalanceCurrency, balance);
            }
        }

        /// <summary>
        ///  A List of Available and Reserved Balances by Currency.  If a Currency is supported by
        /// the Exchange, but is absent from the Balance collection, then a 0 balance should be assumed.
        /// </summary>
        public Dictionary<Currency, Balance> BalanceByCurrency
        {
            get;
            internal set;
        }
    }
}