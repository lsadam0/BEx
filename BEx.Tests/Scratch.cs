using Newtonsoft.Json;
using NUnit.Framework;

namespace BEx.UnitTests
{
    [TestFixture]
    public class Scratch
    {
        private string targetJson =
            "{\"request\": \"/v1/order/new\",\"nonce\": \"635641370147070282\",\"symbol\": \"BTCUSD\",\"amount\": \"0.01\",\"price\": \"1000\",\"exchange\": \"bitfinex\",\"type\": \"exchange limit\",\"side\": \"sell\",\"X-BFX-APIKEY\": \"ieQecmvNQKC7wFcFnVtaVFIRhst3BKiMp31XMBaSS4n\"}";

        internal class CancelOrderPayload
        {
            [JsonProperty("order_id")]
            public int OrderId { get; set; }
        }

        [Test]
        public void ScratchPad()
        {
        }
    }
}