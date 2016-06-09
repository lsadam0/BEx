using System;

namespace BEx.ExchangeEngine.Gdax.WebSocket.JSON
{
    public class Open
    {
        public string type { get; set; }
        public DateTime time { get; set; }
        public string product_id { get; set; }
        public int sequence { get; set; }
        public string order_id { get; set; }
        public string price { get; set; }
        public string remaining_size { get; set; }
        public string side { get; set; }
    }
}