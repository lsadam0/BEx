using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BEx
{

    internal class Ticker
    {

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("avg")]
        public double Avg { get; set; }

        [JsonProperty("vol")]
        public double Vol { get; set; }

        [JsonProperty("vol_cur")]
        public double VolCur { get; set; }

        [JsonProperty("last")]
        public double Last { get; set; }

        [JsonProperty("buy")]
        public double Buy { get; set; }

        [JsonProperty("sell")]
        public double Sell { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("server_time")]
        public long ServerTime { get; set; }
    }

    internal class BTCeTickJSON
    {

        [JsonProperty("ticker")]
        public Ticker Ticker { get; set; }

        public BTCeTickJSON()
        {
            Ticker = new Ticker();

        }
    }
 
}
