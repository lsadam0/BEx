using System;

namespace BEx.ExchangeEngine.Gdax.JSON.WebSocket
{
    public class ChangeSize
    {
        public string type { get; set; }
        public DateTime time { get; set; }
        public int sequence { get; set; }
        public string order_id { get; set; }
        public string product_id { get; set; }
        public string new_size { get; set; }
        public string old_size { get; set; }
        public string price { get; set; }
        public string side { get; set; }
    }
}