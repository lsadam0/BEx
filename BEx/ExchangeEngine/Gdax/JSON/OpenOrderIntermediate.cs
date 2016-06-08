using System;
using BEx.ExchangeEngine.Utilities;

namespace  BEx.ExchangeEngine.Gdax.JSON
{
    public class OpenOrderIntermediate : IExchangeResponseIntermediate<Order>
    {
        public string id { get; set; }
        public string size { get; set; }
        public string price { get; set; }
        public string product_id { get; set; }
        public string status { get; set; }
        public string filled_size { get; set; }
        public string fill_fees { get; set; }
        public bool settled { get; set; }
        public string side { get; set; }
        public DateTime created_at { get; set; }

        public Order Convert(TradingPair pair)
        {
            return new Order(
                Conversion.ToDecimalInvariant(size),
                pair,
                id,
                Conversion.ToDecimalInvariant(price),
                side == "buy" ? OrderType.Buy : OrderType.Sell,
                new DateTime(created_at.Ticks, DateTimeKind.Utc),
                ExchangeType.Gdax);
        }
    }
}