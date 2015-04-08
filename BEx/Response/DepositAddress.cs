// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    /// Deposit Information
    /// </summary>
    public sealed class DepositAddress : BExResult
    {
        internal DepositAddress(string address, Exchange sourceExchange, CurrencyTradingPair pair)
            : base(DateTime.UtcNow, sourceExchange.ExchangeSourceType)
        {
            Address = address;
            DepositCurrency = pair.BaseCurrency;
        }

        internal DepositAddress(string address, DateTime exchangeTimeStamp, Currency depositCurrency, Exchange sourceExchange)
            : base(exchangeTimeStamp, sourceExchange.ExchangeSourceType)
        {
            Address = address;
            DepositCurrency = depositCurrency;
        }

        /// <summary>
        /// Deposit Url
        /// </summary>
        public string Address
        {
            get;
            private set;
        }

        /// <summary>
        /// Currency to be Deposited.
        /// </summary>
        public Currency DepositCurrency
        {
            get;
            private set;
        }

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1}: {2}", SourceExchange, DepositCurrency, Address); }
        }
    }
}