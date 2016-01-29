// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    public struct UserTransaction : IEquatable<UserTransaction>, IExchangeResult
    {
        private OrderType _transactionType;


        internal UserTransaction(
            decimal baseCurrencyAmount,
            decimal counterCurrencyAmount,
            long timeStamp,
            decimal exchangeRate,
            int orderId,
            decimal tradeFee,
            Currency tradeFeeCurrency,
            TradingPair pair,
            ExchangeType sourceExchange,
            OrderType orderType)
            : this()
        {
            this.BaseCurrencyAmount = baseCurrencyAmount;
            this.CounterCurrencyAmount = counterCurrencyAmount;
            this.TransactionType = orderType;
            this.UnixTimeStamp = timeStamp;
            this.ExchangeRate = exchangeRate;
            this.OrderId = orderId;
            this.TradeFee = tradeFee;
            this.TradeFeeCurrency = tradeFeeCurrency;
            this.Pair = pair;
            this.SourceExchange = sourceExchange;
            this.CompletedTime = UnixTimeStamp.ToDateTimeUTC();
            this.ExchangeTimeStampUTC = this.CompletedTime;
            this.LocalTimeStampUTC = DateTime.UtcNow;

        }

        public long UnixTimeStamp { get; }

        public decimal BaseCurrencyAmount { get; private set; }

        public DateTime CompletedTime { get; }

        public decimal CounterCurrencyAmount { get; private set; }

        public decimal ExchangeRate { get; }

        public DateTime ExchangeTimeStampUTC { get; }

        public DateTime LocalTimeStampUTC { get; }

        public int OrderId { get; }

        public TradingPair Pair { get; }

        public ExchangeType SourceExchange { get; }

        public decimal TradeFee { get; }

        public Currency TradeFeeCurrency { get; }

        public OrderType TransactionType
        {
            get { return _transactionType; }
            private set
            {
                if (value == OrderType.Sell)
                {
                    BaseCurrencyAmount = Math.Abs(BaseCurrencyAmount) * -1;
                    CounterCurrencyAmount = Math.Abs(CounterCurrencyAmount);
                }
                else
                {
                    BaseCurrencyAmount = Math.Abs(BaseCurrencyAmount);
                    CounterCurrencyAmount = Math.Abs(CounterCurrencyAmount) * -1;
                }

                _transactionType = value;
            }
        }

        public static bool operator !=(UserTransaction a, UserTransaction b) =>  !(a == b);
        

        public static bool operator ==(UserTransaction a, UserTransaction b)
        {
            return
                a.BaseCurrencyAmount == b.BaseCurrencyAmount
                && a.UnixTimeStamp == b.UnixTimeStamp
                && a.CounterCurrencyAmount == b.CounterCurrencyAmount
                && a.ExchangeRate == b.ExchangeRate
                && a.OrderId == b.OrderId
                && a.Pair == b.Pair
                && a.SourceExchange == b.SourceExchange
                && a.TradeFee == b.TradeFee
                && a.TradeFeeCurrency == b.TradeFeeCurrency
                && a.TransactionType == b.TransactionType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UserTransaction))
            {
                return false;
            }

            return this == (UserTransaction)obj;
        }

        public bool Equals(UserTransaction b) => this == b;
        

        public override int GetHashCode()
        {
            return
                BaseCurrencyAmount.GetHashCode()
                ^ UnixTimeStamp.GetHashCode()
                ^ CounterCurrencyAmount.GetHashCode()
                ^ ExchangeRate.GetHashCode()
                ^ OrderId.GetHashCode()
                ^ Pair.GetHashCode()
                ^ SourceExchange.GetHashCode()
                ^ TradeFee.GetHashCode()
                ^ TradeFeeCurrency.GetHashCode()
                ^ TransactionType.GetHashCode();
        }

        public override string ToString() => $"{SourceExchange} {Pair} - Order Id: {OrderId}";
    }
}