﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;

namespace BEx
{
    /// <summary>
    ///     Complete Balance Information for a specific Currency.
    /// </summary>
    public struct Balance : IEquatable<Balance>, IExchangeResult
    {
        internal Balance(DateTime exchangeTimeStamp, ExchangeType sourceExchange)
            : this()
        {
            ExchangeTimeStampUTC = exchangeTimeStamp;
            SourceExchange = sourceExchange;
            LocalTimeStampUTC = DateTime.UtcNow;
        }

        /// <summary>
        ///     The Un-reserved Available Balance
        /// </summary>
        public decimal AvailableToTrade { get; internal set; }

        /// <summary>
        ///     Source Currency
        /// </summary>
        public Currency BalanceCurrency { get; internal set; }

        /// <summary>
        ///     Total Exchange Balance for BalanceCurrency, include amounts reserved in open trades
        /// </summary>
        public decimal TotalBalance { get; internal set; }

        public override string ToString()
            => $"{SourceExchange} {BalanceCurrency} - Available: {AvailableToTrade} - Total: {TotalBalance}";

        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        public ExchangeType SourceExchange { get; }

        public static bool operator !=(Balance a, Balance b)
        {
            return !(a == b);
        }

        public static bool operator ==(Balance a, Balance b)
        {
            return
                a.AvailableToTrade == b.AvailableToTrade
                && a.BalanceCurrency == b.BalanceCurrency
                && a.SourceExchange == b.SourceExchange
                && a.TotalBalance == b.TotalBalance;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Balance))
            {
                return false;
            }

            return this == (Balance)obj;
        }

        public bool Equals(Balance b)
        {
            return this == b;
        }

        public override int GetHashCode()
        {
            return
                AvailableToTrade.GetHashCode()
                ^ BalanceCurrency.GetHashCode()
                ^ SourceExchange.GetHashCode()
                ^ TotalBalance.GetHashCode();
        }
    }
}