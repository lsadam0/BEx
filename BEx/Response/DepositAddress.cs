// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.Exceptions;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    /// <summary>
    ///     Deposit Information
    /// </summary>
    public struct DepositAddress : IEquatable<DepositAddress>, IExchangeResult
    {
        private string address;

        internal DepositAddress(string address, TradingPair pair, ExchangeType sourceExchange)
                    : this()
        {
            Address = address;
            DepositCurrency = pair.BaseCurrency;
            ExchangeTimeStampUTC = DateTime.UtcNow;
            SourceExchange = sourceExchange;
            LocalTimeStampUTC = DateTime.UtcNow;
        }

        internal DepositAddress(string address, DateTime exchangeTimeStamp, Currency depositCurrency,
            ExchangeType sourceExchange)
            : this()
        {
            Address = address;
            DepositCurrency = depositCurrency;
            ExchangeTimeStampUTC = exchangeTimeStamp;
            SourceExchange = sourceExchange;
            LocalTimeStampUTC = DateTime.UtcNow;
        }
        /// <summary>
        ///     Deposit Url
        /// </summary>
        public string Address
        {
            get { return address; }
            set
            {
                if (ValidateAddress(value))
                {
                    address = value;
                }
            }
        }

        /// <summary>
        ///     Currency to be Deposited.
        /// </summary>
        public Currency DepositCurrency { get; }

        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        public ExchangeType SourceExchange { get; }

        public static bool operator !=(DepositAddress a, DepositAddress b)
        {
            return !(a == b);
        }

        public static bool operator ==(DepositAddress a, DepositAddress b)
        {
            return
                a.Address == b.Address
                && a.DepositCurrency == b.DepositCurrency
                && a.SourceExchange == b.SourceExchange;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DepositAddress))
            {
                return false;
            }

            return this == (DepositAddress)obj;
        }

        public bool Equals(DepositAddress b)
        {
            return this == b;
        }

        public override int GetHashCode()
        {
            return
                Address.GetHashCode()
                ^ DepositCurrency.GetHashCode()
                ^ SourceExchange.GetHashCode();
        }

        public override string ToString() => $"{SourceExchange} {DepositCurrency}: {Address}";

        private bool ValidateAddress(string address)
        {
            if (!AddressValidator.IsValid(address))
            {
                throw new InvalidAddressException($"'{address}' is not a valid address");
            }

            return true;
        }
    }
}