using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BEx.BitFinexSupport
{
    internal class BitFinexTransactionJSON
    {
        [JsonProperty("timestamp")]
        public int timestamp { get; set; }

        [JsonProperty("tid")]
        public int tid { get; set; }
 
        [JsonProperty("price")]
        public string price { get; set; }
 
        [JsonProperty("amount")]
        public string amount { get; set; }
        
        [JsonProperty("exchange")]
        public string exchange { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }
}
