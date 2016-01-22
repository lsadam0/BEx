// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    public struct TradingPair : IEquatable<TradingPair>
    {
        public TradingPair(Currency pairBase, Currency pairCounter)
            : this()
        {
            BaseCurrency = pairBase;
            CounterCurrency = pairCounter;
        }

        public Currency BaseCurrency { get; }

        public Currency CounterCurrency { get; }

        public static bool operator !=(TradingPair x, TradingPair y)
        {
            return !(x == y);
        }

        public static bool operator ==(TradingPair x, TradingPair y)
        {
            return (x.BaseCurrency == y.BaseCurrency)
                   && (x.CounterCurrency == y.CounterCurrency);
        }

        public bool Equals(TradingPair other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return obj is TradingPair && this == (TradingPair) obj;
        }

        public override int GetHashCode()
        {
            return BaseCurrency.GetHashCode() ^ CounterCurrency.GetHashCode();
        }

        public override string ToString() => $"{BaseCurrency}{CounterCurrency}";
    }
}