// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx
{
    /// <summary>
    ///     Deposit Information
    /// </summary>
    public struct DepositAddress : IEquatable<DepositAddress>, IExchangeResult
    {
        

        internal DepositAddress(string address, DateTime exchangeTimeStamp, Currency depositCurrency,
            ExchangeType sourceExchange)
            : this()
        {
            Address = address;
            DepositCurrency = depositCurrency;
            this.ExchangeTimeStampUTC = exchangeTimeStamp;
            this.SourceExchange = sourceExchange;
            this.LocalTimeStampUTC = DateTime.UtcNow;
        }

        /// <summary>
        ///     Deposit Url
        /// </summary>
        public string Address { get; }

        /// <summary>
        ///     Currency to be Deposited.
        /// </summary>
        public Currency DepositCurrency { get; }

        public override string ToString()
        {
            return string.Format("{0} {1}: {2}", SourceExchange, DepositCurrency, Address);
        }

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
            if ((object)a == null
                || (object)b == null)
            {
                return Equals(a, b);
            }

            return
                a.Address == b.Address
                && a.DepositCurrency == b.DepositCurrency
                && a.SourceExchange == b.SourceExchange;


        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Tick))
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
                this.SourceExchange.GetHashCode()
                ^ this.Address.GetHashCode()
                ^ this.DepositCurrency.GetHashCode();

        }
    }
}