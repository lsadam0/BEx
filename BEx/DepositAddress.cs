using System;

namespace BEx
{
    /// <summary>
    /// Deposit Information
    /// </summary>
    public class DepositAddress : APIResult
    {
        internal DepositAddress(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        { }

        internal DepositAddress(string address, ExchangeType sourceExchange)
            : base(DateTime.Now, sourceExchange)
        {
            Address = address;
        }

        internal DepositAddress(string address, ExchangeType sourceExchange, CurrencyTradingPair pair)
            : base(DateTime.Now, sourceExchange)
        {
            Address = address;
            DepositCurrency = pair.BaseCurrency;
        }

        internal DepositAddress(string address, DateTime exchangeTimeStamp, Currency depositCurrency, ExchangeType sourceExchange)
            : base(exchangeTimeStamp, sourceExchange)
        {
            Address = address;
            DepositCurrency = depositCurrency;
        }

        /// <summary>
        /// Deposit Address
        /// </summary>
        public string Address
        {
            get;
            internal set;
        }

        /// <summary>
        /// Currency to be Deposited.
        /// </summary>
        public Currency DepositCurrency
        {
            get;
            internal set;
        }
    }
}