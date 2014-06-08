using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{

    public class BitstampOrderBookJSON
    {
        public string timestamp { get; set; }
        public string[][] bids { get; set; }
        public string[][] asks { get; set; }

        public BitstampOrderBookJSON()
        {

        }
    }

}
