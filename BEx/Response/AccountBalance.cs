// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BEx
{
    /// <summary>
    /// Complete Balance information for the source Account
    /// </summary>
    public sealed class AccountBalance : BExResult
    {
        internal AccountBalance(IEnumerable<Balance> balances, TradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
            Initialize(balances, pair, sourceExchange);
        }

        internal AccountBalance(IEnumerable<IExchangeResponse<Balance>> balances, TradingPair pair, ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
            IList<Balance> convertedBalances = balances
                                                .Select(x => x.Convert(pair))
                                                .OfType<Balance>()
                                                .ToList();

            Initialize(convertedBalances, pair, sourceExchange);
        }

        private void Initialize(IEnumerable<Balance> balances, TradingPair pair,
            Exchange sourceExchange)
        {
            var balanceBuffer = new Dictionary<Currency, Balance>();

            foreach (Balance toAdd in balances)
                balanceBuffer.Add(toAdd.BalanceCurrency, toAdd);

            if (balanceBuffer.Count < sourceExchange.SupportedCurrencies.Count)
            {
                // Insure an entry exists for every supported currency
                foreach (var missingCurrency in sourceExchange.SupportedCurrencies)
                {
                    if (!balanceBuffer.ContainsKey(missingCurrency))
                    {
                        balanceBuffer.Add(missingCurrency, new Balance(DateTime.UtcNow, sourceExchange.ExchangeSourceType)
                        {
                            BalanceCurrency = missingCurrency,
                            AvailableToTrade = 0,
                            TotalBalance = 0
                        });
                    }
                }
            }

            BalanceByCurrency = new ReadOnlyDictionary<Currency, Balance>(balanceBuffer);
        }

        /// <summary>
        ///  A List of Available and Reserved Balances by Currency.  If a Currency is supported by
        /// the Exchange, but is absent from the Balance Collection, then a 0 balance should be assumed.
        /// </summary>
        public IReadOnlyDictionary<Currency, Balance> BalanceByCurrency
        {
            get;
            private set;
        }

        protected override string DebugDisplay
        {


            get { return string.Format("{0} - Balances: {1}", SourceExchange, BalanceByCurrency.Count); }
        }
    }
}