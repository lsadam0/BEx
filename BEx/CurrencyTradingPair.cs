namespace BEx
{
    public struct CurrencyTradingPair
    {
        public CurrencyTradingPair(Currency pairBase, Currency pairCounter)
            : this()
        {
            BaseCurrency = pairBase;
            CounterCurrency = pairCounter;
        }

        public Currency BaseCurrency
        {
            get;
            private set;
        }

        public Currency CounterCurrency
        {
            get;
            private set;
        }

        public static bool operator ==(CurrencyTradingPair x, CurrencyTradingPair y)
        {
            return (x.BaseCurrency == y.BaseCurrency) && (x.CounterCurrency == y.CounterCurrency);
        }

        public static bool operator !=(CurrencyTradingPair x, CurrencyTradingPair y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return obj is CurrencyTradingPair && this == (CurrencyTradingPair)obj;
        }

        public override int GetHashCode()
        {
            return BaseCurrency.GetHashCode() ^ CounterCurrency.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", BaseCurrency, CounterCurrency);
        }
    }
}