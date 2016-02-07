// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    public struct Transaction : IEquatable<Transaction>, IExchangeResult
    {
        internal Transaction(
            string amount,
            TradingPair pair,
            DateTime exchangeTimestamp,
            int transactionId,
            string price,
            ExchangeType sourceExchange)
            : this()
        {
            Amount = Conversion.ToDecimalInvariant(amount);
            Pair = pair;
            CompletedTime = exchangeTimestamp;
            UnixCompletedTimeStamp = (long) CompletedTime.ToUnixTime();
            ExchangeTimeStampUTC = CompletedTime;
            LocalTimeStampUTC = DateTime.UtcNow;
            Price = Conversion.ToDecimalInvariant(price);
            SourceExchange = sourceExchange;
            TransactionId = transactionId;
        }

        internal Transaction(
            string amount,
            TradingPair pair,
            long unixTimeStamp,
            int transactionId,
            string price,
            ExchangeType sourceExchange)
            : this()
        {
            Amount = Conversion.ToDecimalInvariant(amount);
            Pair = pair;
            UnixCompletedTimeStamp = unixTimeStamp;
            CompletedTime = unixTimeStamp.ToDateTimeUTC();
            ExchangeTimeStampUTC = CompletedTime;
            LocalTimeStampUTC = DateTime.UtcNow;
            Price = Conversion.ToDecimalInvariant(price);
            SourceExchange = sourceExchange;
            TransactionId = transactionId;
        }

        public decimal Amount { get; }

        public DateTime CompletedTime { get; }

        public DateTime ExchangeTimeStampUTC { get; }

        public DateTime LocalTimeStampUTC { get; }

        public TradingPair Pair { get; }

        public decimal Price { get; }

        public ExchangeType SourceExchange { get; }

        public long TransactionId { get; }

        public long UnixCompletedTimeStamp { get; }

        public static bool operator !=(Transaction a, Transaction b) => !(a == b);


        public static bool operator ==(Transaction a, Transaction b)
        {
            return
                a.Amount == b.Amount
                && a.UnixCompletedTimeStamp == b.UnixCompletedTimeStamp
                && a.Pair == b.Pair
                && a.Price == b.Price
                && a.SourceExchange == b.SourceExchange
                && a.TransactionId == b.TransactionId;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Transaction))
            {
                return false;
            }

            return this == (Transaction) obj;
        }

        public bool Equals(Transaction b) => this == b;

        public override int GetHashCode()
        {
            return
                Amount.GetHashCode()
                ^ CompletedTime.GetHashCode()
                ^ Pair.GetHashCode()
                ^ UnixCompletedTimeStamp.GetHashCode()
                ^ TransactionId.GetHashCode()
                ^ SourceExchange.GetHashCode();
        }

        public override string ToString() => $"{SourceExchange} {Pair} - Price: {Price} - Amount: {Amount}";
    }
}