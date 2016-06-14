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
        internal AccountBalance(IEnumerable<Balance> balances, TradingPair pair, ExchangeType sourceExchange)
#pragma warning restore RECS0154 // Parameter is never used
            : base(DateTime.UtcNow, sourceExchange)
        {
            BalanceByCurrency = CreateDictionary(balances);
        }

        internal AccountBalance(IEnumerable<IExchangeResponseIntermediate<Balance>> balances, TradingPair pair,
            ExchangeType sourceExchange)
            : base(DateTime.UtcNow, sourceExchange)
        {
            BalanceByCurrency = CreateDictionary(
                balances
                    .Select(x => x.Convert(pair)));
        }

        public IReadOnlyDictionary<Currency, Balance> BalanceByCurrency { get; }

        protected override string DebugDisplay => $"{SourceExchange} - Balances: {BalanceByCurrency.Count}";

        private ReadOnlyDictionary<Currency, Balance> CreateDictionary(IEnumerable<Balance> balances)
        {
            return new ReadOnlyDictionary<Currency, Balance>(
                balances
                    .Where(x => x != default(Balance))
                    .ToDictionary(x => x.BalanceCurrency));
        }
    }
}