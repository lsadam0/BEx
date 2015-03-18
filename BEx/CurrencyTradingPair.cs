using System;

namespace BEx
{
    public struct CurrencyTradingPair
    {
        public Currency BaseCurrency;
        public Currency CounterCurrency;

        public CurrencyTradingPair(Currency pairBase, Currency pairCounter)
        {
            BaseCurrency = pairBase;
            CounterCurrency = pairCounter;
        }

        public override bool Equals(Object obj)
        {
            return obj is CurrencyTradingPair && this == (CurrencyTradingPair)obj;
        }

        public override int GetHashCode()
        {
            return BaseCurrency.GetHashCode() ^ CounterCurrency.GetHashCode();
        }

        public static bool operator ==(CurrencyTradingPair x, CurrencyTradingPair y)
        {
            return (x.BaseCurrency == y.BaseCurrency) && (x.CounterCurrency == y.CounterCurrency);
        }

        public static bool operator !=(CurrencyTradingPair x, CurrencyTradingPair y)
        {
            return !(x == y);
        }
    }
}