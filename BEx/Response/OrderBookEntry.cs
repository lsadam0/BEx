using System;

namespace BEx
{
    public class OrderBookEntry : IExchangeResult
    {
        internal OrderBookEntry(decimal amount, decimal price)
        {
            Amount = amount;
            Price = price;
        }

        public decimal Amount { get; private set; }

        public decimal Price { get; private set; }

        public ExchangeType SourceExchange { get; private set; }

        public DateTime ExchangeTimeStampUTC { get; private set; }

        public DateTime LocalTimeStampUTC { get; private set; }
    }
}