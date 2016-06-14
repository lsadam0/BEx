using Newtonsoft.Json;

namespace BEx.Exchanges.Bitfinex.WebSocket.Models
{
    public class SubscribeToOrderBookModel
    {
        [JsonProperty("event")]
        public string _event { get; set; }

        public string channel { get; set; }
        public string pair { get; set; }

        public string prec
        {
            get
            {
                // Highest Precision
                return "P0";
            }
        }

        public string freq
        {
            get
            {
                // Real-time updates
                return "F0";
            }
        }

        public string len { get; set; }
    }
}