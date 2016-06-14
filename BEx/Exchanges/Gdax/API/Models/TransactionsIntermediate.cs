using System;
using BEx.ExchangeEngine;

using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.API.Commands;

namespace BEx.Exchanges.Gdax.API.Models
{

    public class TransactionModel : IExchangeResponseIntermediate<Transaction>
    {
        public DateTime time { get; set; }
        public int trade_id { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string side { get; set; }

        public Transaction Convert(TradingPair pair)
        {
            return new Transaction(
                size,
                pair,
                new DateTime(time.Ticks, DateTimeKind.Utc),
                trade_id,
                price,
                ExchangeType.Gdax);
        }
    }
}