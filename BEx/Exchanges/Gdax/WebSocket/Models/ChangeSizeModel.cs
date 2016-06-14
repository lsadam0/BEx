using System;

namespace BEx.Exchanges.Gdax.WebSocket.Models
{
    public class ChangeSizeModel
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