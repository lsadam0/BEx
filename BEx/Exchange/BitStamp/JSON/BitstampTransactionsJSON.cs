using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
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

}
