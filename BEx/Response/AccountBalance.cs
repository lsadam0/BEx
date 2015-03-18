using BEx.ExchangeSupport;
using System;
using System.Collections.Generic;

namespace BEx
{
    /// <summary>
    /// Complete Balance information for the source Account
    /// </summary>
    public sealed class AccountBalance : ApiResult
    {
        internal AccountBalance(List<Balance> balances, CurrencyTradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            BalanceByCurrency = new Dictionary<Currency, Balance>();
            balances.ForEach(x => BalanceByCurrency.Add(x.BalanceCurrency, x));
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
        /// the Exchange, but is absent from the Balance collection, then a 0 balance should be assumed.
        /// </summary>
        public Dictionary<Currency, Balance> BalanceByCurrency
        {
            get;
            internal set;
        }
    }
}