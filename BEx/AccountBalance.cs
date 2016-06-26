// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BEx.ExchangeEngine;

using BEx.ExchangeEngine.API;

namespace BEx
{
    public sealed class AccountBalance : BExResult
    {
#pragma warning disable RECS0154 // Parameter is never used
        internal AccountBalance(IEnumerable<Balance> balances, TradingPair pair, IExchangeConfiguration configuration)
#pragma warning restore RECS0154 // Parameter is never used
            : base(DateTime.UtcNow, configuration.ExchangeSourceType)
        {
            BalanceByCurrency = CreateDictionary(balances, configuration);
        }

        internal AccountBalance(
            IEnumerable<IExchangeResponseIntermediate<Balance>> balances,
            TradingPair pair,
            IExchangeConfiguration configuration)
            : base(DateTime.UtcNow, configuration.ExchangeSourceType)
        {
            BalanceByCurrency = CreateDictionary(
                balances
                    .Select(x => x.Convert(pair)),
                configuration);
        }

        public IReadOnlyDictionary<Currency, Balance> BalanceByCurrency { get; }

        public override string ToString() => $"{SourceExchange} - Balances: {BalanceByCurrency.Count}";

        private ReadOnlyDictionary<Currency, Balance> CreateDictionary(IEnumerable<Balance> balances, IExchangeConfiguration configuration)
        {

            var dict = new Dictionary<Currency, Balance>();

            foreach (var currency in configuration.SupportedCurrencies)
            {
                var balance = balances.FirstOrDefault(x => x.BalanceCurrency == currency);

                if (balance == default(Balance))
                {
                    balance = new Balance(
                        0.00m,
                        currency,
                        0.00m,
                        DateTime.UtcNow,
                        configuration.ExchangeSourceType,
                        0.00m);
                }

                dict[currency] = balance;
            }
            return new ReadOnlyDictionary<Currency, Balance>(dict);
        }
    }
}