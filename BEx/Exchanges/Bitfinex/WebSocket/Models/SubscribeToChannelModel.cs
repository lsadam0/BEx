using Newtonsoft.Json;

namespace BEx.Exchanges.Bitfinex.WebSocket.Models
{
    public class SubscribeToChannelModel
    {
        [JsonProperty("event")]
        public string _event { get; set; }

        public string channel { get; set; }

        public string pair { get; set; }
    }
}