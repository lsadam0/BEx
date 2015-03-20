// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    /// Deposit Information
    /// </summary>
    public sealed class DepositAddress : ApiResult
    {
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
        /// Deposit Url
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

        protected override string DebugDisplay
        {
            get { return string.Format("{0} {1}: {2}", SourceExchange, DepositCurrency, Address); }
        }
    }
}