using System;
using BEx.ExchangeEngine;

namespace BEx
{
    public class OrderBookEntry : IExchangeResult
    {
        internal OrderBookEntry(decimal amount, decimal price)
        {
            Amount = amount;
            Price = price;
        }

        public decimal Amount { get; }

        public decimal Price { get; }

        public ExchangeType SourceExchange { get; }

        public DateTime ExchangeTimeStampUTC { get; }

        public DateTime LocalTimeStampUTC { get; }
    }
}