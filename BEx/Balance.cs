﻿using System;

namespace BEx
{
    /// <summary>
    /// Complete Balance Information for a specific Currency.
    /// </summary>
    public class Balance : APIResult
    {
        internal Balance(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        {
        }

        /// <summary>
        /// The Un-reserved Available Balance
        /// </summary>
        public Decimal AvailableToTrade
        {
            get;
            internal set;
        }

        /// <summary>
        /// Source Currency
        /// </summary>
        public Currency BalanceCurrency
        {
            get;
            internal set;
        }

        /// <summary>
        /// Total Exchange Balance for BalanceCurrency, include amounts reserved in open trades
        /// </summary>
        public Decimal TotalBalance
        {
            get;
            internal set;
        }
    }
}