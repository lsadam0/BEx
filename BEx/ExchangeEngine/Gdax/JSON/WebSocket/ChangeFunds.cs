using System;

namespace BEx.ExchangeEngine.Gdax.JSON.WebSocket
{
    public class ChangeFUnds
    {
        public string type { get; set; }
        public DateTime time { get; set; }
        public int sequence { get; set; }
        public string order_id { get; set; }
        public string product_id { get; set; }
        public string new_funds { get; set; }
        public string old_funds { get; set; }
        public string price { get; set; }
        public string side { get; set; }
    }
}