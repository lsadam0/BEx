using System;

namespace BEx
{
    /// <summary>
    /// Deposit Information
    /// </summary>
    public class DepositAddress : APIResult
    {
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


        internal DepositAddress(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        { }

        internal DepositAddress(string address)
            : base(DateTime.Now)
        {
            Address = address;
        }
        internal DepositAddress(string address, DateTime exchangeTimeStamp, Currency depositCurrency)
            : base(exchangeTimeStamp)
        {
            Address = address;
            DepositCurrency = depositCurrency;
        }
    }
}