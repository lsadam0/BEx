using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{

    public class BTCeTickJSON
    {
        public Ticker ticker { get; set; }
    }

    public class Ticker
    {
        public int high { get; set; }
        public float low { get; set; }
        public float avg { get; set; }
        public float vol { get; set; }
        public float vol_cur { get; set; }
        public float last { get; set; }
        public float buy { get; set; }
        public float sell { get; set; }
        public int updated { get; set; }
        public int server_time { get; set; }
    }
 
}
