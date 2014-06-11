using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BEx
{

    internal class BitstampOrderBookJSON
    {

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("bids")]
        public string[][] Bids { get; set; }

        [JsonProperty("asks")]
        public string[][] Asks { get; set; }
    }


}
