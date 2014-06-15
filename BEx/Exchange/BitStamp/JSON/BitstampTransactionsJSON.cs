using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BEx.BitStampSupport
{

    public class BitstampTransactionsJSON
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string date { get; set; }
        public int tid { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
    }

    internal class BitstampTransactionJSON
    {
        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("tid")]
        public int tid { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("amount")]
        public string amount { get; set; }
    }

}
