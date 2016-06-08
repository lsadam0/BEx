using System;

namespace BEx.ExchangeEngine.Gdax.JSON.WebSocket
{
    public class Match
    {
        public string type { get; set; }
        public int trade_id { get; set; }
        public int sequence { get; set; }
        public string maker_order_id { get; set; }
        public string taker_order_id { get; set; }
        public DateTime time { get; set; }
        public string product_id { get; set; }
        public string size { get; set; }
        public string price { get; set; }
        public string side { get; set; }
    }
}