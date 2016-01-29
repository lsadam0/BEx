// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.Exceptions;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    /// <summary>
    /// Validated Exchange Deposit Address
    /// </summary>
    public struct DepositAddress : IEquatable<DepositAddress>, IExchangeResult
    {
        private string _address;

        internal DepositAddress(string address, TradingPair pair, ExchangeType sourceExchange)
            : this(address, DateTime.UtcNow, pair.BaseCurrency, sourceExchange)
        {
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
        /// Verified Exchange Deposit Address for the Currency specified by <seealso cref="DepositCurrency"/>
        /// </summary>
        public string Address
        {
            get { return _address; }
            set
            {
                if (ValidateAddress(value))
                {
                    _address = value;
                }
            }
        }


        /// <summary>
        /// Currency that can be deposited at <seealso cref="Address"/>
        /// </summary>
        public Currency DepositCurrency { get; }

        public DateTime ExchangeTimeStampUTC { get; }

        /// <summary>
        ///     Local Machine TimeStamp marking the time at which an Exchange Command has successfully executed.
        /// </summary>
        public DateTime LocalTimeStampUTC { get; }

        public ExchangeType SourceExchange { get; }

        public static bool operator !=(DepositAddress a, DepositAddress b) => !(a == b);

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

        public bool Equals(DepositAddress b) => this == b;

        public override int GetHashCode() => Address.GetHashCode()
                                             ^ DepositCurrency.GetHashCode()
                                             ^ SourceExchange.GetHashCode();

        public override string ToString() => $"{SourceExchange} {DepositCurrency}: {Address}";


        private bool ValidateAddress(string toValidate)
        {
            if (!AddressValidator.IsValid(toValidate))
            {
                throw new InvalidAddressException($"'{toValidate}' is not a valid address");
            }

            return true;
        }


    }
}