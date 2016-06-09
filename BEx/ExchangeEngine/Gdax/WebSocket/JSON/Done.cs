using System;

namespace BEx.ExchangeEngine.Gdax.WebSocket.JSON
{
    public class Done
    {
        public string type { get; set; }
        public DateTime time { get; set; }
        public string product_id { get; set; }
        public int sequence { get; set; }
        public string price { get; set; }
        public string order_id { get; set; }
        public string reason { get; set; }
        public string side { get; set; }
        public string order_type { get; set; }
        public string remaining_size { get; set; }
    }
}