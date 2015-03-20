using System;
using System.Collections.Generic;
using BEx.ExchangeSupport;


namespace BEx
{
    /// <summary>
    /// Complete Balance information for the source Account
    /// </summary>
    public sealed class AccountBalance : ApiResult
    {
        internal AccountBalance(IEnumerable<Balance> balances, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            BalanceByCurrency = new Dictionary<Currency, Balance>();

            foreach (Balance toAdd in balances)
                BalanceByCurrency.Add(toAdd.BalanceCurrency, toAdd);
        }

        internal AccountBalance(IEnumerable<IExchangeResponse> balances, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            BalanceByCurrency = new Dictionary<Currency, Balance>();

            foreach (IExchangeResponse balance in balances)
            {
                Balance converted = balance.ConvertToStandard(pair) as Balance;

                if (converted != null)
                    BalanceByCurrency.Add(converted.BalanceCurrency, converted);
            }
        }

        /// <summary>
        ///  A List of Available and Reserved Balances by Currency.  If a Currency is supported by
        /// the Exchange, but is absent from the Balance Collection, then a 0 balance should be assumed.
        /// </summary>
        public IDictionary<Currency, Balance> BalanceByCurrency
        {
            get;
            internal set;
        }
    }
}